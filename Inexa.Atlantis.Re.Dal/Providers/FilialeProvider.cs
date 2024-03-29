﻿using System;
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
  public class FilialeProvider
  {

      #region Singleton
      
      private static FilialeProvider _instance;

      public static FilialeProvider Instance
      {
          get { return _instance ?? (_instance = new FilialeProvider()); }
      }

      #endregion

      private readonly IRepository<Filiale> _repositoryFiliale;
     
      public  FilialeProvider()
      {
          _repositoryFiliale = new Repository<Filiale>();
      }

      private FilialeProvider(IRepository<Filiale> repository)
      {    
          _repositoryFiliale = repository;
      }

      public async Task<BusinessResponse<FilialeDto>> GetFilialeById(object id)
      {
          var response = new BusinessResponse<FilialeDto>();

          try
          {
              var item = _repositoryFiliale[id];
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
      public async Task<BusinessResponse<FilialeDto>> GetFilialesByCriteria(BusinessRequest<FilialeDto> request)
      {
          var response = new BusinessResponse<FilialeDto>();

          try
          {
              request.ToFinalRequest();

              // générer expression lambda de l'objet de recherche principal
              var mainexp = GenerateCriteria(request.ItemToSearch);                  
              mainexp = (mainexp == null) ? obj => obj.IsDeleted == false : mainexp.And(obj => obj.IsDeleted == false);                   

              // générer expression lambda des objets de recherche sécondaires
              if (request.ItemsToSearch == null) 
                request.ItemsToSearch = new List<FilialeDto>();                                                    
 
              var exps = new List<Expression<Func<Filiale, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<Filiale, bool>> exp = null;                  
              foreach (var expi in exps)
              {
                  if(request.NotIn)
                      exp = (exp == null) ? expi : exp.And(expi);
                  else
                      exp = (exp == null) ? expi : exp.Or(expi); 
              }

                  // concaténer les critères principaux et sécondaires
                  exp = (exp == null) ? mainexp : mainexp.And(exp);                  
                  
                  Expression<Func<Filiale, object>> orderByExp = GenerateOrderByExpression(request.ItemToSearch);
                  if (string.IsNullOrEmpty(request.SortOrder) || (request.SortOrder != MySortOrder.Ascending && request.SortOrder != MySortOrder.Descending))
                    request.SortOrder = MySortOrder.Descending;

                  int totalRecord;
                  IEnumerable<Filiale> items = _repositoryFiliale.GetMany(exp, request.Index, request.Size, out totalRecord, orderByExp, request.SortOrder);
  
                  response.Count = totalRecord;
                  items = items as IList<Filiale> ?? items.ToList();
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
      public BusinessResponse<decimal> GetSumFilialesByCriteria(BusinessRequest<FilialeDto> request)
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
                request.ItemsToSearch = new List<FilialeDto>();                                                    
 
              var exps = new List<Expression<Func<Filiale, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<Filiale, bool>> exp = null;                  
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

                  var sum = _repositoryFiliale.GetSum(exp, sumExp);
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
  		public async Task<BusinessResponse<FilialeDto>> SaveFiliales(BusinessRequest<FilialeDto> request)
  		{
        var response = new BusinessResponse<FilialeDto>();
  
              try
              {
                  foreach (var item in request.ItemsToSave.TakeWhile(BusinessRequest<FilialeDto>.ValidateRequiredProperties))
                  {
                     var itemToSave = item.ToEntity();
                     Filiale itemSaved = null;

                     itemToSave.DateMAJ = Utilities.CurrentDate; 
                     itemToSave.ModifiedBy = request.IdCurrentUser; 
                     if (itemToSave.IdFiliale == 0)  
                     {  
                        itemToSave.DateCreation = Utilities.CurrentDate;   
                        itemToSave.CreatedBy = request.IdCurrentUser;   

                        itemToSave.IsDeleted = false;
                        itemSaved = _repositoryFiliale.Add(itemToSave);
                     }
                     else //dans le cas d'une modification
                     {
                         itemSaved = _repositoryFiliale.Update(itemToSave, p => p.IdFiliale == itemToSave.IdFiliale);
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
      private static Expression<Func<Filiale, bool>> GenerateCriteria(FilialeDto itemToSearch) 
      {
          Expression<Func<Filiale, bool>> exprinit = obj => true;
          Expression<Func<Filiale, bool>> expi = exprinit;
                                                             
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

          if (itemToSearch.InfoSearchNomFilliale.Consider)
          {
              switch (itemToSearch.InfoSearchNomFilliale.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.NomFilliale != itemToSearch.NomFilliale);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.NomFilliale.StartsWith(itemToSearch.NomFilliale));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.NomFilliale.EndsWith(itemToSearch.NomFilliale));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.NomFilliale == itemToSearch.NomFilliale);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.NomFilliale.Contains(itemToSearch.NomFilliale));
                      break;
              }
          }                               
                 
          if (itemToSearch.InfoSearchAdresseFiliale.Consider)
          {
              switch (itemToSearch.InfoSearchAdresseFiliale.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.AdresseFiliale != itemToSearch.AdresseFiliale);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.AdresseFiliale.StartsWith(itemToSearch.AdresseFiliale));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.AdresseFiliale.EndsWith(itemToSearch.AdresseFiliale));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.AdresseFiliale == itemToSearch.AdresseFiliale);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.AdresseFiliale.Contains(itemToSearch.AdresseFiliale));
                      break;
              }
          }                               
                 
          if (itemToSearch.InfoSearchCodeFiliale.Consider)
          {
              switch (itemToSearch.InfoSearchCodeFiliale.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.CodeFiliale != itemToSearch.CodeFiliale);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.CodeFiliale.StartsWith(itemToSearch.CodeFiliale));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.CodeFiliale.EndsWith(itemToSearch.CodeFiliale));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.CodeFiliale == itemToSearch.CodeFiliale);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.CodeFiliale.Contains(itemToSearch.CodeFiliale));
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
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdPays == itemToSearch.IdPays);                  
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
      private static Expression<Func<Filiale, object>> GenerateOrderByExpression(FilialeDto itemToSearch)
      {
          Expression<Func<Filiale, object>> exp = null;

          if (itemToSearch.InfoSearchIdFiliale.IsOrderByField)
            exp = (o => o.IdFiliale);
          if (itemToSearch.InfoSearchNomFilliale.IsOrderByField)
            exp = (o => o.NomFilliale);
          if (itemToSearch.InfoSearchAdresseFiliale.IsOrderByField)
            exp = (o => o.AdresseFiliale);
          if (itemToSearch.InfoSearchCodeFiliale.IsOrderByField)
            exp = (o => o.CodeFiliale);
          if (itemToSearch.InfoSearchIdPays.IsOrderByField)
            exp = (o => o.IdPays);
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

          return exp ?? (obj => obj.IdFiliale);
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private static Expression<Func<Filiale, decimal?>> GenerateSumExpression(FilialeDto itemToSearch)
      {
          Expression<Func<Filiale, decimal?>> exp = null;

          if (itemToSearch.InfoSearchCreatedBy.IsSumField)
            exp = (o => o.CreatedBy.Value);                     
            if (itemToSearch.InfoSearchModifiedBy.IsSumField)
            exp = (o => o.ModifiedBy.Value);                     
  
          //return exp ?? (obj => obj.IdFiliale);
          return exp;
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private FilialeDto GenerateSum(Expression<Func<Filiale, bool>> criteria, FilialeDto itemToSearch, FilialeDto itemToReturn)
      {
          if (itemToSearch.InfoSearchCreatedBy.IsSumField)
            itemToReturn.InfoSearchCreatedBy.Sum = _repositoryFiliale.GetSum(criteria, o => o.CreatedBy.Value);     
               
            if (itemToSearch.InfoSearchModifiedBy.IsSumField)
            itemToReturn.InfoSearchModifiedBy.Sum = _repositoryFiliale.GetSum(criteria, o => o.ModifiedBy.Value);     
               
  
          return itemToReturn;
      }
  }
}

