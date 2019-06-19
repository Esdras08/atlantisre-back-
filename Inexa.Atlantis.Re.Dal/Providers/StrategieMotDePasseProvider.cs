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
  public class StrategieMotDePasseProvider
  {

      #region Singleton
      
      private static StrategieMotDePasseProvider _instance;

      public static StrategieMotDePasseProvider Instance
      {
          get { return _instance ?? (_instance = new StrategieMotDePasseProvider()); }
      }

      #endregion

      private readonly IRepository<StrategieMotDePasse> _repositoryStrategieMotDePasse;
     
      private StrategieMotDePasseProvider()
      {
          _repositoryStrategieMotDePasse = new Repository<StrategieMotDePasse>();
      }

      private StrategieMotDePasseProvider(IRepository<StrategieMotDePasse> repository)
      {    
          _repositoryStrategieMotDePasse = repository;
      }

      public BusinessResponse<StrategieMotDePasseDto> GetStrategieMotDePasseById(object id)
      {
          var response = new BusinessResponse<StrategieMotDePasseDto>();

          try
          {
              var item = _repositoryStrategieMotDePasse[id];
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
      public BusinessResponse<StrategieMotDePasseDto> GetStrategieMotDePassesByCriteria(BusinessRequest<StrategieMotDePasseDto> request)
      {
          var response = new BusinessResponse<StrategieMotDePasseDto>();

          try
          {
              request.ToFinalRequest();

              // générer expression lambda de l'objet de recherche principal
              var mainexp = GenerateCriteria(request.ItemToSearch);                  
              mainexp = (mainexp == null) ? obj => obj.IsDeleted == false : mainexp.And(obj => obj.IsDeleted == false);                   

              // générer expression lambda des objets de recherche sécondaires
              if (request.ItemsToSearch == null) 
                request.ItemsToSearch = new List<StrategieMotDePasseDto>();                                                    
 
              var exps = new List<Expression<Func<StrategieMotDePasse, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<StrategieMotDePasse, bool>> exp = null;                  
              foreach (var expi in exps)
              {
                  if(request.NotIn)
                      exp = (exp == null) ? expi : exp.And(expi);
                  else
                      exp = (exp == null) ? expi : exp.Or(expi); 
              }

                  // concaténer les critères principaux et sécondaires
                  exp = (exp == null) ? mainexp : mainexp.And(exp);                  
                  
                  Expression<Func<StrategieMotDePasse, object>> orderByExp = GenerateOrderByExpression(request.ItemToSearch);
                  if (string.IsNullOrEmpty(request.SortOrder) || (request.SortOrder != MySortOrder.Ascending && request.SortOrder != MySortOrder.Descending))
                    request.SortOrder = MySortOrder.Descending;

                  int totalRecord;
                  IEnumerable<StrategieMotDePasse> items = _repositoryStrategieMotDePasse.GetMany(exp, request.Index, request.Size, out totalRecord, orderByExp, request.SortOrder);
  
                  response.Count = totalRecord;
                  items = items as IList<StrategieMotDePasse> ?? items.ToList();
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
      public BusinessResponse<decimal> GetSumStrategieMotDePassesByCriteria(BusinessRequest<StrategieMotDePasseDto> request)
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
                request.ItemsToSearch = new List<StrategieMotDePasseDto>();                                                    
 
              var exps = new List<Expression<Func<StrategieMotDePasse, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<StrategieMotDePasse, bool>> exp = null;                  
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

                  var sum = _repositoryStrategieMotDePasse.GetSum(exp, sumExp);
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
  		public BusinessResponse<StrategieMotDePasseDto> SaveStrategieMotDePasses(BusinessRequest<StrategieMotDePasseDto> request)
  		{
        var response = new BusinessResponse<StrategieMotDePasseDto>();
  
              try
              {
                  foreach (var item in request.ItemsToSave.TakeWhile(BusinessRequest<StrategieMotDePasseDto>.ValidateRequiredProperties))
                  {
                     var itemToSave = item.ToEntity();
                     StrategieMotDePasse itemSaved = null;

                     itemToSave.ModifiedBy = request.IdCurrentUser; 
                     if (itemToSave.Id == 0)  
                     {  
                        itemToSave.CreatedBy = request.IdCurrentUser;   

                        itemToSave.IsDeleted = false;
                        itemSaved = _repositoryStrategieMotDePasse.Add(itemToSave);
                     }
                     else //dans le cas d'une modification
                     {
                         itemSaved = _repositoryStrategieMotDePasse.Update(itemToSave, p => p.Id == itemToSave.Id);
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
      private static Expression<Func<StrategieMotDePasse, bool>> GenerateCriteria(StrategieMotDePasseDto itemToSearch) 
      {
          Expression<Func<StrategieMotDePasse, bool>> exprinit = obj => true;
          Expression<Func<StrategieMotDePasse, bool>> expi = exprinit;
                                                             
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

          if (itemToSearch.InfoSearchLongueurMinimale.Consider)
          {
              switch (itemToSearch.InfoSearchLongueurMinimale.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.LongueurMinimale != itemToSearch.LongueurMinimale);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.LongueurMinimale < itemToSearch.LongueurMinimale);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.LongueurMinimale <= itemToSearch.LongueurMinimale);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.LongueurMinimale > itemToSearch.LongueurMinimale);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.LongueurMinimale >= itemToSearch.LongueurMinimale);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.LongueurMinimale >= itemToSearch.InfoSearchLongueurMinimale.Intervalle.Debut && obj.LongueurMinimale <= itemToSearch.InfoSearchLongueurMinimale.Intervalle.Fin);                         
                      break;        
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.LongueurMinimale == itemToSearch.LongueurMinimale);                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchDureeDeVie.Consider)
          {
              switch (itemToSearch.InfoSearchDureeDeVie.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.DureeDeVie != itemToSearch.DureeDeVie);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.DureeDeVie < itemToSearch.DureeDeVie);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.DureeDeVie <= itemToSearch.DureeDeVie);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.DureeDeVie > itemToSearch.DureeDeVie);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.DureeDeVie >= itemToSearch.DureeDeVie);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.DureeDeVie >= itemToSearch.InfoSearchDureeDeVie.Intervalle.Debut && obj.DureeDeVie <= itemToSearch.InfoSearchDureeDeVie.Intervalle.Fin);                         
                      break;        
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.DureeDeVie == itemToSearch.DureeDeVie);                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchChangementALaPremiereConnexion.Consider)
          {
              switch (itemToSearch.InfoSearchChangementALaPremiereConnexion.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.ChangementALaPremiereConnexion != itemToSearch.ChangementALaPremiereConnexion);
                      break;                      
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.ChangementALaPremiereConnexion == itemToSearch.ChangementALaPremiereConnexion);                  
                      break;
              }
          }            

                  if (itemToSearch.InfoSearchTentativeDeConnexion.Consider)
          {
              switch (itemToSearch.InfoSearchTentativeDeConnexion.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.TentativeDeConnexion != itemToSearch.TentativeDeConnexion);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.TentativeDeConnexion < itemToSearch.TentativeDeConnexion);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.TentativeDeConnexion <= itemToSearch.TentativeDeConnexion);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.TentativeDeConnexion > itemToSearch.TentativeDeConnexion);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.TentativeDeConnexion >= itemToSearch.TentativeDeConnexion);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.TentativeDeConnexion >= itemToSearch.InfoSearchTentativeDeConnexion.Intervalle.Debut && obj.TentativeDeConnexion <= itemToSearch.InfoSearchTentativeDeConnexion.Intervalle.Fin);                         
                      break;        
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.TentativeDeConnexion == itemToSearch.TentativeDeConnexion);                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchComplexite.Consider)
          {
              switch (itemToSearch.InfoSearchComplexite.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.Complexite != itemToSearch.Complexite);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.Complexite.StartsWith(itemToSearch.Complexite));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.Complexite.EndsWith(itemToSearch.Complexite));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.Complexite == itemToSearch.Complexite);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.Complexite.Contains(itemToSearch.Complexite));
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
      private static Expression<Func<StrategieMotDePasse, object>> GenerateOrderByExpression(StrategieMotDePasseDto itemToSearch)
      {
          Expression<Func<StrategieMotDePasse, object>> exp = null;

          if (itemToSearch.InfoSearchId.IsOrderByField)
            exp = (o => o.Id);
          if (itemToSearch.InfoSearchLongueurMinimale.IsOrderByField)
            exp = (o => o.LongueurMinimale);
          if (itemToSearch.InfoSearchDureeDeVie.IsOrderByField)
            exp = (o => o.DureeDeVie);
          if (itemToSearch.InfoSearchChangementALaPremiereConnexion.IsOrderByField)
            exp = (o => o.ChangementALaPremiereConnexion);
          if (itemToSearch.InfoSearchTentativeDeConnexion.IsOrderByField)
            exp = (o => o.TentativeDeConnexion);
          if (itemToSearch.InfoSearchComplexite.IsOrderByField)
            exp = (o => o.Complexite);
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

          return exp ?? (obj => obj.Id);
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private static Expression<Func<StrategieMotDePasse, decimal?>> GenerateSumExpression(StrategieMotDePasseDto itemToSearch)
      {
          Expression<Func<StrategieMotDePasse, decimal?>> exp = null;

          if (itemToSearch.InfoSearchLongueurMinimale.IsSumField)
            exp = (o => o.LongueurMinimale);                                         
            if (itemToSearch.InfoSearchDureeDeVie.IsSumField)
            exp = (o => o.DureeDeVie);                                         
            if (itemToSearch.InfoSearchTentativeDeConnexion.IsSumField)
            exp = (o => o.TentativeDeConnexion);                                         
            if (itemToSearch.InfoSearchCreateBy.IsSumField)
            exp = (o => o.CreateBy.Value);                     
            if (itemToSearch.InfoSearchModifiedBy.IsSumField)
            exp = (o => o.ModifiedBy.Value);                     
            if (itemToSearch.InfoSearchCreatedBy.IsSumField)
            exp = (o => o.CreatedBy.Value);                     
  
          //return exp ?? (obj => obj.Id);
          return exp;
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private StrategieMotDePasseDto GenerateSum(Expression<Func<StrategieMotDePasse, bool>> criteria, StrategieMotDePasseDto itemToSearch, StrategieMotDePasseDto itemToReturn)
      {
          if (itemToSearch.InfoSearchLongueurMinimale.IsSumField)
            itemToReturn.InfoSearchLongueurMinimale.Sum = _repositoryStrategieMotDePasse.GetSum(criteria, o => o.LongueurMinimale);
                                        
            if (itemToSearch.InfoSearchDureeDeVie.IsSumField)
            itemToReturn.InfoSearchDureeDeVie.Sum = _repositoryStrategieMotDePasse.GetSum(criteria, o => o.DureeDeVie);
                                        
            if (itemToSearch.InfoSearchTentativeDeConnexion.IsSumField)
            itemToReturn.InfoSearchTentativeDeConnexion.Sum = _repositoryStrategieMotDePasse.GetSum(criteria, o => o.TentativeDeConnexion);
                                        
            if (itemToSearch.InfoSearchCreateBy.IsSumField)
            itemToReturn.InfoSearchCreateBy.Sum = _repositoryStrategieMotDePasse.GetSum(criteria, o => o.CreateBy.Value);     
               
            if (itemToSearch.InfoSearchModifiedBy.IsSumField)
            itemToReturn.InfoSearchModifiedBy.Sum = _repositoryStrategieMotDePasse.GetSum(criteria, o => o.ModifiedBy.Value);     
               
            if (itemToSearch.InfoSearchCreatedBy.IsSumField)
            itemToReturn.InfoSearchCreatedBy.Sum = _repositoryStrategieMotDePasse.GetSum(criteria, o => o.CreatedBy.Value);     
               
  
          return itemToReturn;
      }
  }
}

