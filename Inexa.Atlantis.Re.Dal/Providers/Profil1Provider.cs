using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Inexa.Atlantis.Re.Dal;
using Inexa.Atlantis.Re.Dal.Repository;
using Inexa.Atlantis.Re.Commons.Dtos;
using Inexa.Atlantis.Re.Commons.Infras.Domains;
using Inexa.Atlantis.Re.Commons.Infras.Enums;
using Inexa.Atlantis.Re.Commons.Infras.Helpers;

namespace Inexa.Atlantis.Re.Dal.Providers
{
  public class Profil1Provider
  {

      #region Singleton
      
      private static Profil1Provider _instance;

      public static Profil1Provider Instance
      {
          get { return _instance ?? (_instance = new Profil1Provider()); }
      }

      #endregion

      private readonly IRepository<Profil1> _repositoryProfil1;
     
      private Profil1Provider()
      {
          _repositoryProfil1 = new Repository<Profil1>();
      }

      private Profil1Provider(IRepository<Profil1> repository)
      {    
          _repositoryProfil1 = repository;
      }

      public BusinessResponse<Profil1Dto> GetProfil1ById(object id)
      {
          var response = new BusinessResponse<Profil1Dto>();

          try
          {
              var item = _repositoryProfil1[id];
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
      public BusinessResponse<Profil1Dto> GetProfil1sByCriteria(BusinessRequest<Profil1Dto> request)
      {
          var response = new BusinessResponse<Profil1Dto>();

          try
          {
              request.ToFinalRequest();

              // générer expression lambda de l'objet de recherche principal
              var mainexp = GenerateCriteria(request.ItemToSearch);                  
              mainexp = (mainexp == null) ? obj => obj.IsDeleted == false : mainexp.And(obj => obj.IsDeleted == false);                   

              // générer expression lambda des objets de recherche sécondaires
              if (request.ItemsToSearch == null) 
                request.ItemsToSearch = new List<Profil1Dto>();                                                    
 
              var exps = new List<Expression<Func<Profil1, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<Profil1, bool>> exp = null;                  
              foreach (var expi in exps)
              {
                  if(request.NotIn)
                      exp = (exp == null) ? expi : exp.And(expi);
                  else
                      exp = (exp == null) ? expi : exp.Or(expi); 
              }

                  // concaténer les critères principaux et sécondaires
                  exp = (exp == null) ? mainexp : mainexp.And(exp);                  
                  
                  Expression<Func<Profil1, object>> orderByExp = GenerateOrderByExpression(request.ItemToSearch);
                  if (string.IsNullOrEmpty(request.SortOrder) || (request.SortOrder != MySortOrder.Ascending && request.SortOrder != MySortOrder.Descending))
                    request.SortOrder = MySortOrder.Descending;

                  int totalRecord;
                  IEnumerable<Profil1> items = _repositoryProfil1.GetMany(exp, request.Index, request.Size, out totalRecord, orderByExp, request.SortOrder);
  
                  response.Count = totalRecord;
                  items = items as IList<Profil1> ?? items.ToList();
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
      public BusinessResponse<decimal> GetSumProfil1sByCriteria(BusinessRequest<Profil1Dto> request)
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
                request.ItemsToSearch = new List<Profil1Dto>();                                                    
 
              var exps = new List<Expression<Func<Profil1, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<Profil1, bool>> exp = null;                  
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

                  var sum = _repositoryProfil1.GetSum(exp, sumExp);
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
  		public BusinessResponse<Profil1Dto> SaveProfil1s(BusinessRequest<Profil1Dto> request)
  		{
        var response = new BusinessResponse<Profil1Dto>();
  
              try
              {
                  foreach (var item in request.ItemsToSave.TakeWhile(BusinessRequest<Profil1Dto>.ValidateRequiredProperties))
                  {
                     var itemToSave = item.ToEntity();
                     Profil1 itemSaved = null;

                     itemToSave.ModifiedBy = request.IdCurrentUser; 
                     itemToSave.DateMaj = Utilities.CurrentDate; 
                     if (itemToSave.IdProfil == 0)  
                     {  
                        itemToSave.CreatedBy = request.IdCurrentUser;   
                        itemToSave.DateCreation = Utilities.CurrentDate;   

                        itemToSave.IsDeleted = false;
                        itemSaved = _repositoryProfil1.Add(itemToSave);
                     }
                     else //dans le cas d'une modification
                     {
                         itemSaved = _repositoryProfil1.Update(itemToSave, p => p.IdProfil == itemToSave.IdProfil);
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
      private static Expression<Func<Profil1, bool>> GenerateCriteria(Profil1Dto itemToSearch) 
      {
          Expression<Func<Profil1, bool>> exprinit = obj => true;
          Expression<Func<Profil1, bool>> expi = exprinit;
                                                             
          if (itemToSearch.InfoSearchIdProfil.Consider)
          {
              switch (itemToSearch.InfoSearchIdProfil.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdProfil != itemToSearch.IdProfil);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdProfil < itemToSearch.IdProfil);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdProfil <= itemToSearch.IdProfil);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdProfil > itemToSearch.IdProfil);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdProfil >= itemToSearch.IdProfil);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.IdProfil >= itemToSearch.InfoSearchIdProfil.Intervalle.Debut && obj.IdProfil <= itemToSearch.InfoSearchIdProfil.Intervalle.Fin);                         
                      break;        
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdProfil == itemToSearch.IdProfil);                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchCodeProfil.Consider)
          {
              switch (itemToSearch.InfoSearchCodeProfil.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.CodeProfil != itemToSearch.CodeProfil);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.CodeProfil.StartsWith(itemToSearch.CodeProfil));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.CodeProfil.EndsWith(itemToSearch.CodeProfil));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.CodeProfil == itemToSearch.CodeProfil);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.CodeProfil.Contains(itemToSearch.CodeProfil));
                      break;
              }
          }                               
                 
          if (itemToSearch.InfoSearchIntitule.Consider)
          {
              switch (itemToSearch.InfoSearchIntitule.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.Intitule != itemToSearch.Intitule);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.Intitule.StartsWith(itemToSearch.Intitule));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.Intitule.EndsWith(itemToSearch.Intitule));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.Intitule == itemToSearch.Intitule);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.Intitule.Contains(itemToSearch.Intitule));
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
                                !(obj.DateCreation.Year == itemToSearch.DateCreation.Year
                                  && obj.DateCreation.Month == itemToSearch.DateCreation.Month
                                  && obj.DateCreation.Day == itemToSearch.DateCreation.Day)
                              );
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.DateCreation.CompareTo(itemToSearch.DateCreation) < 0);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.DateCreation.CompareTo(itemToSearch.DateCreation) <= 0);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.DateCreation.CompareTo(itemToSearch.DateCreation) > 0);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.DateCreation.CompareTo(itemToSearch.DateCreation)>= 0);                  
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
                              obj => obj.DateCreation.Year == itemToSearch.DateCreation.Year
                              && obj.DateCreation.Month == itemToSearch.DateCreation.Month
                              && obj.DateCreation.Day == itemToSearch.DateCreation.Day
                            );                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchDateMaj.Consider)
          {
              switch (itemToSearch.InfoSearchDateMaj.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And
                            (obj => 
                                !(obj.DateMaj.Value.Year == itemToSearch.DateMaj.Value.Year
                                && obj.DateMaj.Value.Month == itemToSearch.DateMaj.Value.Month
                                && obj.DateMaj.Value.Day == itemToSearch.DateMaj.Value.Day)
                             );
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateMaj.Value, itemToSearch.DateMaj.Value) < 0);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateMaj.Value, itemToSearch.DateMaj.Value) <= 0);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateMaj.Value, itemToSearch.DateMaj.Value) > 0);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateMaj.Value, itemToSearch.DateMaj.Value) >= 0);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      var debut = itemToSearch.InfoSearchDateMaj.Intervalle.Debut.Date;
                      var fin = itemToSearch.InfoSearchDateMaj.Intervalle.Fin.AddDays(1).Date;
                      expi = expi.And(obj => obj.DateMaj >= debut && obj.DateMaj < fin);                  
                      break;         
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And
                            (
                              obj => obj.DateMaj.Value.Year == itemToSearch.DateMaj.Value.Year
                              && obj.DateMaj.Value.Month == itemToSearch.DateMaj.Value.Month
                              && obj.DateMaj.Value.Day == itemToSearch.DateMaj.Value.Day
                             );                  
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
      private static Expression<Func<Profil1, object>> GenerateOrderByExpression(Profil1Dto itemToSearch)
      {
          Expression<Func<Profil1, object>> exp = null;

          if (itemToSearch.InfoSearchIdProfil.IsOrderByField)
            exp = (o => o.IdProfil);
          if (itemToSearch.InfoSearchCodeProfil.IsOrderByField)
            exp = (o => o.CodeProfil);
          if (itemToSearch.InfoSearchIntitule.IsOrderByField)
            exp = (o => o.Intitule);
          if (itemToSearch.InfoSearchDescription.IsOrderByField)
            exp = (o => o.Description);
          if (itemToSearch.InfoSearchIsDeleted.IsOrderByField)
            exp = (o => o.IsDeleted);
          if (itemToSearch.InfoSearchCreatedBy.IsOrderByField)
            exp = (o => o.CreatedBy);
          if (itemToSearch.InfoSearchModifiedBy.IsOrderByField)
            exp = (o => o.ModifiedBy);
          if (itemToSearch.InfoSearchDateCreation.IsOrderByField)
            exp = (o => o.DateCreation);
          if (itemToSearch.InfoSearchDateMaj.IsOrderByField)
            exp = (o => o.DateMaj);

          return exp ?? (obj => obj.IdProfil);
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private static Expression<Func<Profil1, decimal?>> GenerateSumExpression(Profil1Dto itemToSearch)
      {
          Expression<Func<Profil1, decimal?>> exp = null;

          if (itemToSearch.InfoSearchCreatedBy.IsSumField)
            exp = (o => o.CreatedBy);                                         
            if (itemToSearch.InfoSearchModifiedBy.IsSumField)
            exp = (o => o.ModifiedBy.Value);                     
  
          //return exp ?? (obj => obj.IdProfil);
          return exp;
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private Profil1Dto GenerateSum(Expression<Func<Profil1, bool>> criteria, Profil1Dto itemToSearch, Profil1Dto itemToReturn)
      {
          if (itemToSearch.InfoSearchCreatedBy.IsSumField)
            itemToReturn.InfoSearchCreatedBy.Sum = _repositoryProfil1.GetSum(criteria, o => o.CreatedBy);
                                        
            if (itemToSearch.InfoSearchModifiedBy.IsSumField)
            itemToReturn.InfoSearchModifiedBy.Sum = _repositoryProfil1.GetSum(criteria, o => o.ModifiedBy.Value);     
               
  
          return itemToReturn;
      }
  }
}

