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
  public class AffaireProvider
  {

      #region Singleton
      
      private static AffaireProvider _instance;

      public static AffaireProvider Instance
      {
          get { return _instance ?? (_instance = new AffaireProvider()); }
      }

      #endregion

      private readonly IRepository<Affaire> _repositoryAffaire;
     
      public  AffaireProvider()
      {
          _repositoryAffaire = new Repository<Affaire>();
      }

      private AffaireProvider(IRepository<Affaire> repository)
      {    
          _repositoryAffaire = repository;
      }

      public async Task<BusinessResponse<AffaireDto>> GetAffaireById(object id)
      {
          var response = new BusinessResponse<AffaireDto>();

          try
          {
              var item = _repositoryAffaire[id];
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
      public async Task<BusinessResponse<AffaireDto>> GetAffairesByCriteria(BusinessRequest<AffaireDto> request)
      {
          var response = new BusinessResponse<AffaireDto>();

          try
          {
              request.ToFinalRequest();

              // générer expression lambda de l'objet de recherche principal
              var mainexp = GenerateCriteria(request.ItemToSearch);                  
              mainexp = (mainexp == null) ? obj => obj.IsDeleted == false : mainexp.And(obj => obj.IsDeleted == false);                   

              // générer expression lambda des objets de recherche sécondaires
              if (request.ItemsToSearch == null) 
                request.ItemsToSearch = new List<AffaireDto>();                                                    
 
              var exps = new List<Expression<Func<Affaire, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<Affaire, bool>> exp = null;                  
              foreach (var expi in exps)
              {
                  if(request.NotIn)
                      exp = (exp == null) ? expi : exp.And(expi);
                  else
                      exp = (exp == null) ? expi : exp.Or(expi); 
              }

                  // concaténer les critères principaux et sécondaires
                  exp = (exp == null) ? mainexp : mainexp.And(exp);                  
                  
                  Expression<Func<Affaire, object>> orderByExp = GenerateOrderByExpression(request.ItemToSearch);
                  if (string.IsNullOrEmpty(request.SortOrder) || (request.SortOrder != MySortOrder.Ascending && request.SortOrder != MySortOrder.Descending))
                    request.SortOrder = MySortOrder.Descending;

                  int totalRecord;
                  IEnumerable<Affaire> items = _repositoryAffaire.GetMany(exp, request.Index, request.Size, out totalRecord, orderByExp, request.SortOrder);
  
                  response.Count = totalRecord;
                  items = items as IList<Affaire> ?? items.ToList();
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
      public BusinessResponse<decimal> GetSumAffairesByCriteria(BusinessRequest<AffaireDto> request)
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
                request.ItemsToSearch = new List<AffaireDto>();                                                    
 
              var exps = new List<Expression<Func<Affaire, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<Affaire, bool>> exp = null;                  
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

                  var sum = _repositoryAffaire.GetSum(exp, sumExp);
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
  		public async Task<BusinessResponse<AffaireDto>> SaveAffaires(BusinessRequest<AffaireDto> request)
  		{
        var response = new BusinessResponse<AffaireDto>();
  
              try
              {
                  foreach (var item in request.ItemsToSave.TakeWhile(BusinessRequest<AffaireDto>.ValidateRequiredProperties))
                  {
                     var itemToSave = item.ToEntity();
                     Affaire itemSaved = null;

                     itemToSave.DateMAJ = Utilities.CurrentDate; 
                     itemToSave.ModifiedBy = request.IdCurrentUser; 
                     if (itemToSave.IdAffaire == 0)  
                     {  
                        itemToSave.DateCreation = Utilities.CurrentDate;   
                        itemToSave.CreatedBy = request.IdCurrentUser;   

                        itemToSave.IsDeleted = false;
                        itemSaved = _repositoryAffaire.Add(itemToSave);
                     }
                     else //dans le cas d'une modification
                     {
                         itemSaved = _repositoryAffaire.Update(itemToSave, p => p.IdAffaire == itemToSave.IdAffaire);
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
      private static Expression<Func<Affaire, bool>> GenerateCriteria(AffaireDto itemToSearch) 
      {
          Expression<Func<Affaire, bool>> exprinit = obj => true;
          Expression<Func<Affaire, bool>> expi = exprinit;
                                                             
          if (itemToSearch.InfoSearchIdAffaire.Consider)
          {
              switch (itemToSearch.InfoSearchIdAffaire.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdAffaire != itemToSearch.IdAffaire);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdAffaire < itemToSearch.IdAffaire);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdAffaire <= itemToSearch.IdAffaire);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdAffaire > itemToSearch.IdAffaire);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdAffaire >= itemToSearch.IdAffaire);                  
                      break;  
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdAffaire == itemToSearch.IdAffaire);                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchNumeroOrdre.Consider)
          {
              switch (itemToSearch.InfoSearchNumeroOrdre.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.NumeroOrdre != itemToSearch.NumeroOrdre);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.NumeroOrdre.StartsWith(itemToSearch.NumeroOrdre));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.NumeroOrdre.EndsWith(itemToSearch.NumeroOrdre));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.NumeroOrdre == itemToSearch.NumeroOrdre);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.NumeroOrdre.Contains(itemToSearch.NumeroOrdre));
                      break;
              }
          }                               
                 
          if (itemToSearch.InfoSearchNumeroPolice.Consider)
          {
              switch (itemToSearch.InfoSearchNumeroPolice.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.NumeroPolice != itemToSearch.NumeroPolice);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.NumeroPolice.StartsWith(itemToSearch.NumeroPolice));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.NumeroPolice.EndsWith(itemToSearch.NumeroPolice));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.NumeroPolice == itemToSearch.NumeroPolice);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.NumeroPolice.Contains(itemToSearch.NumeroPolice));
                      break;
              }
          }                               
                 
          if (itemToSearch.InfoSearchCapitauxAssure.Consider)
          {
              switch (itemToSearch.InfoSearchCapitauxAssure.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.CapitauxAssure != itemToSearch.CapitauxAssure);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.CapitauxAssure < itemToSearch.CapitauxAssure);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.CapitauxAssure <= itemToSearch.CapitauxAssure);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.CapitauxAssure > itemToSearch.CapitauxAssure);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.CapitauxAssure >= itemToSearch.CapitauxAssure);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.CapitauxAssure >= itemToSearch.InfoSearchCapitauxAssure.Intervalle.Debut && obj.CapitauxAssure <= itemToSearch.InfoSearchCapitauxAssure.Intervalle.Fin);                         
                      break;    
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.CapitauxAssure == itemToSearch.CapitauxAssure);                  
                      break;
              }
          }            

          if (itemToSearch.InfoSearchActivite.Consider)
          {
              switch (itemToSearch.InfoSearchActivite.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.Activite != itemToSearch.Activite);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.Activite.StartsWith(itemToSearch.Activite));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.Activite.EndsWith(itemToSearch.Activite));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.Activite == itemToSearch.Activite);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.Activite.Contains(itemToSearch.Activite));
                      break;
              }
          }                               
                 
          if (itemToSearch.InfoSearchIdStatutAffaire.Consider)
          {
              switch (itemToSearch.InfoSearchIdStatutAffaire.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdStatutAffaire != itemToSearch.IdStatutAffaire);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdStatutAffaire < itemToSearch.IdStatutAffaire);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdStatutAffaire <= itemToSearch.IdStatutAffaire);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdStatutAffaire > itemToSearch.IdStatutAffaire);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdStatutAffaire >= itemToSearch.IdStatutAffaire);                  
                      break;  
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdStatutAffaire == itemToSearch.IdStatutAffaire);                  
                      break;
              }
          }            

          if (itemToSearch.InfoSearchIdBranche.Consider)
          {
              switch (itemToSearch.InfoSearchIdBranche.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdBranche != itemToSearch.IdBranche);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdBranche < itemToSearch.IdBranche);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdBranche <= itemToSearch.IdBranche);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdBranche > itemToSearch.IdBranche);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdBranche >= itemToSearch.IdBranche);                  
                      break;  
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdBranche == itemToSearch.IdBranche);                  
                      break;
              }
          }            

          if (itemToSearch.InfoSearchIdFiliale.Consider)
          {
              switch (itemToSearch.InfoSearchIdFiliale.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdFiliale != itemToSearch.IdFiliale);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdFiliale < itemToSearch.IdFiliale);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdFiliale <= itemToSearch.IdFiliale);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdFiliale > itemToSearch.IdFiliale);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdFiliale >= itemToSearch.IdFiliale);                  
                      break;  
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdFiliale == itemToSearch.IdFiliale);                  
                      break;
              }
          }            

          if (itemToSearch.InfoSearchIdAssure.Consider)
          {
              switch (itemToSearch.InfoSearchIdAssure.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdAssure != itemToSearch.IdAssure);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdAssure < itemToSearch.IdAssure);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdAssure <= itemToSearch.IdAssure);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdAssure > itemToSearch.IdAssure);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdAssure >= itemToSearch.IdAssure);                  
                      break;  
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdAssure == itemToSearch.IdAssure);                  
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

          return expi == exprinit ? null : expi;
      }

      /// <summary>
        /// Générer une expression lambda à partir d'un DTO
        /// </summary>
        /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
        /// <returns>Expression lambda</returns>
      private static Expression<Func<Affaire, object>> GenerateOrderByExpression(AffaireDto itemToSearch)
      {
          Expression<Func<Affaire, object>> exp = null;

          if (itemToSearch.InfoSearchIdAffaire.IsOrderByField)
            exp = (o => o.IdAffaire);
          if (itemToSearch.InfoSearchNumeroOrdre.IsOrderByField)
            exp = (o => o.NumeroOrdre);
          if (itemToSearch.InfoSearchNumeroPolice.IsOrderByField)
            exp = (o => o.NumeroPolice);
          if (itemToSearch.InfoSearchCapitauxAssure.IsOrderByField)
            exp = (o => o.CapitauxAssure);
          if (itemToSearch.InfoSearchActivite.IsOrderByField)
            exp = (o => o.Activite);
          if (itemToSearch.InfoSearchIdStatutAffaire.IsOrderByField)
            exp = (o => o.IdStatutAffaire);
          if (itemToSearch.InfoSearchIdBranche.IsOrderByField)
            exp = (o => o.IdBranche);
          if (itemToSearch.InfoSearchIdFiliale.IsOrderByField)
            exp = (o => o.IdFiliale);
          if (itemToSearch.InfoSearchIdAssure.IsOrderByField)
            exp = (o => o.IdAssure);
          if (itemToSearch.InfoSearchIsDeleted.IsOrderByField)
            exp = (o => o.IsDeleted);
          if (itemToSearch.InfoSearchDateCreation.IsOrderByField)
            exp = (o => o.DateCreation);
          if (itemToSearch.InfoSearchDateMAJ.IsOrderByField)
            exp = (o => o.DateMAJ);
          if (itemToSearch.InfoSearchDataKey.IsOrderByField)
            exp = (o => o.DataKey);
          if (itemToSearch.InfoSearchCreatedBy.IsOrderByField)
            exp = (o => o.CreatedBy);
          if (itemToSearch.InfoSearchModifiedBy.IsOrderByField)
            exp = (o => o.ModifiedBy);

          return exp ?? (obj => obj.IdAffaire);
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private static Expression<Func<Affaire, decimal?>> GenerateSumExpression(AffaireDto itemToSearch)
      {
          Expression<Func<Affaire, decimal?>> exp = null;

          if (itemToSearch.InfoSearchCapitauxAssure.IsSumField)
            exp = (o => o.CapitauxAssure.Value);                     
            if (itemToSearch.InfoSearchCreatedBy.IsSumField)
            exp = (o => o.CreatedBy);                                         
            if (itemToSearch.InfoSearchModifiedBy.IsSumField)
            exp = (o => o.ModifiedBy.Value);                     
  
          //return exp ?? (obj => obj.IdAffaire);
          return exp;
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private AffaireDto GenerateSum(Expression<Func<Affaire, bool>> criteria, AffaireDto itemToSearch, AffaireDto itemToReturn)
      {
          if (itemToSearch.InfoSearchCapitauxAssure.IsSumField)
            itemToReturn.InfoSearchCapitauxAssure.Sum = _repositoryAffaire.GetSum(criteria, o => o.CapitauxAssure.Value);     
               
            if (itemToSearch.InfoSearchCreatedBy.IsSumField)
            itemToReturn.InfoSearchCreatedBy.Sum = _repositoryAffaire.GetSum(criteria, o => o.CreatedBy);
                                        
            if (itemToSearch.InfoSearchModifiedBy.IsSumField)
            itemToReturn.InfoSearchModifiedBy.Sum = _repositoryAffaire.GetSum(criteria, o => o.ModifiedBy.Value);     
               
  
          return itemToReturn;
      }
  }
}

