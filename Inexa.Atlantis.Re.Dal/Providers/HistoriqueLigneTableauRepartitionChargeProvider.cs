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
  public class HistoriqueLigneTableauRepartitionChargeProvider
  {

      #region Singleton
      
      private static HistoriqueLigneTableauRepartitionChargeProvider _instance;

      public static HistoriqueLigneTableauRepartitionChargeProvider Instance
      {
          get { return _instance ?? (_instance = new HistoriqueLigneTableauRepartitionChargeProvider()); }
      }

      #endregion

      private readonly IRepository<HistoriqueLigneTableauRepartitionCharge> _repositoryHistoriqueLigneTableauRepartitionCharge;
     
      public  HistoriqueLigneTableauRepartitionChargeProvider()
      {
          _repositoryHistoriqueLigneTableauRepartitionCharge = new Repository<HistoriqueLigneTableauRepartitionCharge>();
      }

      private HistoriqueLigneTableauRepartitionChargeProvider(IRepository<HistoriqueLigneTableauRepartitionCharge> repository)
      {    
          _repositoryHistoriqueLigneTableauRepartitionCharge = repository;
      }

      public async Task<BusinessResponse<HistoriqueLigneTableauRepartitionChargeDto>> GetHistoriqueLigneTableauRepartitionChargeById(object id)
      {
          var response = new BusinessResponse<HistoriqueLigneTableauRepartitionChargeDto>();

          try
          {
              var item = _repositoryHistoriqueLigneTableauRepartitionCharge[id];
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
      public async Task<BusinessResponse<HistoriqueLigneTableauRepartitionChargeDto>> GetHistoriqueLigneTableauRepartitionChargesByCriteria(BusinessRequest<HistoriqueLigneTableauRepartitionChargeDto> request)
      {
          var response = new BusinessResponse<HistoriqueLigneTableauRepartitionChargeDto>();

          try
          {
              request.ToFinalRequest();

              // générer expression lambda de l'objet de recherche principal
              var mainexp = GenerateCriteria(request.ItemToSearch);                  
              mainexp = (mainexp == null) ? obj => obj.IsDeleted == false : mainexp.And(obj => obj.IsDeleted == false);                   

              // générer expression lambda des objets de recherche sécondaires
              if (request.ItemsToSearch == null) 
                request.ItemsToSearch = new List<HistoriqueLigneTableauRepartitionChargeDto>();                                                    
 
              var exps = new List<Expression<Func<HistoriqueLigneTableauRepartitionCharge, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<HistoriqueLigneTableauRepartitionCharge, bool>> exp = null;                  
              foreach (var expi in exps)
              {
                  if(request.NotIn)
                      exp = (exp == null) ? expi : exp.And(expi);
                  else
                      exp = (exp == null) ? expi : exp.Or(expi); 
              }

                  // concaténer les critères principaux et sécondaires
                  exp = (exp == null) ? mainexp : mainexp.And(exp);                  
                  
                  Expression<Func<HistoriqueLigneTableauRepartitionCharge, object>> orderByExp = GenerateOrderByExpression(request.ItemToSearch);
                  if (string.IsNullOrEmpty(request.SortOrder) || (request.SortOrder != MySortOrder.Ascending && request.SortOrder != MySortOrder.Descending))
                    request.SortOrder = MySortOrder.Descending;

                  int totalRecord;
                  IEnumerable<HistoriqueLigneTableauRepartitionCharge> items = _repositoryHistoriqueLigneTableauRepartitionCharge.GetMany(exp, request.Index, request.Size, out totalRecord, orderByExp, request.SortOrder);
  
                  response.Count = totalRecord;
                  items = items as IList<HistoriqueLigneTableauRepartitionCharge> ?? items.ToList();
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
      public BusinessResponse<decimal> GetSumHistoriqueLigneTableauRepartitionChargesByCriteria(BusinessRequest<HistoriqueLigneTableauRepartitionChargeDto> request)
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
                request.ItemsToSearch = new List<HistoriqueLigneTableauRepartitionChargeDto>();                                                    
 
              var exps = new List<Expression<Func<HistoriqueLigneTableauRepartitionCharge, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<HistoriqueLigneTableauRepartitionCharge, bool>> exp = null;                  
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

                  var sum = _repositoryHistoriqueLigneTableauRepartitionCharge.GetSum(exp, sumExp);
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
  		public async Task<BusinessResponse<HistoriqueLigneTableauRepartitionChargeDto>> SaveHistoriqueLigneTableauRepartitionCharges(BusinessRequest<HistoriqueLigneTableauRepartitionChargeDto> request)
  		{
        var response = new BusinessResponse<HistoriqueLigneTableauRepartitionChargeDto>();
  
              try
              {
                  foreach (var item in request.ItemsToSave.TakeWhile(BusinessRequest<HistoriqueLigneTableauRepartitionChargeDto>.ValidateRequiredProperties))
                  {
                     var itemToSave = item.ToEntity();
                     HistoriqueLigneTableauRepartitionCharge itemSaved = null;

                     itemToSave.ModifiedBy = request.IdCurrentUser; 
                     itemToSave.DateMAJ = Utilities.CurrentDate; 
                     if (itemToSave.IdHistoriqueLigneTableauRepartitionCharge == 0)  
                     {  
                        itemToSave.CreatedBy = request.IdCurrentUser;   
                        itemToSave.DateCreation = Utilities.CurrentDate;   

                        itemToSave.IsDeleted = false;
                        itemSaved = _repositoryHistoriqueLigneTableauRepartitionCharge.Add(itemToSave);
                     }
                     else //dans le cas d'une modification
                     {
                         itemSaved = _repositoryHistoriqueLigneTableauRepartitionCharge.Update(itemToSave, p => p.IdHistoriqueLigneTableauRepartitionCharge == itemToSave.IdHistoriqueLigneTableauRepartitionCharge);
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
      private static Expression<Func<HistoriqueLigneTableauRepartitionCharge, bool>> GenerateCriteria(HistoriqueLigneTableauRepartitionChargeDto itemToSearch) 
      {
          Expression<Func<HistoriqueLigneTableauRepartitionCharge, bool>> exprinit = obj => true;
          Expression<Func<HistoriqueLigneTableauRepartitionCharge, bool>> expi = exprinit;
                                                             
          if (itemToSearch.InfoSearchIdHistoriqueLigneTableauRepartitionCharge.Consider)
          {
              switch (itemToSearch.InfoSearchIdHistoriqueLigneTableauRepartitionCharge.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdHistoriqueLigneTableauRepartitionCharge != itemToSearch.IdHistoriqueLigneTableauRepartitionCharge);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdHistoriqueLigneTableauRepartitionCharge < itemToSearch.IdHistoriqueLigneTableauRepartitionCharge);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdHistoriqueLigneTableauRepartitionCharge <= itemToSearch.IdHistoriqueLigneTableauRepartitionCharge);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdHistoriqueLigneTableauRepartitionCharge > itemToSearch.IdHistoriqueLigneTableauRepartitionCharge);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdHistoriqueLigneTableauRepartitionCharge >= itemToSearch.IdHistoriqueLigneTableauRepartitionCharge);                  
                      break;  
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdHistoriqueLigneTableauRepartitionCharge == itemToSearch.IdHistoriqueLigneTableauRepartitionCharge);                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchIdTableauRepartition.Consider)
          {
              switch (itemToSearch.InfoSearchIdTableauRepartition.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdTableauRepartition != itemToSearch.IdTableauRepartition);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdTableauRepartition < itemToSearch.IdTableauRepartition);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdTableauRepartition <= itemToSearch.IdTableauRepartition);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdTableauRepartition > itemToSearch.IdTableauRepartition);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdTableauRepartition >= itemToSearch.IdTableauRepartition);                  
                      break;  
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdTableauRepartition == itemToSearch.IdTableauRepartition);                  
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
                 
          if (itemToSearch.InfoSearchProposition.Consider)
          {
              switch (itemToSearch.InfoSearchProposition.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.Proposition != itemToSearch.Proposition);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.Proposition < itemToSearch.Proposition);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.Proposition <= itemToSearch.Proposition);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.Proposition > itemToSearch.Proposition);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.Proposition >= itemToSearch.Proposition);                  
                      break;  
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.Proposition == itemToSearch.Proposition);                  
                      break;
              }
          }            

          if (itemToSearch.InfoSearchIdReassureur.Consider)
          {
              switch (itemToSearch.InfoSearchIdReassureur.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdReassureur != itemToSearch.IdReassureur);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdReassureur < itemToSearch.IdReassureur);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdReassureur <= itemToSearch.IdReassureur);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdReassureur > itemToSearch.IdReassureur);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdReassureur >= itemToSearch.IdReassureur);                  
                      break;  
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdReassureur == itemToSearch.IdReassureur);                  
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
      private static Expression<Func<HistoriqueLigneTableauRepartitionCharge, object>> GenerateOrderByExpression(HistoriqueLigneTableauRepartitionChargeDto itemToSearch)
      {
          Expression<Func<HistoriqueLigneTableauRepartitionCharge, object>> exp = null;

          if (itemToSearch.InfoSearchIdHistoriqueLigneTableauRepartitionCharge.IsOrderByField)
            exp = (o => o.IdHistoriqueLigneTableauRepartitionCharge);
          if (itemToSearch.InfoSearchIdTableauRepartition.IsOrderByField)
            exp = (o => o.IdTableauRepartition);
          if (itemToSearch.InfoSearchIsDeleted.IsOrderByField)
            exp = (o => o.IsDeleted);
          if (itemToSearch.InfoSearchCreatedBy.IsOrderByField)
            exp = (o => o.CreatedBy);
          if (itemToSearch.InfoSearchModifiedBy.IsOrderByField)
            exp = (o => o.ModifiedBy);
          if (itemToSearch.InfoSearchDateCreation.IsOrderByField)
            exp = (o => o.DateCreation);
          if (itemToSearch.InfoSearchDateMAJ.IsOrderByField)
            exp = (o => o.DateMAJ);
          if (itemToSearch.InfoSearchDataKey.IsOrderByField)
            exp = (o => o.DataKey);
          if (itemToSearch.InfoSearchProposition.IsOrderByField)
            exp = (o => o.Proposition);
          if (itemToSearch.InfoSearchIdReassureur.IsOrderByField)
            exp = (o => o.IdReassureur);

          return exp ?? (obj => obj.IdHistoriqueLigneTableauRepartitionCharge);
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private static Expression<Func<HistoriqueLigneTableauRepartitionCharge, decimal?>> GenerateSumExpression(HistoriqueLigneTableauRepartitionChargeDto itemToSearch)
      {
          Expression<Func<HistoriqueLigneTableauRepartitionCharge, decimal?>> exp = null;

          if (itemToSearch.InfoSearchCreatedBy.IsSumField)
            exp = (o => o.CreatedBy.Value);                     
            if (itemToSearch.InfoSearchModifiedBy.IsSumField)
            exp = (o => o.ModifiedBy.Value);                     
            if (itemToSearch.InfoSearchProposition.IsSumField)
            exp = (o => o.Proposition.Value);                     
  
          //return exp ?? (obj => obj.IdHistoriqueLigneTableauRepartitionCharge);
          return exp;
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private HistoriqueLigneTableauRepartitionChargeDto GenerateSum(Expression<Func<HistoriqueLigneTableauRepartitionCharge, bool>> criteria, HistoriqueLigneTableauRepartitionChargeDto itemToSearch, HistoriqueLigneTableauRepartitionChargeDto itemToReturn)
      {
          if (itemToSearch.InfoSearchCreatedBy.IsSumField)
            itemToReturn.InfoSearchCreatedBy.Sum = _repositoryHistoriqueLigneTableauRepartitionCharge.GetSum(criteria, o => o.CreatedBy.Value);     
               
            if (itemToSearch.InfoSearchModifiedBy.IsSumField)
            itemToReturn.InfoSearchModifiedBy.Sum = _repositoryHistoriqueLigneTableauRepartitionCharge.GetSum(criteria, o => o.ModifiedBy.Value);     
               
            if (itemToSearch.InfoSearchProposition.IsSumField)
            itemToReturn.InfoSearchProposition.Sum = _repositoryHistoriqueLigneTableauRepartitionCharge.GetSum(criteria, o => o.Proposition.Value);     
               
  
          return itemToReturn;
      }
  }
}

