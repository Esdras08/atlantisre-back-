using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Inexa.Atlantis.Re.Dal;
using Inexa.Atlantis.Re.Dal.Repository;
using Inexa.Atlantis.Re.Commons.Dtos;
using Inexa.Atlantis.Re.Commons.Infras.Domains;
using Inexa.Atlantis.Re.Commons.Infras.Enums;
using Inexa.Atlantis.Re.Commons.Infras.Helpers;

namespace Inexa.Atlantis.Re.Dal.Providers
{
  public class StructureProvider
  {

      #region Singleton
      
      private static StructureProvider _instance;

      public static StructureProvider Instance
      {
          get { return _instance ?? (_instance = new StructureProvider()); }
      }

      #endregion

      private readonly IRepository<Structure> _repositoryStructure;
     
      public  StructureProvider()
      {
          _repositoryStructure = new Repository<Structure>();
      }

      private StructureProvider(IRepository<Structure> repository)
      {    
          _repositoryStructure = repository;
      }

      public async Task<BusinessResponse<StructureDto>> GetStructureById(object id)
      {
          var response = new BusinessResponse<StructureDto>();

          try
          {
              var item = _repositoryStructure[id];
              if (item != null && item.IsDeleted == false)                  
              {
                  response.Items.Add(item.ToDtoWithRelated(0));
              }
          }
          catch(Exception ex)
          {
              CustomException.Write(response, ex);
          }

          return response;
      }

      /// <summary>
      /// Recupere les données de la table
      /// </summary>
      /// <param name="request">La requete contenant les criteres de recherche</param>
      /// <returns></returns>
      public async Task<BusinessResponse<StructureDto>> GetStructuresByCriteria(BusinessRequest<StructureDto> request)
      {
          var response = new BusinessResponse<StructureDto>();

          try
          {
              request.ToFinalRequest();

              // générer expression lambda de l'objet de recherche principal
              var mainexp = GenerateCriteria(request.ItemToSearch);                  
              mainexp = (mainexp == null) ? obj => obj.IsDeleted == false : mainexp.And(obj => obj.IsDeleted == false);                   
			  // rechercher en fonction de la structure courante 
              if (request.CanFilterByStruct)
                mainexp = (mainexp == null) ? obj => obj.IdStructure == request.IdCurrentStructure : mainexp.And(obj => obj.IdStructure == request.IdCurrentStructure);                   

              // générer expression lambda des objets de recherche sécondaires
              if (request.ItemsToSearch == null) 
                request.ItemsToSearch = new List<StructureDto>();                                                    
 
              var exps = new List<Expression<Func<Structure, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<Structure, bool>> exp = null;                  
              foreach (var expi in exps)
              {
                  if(request.NotIn)
                      exp = (exp == null) ? expi : exp.And(expi);
                  else
                      exp = (exp == null) ? expi : exp.Or(expi); 
              }

                  // concaténer les critères principaux et sécondaires
                  exp = (exp == null) ? mainexp : mainexp.And(exp);                  
                  
                  Expression<Func<Structure, object>> orderByExp = GenerateOrderByExpression(request.ItemToSearch);
                  if (string.IsNullOrEmpty(request.SortOrder) || (request.SortOrder != MySortOrder.Ascending && request.SortOrder != MySortOrder.Descending))
                    request.SortOrder = MySortOrder.Descending;

                  int totalRecord;
                  IEnumerable<Structure> items = _repositoryStructure.GetMany(exp, request.Index, request.Size, out totalRecord, orderByExp, request.SortOrder);
  
                  response.Count = totalRecord;
                  items = items as IList<Structure> ?? items.ToList();
                  response.Items = items.ToDtosWithRelated(request.Deepth);
                  response.IndexDebut = request.Index;
                  response.IndexFin = response.IndexDebut + items.Count() - 1;

                  // add sum value
                  if (request.CanDoSum && response.Count > 0)
                  {
                      var dto = response.Items[0];
                      response.Items[0] = GenerateSum(exp, request.ItemToSearch, dto);
                  }
              }
              catch (Exception ex)
              {
                  CustomException.Write(response, ex);
              }
  
              return response;
  		}

      /// <summary>
      /// Recupere la somme des données d'une colonne en fonction d'un critere
      /// </summary>
      /// <param name="request">La requete contenant les criteres de recherche</param>
      /// <returns></returns>
      public BusinessResponse<decimal> GetSumStructuresByCriteria(BusinessRequest<StructureDto> request)
      {
          var response = new BusinessResponse<decimal>();

          try
          {
              request.ToFinalRequest();

              // générer expression lambda de l'objet de recherche principal
              var mainexp = GenerateCriteria(request.ItemToSearch);                  
              mainexp = (mainexp == null) ? obj => obj.IsDeleted == false : mainexp.And(obj => obj.IsDeleted == false);                   
			  // rechercher en fonction de la structure courante
              mainexp = (mainexp == null) ? obj => obj.IdStructure == request.IdCurrentStructure : mainexp.And(obj => obj.IdStructure == request.IdCurrentStructure);                   

              // générer expression lambda des objets de recherche sécondaires
              if (request.ItemsToSearch == null) 
                request.ItemsToSearch = new List<StructureDto>();                                                    
 
              var exps = new List<Expression<Func<Structure, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<Structure, bool>> exp = null;                  
              foreach (var expi in exps)
              {
                  if(request.NotIn)
                      exp = (exp == null) ? expi : exp.And(expi);
                  else
                      exp = (exp == null) ? expi : exp.Or(expi); 
              }

                  // concaténer les critères principaux et sécondaires
                  exp = (exp == null) ? mainexp : mainexp.And(exp);                  
                  
                  var sumExp = GenerateSumExpression(request.ItemToSearch);

                  var sum = _repositoryStructure.GetSum(exp, sumExp);
                  response.Items.Add(sum);
  
              }
              catch (Exception ex)
              {
                  CustomException.Write(response, ex);
              }
  
              return response;
  		}

      /// <summary>
      /// Enregistre de nouvelles données, contenus dans la requête
      /// </summary>
      /// <param name="request">La requête contenant les données a enregistrer,
      ///  celles-ci etant contenus dans la proprieté "ItemsToSave"</param>
      /// <returns></returns>
  		public async Task<BusinessResponse<StructureDto>> SaveStructures(BusinessRequest<StructureDto> request)
  		{
        var response = new BusinessResponse<StructureDto>();
  
              try
              {
                  foreach (var item in request.ItemsToSave.TakeWhile(BusinessRequest<StructureDto>.ValidateRequiredProperties))
                  {
                     var itemToSave = item.ToEntity();
                     Structure itemSaved = null;

                     itemToSave.IdStructure = request.IdCurrentStructure; 
                     itemToSave.DateMAJ = Utilities.CurrentDate; 
                     itemToSave.ModifiedBy = request.IdCurrentUser; 
                     if (itemToSave.IdStructure == 0)  
                     {  
                        itemToSave.CreatedBy = request.IdCurrentUser;   

                        itemToSave.IsDeleted = false;
                        itemSaved = _repositoryStructure.Add(itemToSave);
                     }
                     else //dans le cas d'une modification
                     {
                         itemSaved = _repositoryStructure.Update(itemToSave, p => p.IdStructure == itemToSave.IdStructure);
                     }
        
                     response.Items.Add(itemSaved.ToDtoWithRelated(0));        
                 }
              }
              catch (Exception ex)
              {
                  CustomException.Write(response, ex);
              }
        
              return response;
  		}


      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda (null si le DTO ne contient pas de champ de recherche)</returns>
      private static Expression<Func<Structure, bool>> GenerateCriteria(StructureDto itemToSearch) 
      {
          Expression<Func<Structure, bool>> exprinit = obj => true;
          Expression<Func<Structure, bool>> expi = exprinit;
                                                             
          if (itemToSearch.InfoSearchIdStructure.Consider)
          {
              switch (itemToSearch.InfoSearchIdStructure.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdStructure != itemToSearch.IdStructure);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdStructure < itemToSearch.IdStructure);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdStructure <= itemToSearch.IdStructure);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdStructure > itemToSearch.IdStructure);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdStructure >= itemToSearch.IdStructure);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.IdStructure >= itemToSearch.InfoSearchIdStructure.Intervalle.Debut && obj.IdStructure <= itemToSearch.InfoSearchIdStructure.Intervalle.Fin);                         
                      break;        
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdStructure == itemToSearch.IdStructure);                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchRaisonSocialeStructure.Consider)
          {
              switch (itemToSearch.InfoSearchRaisonSocialeStructure.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.RaisonSocialeStructure != itemToSearch.RaisonSocialeStructure);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.RaisonSocialeStructure.StartsWith(itemToSearch.RaisonSocialeStructure));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.RaisonSocialeStructure.EndsWith(itemToSearch.RaisonSocialeStructure));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.RaisonSocialeStructure == itemToSearch.RaisonSocialeStructure);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.RaisonSocialeStructure.Contains(itemToSearch.RaisonSocialeStructure));
                      break;
              }
          }                               
                 
          if (itemToSearch.InfoSearchDateCreation1.Consider)
          {
              switch (itemToSearch.InfoSearchDateCreation1.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And
                            (obj => 
                                !(obj.DateCreation1.Value.Year == itemToSearch.DateCreation1.Value.Year
                                && obj.DateCreation1.Value.Month == itemToSearch.DateCreation1.Value.Month
                                && obj.DateCreation1.Value.Day == itemToSearch.DateCreation1.Value.Day)
                             );
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateCreation1.Value, itemToSearch.DateCreation1.Value) < 0);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateCreation1.Value, itemToSearch.DateCreation1.Value) <= 0);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateCreation1.Value, itemToSearch.DateCreation1.Value) > 0);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateCreation1.Value, itemToSearch.DateCreation1.Value) >= 0);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      var debut = itemToSearch.InfoSearchDateCreation1.Intervalle.Debut.Date;
                      var fin = itemToSearch.InfoSearchDateCreation1.Intervalle.Fin.AddDays(1).Date;
                      expi = expi.And(obj => obj.DateCreation1 >= debut && obj.DateCreation1 < fin);                  
                      break;         
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And
                            (
                              obj => obj.DateCreation1.Value.Year == itemToSearch.DateCreation1.Value.Year
                              && obj.DateCreation1.Value.Month == itemToSearch.DateCreation1.Value.Month
                              && obj.DateCreation1.Value.Day == itemToSearch.DateCreation1.Value.Day
                             );                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchDecretCreation.Consider)
          {
              switch (itemToSearch.InfoSearchDecretCreation.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.DecretCreation != itemToSearch.DecretCreation);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.DecretCreation.StartsWith(itemToSearch.DecretCreation));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.DecretCreation.EndsWith(itemToSearch.DecretCreation));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.DecretCreation == itemToSearch.DecretCreation);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.DecretCreation.Contains(itemToSearch.DecretCreation));
                      break;
              }
          }                               
                 
          if (itemToSearch.InfoSearchNumeroAgrement.Consider)
          {
              switch (itemToSearch.InfoSearchNumeroAgrement.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.NumeroAgrement != itemToSearch.NumeroAgrement);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.NumeroAgrement.StartsWith(itemToSearch.NumeroAgrement));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.NumeroAgrement.EndsWith(itemToSearch.NumeroAgrement));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.NumeroAgrement == itemToSearch.NumeroAgrement);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.NumeroAgrement.Contains(itemToSearch.NumeroAgrement));
                      break;
              }
          }                               
                 
          if (itemToSearch.InfoSearchIdDevise.Consider)
          {
              switch (itemToSearch.InfoSearchIdDevise.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdDevise != itemToSearch.IdDevise);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdDevise < itemToSearch.IdDevise);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdDevise <= itemToSearch.IdDevise);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdDevise > itemToSearch.IdDevise);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdDevise >= itemToSearch.IdDevise);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.IdDevise >= itemToSearch.InfoSearchIdDevise.Intervalle.Debut && obj.IdDevise <= itemToSearch.InfoSearchIdDevise.Intervalle.Fin);                         
                      break;        
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdDevise == itemToSearch.IdDevise);                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchCapitalSocial.Consider)
          {
              switch (itemToSearch.InfoSearchCapitalSocial.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.CapitalSocial != itemToSearch.CapitalSocial);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.CapitalSocial < itemToSearch.CapitalSocial);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.CapitalSocial <= itemToSearch.CapitalSocial);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.CapitalSocial > itemToSearch.CapitalSocial);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.CapitalSocial >= itemToSearch.CapitalSocial);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.CapitalSocial >= itemToSearch.InfoSearchCapitalSocial.Intervalle.Debut && obj.CapitalSocial <= itemToSearch.InfoSearchCapitalSocial.Intervalle.Fin);                         
                      break;        
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.CapitalSocial == itemToSearch.CapitalSocial);                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchIdPays.Consider)
          {
              switch (itemToSearch.InfoSearchIdPays.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdPays != itemToSearch.IdPays);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdPays < itemToSearch.IdPays);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdPays <= itemToSearch.IdPays);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdPays > itemToSearch.IdPays);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdPays >= itemToSearch.IdPays);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.IdPays >= itemToSearch.InfoSearchIdPays.Intervalle.Debut && obj.IdPays <= itemToSearch.InfoSearchIdPays.Intervalle.Fin);                         
                      break;        
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdPays == itemToSearch.IdPays);                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchDateMAJ.Consider)
          {
              switch (itemToSearch.InfoSearchDateMAJ.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And
                            (obj => 
                                !(obj.DateMAJ.Value.Year == itemToSearch.DateMAJ.Value.Year
                                && obj.DateMAJ.Value.Month == itemToSearch.DateMAJ.Value.Month
                                && obj.DateMAJ.Value.Day == itemToSearch.DateMAJ.Value.Day)
                             );
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateMAJ.Value, itemToSearch.DateMAJ.Value) < 0);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateMAJ.Value, itemToSearch.DateMAJ.Value) <= 0);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateMAJ.Value, itemToSearch.DateMAJ.Value) > 0);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateMAJ.Value, itemToSearch.DateMAJ.Value) >= 0);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      var debut = itemToSearch.InfoSearchDateMAJ.Intervalle.Debut.Date;
                      var fin = itemToSearch.InfoSearchDateMAJ.Intervalle.Fin.AddDays(1).Date;
                      expi = expi.And(obj => obj.DateMAJ >= debut && obj.DateMAJ < fin);                  
                      break;         
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And
                            (
                              obj => obj.DateMAJ.Value.Year == itemToSearch.DateMAJ.Value.Year
                              && obj.DateMAJ.Value.Month == itemToSearch.DateMAJ.Value.Month
                              && obj.DateMAJ.Value.Day == itemToSearch.DateMAJ.Value.Day
                             );                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchDataKey.Consider)
          {
              switch (itemToSearch.InfoSearchDataKey.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.DataKey != itemToSearch.DataKey);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.DataKey.StartsWith(itemToSearch.DataKey));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.DataKey.EndsWith(itemToSearch.DataKey));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.DataKey == itemToSearch.DataKey);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.DataKey.Contains(itemToSearch.DataKey));
                      break;
              }
          }                               
                 
          if (itemToSearch.InfoSearchModifiedBy.Consider)
          {
              switch (itemToSearch.InfoSearchModifiedBy.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.ModifiedBy != itemToSearch.ModifiedBy);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.ModifiedBy < itemToSearch.ModifiedBy);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.ModifiedBy <= itemToSearch.ModifiedBy);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.ModifiedBy > itemToSearch.ModifiedBy);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.ModifiedBy >= itemToSearch.ModifiedBy);                  
                      break;  
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.ModifiedBy == itemToSearch.ModifiedBy);                  
                      break;
              }
          }            

          if (itemToSearch.InfoSearchIsDeleted.Consider)
          {
              switch (itemToSearch.InfoSearchIsDeleted.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IsDeleted != itemToSearch.IsDeleted);
                      break;                      
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IsDeleted == itemToSearch.IsDeleted);                  
                      break;
              }
          }            

                  if (itemToSearch.InfoSearchCreatedBy.Consider)
          {
              switch (itemToSearch.InfoSearchCreatedBy.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.CreatedBy != itemToSearch.CreatedBy);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.CreatedBy < itemToSearch.CreatedBy);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.CreatedBy <= itemToSearch.CreatedBy);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.CreatedBy > itemToSearch.CreatedBy);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.CreatedBy >= itemToSearch.CreatedBy);                  
                      break;  
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.CreatedBy == itemToSearch.CreatedBy);                  
                      break;
              }
          }            

          return expi == exprinit ? null : expi;
      }

      /// <summary>
        /// Générer une expression lambda à partir d'un DTO
        /// </summary>
        /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
        /// <returns>Expression lambda</returns>
      private static Expression<Func<Structure, object>> GenerateOrderByExpression(StructureDto itemToSearch)
      {
          Expression<Func<Structure, object>> exp = null;

          if (itemToSearch.InfoSearchIdStructure.IsOrderByField)
            exp = (o => o.IdStructure);
          if (itemToSearch.InfoSearchRaisonSocialeStructure.IsOrderByField)
            exp = (o => o.RaisonSocialeStructure);
          if (itemToSearch.InfoSearchDateCreation1.IsOrderByField)
            exp = (o => o.DateCreation1);
          if (itemToSearch.InfoSearchDecretCreation.IsOrderByField)
            exp = (o => o.DecretCreation);
          if (itemToSearch.InfoSearchNumeroAgrement.IsOrderByField)
            exp = (o => o.NumeroAgrement);
          if (itemToSearch.InfoSearchIdDevise.IsOrderByField)
            exp = (o => o.IdDevise);
          if (itemToSearch.InfoSearchCapitalSocial.IsOrderByField)
            exp = (o => o.CapitalSocial);
          if (itemToSearch.InfoSearchIdPays.IsOrderByField)
            exp = (o => o.IdPays);
          if (itemToSearch.InfoSearchDateMAJ.IsOrderByField)
            exp = (o => o.DateMAJ);
          if (itemToSearch.InfoSearchDataKey.IsOrderByField)
            exp = (o => o.DataKey);
          if (itemToSearch.InfoSearchModifiedBy.IsOrderByField)
            exp = (o => o.ModifiedBy);
          if (itemToSearch.InfoSearchIsDeleted.IsOrderByField)
            exp = (o => o.IsDeleted);
          if (itemToSearch.InfoSearchCreatedBy.IsOrderByField)
            exp = (o => o.CreatedBy);

          return exp ?? (obj => obj.IdStructure);
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private static Expression<Func<Structure, decimal?>> GenerateSumExpression(StructureDto itemToSearch)
      {
          Expression<Func<Structure, decimal?>> exp = null;

          if (itemToSearch.InfoSearchCapitalSocial.IsSumField)
            exp = (o => o.CapitalSocial);                                         
            if (itemToSearch.InfoSearchModifiedBy.IsSumField)
            exp = (o => o.ModifiedBy.Value);                     
            if (itemToSearch.InfoSearchCreatedBy.IsSumField)
            exp = (o => o.CreatedBy.Value);                     
  
          //return exp ?? (obj => obj.IdStructure);
          return exp;
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private StructureDto GenerateSum(Expression<Func<Structure, bool>> criteria, StructureDto itemToSearch, StructureDto itemToReturn)
      {
          if (itemToSearch.InfoSearchCapitalSocial.IsSumField)
            itemToReturn.InfoSearchCapitalSocial.Sum = _repositoryStructure.GetSum(criteria, o => o.CapitalSocial);
                                        
            if (itemToSearch.InfoSearchModifiedBy.IsSumField)
            itemToReturn.InfoSearchModifiedBy.Sum = _repositoryStructure.GetSum(criteria, o => o.ModifiedBy.Value);     
               
            if (itemToSearch.InfoSearchCreatedBy.IsSumField)
            itemToReturn.InfoSearchCreatedBy.Sum = _repositoryStructure.GetSum(criteria, o => o.CreatedBy.Value);     
               
  
          return itemToReturn;
      }
  }
}

