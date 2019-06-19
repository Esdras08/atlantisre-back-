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
  public class Trace1Provider
  {

      #region Singleton
      
      private static Trace1Provider _instance;

      public static Trace1Provider Instance
      {
          get { return _instance ?? (_instance = new Trace1Provider()); }
      }

      #endregion

      private readonly IRepository<Trace1> _repositoryTrace1;
     
      private Trace1Provider()
      {
          _repositoryTrace1 = new Repository<Trace1>();
      }

      private Trace1Provider(IRepository<Trace1> repository)
      {    
          _repositoryTrace1 = repository;
      }

      public BusinessResponse<Trace1Dto> GetTrace1ById(object id)
      {
          var response = new BusinessResponse<Trace1Dto>();

          try
          {
              var item = _repositoryTrace1[id];
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
      public BusinessResponse<Trace1Dto> GetTrace1sByCriteria(BusinessRequest<Trace1Dto> request)
      {
          var response = new BusinessResponse<Trace1Dto>();

          try
          {
              request.ToFinalRequest();

              // générer expression lambda de l'objet de recherche principal
              var mainexp = GenerateCriteria(request.ItemToSearch);                  
			  // rechercher en fonction de la structure courante 
              if (request.CanFilterByStruct)
                mainexp = (mainexp == null) ? obj => obj.IdStructure == request.IdCurrentStructure : mainexp.And(obj => obj.IdStructure == request.IdCurrentStructure);                   
              mainexp = mainexp ?? (obj => true);                   

              // générer expression lambda des objets de recherche sécondaires
              if (request.ItemsToSearch == null) 
                request.ItemsToSearch = new List<Trace1Dto>();                                                    
 
              var exps = new List<Expression<Func<Trace1, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<Trace1, bool>> exp = null;                  
              foreach (var expi in exps)
              {
                  if(request.NotIn)
                      exp = (exp == null) ? expi : exp.And(expi);
                  else
                      exp = (exp == null) ? expi : exp.Or(expi); 
              }

                  // concaténer les critères principaux et sécondaires
                  exp = (exp == null) ? mainexp : mainexp.And(exp);                  
                  
                  Expression<Func<Trace1, object>> orderByExp = GenerateOrderByExpression(request.ItemToSearch);
                  if (string.IsNullOrEmpty(request.SortOrder) || (request.SortOrder != MySortOrder.Ascending && request.SortOrder != MySortOrder.Descending))
                    request.SortOrder = MySortOrder.Descending;

                  int totalRecord;
                  IEnumerable<Trace1> items = _repositoryTrace1.GetMany(exp, request.Index, request.Size, out totalRecord, orderByExp, request.SortOrder);
  
                  response.Count = totalRecord;
                  items = items as IList<Trace1> ?? items.ToList();
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
      public BusinessResponse<decimal> GetSumTrace1sByCriteria(BusinessRequest<Trace1Dto> request)
      {
          var response = new BusinessResponse<decimal>();

          try
          {
              request.ToFinalRequest();

              // générer expression lambda de l'objet de recherche principal
              var mainexp = GenerateCriteria(request.ItemToSearch);                  
			  // rechercher en fonction de la structure courante
              mainexp = (mainexp == null) ? obj => obj.IdStructure == request.IdCurrentStructure : mainexp.And(obj => obj.IdStructure == request.IdCurrentStructure);                   
              mainexp = mainexp ?? (obj => true);                   

              // générer expression lambda des objets de recherche sécondaires
              if (request.ItemsToSearch == null) 
                request.ItemsToSearch = new List<Trace1Dto>();                                                    
 
              var exps = new List<Expression<Func<Trace1, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<Trace1, bool>> exp = null;                  
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

                  var sum = _repositoryTrace1.GetSum(exp, sumExp);
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
  		public BusinessResponse<Trace1Dto> SaveTrace1s(BusinessRequest<Trace1Dto> request)
  		{
        var response = new BusinessResponse<Trace1Dto>();
  
              try
              {
                  foreach (var item in request.ItemsToSave.TakeWhile(BusinessRequest<Trace1Dto>.ValidateRequiredProperties))
                  {
                     var itemToSave = item.ToEntity();
                     Trace1 itemSaved = null;

                     itemToSave.IdStructure = request.IdCurrentStructure; 
                     if (itemToSave.IdTrace == 0)  
                     {  
                        itemSaved = _repositoryTrace1.Add(itemToSave);
                     }
                     else //dans le cas d'une modification
                     {
                         itemSaved = _repositoryTrace1.Update(itemToSave, p => p.IdTrace == itemToSave.IdTrace);
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
      private static Expression<Func<Trace1, bool>> GenerateCriteria(Trace1Dto itemToSearch) 
      {
          Expression<Func<Trace1, bool>> exprinit = obj => true;
          Expression<Func<Trace1, bool>> expi = exprinit;
                                                             
          if (itemToSearch.InfoSearchIdTrace.Consider)
          {
              switch (itemToSearch.InfoSearchIdTrace.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdTrace != itemToSearch.IdTrace);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdTrace < itemToSearch.IdTrace);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdTrace <= itemToSearch.IdTrace);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdTrace > itemToSearch.IdTrace);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdTrace >= itemToSearch.IdTrace);                  
                      break;  
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdTrace == itemToSearch.IdTrace);                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchIdStructure.Consider)
          {
              switch (itemToSearch.InfoSearchIdStructure.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdStructure != itemToSearch.IdStructure);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdStructure < itemToSearch.IdStructure);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdStructure <= itemToSearch.IdStructure);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdStructure > itemToSearch.IdStructure);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdStructure >= itemToSearch.IdStructure);                  
                      break;  
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdStructure == itemToSearch.IdStructure);                  
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

          if (itemToSearch.InfoSearchDateOperation.Consider)
          {
              switch (itemToSearch.InfoSearchDateOperation.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And
                            (obj => 
                                !(obj.DateOperation.Value.Year == itemToSearch.DateOperation.Value.Year
                                && obj.DateOperation.Value.Month == itemToSearch.DateOperation.Value.Month
                                && obj.DateOperation.Value.Day == itemToSearch.DateOperation.Value.Day)
                             );
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateOperation.Value, itemToSearch.DateOperation.Value) < 0);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateOperation.Value, itemToSearch.DateOperation.Value) <= 0);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateOperation.Value, itemToSearch.DateOperation.Value) > 0);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateOperation.Value, itemToSearch.DateOperation.Value) >= 0);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      var debut = itemToSearch.InfoSearchDateOperation.Intervalle.Debut.Date;
                      var fin = itemToSearch.InfoSearchDateOperation.Intervalle.Fin.AddDays(1).Date;
                      expi = expi.And(obj => obj.DateOperation >= debut && obj.DateOperation < fin);                  
                      break;         
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And
                            (
                              obj => obj.DateOperation.Value.Year == itemToSearch.DateOperation.Value.Year
                              && obj.DateOperation.Value.Month == itemToSearch.DateOperation.Value.Month
                              && obj.DateOperation.Value.Day == itemToSearch.DateOperation.Value.Day
                             );                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchDateSysteme.Consider)
          {
              switch (itemToSearch.InfoSearchDateSysteme.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And
                            (obj => 
                                !(obj.DateSysteme.Value.Year == itemToSearch.DateSysteme.Value.Year
                                && obj.DateSysteme.Value.Month == itemToSearch.DateSysteme.Value.Month
                                && obj.DateSysteme.Value.Day == itemToSearch.DateSysteme.Value.Day)
                             );
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateSysteme.Value, itemToSearch.DateSysteme.Value) < 0);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateSysteme.Value, itemToSearch.DateSysteme.Value) <= 0);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateSysteme.Value, itemToSearch.DateSysteme.Value) > 0);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateSysteme.Value, itemToSearch.DateSysteme.Value) >= 0);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      var debut = itemToSearch.InfoSearchDateSysteme.Intervalle.Debut.Date;
                      var fin = itemToSearch.InfoSearchDateSysteme.Intervalle.Fin.AddDays(1).Date;
                      expi = expi.And(obj => obj.DateSysteme >= debut && obj.DateSysteme < fin);                  
                      break;         
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And
                            (
                              obj => obj.DateSysteme.Value.Year == itemToSearch.DateSysteme.Value.Year
                              && obj.DateSysteme.Value.Month == itemToSearch.DateSysteme.Value.Month
                              && obj.DateSysteme.Value.Day == itemToSearch.DateSysteme.Value.Day
                             );                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchIdTypeOperation.Consider)
          {
              switch (itemToSearch.InfoSearchIdTypeOperation.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdTypeOperation != itemToSearch.IdTypeOperation);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdTypeOperation < itemToSearch.IdTypeOperation);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdTypeOperation <= itemToSearch.IdTypeOperation);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdTypeOperation > itemToSearch.IdTypeOperation);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdTypeOperation >= itemToSearch.IdTypeOperation);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.IdTypeOperation >= itemToSearch.InfoSearchIdTypeOperation.Intervalle.Debut && obj.IdTypeOperation <= itemToSearch.InfoSearchIdTypeOperation.Intervalle.Fin);                         
                      break;    
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdTypeOperation == itemToSearch.IdTypeOperation);                  
                      break;
              }
          }            

          if (itemToSearch.InfoSearchCodeOperation.Consider)
          {
              switch (itemToSearch.InfoSearchCodeOperation.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.CodeOperation != itemToSearch.CodeOperation);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.CodeOperation.StartsWith(itemToSearch.CodeOperation));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.CodeOperation.EndsWith(itemToSearch.CodeOperation));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.CodeOperation == itemToSearch.CodeOperation);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.CodeOperation.Contains(itemToSearch.CodeOperation));
                      break;
              }
          }                               
                 
          if (itemToSearch.InfoSearchOrigine.Consider)
          {
              switch (itemToSearch.InfoSearchOrigine.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.Origine != itemToSearch.Origine);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.Origine.StartsWith(itemToSearch.Origine));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.Origine.EndsWith(itemToSearch.Origine));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.Origine == itemToSearch.Origine);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.Origine.Contains(itemToSearch.Origine));
                      break;
              }
          }                               
                 
          if (itemToSearch.InfoSearchIdOrigine.Consider)
          {
              switch (itemToSearch.InfoSearchIdOrigine.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdOrigine != itemToSearch.IdOrigine);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdOrigine < itemToSearch.IdOrigine);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdOrigine <= itemToSearch.IdOrigine);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdOrigine > itemToSearch.IdOrigine);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdOrigine >= itemToSearch.IdOrigine);                  
                      break;  
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdOrigine == itemToSearch.IdOrigine);                  
                      break;
              }
          }            

          if (itemToSearch.InfoSearchIdFormulaire.Consider)
          {
              switch (itemToSearch.InfoSearchIdFormulaire.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdFormulaire != itemToSearch.IdFormulaire);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdFormulaire < itemToSearch.IdFormulaire);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdFormulaire <= itemToSearch.IdFormulaire);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdFormulaire > itemToSearch.IdFormulaire);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdFormulaire >= itemToSearch.IdFormulaire);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.IdFormulaire >= itemToSearch.InfoSearchIdFormulaire.Intervalle.Debut && obj.IdFormulaire <= itemToSearch.InfoSearchIdFormulaire.Intervalle.Fin);                         
                      break;    
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdFormulaire == itemToSearch.IdFormulaire);                  
                      break;
              }
          }            

          if (itemToSearch.InfoSearchCodeFormulaire.Consider)
          {
              switch (itemToSearch.InfoSearchCodeFormulaire.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.CodeFormulaire != itemToSearch.CodeFormulaire);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.CodeFormulaire.StartsWith(itemToSearch.CodeFormulaire));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.CodeFormulaire.EndsWith(itemToSearch.CodeFormulaire));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.CodeFormulaire == itemToSearch.CodeFormulaire);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.CodeFormulaire.Contains(itemToSearch.CodeFormulaire));
                      break;
              }
          }                               
                 
          if (itemToSearch.InfoSearchLibelleOperation.Consider)
          {
              switch (itemToSearch.InfoSearchLibelleOperation.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.LibelleOperation != itemToSearch.LibelleOperation);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.LibelleOperation.StartsWith(itemToSearch.LibelleOperation));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.LibelleOperation.EndsWith(itemToSearch.LibelleOperation));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.LibelleOperation == itemToSearch.LibelleOperation);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.LibelleOperation.Contains(itemToSearch.LibelleOperation));
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
      private static Expression<Func<Trace1, object>> GenerateOrderByExpression(Trace1Dto itemToSearch)
      {
          Expression<Func<Trace1, object>> exp = null;

          if (itemToSearch.InfoSearchIdTrace.IsOrderByField)
            exp = (o => o.IdTrace);
          if (itemToSearch.InfoSearchIdStructure.IsOrderByField)
            exp = (o => o.IdStructure);
          if (itemToSearch.InfoSearchIdUtilisateur.IsOrderByField)
            exp = (o => o.IdUtilisateur);
          if (itemToSearch.InfoSearchDateOperation.IsOrderByField)
            exp = (o => o.DateOperation);
          if (itemToSearch.InfoSearchHeureOperation.IsOrderByField)
            exp = (o => o.HeureOperation);
          if (itemToSearch.InfoSearchDateSysteme.IsOrderByField)
            exp = (o => o.DateSysteme);
          if (itemToSearch.InfoSearchHeureSysteme.IsOrderByField)
            exp = (o => o.HeureSysteme);
          if (itemToSearch.InfoSearchIdTypeOperation.IsOrderByField)
            exp = (o => o.IdTypeOperation);
          if (itemToSearch.InfoSearchCodeOperation.IsOrderByField)
            exp = (o => o.CodeOperation);
          if (itemToSearch.InfoSearchOrigine.IsOrderByField)
            exp = (o => o.Origine);
          if (itemToSearch.InfoSearchIdOrigine.IsOrderByField)
            exp = (o => o.IdOrigine);
          if (itemToSearch.InfoSearchIdFormulaire.IsOrderByField)
            exp = (o => o.IdFormulaire);
          if (itemToSearch.InfoSearchCodeFormulaire.IsOrderByField)
            exp = (o => o.CodeFormulaire);
          if (itemToSearch.InfoSearchLibelleOperation.IsOrderByField)
            exp = (o => o.LibelleOperation);

          return exp ?? (obj => obj.IdTrace);
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private static Expression<Func<Trace1, decimal?>> GenerateSumExpression(Trace1Dto itemToSearch)
      {
          Expression<Func<Trace1, decimal?>> exp = null;


          //return exp ?? (obj => obj.IdTrace);
          return exp;
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private Trace1Dto GenerateSum(Expression<Func<Trace1, bool>> criteria, Trace1Dto itemToSearch, Trace1Dto itemToReturn)
      {

          return itemToReturn;
      }
  }
}

