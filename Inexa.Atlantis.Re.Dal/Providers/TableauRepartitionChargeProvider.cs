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
  public class TableauRepartitionChargeProvider
  {

      #region Singleton
      
      private static TableauRepartitionChargeProvider _instance;

      public static TableauRepartitionChargeProvider Instance
      {
          get { return _instance ?? (_instance = new TableauRepartitionChargeProvider()); }
      }

      #endregion

      private readonly IRepository<TableauRepartitionCharge> _repositoryTableauRepartitionCharge;
     
      public  TableauRepartitionChargeProvider()
      {
          _repositoryTableauRepartitionCharge = new Repository<TableauRepartitionCharge>();
      }

      private TableauRepartitionChargeProvider(IRepository<TableauRepartitionCharge> repository)
      {    
          _repositoryTableauRepartitionCharge = repository;
      }

      public async Task<BusinessResponse<TableauRepartitionChargeDto>> GetTableauRepartitionChargeById(object id)
      {
          var response = new BusinessResponse<TableauRepartitionChargeDto>();

          try
          {
              var item = _repositoryTableauRepartitionCharge[id];
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
      public async Task<BusinessResponse<TableauRepartitionChargeDto>> GetTableauRepartitionChargesByCriteria(BusinessRequest<TableauRepartitionChargeDto> request)
      {
          var response = new BusinessResponse<TableauRepartitionChargeDto>();

          try
          {
              request.ToFinalRequest();

              // générer expression lambda de l'objet de recherche principal
              var mainexp = GenerateCriteria(request.ItemToSearch);                  
              mainexp = (mainexp == null) ? obj => obj.IsDeleted == false : mainexp.And(obj => obj.IsDeleted == false);                   

              // générer expression lambda des objets de recherche sécondaires
              if (request.ItemsToSearch == null) 
                request.ItemsToSearch = new List<TableauRepartitionChargeDto>();                                                    
 
              var exps = new List<Expression<Func<TableauRepartitionCharge, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<TableauRepartitionCharge, bool>> exp = null;                  
              foreach (var expi in exps)
              {
                  if(request.NotIn)
                      exp = (exp == null) ? expi : exp.And(expi);
                  else
                      exp = (exp == null) ? expi : exp.Or(expi); 
              }

                  // concaténer les critères principaux et sécondaires
                  exp = (exp == null) ? mainexp : mainexp.And(exp);                  
                  
                  Expression<Func<TableauRepartitionCharge, object>> orderByExp = GenerateOrderByExpression(request.ItemToSearch);
                  if (string.IsNullOrEmpty(request.SortOrder) || (request.SortOrder != MySortOrder.Ascending && request.SortOrder != MySortOrder.Descending))
                    request.SortOrder = MySortOrder.Descending;

                  int totalRecord;
                  IEnumerable<TableauRepartitionCharge> items = _repositoryTableauRepartitionCharge.GetMany(exp, request.Index, request.Size, out totalRecord, orderByExp, request.SortOrder);
  
                  response.Count = totalRecord;
                  items = items as IList<TableauRepartitionCharge> ?? items.ToList();
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
      public BusinessResponse<decimal> GetSumTableauRepartitionChargesByCriteria(BusinessRequest<TableauRepartitionChargeDto> request)
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
                request.ItemsToSearch = new List<TableauRepartitionChargeDto>();                                                    
 
              var exps = new List<Expression<Func<TableauRepartitionCharge, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<TableauRepartitionCharge, bool>> exp = null;                  
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

                  var sum = _repositoryTableauRepartitionCharge.GetSum(exp, sumExp);
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
  		public async Task<BusinessResponse<TableauRepartitionChargeDto>> SaveTableauRepartitionCharges(BusinessRequest<TableauRepartitionChargeDto> request)
  		{
        var response = new BusinessResponse<TableauRepartitionChargeDto>();
  
              try
              {
                  foreach (var item in request.ItemsToSave.TakeWhile(BusinessRequest<TableauRepartitionChargeDto>.ValidateRequiredProperties))
                  {
                     var itemToSave = item.ToEntity();
                     TableauRepartitionCharge itemSaved = null;

                     itemToSave.DateMAJ = Utilities.CurrentDate; 
                     itemToSave.ModifiedBy = request.IdCurrentUser; 
                     if (itemToSave.IdTableauRepartitionCharge == 0)  
                     {  
                        itemToSave.DateCreation = Utilities.CurrentDate;   
                        itemToSave.CreatedBy = request.IdCurrentUser;   

                        itemToSave.IsDeleted = false;
                        itemSaved = _repositoryTableauRepartitionCharge.Add(itemToSave);
                     }
                     else //dans le cas d'une modification
                     {
                         itemSaved = _repositoryTableauRepartitionCharge.Update(itemToSave, p => p.IdTableauRepartitionCharge == itemToSave.IdTableauRepartitionCharge);
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
      private static Expression<Func<TableauRepartitionCharge, bool>> GenerateCriteria(TableauRepartitionChargeDto itemToSearch) 
      {
          Expression<Func<TableauRepartitionCharge, bool>> exprinit = obj => true;
          Expression<Func<TableauRepartitionCharge, bool>> expi = exprinit;
                                                             
          if (itemToSearch.InfoSearchIdTableauRepartitionCharge.Consider)
          {
              switch (itemToSearch.InfoSearchIdTableauRepartitionCharge.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdTableauRepartitionCharge != itemToSearch.IdTableauRepartitionCharge);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdTableauRepartitionCharge < itemToSearch.IdTableauRepartitionCharge);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdTableauRepartitionCharge <= itemToSearch.IdTableauRepartitionCharge);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdTableauRepartitionCharge > itemToSearch.IdTableauRepartitionCharge);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdTableauRepartitionCharge >= itemToSearch.IdTableauRepartitionCharge);                  
                      break;  
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdTableauRepartitionCharge == itemToSearch.IdTableauRepartitionCharge);                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchNumeroFiche.Consider)
          {
              switch (itemToSearch.InfoSearchNumeroFiche.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.NumeroFiche != itemToSearch.NumeroFiche);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.NumeroFiche.StartsWith(itemToSearch.NumeroFiche));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.NumeroFiche.EndsWith(itemToSearch.NumeroFiche));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.NumeroFiche == itemToSearch.NumeroFiche);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.NumeroFiche.Contains(itemToSearch.NumeroFiche));
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
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.Proposition >= itemToSearch.InfoSearchProposition.Intervalle.Debut && obj.Proposition <= itemToSearch.InfoSearchProposition.Intervalle.Fin);                         
                      break;    
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.Proposition == itemToSearch.Proposition);                  
                      break;
              }
          }            

          if (itemToSearch.InfoSearchIdProcessus.Consider)
          {
              switch (itemToSearch.InfoSearchIdProcessus.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdProcessus != itemToSearch.IdProcessus);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdProcessus < itemToSearch.IdProcessus);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdProcessus <= itemToSearch.IdProcessus);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdProcessus > itemToSearch.IdProcessus);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdProcessus >= itemToSearch.IdProcessus);                  
                      break;  
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdProcessus == itemToSearch.IdProcessus);                  
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
      private static Expression<Func<TableauRepartitionCharge, object>> GenerateOrderByExpression(TableauRepartitionChargeDto itemToSearch)
      {
          Expression<Func<TableauRepartitionCharge, object>> exp = null;

          if (itemToSearch.InfoSearchIdTableauRepartitionCharge.IsOrderByField)
            exp = (o => o.IdTableauRepartitionCharge);
          if (itemToSearch.InfoSearchNumeroFiche.IsOrderByField)
            exp = (o => o.NumeroFiche);
          if (itemToSearch.InfoSearchProposition.IsOrderByField)
            exp = (o => o.Proposition);
          if (itemToSearch.InfoSearchIdProcessus.IsOrderByField)
            exp = (o => o.IdProcessus);
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

          return exp ?? (obj => obj.IdTableauRepartitionCharge);
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private static Expression<Func<TableauRepartitionCharge, decimal?>> GenerateSumExpression(TableauRepartitionChargeDto itemToSearch)
      {
          Expression<Func<TableauRepartitionCharge, decimal?>> exp = null;

          if (itemToSearch.InfoSearchProposition.IsSumField)
            exp = (o => o.Proposition.Value);                     
            if (itemToSearch.InfoSearchCreatedBy.IsSumField)
            exp = (o => o.CreatedBy.Value);                     
            if (itemToSearch.InfoSearchModifiedBy.IsSumField)
            exp = (o => o.ModifiedBy.Value);                     
  
          //return exp ?? (obj => obj.IdTableauRepartitionCharge);
          return exp;
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private TableauRepartitionChargeDto GenerateSum(Expression<Func<TableauRepartitionCharge, bool>> criteria, TableauRepartitionChargeDto itemToSearch, TableauRepartitionChargeDto itemToReturn)
      {
          if (itemToSearch.InfoSearchProposition.IsSumField)
            itemToReturn.InfoSearchProposition.Sum = _repositoryTableauRepartitionCharge.GetSum(criteria, o => o.Proposition.Value);     
               
            if (itemToSearch.InfoSearchCreatedBy.IsSumField)
            itemToReturn.InfoSearchCreatedBy.Sum = _repositoryTableauRepartitionCharge.GetSum(criteria, o => o.CreatedBy.Value);     
               
            if (itemToSearch.InfoSearchModifiedBy.IsSumField)
            itemToReturn.InfoSearchModifiedBy.Sum = _repositoryTableauRepartitionCharge.GetSum(criteria, o => o.ModifiedBy.Value);     
               
  
          return itemToReturn;
      }
  }
}

