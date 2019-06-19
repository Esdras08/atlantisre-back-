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
  public class SysStrategieMotDePasseProvider
  {

      #region Singleton
      
      private static SysStrategieMotDePasseProvider _instance;

      public static SysStrategieMotDePasseProvider Instance
      {
          get { return _instance ?? (_instance = new SysStrategieMotDePasseProvider()); }
      }

      #endregion

      private readonly IRepository<SysStrategieMotDePasse> _repositorySysStrategieMotDePasse;
     
      public  SysStrategieMotDePasseProvider()
      {
          _repositorySysStrategieMotDePasse = new Repository<SysStrategieMotDePasse>();
      }

      private SysStrategieMotDePasseProvider(IRepository<SysStrategieMotDePasse> repository)
      {    
          _repositorySysStrategieMotDePasse = repository;
      }

      public async Task<BusinessResponse<SysStrategieMotDePasseDto>> GetSysStrategieMotDePasseById(object id)
      {
          var response = new BusinessResponse<SysStrategieMotDePasseDto>();

          try
          {
              var item = _repositorySysStrategieMotDePasse[id];
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
      public async Task<BusinessResponse<SysStrategieMotDePasseDto>> GetSysStrategieMotDePassesByCriteria(BusinessRequest<SysStrategieMotDePasseDto> request)
      {
          var response = new BusinessResponse<SysStrategieMotDePasseDto>();

          try
          {
              request.ToFinalRequest();

              // générer expression lambda de l'objet de recherche principal
              var mainexp = GenerateCriteria(request.ItemToSearch);                  
              mainexp = (mainexp == null) ? obj => obj.IsDeleted == false : mainexp.And(obj => obj.IsDeleted == false);                   

              // générer expression lambda des objets de recherche sécondaires
              if (request.ItemsToSearch == null) 
                request.ItemsToSearch = new List<SysStrategieMotDePasseDto>();                                                    
 
              var exps = new List<Expression<Func<SysStrategieMotDePasse, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<SysStrategieMotDePasse, bool>> exp = null;                  
              foreach (var expi in exps)
              {
                  if(request.NotIn)
                      exp = (exp == null) ? expi : exp.And(expi);
                  else
                      exp = (exp == null) ? expi : exp.Or(expi); 
              }

                  // concaténer les critères principaux et sécondaires
                  exp = (exp == null) ? mainexp : mainexp.And(exp);                  
                  
                  Expression<Func<SysStrategieMotDePasse, object>> orderByExp = GenerateOrderByExpression(request.ItemToSearch);
                  if (string.IsNullOrEmpty(request.SortOrder) || (request.SortOrder != MySortOrder.Ascending && request.SortOrder != MySortOrder.Descending))
                    request.SortOrder = MySortOrder.Descending;

                  int totalRecord;
                  IEnumerable<SysStrategieMotDePasse> items = _repositorySysStrategieMotDePasse.GetMany(exp, request.Index, request.Size, out totalRecord, orderByExp, request.SortOrder);
  
                  response.Count = totalRecord;
                  items = items as IList<SysStrategieMotDePasse> ?? items.ToList();
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
      public BusinessResponse<decimal> GetSumSysStrategieMotDePassesByCriteria(BusinessRequest<SysStrategieMotDePasseDto> request)
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
                request.ItemsToSearch = new List<SysStrategieMotDePasseDto>();                                                    
 
              var exps = new List<Expression<Func<SysStrategieMotDePasse, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<SysStrategieMotDePasse, bool>> exp = null;                  
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

                  var sum = _repositorySysStrategieMotDePasse.GetSum(exp, sumExp);
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
  		public async Task<BusinessResponse<SysStrategieMotDePasseDto>> SaveSysStrategieMotDePasses(BusinessRequest<SysStrategieMotDePasseDto> request)
  		{
        var response = new BusinessResponse<SysStrategieMotDePasseDto>();
  
              try
              {
                  foreach (var item in request.ItemsToSave.TakeWhile(BusinessRequest<SysStrategieMotDePasseDto>.ValidateRequiredProperties))
                  {
                     var itemToSave = item.ToEntity();
                     SysStrategieMotDePasse itemSaved = null;

                     itemToSave.DateMaj = Utilities.CurrentDate; 
                     itemToSave.ModifiedBy = request.IdCurrentUser; 
                     if (itemToSave.IdSysStrategieMotDePasse == 0)  
                     {  
                        itemToSave.DateCreation = Utilities.CurrentDate;   
                        itemToSave.CreatedBy = request.IdCurrentUser;   

                        itemToSave.IsDeleted = false;
                        itemSaved = _repositorySysStrategieMotDePasse.Add(itemToSave);
                     }
                     else //dans le cas d'une modification
                     {
                         itemSaved = _repositorySysStrategieMotDePasse.Update(itemToSave, p => p.IdSysStrategieMotDePasse == itemToSave.IdSysStrategieMotDePasse);
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
      private static Expression<Func<SysStrategieMotDePasse, bool>> GenerateCriteria(SysStrategieMotDePasseDto itemToSearch) 
      {
          Expression<Func<SysStrategieMotDePasse, bool>> exprinit = obj => true;
          Expression<Func<SysStrategieMotDePasse, bool>> expi = exprinit;
                                                             
          if (itemToSearch.InfoSearchIdSysStrategieMotDePasse.Consider)
          {
              switch (itemToSearch.InfoSearchIdSysStrategieMotDePasse.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdSysStrategieMotDePasse != itemToSearch.IdSysStrategieMotDePasse);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdSysStrategieMotDePasse < itemToSearch.IdSysStrategieMotDePasse);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdSysStrategieMotDePasse <= itemToSearch.IdSysStrategieMotDePasse);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdSysStrategieMotDePasse > itemToSearch.IdSysStrategieMotDePasse);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdSysStrategieMotDePasse >= itemToSearch.IdSysStrategieMotDePasse);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.IdSysStrategieMotDePasse >= itemToSearch.InfoSearchIdSysStrategieMotDePasse.Intervalle.Debut && obj.IdSysStrategieMotDePasse <= itemToSearch.InfoSearchIdSysStrategieMotDePasse.Intervalle.Fin);                         
                      break;        
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdSysStrategieMotDePasse == itemToSearch.IdSysStrategieMotDePasse);                  
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
                 
          if (itemToSearch.InfoSearchLongeurMinimum.Consider)
          {
              switch (itemToSearch.InfoSearchLongeurMinimum.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.LongeurMinimum != itemToSearch.LongeurMinimum);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.LongeurMinimum < itemToSearch.LongeurMinimum);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.LongeurMinimum <= itemToSearch.LongeurMinimum);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.LongeurMinimum > itemToSearch.LongeurMinimum);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.LongeurMinimum >= itemToSearch.LongeurMinimum);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.LongeurMinimum >= itemToSearch.InfoSearchLongeurMinimum.Intervalle.Debut && obj.LongeurMinimum <= itemToSearch.InfoSearchLongeurMinimum.Intervalle.Fin);                         
                      break;    
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.LongeurMinimum == itemToSearch.LongeurMinimum);                  
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

          if (itemToSearch.InfoSearchNombreTentativeAvantVerrouillage.Consider)
          {
              switch (itemToSearch.InfoSearchNombreTentativeAvantVerrouillage.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.NombreTentativeAvantVerrouillage != itemToSearch.NombreTentativeAvantVerrouillage);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.NombreTentativeAvantVerrouillage < itemToSearch.NombreTentativeAvantVerrouillage);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.NombreTentativeAvantVerrouillage <= itemToSearch.NombreTentativeAvantVerrouillage);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.NombreTentativeAvantVerrouillage > itemToSearch.NombreTentativeAvantVerrouillage);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.NombreTentativeAvantVerrouillage >= itemToSearch.NombreTentativeAvantVerrouillage);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.NombreTentativeAvantVerrouillage >= itemToSearch.InfoSearchNombreTentativeAvantVerrouillage.Intervalle.Debut && obj.NombreTentativeAvantVerrouillage <= itemToSearch.InfoSearchNombreTentativeAvantVerrouillage.Intervalle.Fin);                         
                      break;    
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.NombreTentativeAvantVerrouillage == itemToSearch.NombreTentativeAvantVerrouillage);                  
                      break;
              }
          }            

          if (itemToSearch.InfoSearchIdSysComplexiteMotDePasse.Consider)
          {
              switch (itemToSearch.InfoSearchIdSysComplexiteMotDePasse.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdSysComplexiteMotDePasse != itemToSearch.IdSysComplexiteMotDePasse);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdSysComplexiteMotDePasse < itemToSearch.IdSysComplexiteMotDePasse);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdSysComplexiteMotDePasse <= itemToSearch.IdSysComplexiteMotDePasse);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdSysComplexiteMotDePasse > itemToSearch.IdSysComplexiteMotDePasse);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdSysComplexiteMotDePasse >= itemToSearch.IdSysComplexiteMotDePasse);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.IdSysComplexiteMotDePasse >= itemToSearch.InfoSearchIdSysComplexiteMotDePasse.Intervalle.Debut && obj.IdSysComplexiteMotDePasse <= itemToSearch.InfoSearchIdSysComplexiteMotDePasse.Intervalle.Fin);                         
                      break;        
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdSysComplexiteMotDePasse == itemToSearch.IdSysComplexiteMotDePasse);                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchUtiliserAncienMotDePasse.Consider)
          {
              switch (itemToSearch.InfoSearchUtiliserAncienMotDePasse.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.UtiliserAncienMotDePasse != itemToSearch.UtiliserAncienMotDePasse);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.UtiliserAncienMotDePasse.StartsWith(itemToSearch.UtiliserAncienMotDePasse));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.UtiliserAncienMotDePasse.EndsWith(itemToSearch.UtiliserAncienMotDePasse));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.UtiliserAncienMotDePasse == itemToSearch.UtiliserAncienMotDePasse);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.UtiliserAncienMotDePasse.Contains(itemToSearch.UtiliserAncienMotDePasse));
                      break;
              }
          }                               
                 
          if (itemToSearch.InfoSearchChangerMotDePasseApresAttribution.Consider)
          {
              switch (itemToSearch.InfoSearchChangerMotDePasseApresAttribution.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.ChangerMotDePasseApresAttribution != itemToSearch.ChangerMotDePasseApresAttribution);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.ChangerMotDePasseApresAttribution.StartsWith(itemToSearch.ChangerMotDePasseApresAttribution));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.ChangerMotDePasseApresAttribution.EndsWith(itemToSearch.ChangerMotDePasseApresAttribution));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.ChangerMotDePasseApresAttribution == itemToSearch.ChangerMotDePasseApresAttribution);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.ChangerMotDePasseApresAttribution.Contains(itemToSearch.ChangerMotDePasseApresAttribution));
                      break;
              }
          }                               
                 
          if (itemToSearch.InfoSearchActif.Consider)
          {
              switch (itemToSearch.InfoSearchActif.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.Actif != itemToSearch.Actif);
                      break;                      
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.Actif == itemToSearch.Actif);                  
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
                                !(obj.DateMaj.Year == itemToSearch.DateMaj.Year
                                  && obj.DateMaj.Month == itemToSearch.DateMaj.Month
                                  && obj.DateMaj.Day == itemToSearch.DateMaj.Day)
                              );
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.DateMaj.CompareTo(itemToSearch.DateMaj) < 0);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.DateMaj.CompareTo(itemToSearch.DateMaj) <= 0);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.DateMaj.CompareTo(itemToSearch.DateMaj) > 0);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.DateMaj.CompareTo(itemToSearch.DateMaj)>= 0);                  
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
                              obj => obj.DateMaj.Year == itemToSearch.DateMaj.Year
                              && obj.DateMaj.Month == itemToSearch.DateMaj.Month
                              && obj.DateMaj.Day == itemToSearch.DateMaj.Day
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
      private static Expression<Func<SysStrategieMotDePasse, object>> GenerateOrderByExpression(SysStrategieMotDePasseDto itemToSearch)
      {
          Expression<Func<SysStrategieMotDePasse, object>> exp = null;

          if (itemToSearch.InfoSearchIdSysStrategieMotDePasse.IsOrderByField)
            exp = (o => o.IdSysStrategieMotDePasse);
          if (itemToSearch.InfoSearchLibelle.IsOrderByField)
            exp = (o => o.Libelle);
          if (itemToSearch.InfoSearchLongeurMinimum.IsOrderByField)
            exp = (o => o.LongeurMinimum);
          if (itemToSearch.InfoSearchDureeDeVie.IsOrderByField)
            exp = (o => o.DureeDeVie);
          if (itemToSearch.InfoSearchNombreTentativeAvantVerrouillage.IsOrderByField)
            exp = (o => o.NombreTentativeAvantVerrouillage);
          if (itemToSearch.InfoSearchIdSysComplexiteMotDePasse.IsOrderByField)
            exp = (o => o.IdSysComplexiteMotDePasse);
          if (itemToSearch.InfoSearchUtiliserAncienMotDePasse.IsOrderByField)
            exp = (o => o.UtiliserAncienMotDePasse);
          if (itemToSearch.InfoSearchChangerMotDePasseApresAttribution.IsOrderByField)
            exp = (o => o.ChangerMotDePasseApresAttribution);
          if (itemToSearch.InfoSearchActif.IsOrderByField)
            exp = (o => o.Actif);
          if (itemToSearch.InfoSearchIsDeleted.IsOrderByField)
            exp = (o => o.IsDeleted);
          if (itemToSearch.InfoSearchDateCreation.IsOrderByField)
            exp = (o => o.DateCreation);
          if (itemToSearch.InfoSearchDateMaj.IsOrderByField)
            exp = (o => o.DateMaj);
          if (itemToSearch.InfoSearchCreatedBy.IsOrderByField)
            exp = (o => o.CreatedBy);
          if (itemToSearch.InfoSearchModifiedBy.IsOrderByField)
            exp = (o => o.ModifiedBy);
          if (itemToSearch.InfoSearchDataKey.IsOrderByField)
            exp = (o => o.DataKey);

          return exp ?? (obj => obj.IdSysStrategieMotDePasse);
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private static Expression<Func<SysStrategieMotDePasse, decimal?>> GenerateSumExpression(SysStrategieMotDePasseDto itemToSearch)
      {
          Expression<Func<SysStrategieMotDePasse, decimal?>> exp = null;

          if (itemToSearch.InfoSearchLongeurMinimum.IsSumField)
            exp = (o => o.LongeurMinimum.Value);                     
            if (itemToSearch.InfoSearchDureeDeVie.IsSumField)
            exp = (o => o.DureeDeVie.Value);                     
            if (itemToSearch.InfoSearchNombreTentativeAvantVerrouillage.IsSumField)
            exp = (o => o.NombreTentativeAvantVerrouillage.Value);                     
            if (itemToSearch.InfoSearchCreatedBy.IsSumField)
            exp = (o => o.CreatedBy);                                         
            if (itemToSearch.InfoSearchModifiedBy.IsSumField)
            exp = (o => o.ModifiedBy);                                         
  
          //return exp ?? (obj => obj.IdSysStrategieMotDePasse);
          return exp;
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private SysStrategieMotDePasseDto GenerateSum(Expression<Func<SysStrategieMotDePasse, bool>> criteria, SysStrategieMotDePasseDto itemToSearch, SysStrategieMotDePasseDto itemToReturn)
      {
          if (itemToSearch.InfoSearchLongeurMinimum.IsSumField)
            itemToReturn.InfoSearchLongeurMinimum.Sum = _repositorySysStrategieMotDePasse.GetSum(criteria, o => o.LongeurMinimum.Value);     
               
            if (itemToSearch.InfoSearchDureeDeVie.IsSumField)
            itemToReturn.InfoSearchDureeDeVie.Sum = _repositorySysStrategieMotDePasse.GetSum(criteria, o => o.DureeDeVie.Value);     
               
            if (itemToSearch.InfoSearchNombreTentativeAvantVerrouillage.IsSumField)
            itemToReturn.InfoSearchNombreTentativeAvantVerrouillage.Sum = _repositorySysStrategieMotDePasse.GetSum(criteria, o => o.NombreTentativeAvantVerrouillage.Value);     
               
            if (itemToSearch.InfoSearchCreatedBy.IsSumField)
            itemToReturn.InfoSearchCreatedBy.Sum = _repositorySysStrategieMotDePasse.GetSum(criteria, o => o.CreatedBy);
                                        
            if (itemToSearch.InfoSearchModifiedBy.IsSumField)
            itemToReturn.InfoSearchModifiedBy.Sum = _repositorySysStrategieMotDePasse.GetSum(criteria, o => o.ModifiedBy);
                                        
  
          return itemToReturn;
      }
  }
}
