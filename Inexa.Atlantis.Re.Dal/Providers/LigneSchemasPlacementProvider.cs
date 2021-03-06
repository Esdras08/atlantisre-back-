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
  public class LigneSchemasPlacementProvider
  {

      #region Singleton
      
      private static LigneSchemasPlacementProvider _instance;

      public static LigneSchemasPlacementProvider Instance
      {
          get { return _instance ?? (_instance = new LigneSchemasPlacementProvider()); }
      }

      #endregion

      private readonly IRepository<LigneSchemasPlacement> _repositoryLigneSchemasPlacement;
     
      public  LigneSchemasPlacementProvider()
      {
          _repositoryLigneSchemasPlacement = new Repository<LigneSchemasPlacement>();
      }

      private LigneSchemasPlacementProvider(IRepository<LigneSchemasPlacement> repository)
      {    
          _repositoryLigneSchemasPlacement = repository;
      }

      public async Task<BusinessResponse<LigneSchemasPlacementDto>> GetLigneSchemasPlacementById(object id)
      {
          var response = new BusinessResponse<LigneSchemasPlacementDto>();

          try
          {
              var item = _repositoryLigneSchemasPlacement[id];
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
      public async Task<BusinessResponse<LigneSchemasPlacementDto>> GetLigneSchemasPlacementsByCriteria(BusinessRequest<LigneSchemasPlacementDto> request)
      {
          var response = new BusinessResponse<LigneSchemasPlacementDto>();

          try
          {
              request.ToFinalRequest();

              // générer expression lambda de l'objet de recherche principal
              var mainexp = GenerateCriteria(request.ItemToSearch);                  
              mainexp = (mainexp == null) ? obj => obj.IsDeleted == false : mainexp.And(obj => obj.IsDeleted == false);                   

              // générer expression lambda des objets de recherche sécondaires
              if (request.ItemsToSearch == null) 
                request.ItemsToSearch = new List<LigneSchemasPlacementDto>();                                                    
 
              var exps = new List<Expression<Func<LigneSchemasPlacement, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<LigneSchemasPlacement, bool>> exp = null;                  
              foreach (var expi in exps)
              {
                  if(request.NotIn)
                      exp = (exp == null) ? expi : exp.And(expi);
                  else
                      exp = (exp == null) ? expi : exp.Or(expi); 
              }

                  // concaténer les critères principaux et sécondaires
                  exp = (exp == null) ? mainexp : mainexp.And(exp);                  
                  
                  Expression<Func<LigneSchemasPlacement, object>> orderByExp = GenerateOrderByExpression(request.ItemToSearch);
                  if (string.IsNullOrEmpty(request.SortOrder) || (request.SortOrder != MySortOrder.Ascending && request.SortOrder != MySortOrder.Descending))
                    request.SortOrder = MySortOrder.Descending;

                  int totalRecord;
                  IEnumerable<LigneSchemasPlacement> items = _repositoryLigneSchemasPlacement.GetMany(exp, request.Index, request.Size, out totalRecord, orderByExp, request.SortOrder);
  
                  response.Count = totalRecord;
                  items = items as IList<LigneSchemasPlacement> ?? items.ToList();
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
      public BusinessResponse<decimal> GetSumLigneSchemasPlacementsByCriteria(BusinessRequest<LigneSchemasPlacementDto> request)
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
                request.ItemsToSearch = new List<LigneSchemasPlacementDto>();                                                    
 
              var exps = new List<Expression<Func<LigneSchemasPlacement, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<LigneSchemasPlacement, bool>> exp = null;                  
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

                  var sum = _repositoryLigneSchemasPlacement.GetSum(exp, sumExp);
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
  		public async Task<BusinessResponse<LigneSchemasPlacementDto>> SaveLigneSchemasPlacements(BusinessRequest<LigneSchemasPlacementDto> request)
  		{
        var response = new BusinessResponse<LigneSchemasPlacementDto>();
  
              try
              {
                  foreach (var item in request.ItemsToSave.TakeWhile(BusinessRequest<LigneSchemasPlacementDto>.ValidateRequiredProperties))
                  {
                     var itemToSave = item.ToEntity();
                     LigneSchemasPlacement itemSaved = null;

                     itemToSave.DateMAJ = Utilities.CurrentDate; 
                     itemToSave.ModifiedBy = request.IdCurrentUser; 
                     if (itemToSave.IdLigneSchemaPlacement == 0)  
                     {  
                        itemToSave.CreatedBy = request.IdCurrentUser;   
                        itemToSave.DateCreation = Utilities.CurrentDate;   

                        itemToSave.IsDeleted = false;
                        itemSaved = _repositoryLigneSchemasPlacement.Add(itemToSave);
                     }
                     else //dans le cas d'une modification
                     {
                         itemSaved = _repositoryLigneSchemasPlacement.Update(itemToSave, p => p.IdLigneSchemaPlacement == itemToSave.IdLigneSchemaPlacement);
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
      private static Expression<Func<LigneSchemasPlacement, bool>> GenerateCriteria(LigneSchemasPlacementDto itemToSearch) 
      {
          Expression<Func<LigneSchemasPlacement, bool>> exprinit = obj => true;
          Expression<Func<LigneSchemasPlacement, bool>> expi = exprinit;
                                                             
          if (itemToSearch.InfoSearchIdLigneSchemaPlacement.Consider)
          {
              switch (itemToSearch.InfoSearchIdLigneSchemaPlacement.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdLigneSchemaPlacement != itemToSearch.IdLigneSchemaPlacement);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdLigneSchemaPlacement < itemToSearch.IdLigneSchemaPlacement);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdLigneSchemaPlacement <= itemToSearch.IdLigneSchemaPlacement);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdLigneSchemaPlacement > itemToSearch.IdLigneSchemaPlacement);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdLigneSchemaPlacement >= itemToSearch.IdLigneSchemaPlacement);                  
                      break;  
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdLigneSchemaPlacement == itemToSearch.IdLigneSchemaPlacement);                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchProposition.Consider)
          {
              switch (itemToSearch.InfoSearchProposition.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.Proposition != itemToSearch.Proposition);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.Proposition < itemToSearch.Proposition);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.Proposition <= itemToSearch.Proposition);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.Proposition > itemToSearch.Proposition);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.Proposition >= itemToSearch.Proposition);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.Proposition >= itemToSearch.InfoSearchProposition.Intervalle.Debut && obj.Proposition <= itemToSearch.InfoSearchProposition.Intervalle.Fin);                         
                      break;        
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.Proposition == itemToSearch.Proposition);                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchPourcentage.Consider)
          {
              switch (itemToSearch.InfoSearchPourcentage.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.Pourcentage != itemToSearch.Pourcentage);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.Pourcentage < itemToSearch.Pourcentage);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.Pourcentage <= itemToSearch.Pourcentage);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.Pourcentage > itemToSearch.Pourcentage);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.Pourcentage >= itemToSearch.Pourcentage);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.Pourcentage >= itemToSearch.InfoSearchPourcentage.Intervalle.Debut && obj.Pourcentage <= itemToSearch.InfoSearchPourcentage.Intervalle.Fin);                         
                      break;        
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.Pourcentage == itemToSearch.Pourcentage);                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchCapitauxAssures.Consider)
          {
              switch (itemToSearch.InfoSearchCapitauxAssures.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.CapitauxAssures != itemToSearch.CapitauxAssures);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.CapitauxAssures < itemToSearch.CapitauxAssures);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.CapitauxAssures <= itemToSearch.CapitauxAssures);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.CapitauxAssures > itemToSearch.CapitauxAssures);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.CapitauxAssures >= itemToSearch.CapitauxAssures);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.CapitauxAssures >= itemToSearch.InfoSearchCapitauxAssures.Intervalle.Debut && obj.CapitauxAssures <= itemToSearch.InfoSearchCapitauxAssures.Intervalle.Fin);                         
                      break;        
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.CapitauxAssures == itemToSearch.CapitauxAssures);                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchTauxCommission.Consider)
          {
              switch (itemToSearch.InfoSearchTauxCommission.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.TauxCommission != itemToSearch.TauxCommission);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.TauxCommission < itemToSearch.TauxCommission);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.TauxCommission <= itemToSearch.TauxCommission);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.TauxCommission > itemToSearch.TauxCommission);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.TauxCommission >= itemToSearch.TauxCommission);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.TauxCommission >= itemToSearch.InfoSearchTauxCommission.Intervalle.Debut && obj.TauxCommission <= itemToSearch.InfoSearchTauxCommission.Intervalle.Fin);                         
                      break;        
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.TauxCommission == itemToSearch.TauxCommission);                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchCommission.Consider)
          {
              switch (itemToSearch.InfoSearchCommission.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.Commission != itemToSearch.Commission);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.Commission < itemToSearch.Commission);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.Commission <= itemToSearch.Commission);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.Commission > itemToSearch.Commission);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.Commission >= itemToSearch.Commission);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.Commission >= itemToSearch.InfoSearchCommission.Intervalle.Debut && obj.Commission <= itemToSearch.InfoSearchCommission.Intervalle.Fin);                         
                      break;        
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.Commission == itemToSearch.Commission);                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchPrimeNette.Consider)
          {
              switch (itemToSearch.InfoSearchPrimeNette.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.PrimeNette != itemToSearch.PrimeNette);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.PrimeNette < itemToSearch.PrimeNette);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.PrimeNette <= itemToSearch.PrimeNette);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.PrimeNette > itemToSearch.PrimeNette);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.PrimeNette >= itemToSearch.PrimeNette);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.PrimeNette >= itemToSearch.InfoSearchPrimeNette.Intervalle.Debut && obj.PrimeNette <= itemToSearch.InfoSearchPrimeNette.Intervalle.Fin);                         
                      break;        
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.PrimeNette == itemToSearch.PrimeNette);                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchIdReassureur.Consider)
          {
              switch (itemToSearch.InfoSearchIdReassureur.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdReassureur != itemToSearch.IdReassureur);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdReassureur < itemToSearch.IdReassureur);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdReassureur <= itemToSearch.IdReassureur);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdReassureur > itemToSearch.IdReassureur);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdReassureur >= itemToSearch.IdReassureur);                  
                      break;  
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdReassureur == itemToSearch.IdReassureur);                  
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

          if (itemToSearch.InfoSearchIdSchemasPlacement.Consider)
          {
              switch (itemToSearch.InfoSearchIdSchemasPlacement.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdSchemasPlacement != itemToSearch.IdSchemasPlacement);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdSchemasPlacement < itemToSearch.IdSchemasPlacement);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdSchemasPlacement <= itemToSearch.IdSchemasPlacement);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdSchemasPlacement > itemToSearch.IdSchemasPlacement);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdSchemasPlacement >= itemToSearch.IdSchemasPlacement);                  
                      break;  
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdSchemasPlacement == itemToSearch.IdSchemasPlacement);                  
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
      private static Expression<Func<LigneSchemasPlacement, object>> GenerateOrderByExpression(LigneSchemasPlacementDto itemToSearch)
      {
          Expression<Func<LigneSchemasPlacement, object>> exp = null;

          if (itemToSearch.InfoSearchIdLigneSchemaPlacement.IsOrderByField)
            exp = (o => o.IdLigneSchemaPlacement);
          if (itemToSearch.InfoSearchProposition.IsOrderByField)
            exp = (o => o.Proposition);
          if (itemToSearch.InfoSearchPourcentage.IsOrderByField)
            exp = (o => o.Pourcentage);
          if (itemToSearch.InfoSearchCapitauxAssures.IsOrderByField)
            exp = (o => o.CapitauxAssures);
          if (itemToSearch.InfoSearchTauxCommission.IsOrderByField)
            exp = (o => o.TauxCommission);
          if (itemToSearch.InfoSearchCommission.IsOrderByField)
            exp = (o => o.Commission);
          if (itemToSearch.InfoSearchPrimeNette.IsOrderByField)
            exp = (o => o.PrimeNette);
          if (itemToSearch.InfoSearchIdReassureur.IsOrderByField)
            exp = (o => o.IdReassureur);
          if (itemToSearch.InfoSearchIsDeleted.IsOrderByField)
            exp = (o => o.IsDeleted);
          if (itemToSearch.InfoSearchCreatedBy.IsOrderByField)
            exp = (o => o.CreatedBy);
          if (itemToSearch.InfoSearchDateCreation.IsOrderByField)
            exp = (o => o.DateCreation);
          if (itemToSearch.InfoSearchDateMAJ.IsOrderByField)
            exp = (o => o.DateMAJ);
          if (itemToSearch.InfoSearchDataKey.IsOrderByField)
            exp = (o => o.DataKey);
          if (itemToSearch.InfoSearchModifiedBy.IsOrderByField)
            exp = (o => o.ModifiedBy);
          if (itemToSearch.InfoSearchIdSchemasPlacement.IsOrderByField)
            exp = (o => o.IdSchemasPlacement);

          return exp ?? (obj => obj.IdLigneSchemaPlacement);
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private static Expression<Func<LigneSchemasPlacement, decimal?>> GenerateSumExpression(LigneSchemasPlacementDto itemToSearch)
      {
          Expression<Func<LigneSchemasPlacement, decimal?>> exp = null;

          if (itemToSearch.InfoSearchProposition.IsSumField)
            exp = (o => o.Proposition);                                         
            if (itemToSearch.InfoSearchPourcentage.IsSumField)
            exp = (o => o.Pourcentage);                                         
            if (itemToSearch.InfoSearchCapitauxAssures.IsSumField)
            exp = (o => o.CapitauxAssures);                                         
            if (itemToSearch.InfoSearchTauxCommission.IsSumField)
            exp = (o => o.TauxCommission);                                         
            if (itemToSearch.InfoSearchCommission.IsSumField)
            exp = (o => o.Commission);                                         
            if (itemToSearch.InfoSearchPrimeNette.IsSumField)
            exp = (o => o.PrimeNette);                                         
            if (itemToSearch.InfoSearchCreatedBy.IsSumField)
            exp = (o => o.CreatedBy.Value);                     
            if (itemToSearch.InfoSearchModifiedBy.IsSumField)
            exp = (o => o.ModifiedBy.Value);                     
  
          //return exp ?? (obj => obj.IdLigneSchemaPlacement);
          return exp;
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private LigneSchemasPlacementDto GenerateSum(Expression<Func<LigneSchemasPlacement, bool>> criteria, LigneSchemasPlacementDto itemToSearch, LigneSchemasPlacementDto itemToReturn)
      {
          if (itemToSearch.InfoSearchProposition.IsSumField)
            itemToReturn.InfoSearchProposition.Sum = _repositoryLigneSchemasPlacement.GetSum(criteria, o => o.Proposition);
                                        
            if (itemToSearch.InfoSearchPourcentage.IsSumField)
            itemToReturn.InfoSearchPourcentage.Sum = _repositoryLigneSchemasPlacement.GetSum(criteria, o => o.Pourcentage);
                                        
            if (itemToSearch.InfoSearchCapitauxAssures.IsSumField)
            itemToReturn.InfoSearchCapitauxAssures.Sum = _repositoryLigneSchemasPlacement.GetSum(criteria, o => o.CapitauxAssures);
                                        
            if (itemToSearch.InfoSearchTauxCommission.IsSumField)
            itemToReturn.InfoSearchTauxCommission.Sum = _repositoryLigneSchemasPlacement.GetSum(criteria, o => o.TauxCommission);
                                        
            if (itemToSearch.InfoSearchCommission.IsSumField)
            itemToReturn.InfoSearchCommission.Sum = _repositoryLigneSchemasPlacement.GetSum(criteria, o => o.Commission);
                                        
            if (itemToSearch.InfoSearchPrimeNette.IsSumField)
            itemToReturn.InfoSearchPrimeNette.Sum = _repositoryLigneSchemasPlacement.GetSum(criteria, o => o.PrimeNette);
                                        
            if (itemToSearch.InfoSearchCreatedBy.IsSumField)
            itemToReturn.InfoSearchCreatedBy.Sum = _repositoryLigneSchemasPlacement.GetSum(criteria, o => o.CreatedBy.Value);     
               
            if (itemToSearch.InfoSearchModifiedBy.IsSumField)
            itemToReturn.InfoSearchModifiedBy.Sum = _repositoryLigneSchemasPlacement.GetSum(criteria, o => o.ModifiedBy.Value);     
               
  
          return itemToReturn;
      }
  }
}

