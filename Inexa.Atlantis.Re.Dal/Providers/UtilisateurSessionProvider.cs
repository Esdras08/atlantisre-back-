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
  public class UtilisateurSessionProvider
  {

      #region Singleton
      
      private static UtilisateurSessionProvider _instance;

      public static UtilisateurSessionProvider Instance
      {
          get { return _instance ?? (_instance = new UtilisateurSessionProvider()); }
      }

      #endregion

      private readonly IRepository<UtilisateurSession> _repositoryUtilisateurSession;
     
      public  UtilisateurSessionProvider()
      {
          _repositoryUtilisateurSession = new Repository<UtilisateurSession>();
      }

      private UtilisateurSessionProvider(IRepository<UtilisateurSession> repository)
      {    
          _repositoryUtilisateurSession = repository;
      }

      public async Task<BusinessResponse<UtilisateurSessionDto>> GetUtilisateurSessionById(object id)
      {
          var response = new BusinessResponse<UtilisateurSessionDto>();

          try
          {
              var item = _repositoryUtilisateurSession[id];
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
      public async Task<BusinessResponse<UtilisateurSessionDto>> GetUtilisateurSessionsByCriteria(BusinessRequest<UtilisateurSessionDto> request)
      {
          var response = new BusinessResponse<UtilisateurSessionDto>();

          try
          {
              request.ToFinalRequest();

              // générer expression lambda de l'objet de recherche principal
              var mainexp = GenerateCriteria(request.ItemToSearch);                  
              mainexp = (mainexp == null) ? obj => obj.IsDeleted == false : mainexp.And(obj => obj.IsDeleted == false);                   

              // générer expression lambda des objets de recherche sécondaires
              if (request.ItemsToSearch == null) 
                request.ItemsToSearch = new List<UtilisateurSessionDto>();                                                    
 
              var exps = new List<Expression<Func<UtilisateurSession, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<UtilisateurSession, bool>> exp = null;                  
              foreach (var expi in exps)
              {
                  if(request.NotIn)
                      exp = (exp == null) ? expi : exp.And(expi);
                  else
                      exp = (exp == null) ? expi : exp.Or(expi); 
              }

                  // concaténer les critères principaux et sécondaires
                  exp = (exp == null) ? mainexp : mainexp.And(exp);                  
                  
                  Expression<Func<UtilisateurSession, object>> orderByExp = GenerateOrderByExpression(request.ItemToSearch);
                  if (string.IsNullOrEmpty(request.SortOrder) || (request.SortOrder != MySortOrder.Ascending && request.SortOrder != MySortOrder.Descending))
                    request.SortOrder = MySortOrder.Descending;

                  int totalRecord;
                  IEnumerable<UtilisateurSession> items = _repositoryUtilisateurSession.GetMany(exp, request.Index, request.Size, out totalRecord, orderByExp, request.SortOrder);
  
                  response.Count = totalRecord;
                  items = items as IList<UtilisateurSession> ?? items.ToList();
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
      public BusinessResponse<decimal> GetSumUtilisateurSessionsByCriteria(BusinessRequest<UtilisateurSessionDto> request)
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
                request.ItemsToSearch = new List<UtilisateurSessionDto>();                                                    
 
              var exps = new List<Expression<Func<UtilisateurSession, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<UtilisateurSession, bool>> exp = null;                  
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

                  var sum = _repositoryUtilisateurSession.GetSum(exp, sumExp);
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
  		public async Task<BusinessResponse<UtilisateurSessionDto>> SaveUtilisateurSessions(BusinessRequest<UtilisateurSessionDto> request)
  		{
        var response = new BusinessResponse<UtilisateurSessionDto>();
  
              try
              {
                  foreach (var item in request.ItemsToSave.TakeWhile(BusinessRequest<UtilisateurSessionDto>.ValidateRequiredProperties))
                  {
                     var itemToSave = item.ToEntity();
                     UtilisateurSession itemSaved = null;

                     itemToSave.DateMaj = Utilities.CurrentDate; 
                     if (itemToSave.IdUtilisateurSession == 0)  
                     {  
                        itemToSave.DateCreation = Utilities.CurrentDate;   

                        itemToSave.IsDeleted = false;
                        itemSaved = _repositoryUtilisateurSession.Add(itemToSave);
                     }
                     else //dans le cas d'une modification
                     {
                         itemSaved = _repositoryUtilisateurSession.Update(itemToSave, p => p.IdUtilisateurSession == itemToSave.IdUtilisateurSession);
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
      private static Expression<Func<UtilisateurSession, bool>> GenerateCriteria(UtilisateurSessionDto itemToSearch) 
      {
          Expression<Func<UtilisateurSession, bool>> exprinit = obj => true;
          Expression<Func<UtilisateurSession, bool>> expi = exprinit;
                                                             
          if (itemToSearch.InfoSearchIdUtilisateurSession.Consider)
          {
              switch (itemToSearch.InfoSearchIdUtilisateurSession.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdUtilisateurSession != itemToSearch.IdUtilisateurSession);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdUtilisateurSession < itemToSearch.IdUtilisateurSession);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdUtilisateurSession <= itemToSearch.IdUtilisateurSession);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdUtilisateurSession > itemToSearch.IdUtilisateurSession);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdUtilisateurSession >= itemToSearch.IdUtilisateurSession);                  
                      break;  
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdUtilisateurSession == itemToSearch.IdUtilisateurSession);                  
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
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdUtilisateur == itemToSearch.IdUtilisateur);                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchTimeSession.Consider)
          {
              switch (itemToSearch.InfoSearchTimeSession.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.TimeSession != itemToSearch.TimeSession);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.TimeSession < itemToSearch.TimeSession);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.TimeSession <= itemToSearch.TimeSession);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.TimeSession > itemToSearch.TimeSession);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.TimeSession >= itemToSearch.TimeSession);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.TimeSession >= itemToSearch.InfoSearchTimeSession.Intervalle.Debut && obj.TimeSession <= itemToSearch.InfoSearchTimeSession.Intervalle.Fin);                         
                      break;    
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.TimeSession == itemToSearch.TimeSession);                  
                      break;
              }
          }            

          if (itemToSearch.InfoSearchIsConnected.Consider)
          {
              switch (itemToSearch.InfoSearchIsConnected.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IsConnected != itemToSearch.IsConnected);
                      break;                      
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IsConnected == itemToSearch.IsConnected);                  
                      break;
              }
          }            

                  if (itemToSearch.InfoSearchDateLastConnection.Consider)
          {
              switch (itemToSearch.InfoSearchDateLastConnection.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And
                            (obj => 
                                !(obj.DateLastConnection.Value.Year == itemToSearch.DateLastConnection.Value.Year
                                && obj.DateLastConnection.Value.Month == itemToSearch.DateLastConnection.Value.Month
                                && obj.DateLastConnection.Value.Day == itemToSearch.DateLastConnection.Value.Day)
                             );
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateLastConnection.Value, itemToSearch.DateLastConnection.Value) < 0);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateLastConnection.Value, itemToSearch.DateLastConnection.Value) <= 0);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateLastConnection.Value, itemToSearch.DateLastConnection.Value) > 0);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateLastConnection.Value, itemToSearch.DateLastConnection.Value) >= 0);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      var debut = itemToSearch.InfoSearchDateLastConnection.Intervalle.Debut.Date;
                      var fin = itemToSearch.InfoSearchDateLastConnection.Intervalle.Fin.AddDays(1).Date;
                      expi = expi.And(obj => obj.DateLastConnection >= debut && obj.DateLastConnection < fin);                  
                      break;         
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And
                            (
                              obj => obj.DateLastConnection.Value.Year == itemToSearch.DateLastConnection.Value.Year
                              && obj.DateLastConnection.Value.Month == itemToSearch.DateLastConnection.Value.Month
                              && obj.DateLastConnection.Value.Day == itemToSearch.DateLastConnection.Value.Day
                             );                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchDateLastActivity.Consider)
          {
              switch (itemToSearch.InfoSearchDateLastActivity.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And
                            (obj => 
                                !(obj.DateLastActivity.Value.Year == itemToSearch.DateLastActivity.Value.Year
                                && obj.DateLastActivity.Value.Month == itemToSearch.DateLastActivity.Value.Month
                                && obj.DateLastActivity.Value.Day == itemToSearch.DateLastActivity.Value.Day)
                             );
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateLastActivity.Value, itemToSearch.DateLastActivity.Value) < 0);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateLastActivity.Value, itemToSearch.DateLastActivity.Value) <= 0);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateLastActivity.Value, itemToSearch.DateLastActivity.Value) > 0);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateLastActivity.Value, itemToSearch.DateLastActivity.Value) >= 0);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      var debut = itemToSearch.InfoSearchDateLastActivity.Intervalle.Debut.Date;
                      var fin = itemToSearch.InfoSearchDateLastActivity.Intervalle.Fin.AddDays(1).Date;
                      expi = expi.And(obj => obj.DateLastActivity >= debut && obj.DateLastActivity < fin);                  
                      break;         
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And
                            (
                              obj => obj.DateLastActivity.Value.Year == itemToSearch.DateLastActivity.Value.Year
                              && obj.DateLastActivity.Value.Month == itemToSearch.DateLastActivity.Value.Month
                              && obj.DateLastActivity.Value.Day == itemToSearch.DateLastActivity.Value.Day
                             );                  
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
      private static Expression<Func<UtilisateurSession, object>> GenerateOrderByExpression(UtilisateurSessionDto itemToSearch)
      {
          Expression<Func<UtilisateurSession, object>> exp = null;

          if (itemToSearch.InfoSearchIdUtilisateurSession.IsOrderByField)
            exp = (o => o.IdUtilisateurSession);
          if (itemToSearch.InfoSearchIdUtilisateur.IsOrderByField)
            exp = (o => o.IdUtilisateur);
          if (itemToSearch.InfoSearchTimeSession.IsOrderByField)
            exp = (o => o.TimeSession);
          if (itemToSearch.InfoSearchIsConnected.IsOrderByField)
            exp = (o => o.IsConnected);
          if (itemToSearch.InfoSearchDateLastConnection.IsOrderByField)
            exp = (o => o.DateLastConnection);
          if (itemToSearch.InfoSearchDateLastActivity.IsOrderByField)
            exp = (o => o.DateLastActivity);
          if (itemToSearch.InfoSearchDateCreation.IsOrderByField)
            exp = (o => o.DateCreation);
          if (itemToSearch.InfoSearchDateMaj.IsOrderByField)
            exp = (o => o.DateMaj);
          if (itemToSearch.InfoSearchIsDeleted.IsOrderByField)
            exp = (o => o.IsDeleted);
          if (itemToSearch.InfoSearchDataKey.IsOrderByField)
            exp = (o => o.DataKey);

          return exp ?? (obj => obj.IdUtilisateurSession);
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private static Expression<Func<UtilisateurSession, decimal?>> GenerateSumExpression(UtilisateurSessionDto itemToSearch)
      {
          Expression<Func<UtilisateurSession, decimal?>> exp = null;

          if (itemToSearch.InfoSearchTimeSession.IsSumField)
            exp = (o => o.TimeSession.Value);                     
  
          //return exp ?? (obj => obj.IdUtilisateurSession);
          return exp;
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private UtilisateurSessionDto GenerateSum(Expression<Func<UtilisateurSession, bool>> criteria, UtilisateurSessionDto itemToSearch, UtilisateurSessionDto itemToReturn)
      {
          if (itemToSearch.InfoSearchTimeSession.IsSumField)
            itemToReturn.InfoSearchTimeSession.Sum = _repositoryUtilisateurSession.GetSum(criteria, o => o.TimeSession.Value);     
               
  
          return itemToReturn;
      }
  }
}

