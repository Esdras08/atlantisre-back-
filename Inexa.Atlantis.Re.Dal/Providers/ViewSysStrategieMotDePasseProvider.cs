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
  public class ViewSysStrategieMotDePasseProvider
  {

      #region Singleton
      
      private static ViewSysStrategieMotDePasseProvider _instance;

      public static ViewSysStrategieMotDePasseProvider Instance
      {
          get { return _instance ?? (_instance = new ViewSysStrategieMotDePasseProvider()); }
      }

      #endregion

      private readonly IRepository<ViewSysStrategieMotDePasse> _repositoryViewSysStrategieMotDePasse;
     
      public  ViewSysStrategieMotDePasseProvider()
      {
          _repositoryViewSysStrategieMotDePasse = new Repository<ViewSysStrategieMotDePasse>();
      }

      private ViewSysStrategieMotDePasseProvider(IRepository<ViewSysStrategieMotDePasse> repository)
      {    
          _repositoryViewSysStrategieMotDePasse = repository;
      }

      /// <summary>
      /// Recupere les données de la table
      /// </summary>
      /// <param name="request">La requete contenant les criteres de recherche</param>
      /// <returns></returns>
      public async Task<BusinessResponse<ViewSysStrategieMotDePasseDto>> GetViewSysStrategieMotDePassesByCriteria(BusinessRequest<ViewSysStrategieMotDePasseDto> request)
      {
          var response = new BusinessResponse<ViewSysStrategieMotDePasseDto>();

          try
          {
              request.ToFinalRequest();

              // générer expression lambda de l'objet de recherche principal
              var mainexp = GenerateCriteria(request.ItemToSearch);                  
              mainexp = (mainexp == null) ? obj => obj.IsDeleted == false : mainexp.And(obj => obj.IsDeleted == false);                   

              // générer expression lambda des objets de recherche sécondaires
              if (request.ItemsToSearch == null) 
                request.ItemsToSearch = new List<ViewSysStrategieMotDePasseDto>();                                                    
 
              var exps = new List<Expression<Func<ViewSysStrategieMotDePasse, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<ViewSysStrategieMotDePasse, bool>> exp = null;                  
              foreach (var expi in exps)
              {
                  if(request.NotIn)
                      exp = (exp == null) ? expi : exp.And(expi);
                  else
                      exp = (exp == null) ? expi : exp.Or(expi); 
              }

                  // concaténer les critères principaux et sécondaires
                  exp = (exp == null) ? mainexp : mainexp.And(exp);                  
                  
                  Expression<Func<ViewSysStrategieMotDePasse, object>> orderByExp = GenerateOrderByExpression(request.ItemToSearch);
                  if (string.IsNullOrEmpty(request.SortOrder) || (request.SortOrder != MySortOrder.Ascending && request.SortOrder != MySortOrder.Descending))
                    request.SortOrder = MySortOrder.Descending;

                  int totalRecord;
                  IEnumerable<ViewSysStrategieMotDePasse> items = _repositoryViewSysStrategieMotDePasse.GetMany(exp, request.Index, request.Size, out totalRecord, orderByExp, request.SortOrder);
  
                  response.Count = totalRecord;
                  items = items as IList<ViewSysStrategieMotDePasse> ?? items.ToList();
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
      public BusinessResponse<decimal> GetSumViewSysStrategieMotDePassesByCriteria(BusinessRequest<ViewSysStrategieMotDePasseDto> request)
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
                request.ItemsToSearch = new List<ViewSysStrategieMotDePasseDto>();                                                    
 
              var exps = new List<Expression<Func<ViewSysStrategieMotDePasse, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<ViewSysStrategieMotDePasse, bool>> exp = null;                  
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

                  var sum = _repositoryViewSysStrategieMotDePasse.GetSum(exp, sumExp);
                  response.Items.Add(sum);
  
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
      private static Expression<Func<ViewSysStrategieMotDePasse, bool>> GenerateCriteria(ViewSysStrategieMotDePasseDto itemToSearch) 
      {
          Expression<Func<ViewSysStrategieMotDePasse, bool>> exprinit = obj => true;
          Expression<Func<ViewSysStrategieMotDePasse, bool>> expi = exprinit;
                                                             
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

          if (itemToSearch.InfoSearchLibelleComplexite.Consider)
          {
              switch (itemToSearch.InfoSearchLibelleComplexite.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.LibelleComplexite != itemToSearch.LibelleComplexite);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.LibelleComplexite.StartsWith(itemToSearch.LibelleComplexite));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.LibelleComplexite.EndsWith(itemToSearch.LibelleComplexite));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.LibelleComplexite == itemToSearch.LibelleComplexite);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.LibelleComplexite.Contains(itemToSearch.LibelleComplexite));
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
                 
          return expi == exprinit ? null : expi;
      }

      /// <summary>
        /// Générer une expression lambda à partir d'un DTO
        /// </summary>
        /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
        /// <returns>Expression lambda</returns>
      private static Expression<Func<ViewSysStrategieMotDePasse, object>> GenerateOrderByExpression(ViewSysStrategieMotDePasseDto itemToSearch)
      {
          Expression<Func<ViewSysStrategieMotDePasse, object>> exp = null;

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
          if (itemToSearch.InfoSearchLibelleComplexite.IsOrderByField)
            exp = (o => o.LibelleComplexite);
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
          if (itemToSearch.InfoSearchCode.IsOrderByField)
            exp = (o => o.Code);

          return exp ?? (obj => obj.IdSysStrategieMotDePasse);
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private static Expression<Func<ViewSysStrategieMotDePasse, decimal?>> GenerateSumExpression(ViewSysStrategieMotDePasseDto itemToSearch)
      {
          Expression<Func<ViewSysStrategieMotDePasse, decimal?>> exp = null;

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
      private ViewSysStrategieMotDePasseDto GenerateSum(Expression<Func<ViewSysStrategieMotDePasse, bool>> criteria, ViewSysStrategieMotDePasseDto itemToSearch, ViewSysStrategieMotDePasseDto itemToReturn)
      {
          if (itemToSearch.InfoSearchLongeurMinimum.IsSumField)
            itemToReturn.InfoSearchLongeurMinimum.Sum = _repositoryViewSysStrategieMotDePasse.GetSum(criteria, o => o.LongeurMinimum.Value);     
               
            if (itemToSearch.InfoSearchDureeDeVie.IsSumField)
            itemToReturn.InfoSearchDureeDeVie.Sum = _repositoryViewSysStrategieMotDePasse.GetSum(criteria, o => o.DureeDeVie.Value);     
               
            if (itemToSearch.InfoSearchNombreTentativeAvantVerrouillage.IsSumField)
            itemToReturn.InfoSearchNombreTentativeAvantVerrouillage.Sum = _repositoryViewSysStrategieMotDePasse.GetSum(criteria, o => o.NombreTentativeAvantVerrouillage.Value);     
               
            if (itemToSearch.InfoSearchCreatedBy.IsSumField)
            itemToReturn.InfoSearchCreatedBy.Sum = _repositoryViewSysStrategieMotDePasse.GetSum(criteria, o => o.CreatedBy);
                                        
            if (itemToSearch.InfoSearchModifiedBy.IsSumField)
            itemToReturn.InfoSearchModifiedBy.Sum = _repositoryViewSysStrategieMotDePasse.GetSum(criteria, o => o.ModifiedBy);
                                        
  
          return itemToReturn;
      }
  }
}

