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
  public class HistoriquePasswordProvider
  {

      #region Singleton
      
      private static HistoriquePasswordProvider _instance;

      public static HistoriquePasswordProvider Instance
      {
          get { return _instance ?? (_instance = new HistoriquePasswordProvider()); }
      }

      #endregion

      private readonly IRepository<HistoriquePassword> _repositoryHistoriquePassword;
     
      private HistoriquePasswordProvider()
      {
          _repositoryHistoriquePassword = new Repository<HistoriquePassword>();
      }

      private HistoriquePasswordProvider(IRepository<HistoriquePassword> repository)
      {    
          _repositoryHistoriquePassword = repository;
      }

      public BusinessResponse<HistoriquePasswordDto> GetHistoriquePasswordById(object id)
      {
          var response = new BusinessResponse<HistoriquePasswordDto>();

          try
          {
              var item = _repositoryHistoriquePassword[id];
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
      public BusinessResponse<HistoriquePasswordDto> GetHistoriquePasswordsByCriteria(BusinessRequest<HistoriquePasswordDto> request)
      {
          var response = new BusinessResponse<HistoriquePasswordDto>();

          try
          {
              request.ToFinalRequest();

              // générer expression lambda de l'objet de recherche principal
              var mainexp = GenerateCriteria(request.ItemToSearch);                  
              mainexp = (mainexp == null) ? obj => obj.IsDeleted == false : mainexp.And(obj => obj.IsDeleted == false);                   

              // générer expression lambda des objets de recherche sécondaires
              if (request.ItemsToSearch == null) 
                request.ItemsToSearch = new List<HistoriquePasswordDto>();                                                    
 
              var exps = new List<Expression<Func<HistoriquePassword, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<HistoriquePassword, bool>> exp = null;                  
              foreach (var expi in exps)
              {
                  if(request.NotIn)
                      exp = (exp == null) ? expi : exp.And(expi);
                  else
                      exp = (exp == null) ? expi : exp.Or(expi); 
              }

                  // concaténer les critères principaux et sécondaires
                  exp = (exp == null) ? mainexp : mainexp.And(exp);                  
                  
                  Expression<Func<HistoriquePassword, object>> orderByExp = GenerateOrderByExpression(request.ItemToSearch);
                  if (string.IsNullOrEmpty(request.SortOrder) || (request.SortOrder != MySortOrder.Ascending && request.SortOrder != MySortOrder.Descending))
                    request.SortOrder = MySortOrder.Descending;

                  int totalRecord;
                  IEnumerable<HistoriquePassword> items = _repositoryHistoriquePassword.GetMany(exp, request.Index, request.Size, out totalRecord, orderByExp, request.SortOrder);
  
                  response.Count = totalRecord;
                  items = items as IList<HistoriquePassword> ?? items.ToList();
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
      public BusinessResponse<decimal> GetSumHistoriquePasswordsByCriteria(BusinessRequest<HistoriquePasswordDto> request)
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
                request.ItemsToSearch = new List<HistoriquePasswordDto>();                                                    
 
              var exps = new List<Expression<Func<HistoriquePassword, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<HistoriquePassword, bool>> exp = null;                  
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

                  var sum = _repositoryHistoriquePassword.GetSum(exp, sumExp);
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
  		public BusinessResponse<HistoriquePasswordDto> SaveHistoriquePasswords(BusinessRequest<HistoriquePasswordDto> request)
  		{
        var response = new BusinessResponse<HistoriquePasswordDto>();
  
              try
              {
                  foreach (var item in request.ItemsToSave.TakeWhile(BusinessRequest<HistoriquePasswordDto>.ValidateRequiredProperties))
                  {
                     var itemToSave = item.ToEntity();
                     HistoriquePassword itemSaved = null;

                     itemToSave.ModifiedBy = request.IdCurrentUser; 
                     if (itemToSave.IdHistoriquePasword == 0)  
                     {  
                        itemToSave.CreatedBy = request.IdCurrentUser;   

                        itemToSave.IsDeleted = false;
                        itemSaved = _repositoryHistoriquePassword.Add(itemToSave);
                     }
                     else //dans le cas d'une modification
                     {
                         itemSaved = _repositoryHistoriquePassword.Update(itemToSave, p => p.IdHistoriquePasword == itemToSave.IdHistoriquePasword);
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
      private static Expression<Func<HistoriquePassword, bool>> GenerateCriteria(HistoriquePasswordDto itemToSearch) 
      {
          Expression<Func<HistoriquePassword, bool>> exprinit = obj => true;
          Expression<Func<HistoriquePassword, bool>> expi = exprinit;
                                                             
          if (itemToSearch.InfoSearchIdHistoriquePasword.Consider)
          {
              switch (itemToSearch.InfoSearchIdHistoriquePasword.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdHistoriquePasword != itemToSearch.IdHistoriquePasword);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdHistoriquePasword < itemToSearch.IdHistoriquePasword);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdHistoriquePasword <= itemToSearch.IdHistoriquePasword);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdHistoriquePasword > itemToSearch.IdHistoriquePasword);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdHistoriquePasword >= itemToSearch.IdHistoriquePasword);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.IdHistoriquePasword >= itemToSearch.InfoSearchIdHistoriquePasword.Intervalle.Debut && obj.IdHistoriquePasword <= itemToSearch.InfoSearchIdHistoriquePasword.Intervalle.Fin);                         
                      break;        
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdHistoriquePasword == itemToSearch.IdHistoriquePasword);                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchIdUtilisateur.Consider)
          {
              switch (itemToSearch.InfoSearchIdUtilisateur.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdUtilisateur != itemToSearch.IdUtilisateur);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdUtilisateur < itemToSearch.IdUtilisateur);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdUtilisateur <= itemToSearch.IdUtilisateur);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdUtilisateur > itemToSearch.IdUtilisateur);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdUtilisateur >= itemToSearch.IdUtilisateur);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.IdUtilisateur >= itemToSearch.InfoSearchIdUtilisateur.Intervalle.Debut && obj.IdUtilisateur <= itemToSearch.InfoSearchIdUtilisateur.Intervalle.Fin);                         
                      break;        
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdUtilisateur == itemToSearch.IdUtilisateur);                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchPassword.Consider)
          {
              switch (itemToSearch.InfoSearchPassword.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.Password != itemToSearch.Password);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.Password.StartsWith(itemToSearch.Password));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.Password.EndsWith(itemToSearch.Password));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.Password == itemToSearch.Password);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.Password.Contains(itemToSearch.Password));
                      break;
              }
          }                               
                 
          if (itemToSearch.InfoSearchDateHistorique.Consider)
          {
              switch (itemToSearch.InfoSearchDateHistorique.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And
                              (obj => 
                                !(obj.DateHistorique.Year == itemToSearch.DateHistorique.Year
                                  && obj.DateHistorique.Month == itemToSearch.DateHistorique.Month
                                  && obj.DateHistorique.Day == itemToSearch.DateHistorique.Day)
                              );
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.DateHistorique.CompareTo(itemToSearch.DateHistorique) < 0);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.DateHistorique.CompareTo(itemToSearch.DateHistorique) <= 0);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.DateHistorique.CompareTo(itemToSearch.DateHistorique) > 0);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.DateHistorique.CompareTo(itemToSearch.DateHistorique)>= 0);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      var debut = itemToSearch.InfoSearchDateHistorique.Intervalle.Debut.Date;
                      var fin = itemToSearch.InfoSearchDateHistorique.Intervalle.Fin.AddDays(1).Date;
                      expi = expi.And(obj => obj.DateHistorique >= debut && obj.DateHistorique < fin);                  
                      break;         
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And
                            (
                              obj => obj.DateHistorique.Year == itemToSearch.DateHistorique.Year
                              && obj.DateHistorique.Month == itemToSearch.DateHistorique.Month
                              && obj.DateHistorique.Day == itemToSearch.DateHistorique.Day
                            );                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchDateValiditePassword.Consider)
          {
              switch (itemToSearch.InfoSearchDateValiditePassword.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And
                            (obj => 
                                !(obj.DateValiditePassword.Value.Year == itemToSearch.DateValiditePassword.Value.Year
                                && obj.DateValiditePassword.Value.Month == itemToSearch.DateValiditePassword.Value.Month
                                && obj.DateValiditePassword.Value.Day == itemToSearch.DateValiditePassword.Value.Day)
                             );
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateValiditePassword.Value, itemToSearch.DateValiditePassword.Value) < 0);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateValiditePassword.Value, itemToSearch.DateValiditePassword.Value) <= 0);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateValiditePassword.Value, itemToSearch.DateValiditePassword.Value) > 0);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateValiditePassword.Value, itemToSearch.DateValiditePassword.Value) >= 0);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      var debut = itemToSearch.InfoSearchDateValiditePassword.Intervalle.Debut.Date;
                      var fin = itemToSearch.InfoSearchDateValiditePassword.Intervalle.Fin.AddDays(1).Date;
                      expi = expi.And(obj => obj.DateValiditePassword >= debut && obj.DateValiditePassword < fin);                  
                      break;         
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And
                            (
                              obj => obj.DateValiditePassword.Value.Year == itemToSearch.DateValiditePassword.Value.Year
                              && obj.DateValiditePassword.Value.Month == itemToSearch.DateValiditePassword.Value.Month
                              && obj.DateValiditePassword.Value.Day == itemToSearch.DateValiditePassword.Value.Day
                             );                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchFinValiditePassword.Consider)
          {
              switch (itemToSearch.InfoSearchFinValiditePassword.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And
                            (obj => 
                                !(obj.FinValiditePassword.Value.Year == itemToSearch.FinValiditePassword.Value.Year
                                && obj.FinValiditePassword.Value.Month == itemToSearch.FinValiditePassword.Value.Month
                                && obj.FinValiditePassword.Value.Day == itemToSearch.FinValiditePassword.Value.Day)
                             );
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.FinValiditePassword.Value, itemToSearch.FinValiditePassword.Value) < 0);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.FinValiditePassword.Value, itemToSearch.FinValiditePassword.Value) <= 0);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.FinValiditePassword.Value, itemToSearch.FinValiditePassword.Value) > 0);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.FinValiditePassword.Value, itemToSearch.FinValiditePassword.Value) >= 0);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      var debut = itemToSearch.InfoSearchFinValiditePassword.Intervalle.Debut.Date;
                      var fin = itemToSearch.InfoSearchFinValiditePassword.Intervalle.Fin.AddDays(1).Date;
                      expi = expi.And(obj => obj.FinValiditePassword >= debut && obj.FinValiditePassword < fin);                  
                      break;         
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And
                            (
                              obj => obj.FinValiditePassword.Value.Year == itemToSearch.FinValiditePassword.Value.Year
                              && obj.FinValiditePassword.Value.Month == itemToSearch.FinValiditePassword.Value.Month
                              && obj.FinValiditePassword.Value.Day == itemToSearch.FinValiditePassword.Value.Day
                             );                  
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

                  if (itemToSearch.InfoSearchCreateOn.Consider)
          {
              switch (itemToSearch.InfoSearchCreateOn.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And
                            (obj => 
                                !(obj.CreateOn.Value.Year == itemToSearch.CreateOn.Value.Year
                                && obj.CreateOn.Value.Month == itemToSearch.CreateOn.Value.Month
                                && obj.CreateOn.Value.Day == itemToSearch.CreateOn.Value.Day)
                             );
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.CreateOn.Value, itemToSearch.CreateOn.Value) < 0);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.CreateOn.Value, itemToSearch.CreateOn.Value) <= 0);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.CreateOn.Value, itemToSearch.CreateOn.Value) > 0);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.CreateOn.Value, itemToSearch.CreateOn.Value) >= 0);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      var debut = itemToSearch.InfoSearchCreateOn.Intervalle.Debut.Date;
                      var fin = itemToSearch.InfoSearchCreateOn.Intervalle.Fin.AddDays(1).Date;
                      expi = expi.And(obj => obj.CreateOn >= debut && obj.CreateOn < fin);                  
                      break;         
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And
                            (
                              obj => obj.CreateOn.Value.Year == itemToSearch.CreateOn.Value.Year
                              && obj.CreateOn.Value.Month == itemToSearch.CreateOn.Value.Month
                              && obj.CreateOn.Value.Day == itemToSearch.CreateOn.Value.Day
                             );                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchModifiedOn.Consider)
          {
              switch (itemToSearch.InfoSearchModifiedOn.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And
                            (obj => 
                                !(obj.ModifiedOn.Value.Year == itemToSearch.ModifiedOn.Value.Year
                                && obj.ModifiedOn.Value.Month == itemToSearch.ModifiedOn.Value.Month
                                && obj.ModifiedOn.Value.Day == itemToSearch.ModifiedOn.Value.Day)
                             );
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.ModifiedOn.Value, itemToSearch.ModifiedOn.Value) < 0);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.ModifiedOn.Value, itemToSearch.ModifiedOn.Value) <= 0);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.ModifiedOn.Value, itemToSearch.ModifiedOn.Value) > 0);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.ModifiedOn.Value, itemToSearch.ModifiedOn.Value) >= 0);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      var debut = itemToSearch.InfoSearchModifiedOn.Intervalle.Debut.Date;
                      var fin = itemToSearch.InfoSearchModifiedOn.Intervalle.Fin.AddDays(1).Date;
                      expi = expi.And(obj => obj.ModifiedOn >= debut && obj.ModifiedOn < fin);                  
                      break;         
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And
                            (
                              obj => obj.ModifiedOn.Value.Year == itemToSearch.ModifiedOn.Value.Year
                              && obj.ModifiedOn.Value.Month == itemToSearch.ModifiedOn.Value.Month
                              && obj.ModifiedOn.Value.Day == itemToSearch.ModifiedOn.Value.Day
                             );                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchCreateBy.Consider)
          {
              switch (itemToSearch.InfoSearchCreateBy.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.CreateBy != itemToSearch.CreateBy);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.CreateBy < itemToSearch.CreateBy);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.CreateBy <= itemToSearch.CreateBy);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.CreateBy > itemToSearch.CreateBy);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.CreateBy >= itemToSearch.CreateBy);                  
                      break;  
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.CreateBy == itemToSearch.CreateBy);                  
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
      private static Expression<Func<HistoriquePassword, object>> GenerateOrderByExpression(HistoriquePasswordDto itemToSearch)
      {
          Expression<Func<HistoriquePassword, object>> exp = null;

          if (itemToSearch.InfoSearchIdHistoriquePasword.IsOrderByField)
            exp = (o => o.IdHistoriquePasword);
          if (itemToSearch.InfoSearchIdUtilisateur.IsOrderByField)
            exp = (o => o.IdUtilisateur);
          if (itemToSearch.InfoSearchPassword.IsOrderByField)
            exp = (o => o.Password);
          if (itemToSearch.InfoSearchDateHistorique.IsOrderByField)
            exp = (o => o.DateHistorique);
          if (itemToSearch.InfoSearchDateValiditePassword.IsOrderByField)
            exp = (o => o.DateValiditePassword);
          if (itemToSearch.InfoSearchFinValiditePassword.IsOrderByField)
            exp = (o => o.FinValiditePassword);
          if (itemToSearch.InfoSearchIsDeleted.IsOrderByField)
            exp = (o => o.IsDeleted);
          if (itemToSearch.InfoSearchCreateOn.IsOrderByField)
            exp = (o => o.CreateOn);
          if (itemToSearch.InfoSearchModifiedOn.IsOrderByField)
            exp = (o => o.ModifiedOn);
          if (itemToSearch.InfoSearchCreateBy.IsOrderByField)
            exp = (o => o.CreateBy);
          if (itemToSearch.InfoSearchModifiedBy.IsOrderByField)
            exp = (o => o.ModifiedBy);
          if (itemToSearch.InfoSearchCreatedBy.IsOrderByField)
            exp = (o => o.CreatedBy);

          return exp ?? (obj => obj.IdHistoriquePasword);
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private static Expression<Func<HistoriquePassword, decimal?>> GenerateSumExpression(HistoriquePasswordDto itemToSearch)
      {
          Expression<Func<HistoriquePassword, decimal?>> exp = null;

          if (itemToSearch.InfoSearchCreateBy.IsSumField)
            exp = (o => o.CreateBy.Value);                     
            if (itemToSearch.InfoSearchModifiedBy.IsSumField)
            exp = (o => o.ModifiedBy.Value);                     
            if (itemToSearch.InfoSearchCreatedBy.IsSumField)
            exp = (o => o.CreatedBy.Value);                     
  
          //return exp ?? (obj => obj.IdHistoriquePasword);
          return exp;
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private HistoriquePasswordDto GenerateSum(Expression<Func<HistoriquePassword, bool>> criteria, HistoriquePasswordDto itemToSearch, HistoriquePasswordDto itemToReturn)
      {
          if (itemToSearch.InfoSearchCreateBy.IsSumField)
            itemToReturn.InfoSearchCreateBy.Sum = _repositoryHistoriquePassword.GetSum(criteria, o => o.CreateBy.Value);     
               
            if (itemToSearch.InfoSearchModifiedBy.IsSumField)
            itemToReturn.InfoSearchModifiedBy.Sum = _repositoryHistoriquePassword.GetSum(criteria, o => o.ModifiedBy.Value);     
               
            if (itemToSearch.InfoSearchCreatedBy.IsSumField)
            itemToReturn.InfoSearchCreatedBy.Sum = _repositoryHistoriquePassword.GetSum(criteria, o => o.CreatedBy.Value);     
               
  
          return itemToReturn;
      }
  }
}

