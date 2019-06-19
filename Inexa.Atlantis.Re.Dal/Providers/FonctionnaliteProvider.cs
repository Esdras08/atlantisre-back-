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
  public class FonctionnaliteProvider
  {

      #region Singleton
      
      private static FonctionnaliteProvider _instance;

      public static FonctionnaliteProvider Instance
      {
          get { return _instance ?? (_instance = new FonctionnaliteProvider()); }
      }

      #endregion

      private readonly IRepository<Fonctionnalite> _repositoryFonctionnalite;
     
      public  FonctionnaliteProvider()
      {
          _repositoryFonctionnalite = new Repository<Fonctionnalite>();
      }

      private FonctionnaliteProvider(IRepository<Fonctionnalite> repository)
      {    
          _repositoryFonctionnalite = repository;
      }

      public async Task<BusinessResponse<FonctionnaliteDto>> GetFonctionnaliteById(object id)
      {
          var response = new BusinessResponse<FonctionnaliteDto>();

          try
          {
              var item = _repositoryFonctionnalite[id];
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
      public async Task<BusinessResponse<FonctionnaliteDto>> GetFonctionnalitesByCriteria(BusinessRequest<FonctionnaliteDto> request)
      {
          var response = new BusinessResponse<FonctionnaliteDto>();

          try
          {
              request.ToFinalRequest();

              // générer expression lambda de l'objet de recherche principal
              var mainexp = GenerateCriteria(request.ItemToSearch);                  
              mainexp = (mainexp == null) ? obj => obj.IsDeleted == false : mainexp.And(obj => obj.IsDeleted == false);                   

              // générer expression lambda des objets de recherche sécondaires
              if (request.ItemsToSearch == null) 
                request.ItemsToSearch = new List<FonctionnaliteDto>();                                                    
 
              var exps = new List<Expression<Func<Fonctionnalite, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<Fonctionnalite, bool>> exp = null;                  
              foreach (var expi in exps)
              {
                  if(request.NotIn)
                      exp = (exp == null) ? expi : exp.And(expi);
                  else
                      exp = (exp == null) ? expi : exp.Or(expi); 
              }

                  // concaténer les critères principaux et sécondaires
                  exp = (exp == null) ? mainexp : mainexp.And(exp);                  
                  
                  Expression<Func<Fonctionnalite, object>> orderByExp = GenerateOrderByExpression(request.ItemToSearch);
                  if (string.IsNullOrEmpty(request.SortOrder) || (request.SortOrder != MySortOrder.Ascending && request.SortOrder != MySortOrder.Descending))
                    request.SortOrder = MySortOrder.Descending;

                  int totalRecord;
                  IEnumerable<Fonctionnalite> items = _repositoryFonctionnalite.GetMany(exp, request.Index, request.Size, out totalRecord, orderByExp, request.SortOrder);
  
                  response.Count = totalRecord;
                  items = items as IList<Fonctionnalite> ?? items.ToList();
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
      public BusinessResponse<decimal> GetSumFonctionnalitesByCriteria(BusinessRequest<FonctionnaliteDto> request)
      {
          var response = new BusinessResponse<decimal>();

          try
          {
              request.ToFinalRequest();

              // générer expression lambda de l'objet de recherche principal
              var mainexp = GenerateCriteria(request.ItemToSearch);                  
              mainexp = (mainexp == null) ? obj => obj.IsDeleted == false : mainexp.And(obj => obj.IsDeleted == false);                   

              // générer expression lambda des objets de recherche sécondaires
              if (request.ItemsToSearch == null) 
                request.ItemsToSearch = new List<FonctionnaliteDto>();                                                    
 
              var exps = new List<Expression<Func<Fonctionnalite, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<Fonctionnalite, bool>> exp = null;                  
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

                  var sum = _repositoryFonctionnalite.GetSum(exp, sumExp);
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
  		public async Task<BusinessResponse<FonctionnaliteDto>> SaveFonctionnalites(BusinessRequest<FonctionnaliteDto> request)
  		{
        var response = new BusinessResponse<FonctionnaliteDto>();
  
              try
              {
                  foreach (var item in request.ItemsToSave.TakeWhile(BusinessRequest<FonctionnaliteDto>.ValidateRequiredProperties))
                  {
                     var itemToSave = item.ToEntity();
                     Fonctionnalite itemSaved = null;

                     itemToSave.DateMAJ = Utilities.CurrentDate; 
                     if (itemToSave.IdFonctionnalite == 0)  
                     {  
                        itemToSave.CreatedBy = request.IdCurrentUser;   
                        itemToSave.DateCreation = Utilities.CurrentDate;   

                        itemToSave.IsDeleted = false;
                        itemSaved = _repositoryFonctionnalite.Add(itemToSave);
                     }
                     else //dans le cas d'une modification
                     {
                         itemSaved = _repositoryFonctionnalite.Update(itemToSave, p => p.IdFonctionnalite == itemToSave.IdFonctionnalite);
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
      private static Expression<Func<Fonctionnalite, bool>> GenerateCriteria(FonctionnaliteDto itemToSearch) 
      {
          Expression<Func<Fonctionnalite, bool>> exprinit = obj => true;
          Expression<Func<Fonctionnalite, bool>> expi = exprinit;
                                                             
          if (itemToSearch.InfoSearchIdFonctionnalite.Consider)
          {
              switch (itemToSearch.InfoSearchIdFonctionnalite.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdFonctionnalite != itemToSearch.IdFonctionnalite);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdFonctionnalite < itemToSearch.IdFonctionnalite);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdFonctionnalite <= itemToSearch.IdFonctionnalite);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdFonctionnalite > itemToSearch.IdFonctionnalite);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdFonctionnalite >= itemToSearch.IdFonctionnalite);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.IdFonctionnalite >= itemToSearch.InfoSearchIdFonctionnalite.Intervalle.Debut && obj.IdFonctionnalite <= itemToSearch.InfoSearchIdFonctionnalite.Intervalle.Fin);                         
                      break;        
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdFonctionnalite == itemToSearch.IdFonctionnalite);                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchCodeParent.Consider)
          {
              switch (itemToSearch.InfoSearchCodeParent.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.CodeParent != itemToSearch.CodeParent);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.CodeParent.StartsWith(itemToSearch.CodeParent));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.CodeParent.EndsWith(itemToSearch.CodeParent));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.CodeParent == itemToSearch.CodeParent);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.CodeParent.Contains(itemToSearch.CodeParent));
                      break;
              }
          }                               
                 
          if (itemToSearch.InfoSearchCode.Consider)
          {
              switch (itemToSearch.InfoSearchCode.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.Code != itemToSearch.Code);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.Code.StartsWith(itemToSearch.Code));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.Code.EndsWith(itemToSearch.Code));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.Code == itemToSearch.Code);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.Code.Contains(itemToSearch.Code));
                      break;
              }
          }                               
                 
          if (itemToSearch.InfoSearchLibelle.Consider)
          {
              switch (itemToSearch.InfoSearchLibelle.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.Libelle != itemToSearch.Libelle);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.Libelle.StartsWith(itemToSearch.Libelle));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.Libelle.EndsWith(itemToSearch.Libelle));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.Libelle == itemToSearch.Libelle);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.Libelle.Contains(itemToSearch.Libelle));
                      break;
              }
          }                               
                 
          if (itemToSearch.InfoSearchDescription.Consider)
          {
              switch (itemToSearch.InfoSearchDescription.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.Description != itemToSearch.Description);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.Description.StartsWith(itemToSearch.Description));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.Description.EndsWith(itemToSearch.Description));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.Description == itemToSearch.Description);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.Description.Contains(itemToSearch.Description));
                      break;
              }
          }                               
                 
          if (itemToSearch.InfoSearchIdModule.Consider)
          {
              switch (itemToSearch.InfoSearchIdModule.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdModule != itemToSearch.IdModule);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdModule < itemToSearch.IdModule);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdModule <= itemToSearch.IdModule);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdModule > itemToSearch.IdModule);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdModule >= itemToSearch.IdModule);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.IdModule >= itemToSearch.InfoSearchIdModule.Intervalle.Debut && obj.IdModule <= itemToSearch.InfoSearchIdModule.Intervalle.Fin);                         
                      break;        
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdModule == itemToSearch.IdModule);                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchCodeSousModule.Consider)
          {
              switch (itemToSearch.InfoSearchCodeSousModule.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.CodeSousModule != itemToSearch.CodeSousModule);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.CodeSousModule.StartsWith(itemToSearch.CodeSousModule));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.CodeSousModule.EndsWith(itemToSearch.CodeSousModule));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.CodeSousModule == itemToSearch.CodeSousModule);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.CodeSousModule.Contains(itemToSearch.CodeSousModule));
                      break;
              }
          }                               
                 
          if (itemToSearch.InfoSearchOrdreSousModule.Consider)
          {
              switch (itemToSearch.InfoSearchOrdreSousModule.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.OrdreSousModule != itemToSearch.OrdreSousModule);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.OrdreSousModule < itemToSearch.OrdreSousModule);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.OrdreSousModule <= itemToSearch.OrdreSousModule);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.OrdreSousModule > itemToSearch.OrdreSousModule);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.OrdreSousModule >= itemToSearch.OrdreSousModule);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.OrdreSousModule >= itemToSearch.InfoSearchOrdreSousModule.Intervalle.Debut && obj.OrdreSousModule <= itemToSearch.InfoSearchOrdreSousModule.Intervalle.Fin);                         
                      break;    
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.OrdreSousModule == itemToSearch.OrdreSousModule);                  
                      break;
              }
          }            

          if (itemToSearch.InfoSearchOrdre.Consider)
          {
              switch (itemToSearch.InfoSearchOrdre.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.Ordre != itemToSearch.Ordre);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.Ordre < itemToSearch.Ordre);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.Ordre <= itemToSearch.Ordre);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.Ordre > itemToSearch.Ordre);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.Ordre >= itemToSearch.Ordre);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.Ordre >= itemToSearch.InfoSearchOrdre.Intervalle.Debut && obj.Ordre <= itemToSearch.InfoSearchOrdre.Intervalle.Fin);                         
                      break;    
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.Ordre == itemToSearch.Ordre);                  
                      break;
              }
          }            

          if (itemToSearch.InfoSearchActiver.Consider)
          {
              switch (itemToSearch.InfoSearchActiver.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.Activer != itemToSearch.Activer);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.Activer.StartsWith(itemToSearch.Activer));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.Activer.EndsWith(itemToSearch.Activer));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.Activer == itemToSearch.Activer);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.Activer.Contains(itemToSearch.Activer));
                      break;
              }
          }                               
                 
          if (itemToSearch.InfoSearchPolice.Consider)
          {
              switch (itemToSearch.InfoSearchPolice.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.Police != itemToSearch.Police);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.Police.StartsWith(itemToSearch.Police));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.Police.EndsWith(itemToSearch.Police));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.Police == itemToSearch.Police);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.Police.Contains(itemToSearch.Police));
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

          if (itemToSearch.InfoSearchDateCreation.Consider)
          {
              switch (itemToSearch.InfoSearchDateCreation.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And
                            (obj => 
                                !(obj.DateCreation.Value.Year == itemToSearch.DateCreation.Value.Year
                                && obj.DateCreation.Value.Month == itemToSearch.DateCreation.Value.Month
                                && obj.DateCreation.Value.Day == itemToSearch.DateCreation.Value.Day)
                             );
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateCreation.Value, itemToSearch.DateCreation.Value) < 0);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateCreation.Value, itemToSearch.DateCreation.Value) <= 0);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateCreation.Value, itemToSearch.DateCreation.Value) > 0);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateCreation.Value, itemToSearch.DateCreation.Value) >= 0);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      var debut = itemToSearch.InfoSearchDateCreation.Intervalle.Debut.Date;
                      var fin = itemToSearch.InfoSearchDateCreation.Intervalle.Fin.AddDays(1).Date;
                      expi = expi.And(obj => obj.DateCreation >= debut && obj.DateCreation < fin);                  
                      break;         
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And
                            (
                              obj => obj.DateCreation.Value.Year == itemToSearch.DateCreation.Value.Year
                              && obj.DateCreation.Value.Month == itemToSearch.DateCreation.Value.Month
                              && obj.DateCreation.Value.Day == itemToSearch.DateCreation.Value.Day
                             );                  
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
                 
          return expi == exprinit ? null : expi;
      }

      /// <summary>
        /// Générer une expression lambda à partir d'un DTO
        /// </summary>
        /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
        /// <returns>Expression lambda</returns>
      private static Expression<Func<Fonctionnalite, object>> GenerateOrderByExpression(FonctionnaliteDto itemToSearch)
      {
          Expression<Func<Fonctionnalite, object>> exp = null;

          if (itemToSearch.InfoSearchIdFonctionnalite.IsOrderByField)
            exp = (o => o.IdFonctionnalite);
          if (itemToSearch.InfoSearchCodeParent.IsOrderByField)
            exp = (o => o.CodeParent);
          if (itemToSearch.InfoSearchCode.IsOrderByField)
            exp = (o => o.Code);
          if (itemToSearch.InfoSearchLibelle.IsOrderByField)
            exp = (o => o.Libelle);
          if (itemToSearch.InfoSearchDescription.IsOrderByField)
            exp = (o => o.Description);
          if (itemToSearch.InfoSearchIdModule.IsOrderByField)
            exp = (o => o.IdModule);
          if (itemToSearch.InfoSearchCodeSousModule.IsOrderByField)
            exp = (o => o.CodeSousModule);
          if (itemToSearch.InfoSearchOrdreSousModule.IsOrderByField)
            exp = (o => o.OrdreSousModule);
          if (itemToSearch.InfoSearchOrdre.IsOrderByField)
            exp = (o => o.Ordre);
          if (itemToSearch.InfoSearchActiver.IsOrderByField)
            exp = (o => o.Activer);
          if (itemToSearch.InfoSearchPolice.IsOrderByField)
            exp = (o => o.Police);
          if (itemToSearch.InfoSearchIsDeleted.IsOrderByField)
            exp = (o => o.IsDeleted);
          if (itemToSearch.InfoSearchCreatedBy.IsOrderByField)
            exp = (o => o.CreatedBy);
          if (itemToSearch.InfoSearchDateCreation.IsOrderByField)
            exp = (o => o.DateCreation);
          if (itemToSearch.InfoSearchDateMAJ.IsOrderByField)
            exp = (o => o.DateMAJ);
          if (itemToSearch.InfoSearchDataKey.IsOrderByField)
            exp = (o => o.DataKey);

          return exp ?? (obj => obj.IdFonctionnalite);
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private static Expression<Func<Fonctionnalite, decimal?>> GenerateSumExpression(FonctionnaliteDto itemToSearch)
      {
          Expression<Func<Fonctionnalite, decimal?>> exp = null;

          if (itemToSearch.InfoSearchOrdreSousModule.IsSumField)
            exp = (o => o.OrdreSousModule.Value);                     
            if (itemToSearch.InfoSearchOrdre.IsSumField)
            exp = (o => o.Ordre.Value);                     
            if (itemToSearch.InfoSearchCreatedBy.IsSumField)
            exp = (o => o.CreatedBy.Value);                     
  
          //return exp ?? (obj => obj.IdFonctionnalite);
          return exp;
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private FonctionnaliteDto GenerateSum(Expression<Func<Fonctionnalite, bool>> criteria, FonctionnaliteDto itemToSearch, FonctionnaliteDto itemToReturn)
      {
          if (itemToSearch.InfoSearchOrdreSousModule.IsSumField)
            itemToReturn.InfoSearchOrdreSousModule.Sum = _repositoryFonctionnalite.GetSum(criteria, o => o.OrdreSousModule.Value);     
               
            if (itemToSearch.InfoSearchOrdre.IsSumField)
            itemToReturn.InfoSearchOrdre.Sum = _repositoryFonctionnalite.GetSum(criteria, o => o.Ordre.Value);     
               
            if (itemToSearch.InfoSearchCreatedBy.IsSumField)
            itemToReturn.InfoSearchCreatedBy.Sum = _repositoryFonctionnalite.GetSum(criteria, o => o.CreatedBy.Value);     
               
  
          return itemToReturn;
      }
  }
}

