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
  public class TraceConformiteProvider
  {

      #region Singleton
      
      private static TraceConformiteProvider _instance;

      public static TraceConformiteProvider Instance
      {
          get { return _instance ?? (_instance = new TraceConformiteProvider()); }
      }

      #endregion

      private readonly IRepository<TraceConformite> _repositoryTraceConformite;
     
      private TraceConformiteProvider()
      {
          _repositoryTraceConformite = new Repository<TraceConformite>();
      }

      private TraceConformiteProvider(IRepository<TraceConformite> repository)
      {    
          _repositoryTraceConformite = repository;
      }

      public BusinessResponse<TraceConformiteDto> GetTraceConformiteById(object id)
      {
          var response = new BusinessResponse<TraceConformiteDto>();

          try
          {
              var item = _repositoryTraceConformite[id];
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
      public BusinessResponse<TraceConformiteDto> GetTraceConformitesByCriteria(BusinessRequest<TraceConformiteDto> request)
      {
          var response = new BusinessResponse<TraceConformiteDto>();

          try
          {
              request.ToFinalRequest();

              // générer expression lambda de l'objet de recherche principal
              var mainexp = GenerateCriteria(request.ItemToSearch);                  
              mainexp = (mainexp == null) ? obj => obj.IsDeleted == false : mainexp.And(obj => obj.IsDeleted == false);                   

              // générer expression lambda des objets de recherche sécondaires
              if (request.ItemsToSearch == null) 
                request.ItemsToSearch = new List<TraceConformiteDto>();                                                    
 
              var exps = new List<Expression<Func<TraceConformite, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<TraceConformite, bool>> exp = null;                  
              foreach (var expi in exps)
              {
                  if(request.NotIn)
                      exp = (exp == null) ? expi : exp.And(expi);
                  else
                      exp = (exp == null) ? expi : exp.Or(expi); 
              }

                  // concaténer les critères principaux et sécondaires
                  exp = (exp == null) ? mainexp : mainexp.And(exp);                  
                  
                  Expression<Func<TraceConformite, object>> orderByExp = GenerateOrderByExpression(request.ItemToSearch);
                  if (string.IsNullOrEmpty(request.SortOrder) || (request.SortOrder != MySortOrder.Ascending && request.SortOrder != MySortOrder.Descending))
                    request.SortOrder = MySortOrder.Descending;

                  int totalRecord;
                  IEnumerable<TraceConformite> items = _repositoryTraceConformite.GetMany(exp, request.Index, request.Size, out totalRecord, orderByExp, request.SortOrder);
  
                  response.Count = totalRecord;
                  items = items as IList<TraceConformite> ?? items.ToList();
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
      public BusinessResponse<decimal> GetSumTraceConformitesByCriteria(BusinessRequest<TraceConformiteDto> request)
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
                request.ItemsToSearch = new List<TraceConformiteDto>();                                                    
 
              var exps = new List<Expression<Func<TraceConformite, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<TraceConformite, bool>> exp = null;                  
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

                  var sum = _repositoryTraceConformite.GetSum(exp, sumExp);
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
  		public BusinessResponse<TraceConformiteDto> SaveTraceConformites(BusinessRequest<TraceConformiteDto> request)
  		{
        var response = new BusinessResponse<TraceConformiteDto>();
  
              try
              {
                  foreach (var item in request.ItemsToSave.TakeWhile(BusinessRequest<TraceConformiteDto>.ValidateRequiredProperties))
                  {
                     var itemToSave = item.ToEntity();
                     TraceConformite itemSaved = null;

                     itemToSave.ModifiedBy = request.IdCurrentUser; 
                     if (itemToSave.IdTraceConformite == 0)  
                     {  
                        itemToSave.CreatedBy = request.IdCurrentUser;   

                        itemToSave.IsDeleted = false;
                        itemSaved = _repositoryTraceConformite.Add(itemToSave);
                     }
                     else //dans le cas d'une modification
                     {
                         itemSaved = _repositoryTraceConformite.Update(itemToSave, p => p.IdTraceConformite == itemToSave.IdTraceConformite);
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
      private static Expression<Func<TraceConformite, bool>> GenerateCriteria(TraceConformiteDto itemToSearch) 
      {
          Expression<Func<TraceConformite, bool>> exprinit = obj => true;
          Expression<Func<TraceConformite, bool>> expi = exprinit;
                                                             
          if (itemToSearch.InfoSearchIdTraceConformite.Consider)
          {
              switch (itemToSearch.InfoSearchIdTraceConformite.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdTraceConformite != itemToSearch.IdTraceConformite);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdTraceConformite < itemToSearch.IdTraceConformite);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdTraceConformite <= itemToSearch.IdTraceConformite);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdTraceConformite > itemToSearch.IdTraceConformite);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdTraceConformite >= itemToSearch.IdTraceConformite);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.IdTraceConformite >= itemToSearch.InfoSearchIdTraceConformite.Intervalle.Debut && obj.IdTraceConformite <= itemToSearch.InfoSearchIdTraceConformite.Intervalle.Fin);                         
                      break;        
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdTraceConformite == itemToSearch.IdTraceConformite);                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchIdEntity.Consider)
          {
              switch (itemToSearch.InfoSearchIdEntity.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdEntity != itemToSearch.IdEntity);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdEntity < itemToSearch.IdEntity);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdEntity <= itemToSearch.IdEntity);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdEntity > itemToSearch.IdEntity);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdEntity >= itemToSearch.IdEntity);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.IdEntity >= itemToSearch.InfoSearchIdEntity.Intervalle.Debut && obj.IdEntity <= itemToSearch.InfoSearchIdEntity.Intervalle.Fin);                         
                      break;    
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdEntity == itemToSearch.IdEntity);                  
                      break;
              }
          }            

          if (itemToSearch.InfoSearchOrigineEntity.Consider)
          {
              switch (itemToSearch.InfoSearchOrigineEntity.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.OrigineEntity != itemToSearch.OrigineEntity);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.OrigineEntity.StartsWith(itemToSearch.OrigineEntity));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.OrigineEntity.EndsWith(itemToSearch.OrigineEntity));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.OrigineEntity == itemToSearch.OrigineEntity);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.OrigineEntity.Contains(itemToSearch.OrigineEntity));
                      break;
              }
          }                               
                 
          if (itemToSearch.InfoSearchIdRegle.Consider)
          {
              switch (itemToSearch.InfoSearchIdRegle.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdRegle != itemToSearch.IdRegle);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdRegle < itemToSearch.IdRegle);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdRegle <= itemToSearch.IdRegle);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdRegle > itemToSearch.IdRegle);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdRegle >= itemToSearch.IdRegle);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.IdRegle >= itemToSearch.InfoSearchIdRegle.Intervalle.Debut && obj.IdRegle <= itemToSearch.InfoSearchIdRegle.Intervalle.Fin);                         
                      break;    
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdRegle == itemToSearch.IdRegle);                  
                      break;
              }
          }            

          if (itemToSearch.InfoSearchOrigineRegle.Consider)
          {
              switch (itemToSearch.InfoSearchOrigineRegle.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.OrigineRegle != itemToSearch.OrigineRegle);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.OrigineRegle < itemToSearch.OrigineRegle);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.OrigineRegle <= itemToSearch.OrigineRegle);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.OrigineRegle > itemToSearch.OrigineRegle);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.OrigineRegle >= itemToSearch.OrigineRegle);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.OrigineRegle >= itemToSearch.InfoSearchOrigineRegle.Intervalle.Debut && obj.OrigineRegle <= itemToSearch.InfoSearchOrigineRegle.Intervalle.Fin);                         
                      break;    
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.OrigineRegle == itemToSearch.OrigineRegle);                  
                      break;
              }
          }            

          if (itemToSearch.InfoSearchLibelleRegle.Consider)
          {
              switch (itemToSearch.InfoSearchLibelleRegle.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.LibelleRegle != itemToSearch.LibelleRegle);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.LibelleRegle.StartsWith(itemToSearch.LibelleRegle));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.LibelleRegle.EndsWith(itemToSearch.LibelleRegle));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.LibelleRegle == itemToSearch.LibelleRegle);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.LibelleRegle.Contains(itemToSearch.LibelleRegle));
                      break;
              }
          }                               
                 
          if (itemToSearch.InfoSearchDateViolation.Consider)
          {
              switch (itemToSearch.InfoSearchDateViolation.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And
                            (obj => 
                                !(obj.DateViolation.Value.Year == itemToSearch.DateViolation.Value.Year
                                && obj.DateViolation.Value.Month == itemToSearch.DateViolation.Value.Month
                                && obj.DateViolation.Value.Day == itemToSearch.DateViolation.Value.Day)
                             );
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateViolation.Value, itemToSearch.DateViolation.Value) < 0);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateViolation.Value, itemToSearch.DateViolation.Value) <= 0);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateViolation.Value, itemToSearch.DateViolation.Value) > 0);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateViolation.Value, itemToSearch.DateViolation.Value) >= 0);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      var debut = itemToSearch.InfoSearchDateViolation.Intervalle.Debut.Date;
                      var fin = itemToSearch.InfoSearchDateViolation.Intervalle.Fin.AddDays(1).Date;
                      expi = expi.And(obj => obj.DateViolation >= debut && obj.DateViolation < fin);                  
                      break;         
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And
                            (
                              obj => obj.DateViolation.Value.Year == itemToSearch.DateViolation.Value.Year
                              && obj.DateViolation.Value.Month == itemToSearch.DateViolation.Value.Month
                              && obj.DateViolation.Value.Day == itemToSearch.DateViolation.Value.Day
                             );                  
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

          return expi == exprinit ? null : expi;
      }

      /// <summary>
        /// Générer une expression lambda à partir d'un DTO
        /// </summary>
        /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
        /// <returns>Expression lambda</returns>
      private static Expression<Func<TraceConformite, object>> GenerateOrderByExpression(TraceConformiteDto itemToSearch)
      {
          Expression<Func<TraceConformite, object>> exp = null;

          if (itemToSearch.InfoSearchIdTraceConformite.IsOrderByField)
            exp = (o => o.IdTraceConformite);
          if (itemToSearch.InfoSearchIdEntity.IsOrderByField)
            exp = (o => o.IdEntity);
          if (itemToSearch.InfoSearchOrigineEntity.IsOrderByField)
            exp = (o => o.OrigineEntity);
          if (itemToSearch.InfoSearchIdRegle.IsOrderByField)
            exp = (o => o.IdRegle);
          if (itemToSearch.InfoSearchOrigineRegle.IsOrderByField)
            exp = (o => o.OrigineRegle);
          if (itemToSearch.InfoSearchLibelleRegle.IsOrderByField)
            exp = (o => o.LibelleRegle);
          if (itemToSearch.InfoSearchDateViolation.IsOrderByField)
            exp = (o => o.DateViolation);
          if (itemToSearch.InfoSearchIdUtilisateur.IsOrderByField)
            exp = (o => o.IdUtilisateur);
          if (itemToSearch.InfoSearchCreateOn.IsOrderByField)
            exp = (o => o.CreateOn);
          if (itemToSearch.InfoSearchModifiedOn.IsOrderByField)
            exp = (o => o.ModifiedOn);
          if (itemToSearch.InfoSearchCreateBy.IsOrderByField)
            exp = (o => o.CreateBy);
          if (itemToSearch.InfoSearchModifiedBy.IsOrderByField)
            exp = (o => o.ModifiedBy);
          if (itemToSearch.InfoSearchIsDeleted.IsOrderByField)
            exp = (o => o.IsDeleted);
          if (itemToSearch.InfoSearchCreatedBy.IsOrderByField)
            exp = (o => o.CreatedBy);

          return exp ?? (obj => obj.IdTraceConformite);
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private static Expression<Func<TraceConformite, decimal?>> GenerateSumExpression(TraceConformiteDto itemToSearch)
      {
          Expression<Func<TraceConformite, decimal?>> exp = null;

          if (itemToSearch.InfoSearchOrigineRegle.IsSumField)
            exp = (o => o.OrigineRegle.Value);                     
            if (itemToSearch.InfoSearchCreateBy.IsSumField)
            exp = (o => o.CreateBy.Value);                     
            if (itemToSearch.InfoSearchModifiedBy.IsSumField)
            exp = (o => o.ModifiedBy.Value);                     
            if (itemToSearch.InfoSearchCreatedBy.IsSumField)
            exp = (o => o.CreatedBy.Value);                     
  
          //return exp ?? (obj => obj.IdTraceConformite);
          return exp;
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private TraceConformiteDto GenerateSum(Expression<Func<TraceConformite, bool>> criteria, TraceConformiteDto itemToSearch, TraceConformiteDto itemToReturn)
      {
          if (itemToSearch.InfoSearchOrigineRegle.IsSumField)
            itemToReturn.InfoSearchOrigineRegle.Sum = _repositoryTraceConformite.GetSum(criteria, o => o.OrigineRegle.Value);     
               
            if (itemToSearch.InfoSearchCreateBy.IsSumField)
            itemToReturn.InfoSearchCreateBy.Sum = _repositoryTraceConformite.GetSum(criteria, o => o.CreateBy.Value);     
               
            if (itemToSearch.InfoSearchModifiedBy.IsSumField)
            itemToReturn.InfoSearchModifiedBy.Sum = _repositoryTraceConformite.GetSum(criteria, o => o.ModifiedBy.Value);     
               
            if (itemToSearch.InfoSearchCreatedBy.IsSumField)
            itemToReturn.InfoSearchCreatedBy.Sum = _repositoryTraceConformite.GetSum(criteria, o => o.CreatedBy.Value);     
               
  
          return itemToReturn;
      }
  }
}

