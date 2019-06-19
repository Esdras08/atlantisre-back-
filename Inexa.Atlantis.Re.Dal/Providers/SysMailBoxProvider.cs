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
  public class SysMailBoxProvider
  {

      #region Singleton
      
      private static SysMailBoxProvider _instance;

      public static SysMailBoxProvider Instance
      {
          get { return _instance ?? (_instance = new SysMailBoxProvider()); }
      }

      #endregion

      private readonly IRepository<SysMailBox> _repositorySysMailBox;
     
      public  SysMailBoxProvider()
      {
          _repositorySysMailBox = new Repository<SysMailBox>();
      }

      private SysMailBoxProvider(IRepository<SysMailBox> repository)
      {    
          _repositorySysMailBox = repository;
      }

      public async Task<BusinessResponse<SysMailBoxDto>> GetSysMailBoxById(object id)
      {
          var response = new BusinessResponse<SysMailBoxDto>();

          try
          {
              var item = _repositorySysMailBox[id];
              if (item != null)                  
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
      public async Task<BusinessResponse<SysMailBoxDto>> GetSysMailBoxsByCriteria(BusinessRequest<SysMailBoxDto> request)
      {
          var response = new BusinessResponse<SysMailBoxDto>();

          try
          {
              request.ToFinalRequest();

              // générer expression lambda de l'objet de recherche principal
              var mainexp = GenerateCriteria(request.ItemToSearch);                  
              mainexp = mainexp ?? (obj => true);                   

              // générer expression lambda des objets de recherche sécondaires
              if (request.ItemsToSearch == null) 
                request.ItemsToSearch = new List<SysMailBoxDto>();                                                    
 
              var exps = new List<Expression<Func<SysMailBox, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<SysMailBox, bool>> exp = null;                  
              foreach (var expi in exps)
              {
                  if(request.NotIn)
                      exp = (exp == null) ? expi : exp.And(expi);
                  else
                      exp = (exp == null) ? expi : exp.Or(expi); 
              }

                  // concaténer les critères principaux et sécondaires
                  exp = (exp == null) ? mainexp : mainexp.And(exp);                  
                  
                  Expression<Func<SysMailBox, object>> orderByExp = GenerateOrderByExpression(request.ItemToSearch);
                  if (string.IsNullOrEmpty(request.SortOrder) || (request.SortOrder != MySortOrder.Ascending && request.SortOrder != MySortOrder.Descending))
                    request.SortOrder = MySortOrder.Descending;

                  int totalRecord;
                  IEnumerable<SysMailBox> items = _repositorySysMailBox.GetMany(exp, request.Index, request.Size, out totalRecord, orderByExp, request.SortOrder);
  
                  response.Count = totalRecord;
                  items = items as IList<SysMailBox> ?? items.ToList();
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
      public BusinessResponse<decimal> GetSumSysMailBoxsByCriteria(BusinessRequest<SysMailBoxDto> request)
      {
          var response = new BusinessResponse<decimal>();

          try
          {
              request.ToFinalRequest();

              // générer expression lambda de l'objet de recherche principal
              var mainexp = GenerateCriteria(request.ItemToSearch);                  
              mainexp = mainexp ?? (obj => true);                   

              // générer expression lambda des objets de recherche sécondaires
              if (request.ItemsToSearch == null) 
                request.ItemsToSearch = new List<SysMailBoxDto>();                                                    
 
              var exps = new List<Expression<Func<SysMailBox, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<SysMailBox, bool>> exp = null;                  
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

                  var sum = _repositorySysMailBox.GetSum(exp, sumExp);
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
  		public async Task<BusinessResponse<SysMailBoxDto>> SaveSysMailBoxs(BusinessRequest<SysMailBoxDto> request)
  		{
        var response = new BusinessResponse<SysMailBoxDto>();
  
              try
              {
                  foreach (var item in request.ItemsToSave.TakeWhile(BusinessRequest<SysMailBoxDto>.ValidateRequiredProperties))
                  {
                     var itemToSave = item.ToEntity();
                     SysMailBox itemSaved = null;

                     itemToSave.DateMAJ = Utilities.CurrentDate; 
                     if (itemToSave.IdSysMailBox == 0)  
                     {  
                        itemToSave.DateCreation = Utilities.CurrentDate;   
                        itemSaved = _repositorySysMailBox.Add(itemToSave);
                     }
                     else //dans le cas d'une modification
                     {
                         itemSaved = _repositorySysMailBox.Update(itemToSave, p => p.IdSysMailBox == itemToSave.IdSysMailBox);
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
      private static Expression<Func<SysMailBox, bool>> GenerateCriteria(SysMailBoxDto itemToSearch) 
      {
          Expression<Func<SysMailBox, bool>> exprinit = obj => true;
          Expression<Func<SysMailBox, bool>> expi = exprinit;
                                                             
          if (itemToSearch.InfoSearchIdSysMailBox.Consider)
          {
              switch (itemToSearch.InfoSearchIdSysMailBox.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdSysMailBox != itemToSearch.IdSysMailBox);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdSysMailBox < itemToSearch.IdSysMailBox);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdSysMailBox <= itemToSearch.IdSysMailBox);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdSysMailBox > itemToSearch.IdSysMailBox);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdSysMailBox >= itemToSearch.IdSysMailBox);                  
                      break;  
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdSysMailBox == itemToSearch.IdSysMailBox);                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchEntite.Consider)
          {
              switch (itemToSearch.InfoSearchEntite.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.Entite != itemToSearch.Entite);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.Entite.StartsWith(itemToSearch.Entite));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.Entite.EndsWith(itemToSearch.Entite));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.Entite == itemToSearch.Entite);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.Entite.Contains(itemToSearch.Entite));
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
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdEntite < itemToSearch.IdEntite);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdEntite <= itemToSearch.IdEntite);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdEntite > itemToSearch.IdEntite);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdEntite >= itemToSearch.IdEntite);                  
                      break;  
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdEntite == itemToSearch.IdEntite);                  
                      break;
              }
          }            

          if (itemToSearch.InfoSearchIdMailitem.Consider)
          {
              switch (itemToSearch.InfoSearchIdMailitem.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdMailitem != itemToSearch.IdMailitem);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdMailitem < itemToSearch.IdMailitem);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdMailitem <= itemToSearch.IdMailitem);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdMailitem > itemToSearch.IdMailitem);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdMailitem >= itemToSearch.IdMailitem);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.IdMailitem >= itemToSearch.InfoSearchIdMailitem.Intervalle.Debut && obj.IdMailitem <= itemToSearch.InfoSearchIdMailitem.Intervalle.Fin);                         
                      break;    
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdMailitem == itemToSearch.IdMailitem);                  
                      break;
              }
          }            

          if (itemToSearch.InfoSearchStatut.Consider)
          {
              switch (itemToSearch.InfoSearchStatut.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.Statut != itemToSearch.Statut);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.Statut.StartsWith(itemToSearch.Statut));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.Statut.EndsWith(itemToSearch.Statut));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.Statut == itemToSearch.Statut);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.Statut.Contains(itemToSearch.Statut));
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

          if (itemToSearch.InfoSearchMailMessage.Consider)
          {
              switch (itemToSearch.InfoSearchMailMessage.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.MailMessage != itemToSearch.MailMessage);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.MailMessage.StartsWith(itemToSearch.MailMessage));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.MailMessage.EndsWith(itemToSearch.MailMessage));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.MailMessage == itemToSearch.MailMessage);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.MailMessage.Contains(itemToSearch.MailMessage));
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
      private static Expression<Func<SysMailBox, object>> GenerateOrderByExpression(SysMailBoxDto itemToSearch)
      {
          Expression<Func<SysMailBox, object>> exp = null;

          if (itemToSearch.InfoSearchIdSysMailBox.IsOrderByField)
            exp = (o => o.IdSysMailBox);
          if (itemToSearch.InfoSearchEntite.IsOrderByField)
            exp = (o => o.Entite);
          if (itemToSearch.InfoSearchIdEntite.IsOrderByField)
            exp = (o => o.IdEntite);
          if (itemToSearch.InfoSearchIdMailitem.IsOrderByField)
            exp = (o => o.IdMailitem);
          if (itemToSearch.InfoSearchStatut.IsOrderByField)
            exp = (o => o.Statut);
          if (itemToSearch.InfoSearchDateCreation.IsOrderByField)
            exp = (o => o.DateCreation);
          if (itemToSearch.InfoSearchMailMessage.IsOrderByField)
            exp = (o => o.MailMessage);
          if (itemToSearch.InfoSearchDateMAJ.IsOrderByField)
            exp = (o => o.DateMAJ);
          if (itemToSearch.InfoSearchDataKey.IsOrderByField)
            exp = (o => o.DataKey);

          return exp ?? (obj => obj.IdSysMailBox);
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private static Expression<Func<SysMailBox, decimal?>> GenerateSumExpression(SysMailBoxDto itemToSearch)
      {
          Expression<Func<SysMailBox, decimal?>> exp = null;


          //return exp ?? (obj => obj.IdSysMailBox);
          return exp;
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private SysMailBoxDto GenerateSum(Expression<Func<SysMailBox, bool>> criteria, SysMailBoxDto itemToSearch, SysMailBoxDto itemToReturn)
      {

          return itemToReturn;
      }
  }
}

