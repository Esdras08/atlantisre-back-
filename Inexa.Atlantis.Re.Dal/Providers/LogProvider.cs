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
  public class LogProvider
  {

      #region Singleton
      
      private static LogProvider _instance;

      public static LogProvider Instance
      {
          get { return _instance ?? (_instance = new LogProvider()); }
      }

      #endregion

      private readonly IRepository<Log> _repositoryLog;
     
      private LogProvider()
      {
          _repositoryLog = new Repository<Log>();
      }

      private LogProvider(IRepository<Log> repository)
      {    
          _repositoryLog = repository;
      }

      public BusinessResponse<LogDto> GetLogById(object id)
      {
          var response = new BusinessResponse<LogDto>();

          try
          {
              var item = _repositoryLog[id];
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
      public BusinessResponse<LogDto> GetLogsByCriteria(BusinessRequest<LogDto> request)
      {
          var response = new BusinessResponse<LogDto>();

          try
          {
              request.ToFinalRequest();

              // générer expression lambda de l'objet de recherche principal
              var mainexp = GenerateCriteria(request.ItemToSearch);                  
              mainexp = (mainexp == null) ? obj => obj.IsDeleted == false : mainexp.And(obj => obj.IsDeleted == false);                   

              // générer expression lambda des objets de recherche sécondaires
              if (request.ItemsToSearch == null) 
                request.ItemsToSearch = new List<LogDto>();                                                    
 
              var exps = new List<Expression<Func<Log, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<Log, bool>> exp = null;                  
              foreach (var expi in exps)
              {
                  if(request.NotIn)
                      exp = (exp == null) ? expi : exp.And(expi);
                  else
                      exp = (exp == null) ? expi : exp.Or(expi); 
              }

                  // concaténer les critères principaux et sécondaires
                  exp = (exp == null) ? mainexp : mainexp.And(exp);                  
                  
                  Expression<Func<Log, object>> orderByExp = GenerateOrderByExpression(request.ItemToSearch);
                  if (string.IsNullOrEmpty(request.SortOrder) || (request.SortOrder != MySortOrder.Ascending && request.SortOrder != MySortOrder.Descending))
                    request.SortOrder = MySortOrder.Descending;

                  int totalRecord;
                  IEnumerable<Log> items = _repositoryLog.GetMany(exp, request.Index, request.Size, out totalRecord, orderByExp, request.SortOrder);
  
                  response.Count = totalRecord;
                  items = items as IList<Log> ?? items.ToList();
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
      public BusinessResponse<decimal> GetSumLogsByCriteria(BusinessRequest<LogDto> request)
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
                request.ItemsToSearch = new List<LogDto>();                                                    
 
              var exps = new List<Expression<Func<Log, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<Log, bool>> exp = null;                  
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

                  var sum = _repositoryLog.GetSum(exp, sumExp);
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
  		public BusinessResponse<LogDto> SaveLogs(BusinessRequest<LogDto> request)
  		{
        var response = new BusinessResponse<LogDto>();
  
              try
              {
                  foreach (var item in request.ItemsToSave.TakeWhile(BusinessRequest<LogDto>.ValidateRequiredProperties))
                  {
                     var itemToSave = item.ToEntity();
                     Log itemSaved = null;

                     itemToSave.ModifiedBy = request.IdCurrentUser; 
                     if (itemToSave.Id == 0)  
                     {  
                        itemToSave.CreatedBy = request.IdCurrentUser;   

                        itemToSave.IsDeleted = false;
                        itemSaved = _repositoryLog.Add(itemToSave);
                     }
                     else //dans le cas d'une modification
                     {
                         itemSaved = _repositoryLog.Update(itemToSave, p => p.Id == itemToSave.Id);
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
      private static Expression<Func<Log, bool>> GenerateCriteria(LogDto itemToSearch) 
      {
          Expression<Func<Log, bool>> exprinit = obj => true;
          Expression<Func<Log, bool>> expi = exprinit;
                                                             
          if (itemToSearch.InfoSearchId.Consider)
          {
              switch (itemToSearch.InfoSearchId.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.Id != itemToSearch.Id);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.Id < itemToSearch.Id);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.Id <= itemToSearch.Id);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.Id > itemToSearch.Id);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.Id >= itemToSearch.Id);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.Id >= itemToSearch.InfoSearchId.Intervalle.Debut && obj.Id <= itemToSearch.InfoSearchId.Intervalle.Fin);                         
                      break;        
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.Id == itemToSearch.Id);                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchDate.Consider)
          {
              switch (itemToSearch.InfoSearchDate.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And
                              (obj => 
                                !(obj.Date.Year == itemToSearch.Date.Year
                                  && obj.Date.Month == itemToSearch.Date.Month
                                  && obj.Date.Day == itemToSearch.Date.Day)
                              );
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.Date.CompareTo(itemToSearch.Date) < 0);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.Date.CompareTo(itemToSearch.Date) <= 0);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.Date.CompareTo(itemToSearch.Date) > 0);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.Date.CompareTo(itemToSearch.Date)>= 0);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      var debut = itemToSearch.InfoSearchDate.Intervalle.Debut.Date;
                      var fin = itemToSearch.InfoSearchDate.Intervalle.Fin.AddDays(1).Date;
                      expi = expi.And(obj => obj.Date >= debut && obj.Date < fin);                  
                      break;         
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And
                            (
                              obj => obj.Date.Year == itemToSearch.Date.Year
                              && obj.Date.Month == itemToSearch.Date.Month
                              && obj.Date.Day == itemToSearch.Date.Day
                            );                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchThread.Consider)
          {
              switch (itemToSearch.InfoSearchThread.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.Thread != itemToSearch.Thread);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.Thread.StartsWith(itemToSearch.Thread));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.Thread.EndsWith(itemToSearch.Thread));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.Thread == itemToSearch.Thread);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.Thread.Contains(itemToSearch.Thread));
                      break;
              }
          }                               
                 
          if (itemToSearch.InfoSearchLevel.Consider)
          {
              switch (itemToSearch.InfoSearchLevel.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.Level != itemToSearch.Level);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.Level.StartsWith(itemToSearch.Level));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.Level.EndsWith(itemToSearch.Level));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.Level == itemToSearch.Level);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.Level.Contains(itemToSearch.Level));
                      break;
              }
          }                               
                 
          if (itemToSearch.InfoSearchLogger.Consider)
          {
              switch (itemToSearch.InfoSearchLogger.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.Logger != itemToSearch.Logger);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.Logger.StartsWith(itemToSearch.Logger));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.Logger.EndsWith(itemToSearch.Logger));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.Logger == itemToSearch.Logger);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.Logger.Contains(itemToSearch.Logger));
                      break;
              }
          }                               
                 
          if (itemToSearch.InfoSearchMessage.Consider)
          {
              switch (itemToSearch.InfoSearchMessage.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.Message != itemToSearch.Message);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.Message.StartsWith(itemToSearch.Message));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.Message.EndsWith(itemToSearch.Message));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.Message == itemToSearch.Message);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.Message.Contains(itemToSearch.Message));
                      break;
              }
          }                               
                 
          if (itemToSearch.InfoSearchException.Consider)
          {
              switch (itemToSearch.InfoSearchException.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.Exception != itemToSearch.Exception);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.Exception.StartsWith(itemToSearch.Exception));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.Exception.EndsWith(itemToSearch.Exception));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.Exception == itemToSearch.Exception);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.Exception.Contains(itemToSearch.Exception));
                      break;
              }
          }                               
                 
          if (itemToSearch.InfoSearchIdSession.Consider)
          {
              switch (itemToSearch.InfoSearchIdSession.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdSession != itemToSearch.IdSession);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.IdSession.StartsWith(itemToSearch.IdSession));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.IdSession.EndsWith(itemToSearch.IdSession));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.IdSession == itemToSearch.IdSession);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.IdSession.Contains(itemToSearch.IdSession));
                      break;
              }
          }                               
                 
          if (itemToSearch.InfoSearchIdEntite.Consider)
          {
              switch (itemToSearch.InfoSearchIdEntite.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdEntite != itemToSearch.IdEntite);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.IdEntite.StartsWith(itemToSearch.IdEntite));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.IdEntite.EndsWith(itemToSearch.IdEntite));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.IdEntite == itemToSearch.IdEntite);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.IdEntite.Contains(itemToSearch.IdEntite));
                      break;
              }
          }                               
                 
          if (itemToSearch.InfoSearchTypeEntite.Consider)
          {
              switch (itemToSearch.InfoSearchTypeEntite.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.TypeEntite != itemToSearch.TypeEntite);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.TypeEntite.StartsWith(itemToSearch.TypeEntite));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.TypeEntite.EndsWith(itemToSearch.TypeEntite));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.TypeEntite == itemToSearch.TypeEntite);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.TypeEntite.Contains(itemToSearch.TypeEntite));
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
                                !(obj.CreateOn.Year == itemToSearch.CreateOn.Year
                                  && obj.CreateOn.Month == itemToSearch.CreateOn.Month
                                  && obj.CreateOn.Day == itemToSearch.CreateOn.Day)
                              );
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.CreateOn.CompareTo(itemToSearch.CreateOn) < 0);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.CreateOn.CompareTo(itemToSearch.CreateOn) <= 0);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.CreateOn.CompareTo(itemToSearch.CreateOn) > 0);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.CreateOn.CompareTo(itemToSearch.CreateOn)>= 0);                  
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
                              obj => obj.CreateOn.Year == itemToSearch.CreateOn.Year
                              && obj.CreateOn.Month == itemToSearch.CreateOn.Month
                              && obj.CreateOn.Day == itemToSearch.CreateOn.Day
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
                                !(obj.ModifiedOn.Year == itemToSearch.ModifiedOn.Year
                                  && obj.ModifiedOn.Month == itemToSearch.ModifiedOn.Month
                                  && obj.ModifiedOn.Day == itemToSearch.ModifiedOn.Day)
                              );
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.ModifiedOn.CompareTo(itemToSearch.ModifiedOn) < 0);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.ModifiedOn.CompareTo(itemToSearch.ModifiedOn) <= 0);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.ModifiedOn.CompareTo(itemToSearch.ModifiedOn) > 0);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.ModifiedOn.CompareTo(itemToSearch.ModifiedOn)>= 0);                  
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
                              obj => obj.ModifiedOn.Year == itemToSearch.ModifiedOn.Year
                              && obj.ModifiedOn.Month == itemToSearch.ModifiedOn.Month
                              && obj.ModifiedOn.Day == itemToSearch.ModifiedOn.Day
                            );                  
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
      private static Expression<Func<Log, object>> GenerateOrderByExpression(LogDto itemToSearch)
      {
          Expression<Func<Log, object>> exp = null;

          if (itemToSearch.InfoSearchId.IsOrderByField)
            exp = (o => o.Id);
          if (itemToSearch.InfoSearchDate.IsOrderByField)
            exp = (o => o.Date);
          if (itemToSearch.InfoSearchThread.IsOrderByField)
            exp = (o => o.Thread);
          if (itemToSearch.InfoSearchLevel.IsOrderByField)
            exp = (o => o.Level);
          if (itemToSearch.InfoSearchLogger.IsOrderByField)
            exp = (o => o.Logger);
          if (itemToSearch.InfoSearchMessage.IsOrderByField)
            exp = (o => o.Message);
          if (itemToSearch.InfoSearchException.IsOrderByField)
            exp = (o => o.Exception);
          if (itemToSearch.InfoSearchIdSession.IsOrderByField)
            exp = (o => o.IdSession);
          if (itemToSearch.InfoSearchIdEntite.IsOrderByField)
            exp = (o => o.IdEntite);
          if (itemToSearch.InfoSearchTypeEntite.IsOrderByField)
            exp = (o => o.TypeEntite);
          if (itemToSearch.InfoSearchIsDeleted.IsOrderByField)
            exp = (o => o.IsDeleted);
          if (itemToSearch.InfoSearchCreateOn.IsOrderByField)
            exp = (o => o.CreateOn);
          if (itemToSearch.InfoSearchModifiedOn.IsOrderByField)
            exp = (o => o.ModifiedOn);
          if (itemToSearch.InfoSearchCreatedBy.IsOrderByField)
            exp = (o => o.CreatedBy);
          if (itemToSearch.InfoSearchModifiedBy.IsOrderByField)
            exp = (o => o.ModifiedBy);

          return exp ?? (obj => obj.Id);
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private static Expression<Func<Log, decimal?>> GenerateSumExpression(LogDto itemToSearch)
      {
          Expression<Func<Log, decimal?>> exp = null;

          if (itemToSearch.InfoSearchCreatedBy.IsSumField)
            exp = (o => o.CreatedBy.Value);                     
            if (itemToSearch.InfoSearchModifiedBy.IsSumField)
            exp = (o => o.ModifiedBy.Value);                     
  
          //return exp ?? (obj => obj.Id);
          return exp;
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private LogDto GenerateSum(Expression<Func<Log, bool>> criteria, LogDto itemToSearch, LogDto itemToReturn)
      {
          if (itemToSearch.InfoSearchCreatedBy.IsSumField)
            itemToReturn.InfoSearchCreatedBy.Sum = _repositoryLog.GetSum(criteria, o => o.CreatedBy.Value);     
               
            if (itemToSearch.InfoSearchModifiedBy.IsSumField)
            itemToReturn.InfoSearchModifiedBy.Sum = _repositoryLog.GetSum(criteria, o => o.ModifiedBy.Value);     
               
  
          return itemToReturn;
      }
  }
}

