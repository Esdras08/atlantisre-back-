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
  public class Fonctionnalite1Provider
  {

      #region Singleton
      
      private static Fonctionnalite1Provider _instance;

      public static Fonctionnalite1Provider Instance
      {
          get { return _instance ?? (_instance = new Fonctionnalite1Provider()); }
      }

      #endregion

      private readonly IRepository<Fonctionnalite1> _repositoryFonctionnalite1;
     
      private Fonctionnalite1Provider()
      {
          _repositoryFonctionnalite1 = new Repository<Fonctionnalite1>();
      }

      private Fonctionnalite1Provider(IRepository<Fonctionnalite1> repository)
      {    
          _repositoryFonctionnalite1 = repository;
      }

      public BusinessResponse<Fonctionnalite1Dto> GetFonctionnalite1ById(object id)
      {
          var response = new BusinessResponse<Fonctionnalite1Dto>();

          try
          {
              var item = _repositoryFonctionnalite1[id];
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
      public BusinessResponse<Fonctionnalite1Dto> GetFonctionnalite1sByCriteria(BusinessRequest<Fonctionnalite1Dto> request)
      {
          var response = new BusinessResponse<Fonctionnalite1Dto>();

          try
          {
              request.ToFinalRequest();

              // générer expression lambda de l'objet de recherche principal
              var mainexp = GenerateCriteria(request.ItemToSearch);                  
              mainexp = mainexp ?? (obj => true);                   

              // générer expression lambda des objets de recherche sécondaires
              if (request.ItemsToSearch == null) 
                request.ItemsToSearch = new List<Fonctionnalite1Dto>();                                                    
 
              var exps = new List<Expression<Func<Fonctionnalite1, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<Fonctionnalite1, bool>> exp = null;                  
              foreach (var expi in exps)
              {
                  if(request.NotIn)
                      exp = (exp == null) ? expi : exp.And(expi);
                  else
                      exp = (exp == null) ? expi : exp.Or(expi); 
              }

                  // concaténer les critères principaux et sécondaires
                  exp = (exp == null) ? mainexp : mainexp.And(exp);                  
                  
                  Expression<Func<Fonctionnalite1, object>> orderByExp = GenerateOrderByExpression(request.ItemToSearch);
                  if (string.IsNullOrEmpty(request.SortOrder) || (request.SortOrder != MySortOrder.Ascending && request.SortOrder != MySortOrder.Descending))
                    request.SortOrder = MySortOrder.Descending;

                  int totalRecord;
                  IEnumerable<Fonctionnalite1> items = _repositoryFonctionnalite1.GetMany(exp, request.Index, request.Size, out totalRecord, orderByExp, request.SortOrder);
  
                  response.Count = totalRecord;
                  items = items as IList<Fonctionnalite1> ?? items.ToList();
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
      public BusinessResponse<decimal> GetSumFonctionnalite1sByCriteria(BusinessRequest<Fonctionnalite1Dto> request)
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
                request.ItemsToSearch = new List<Fonctionnalite1Dto>();                                                    
 
              var exps = new List<Expression<Func<Fonctionnalite1, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<Fonctionnalite1, bool>> exp = null;                  
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

                  var sum = _repositoryFonctionnalite1.GetSum(exp, sumExp);
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
  		public BusinessResponse<Fonctionnalite1Dto> SaveFonctionnalite1s(BusinessRequest<Fonctionnalite1Dto> request)
  		{
        var response = new BusinessResponse<Fonctionnalite1Dto>();
  
              try
              {
                  foreach (var item in request.ItemsToSave.TakeWhile(BusinessRequest<Fonctionnalite1Dto>.ValidateRequiredProperties))
                  {
                     var itemToSave = item.ToEntity();
                     Fonctionnalite1 itemSaved = null;

                     if (string.IsNullOrEmpty(itemToSave.Code))
                     {  
                        itemSaved = _repositoryFonctionnalite1.Add(itemToSave);
                     }
                     else //dans le cas d'une modification
                     {
                         itemSaved = _repositoryFonctionnalite1.Update(itemToSave, p => p.Code == itemToSave.Code);
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
      private static Expression<Func<Fonctionnalite1, bool>> GenerateCriteria(Fonctionnalite1Dto itemToSearch) 
      {
          Expression<Func<Fonctionnalite1, bool>> exprinit = obj => true;
          Expression<Func<Fonctionnalite1, bool>> expi = exprinit;
                                                             
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
                 
          return expi == exprinit ? null : expi;
      }

      /// <summary>
        /// Générer une expression lambda à partir d'un DTO
        /// </summary>
        /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
        /// <returns>Expression lambda</returns>
      private static Expression<Func<Fonctionnalite1, object>> GenerateOrderByExpression(Fonctionnalite1Dto itemToSearch)
      {
          Expression<Func<Fonctionnalite1, object>> exp = null;

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

          return exp ?? (obj => obj.Code);
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private static Expression<Func<Fonctionnalite1, decimal?>> GenerateSumExpression(Fonctionnalite1Dto itemToSearch)
      {
          Expression<Func<Fonctionnalite1, decimal?>> exp = null;

          if (itemToSearch.InfoSearchOrdreSousModule.IsSumField)
            exp = (o => o.OrdreSousModule.Value);                     
            if (itemToSearch.InfoSearchOrdre.IsSumField)
            exp = (o => o.Ordre.Value);                     
  
          //return exp ?? (obj => obj.Code);
          return exp;
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private Fonctionnalite1Dto GenerateSum(Expression<Func<Fonctionnalite1, bool>> criteria, Fonctionnalite1Dto itemToSearch, Fonctionnalite1Dto itemToReturn)
      {
          if (itemToSearch.InfoSearchOrdreSousModule.IsSumField)
            itemToReturn.InfoSearchOrdreSousModule.Sum = _repositoryFonctionnalite1.GetSum(criteria, o => o.OrdreSousModule.Value);     
               
            if (itemToSearch.InfoSearchOrdre.IsSumField)
            itemToReturn.InfoSearchOrdre.Sum = _repositoryFonctionnalite1.GetSum(criteria, o => o.Ordre.Value);     
               
  
          return itemToReturn;
      }
  }
}

