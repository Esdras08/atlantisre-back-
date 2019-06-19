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
  public class ViewUtilisateurProfilProvider
  {

      #region Singleton
      
      private static ViewUtilisateurProfilProvider _instance;

      public static ViewUtilisateurProfilProvider Instance
      {
          get { return _instance ?? (_instance = new ViewUtilisateurProfilProvider()); }
      }

      #endregion

      private readonly IRepository<ViewUtilisateurProfil> _repositoryViewUtilisateurProfil;
     
      public  ViewUtilisateurProfilProvider()
      {
          _repositoryViewUtilisateurProfil = new Repository<ViewUtilisateurProfil>();
      }

      private ViewUtilisateurProfilProvider(IRepository<ViewUtilisateurProfil> repository)
      {    
          _repositoryViewUtilisateurProfil = repository;
      }

      /// <summary>
      /// Recupere les données de la table
      /// </summary>
      /// <param name="request">La requete contenant les criteres de recherche</param>
      /// <returns></returns>
      public async Task<BusinessResponse<ViewUtilisateurProfilDto>> GetViewUtilisateurProfilsByCriteria(BusinessRequest<ViewUtilisateurProfilDto> request)
      {
          var response = new BusinessResponse<ViewUtilisateurProfilDto>();

          try
          {
              request.ToFinalRequest();

              // générer expression lambda de l'objet de recherche principal
              var mainexp = GenerateCriteria(request.ItemToSearch);                  
              mainexp = (mainexp == null) ? obj => obj.IsDeleted == false : mainexp.And(obj => obj.IsDeleted == false);                   

              // générer expression lambda des objets de recherche sécondaires
              if (request.ItemsToSearch == null) 
                request.ItemsToSearch = new List<ViewUtilisateurProfilDto>();                                                    
 
              var exps = new List<Expression<Func<ViewUtilisateurProfil, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<ViewUtilisateurProfil, bool>> exp = null;                  
              foreach (var expi in exps)
              {
                  if(request.NotIn)
                      exp = (exp == null) ? expi : exp.And(expi);
                  else
                      exp = (exp == null) ? expi : exp.Or(expi); 
              }

                  // concaténer les critères principaux et sécondaires
                  exp = (exp == null) ? mainexp : mainexp.And(exp);                  
                  
                  Expression<Func<ViewUtilisateurProfil, object>> orderByExp = GenerateOrderByExpression(request.ItemToSearch);
                  if (string.IsNullOrEmpty(request.SortOrder) || (request.SortOrder != MySortOrder.Ascending && request.SortOrder != MySortOrder.Descending))
                    request.SortOrder = MySortOrder.Descending;

                  int totalRecord;
                  IEnumerable<ViewUtilisateurProfil> items = _repositoryViewUtilisateurProfil.GetMany(exp, request.Index, request.Size, out totalRecord, orderByExp, request.SortOrder);
  
                  response.Count = totalRecord;
                  items = items as IList<ViewUtilisateurProfil> ?? items.ToList();
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
      public BusinessResponse<decimal> GetSumViewUtilisateurProfilsByCriteria(BusinessRequest<ViewUtilisateurProfilDto> request)
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
                request.ItemsToSearch = new List<ViewUtilisateurProfilDto>();                                                    
 
              var exps = new List<Expression<Func<ViewUtilisateurProfil, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<ViewUtilisateurProfil, bool>> exp = null;                  
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

                  var sum = _repositoryViewUtilisateurProfil.GetSum(exp, sumExp);
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
      private static Expression<Func<ViewUtilisateurProfil, bool>> GenerateCriteria(ViewUtilisateurProfilDto itemToSearch) 
      {
          Expression<Func<ViewUtilisateurProfil, bool>> exprinit = obj => true;
          Expression<Func<ViewUtilisateurProfil, bool>> expi = exprinit;
                                                             
          if (itemToSearch.InfoSearchIdUtilisateurProfil.Consider)
          {
              switch (itemToSearch.InfoSearchIdUtilisateurProfil.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdUtilisateurProfil != itemToSearch.IdUtilisateurProfil);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdUtilisateurProfil < itemToSearch.IdUtilisateurProfil);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdUtilisateurProfil <= itemToSearch.IdUtilisateurProfil);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdUtilisateurProfil > itemToSearch.IdUtilisateurProfil);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdUtilisateurProfil >= itemToSearch.IdUtilisateurProfil);                  
                      break;  
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdUtilisateurProfil == itemToSearch.IdUtilisateurProfil);                  
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

          if (itemToSearch.InfoSearchNomUtilisateur.Consider)
          {
              switch (itemToSearch.InfoSearchNomUtilisateur.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.NomUtilisateur != itemToSearch.NomUtilisateur);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.NomUtilisateur.StartsWith(itemToSearch.NomUtilisateur));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.NomUtilisateur.EndsWith(itemToSearch.NomUtilisateur));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.NomUtilisateur == itemToSearch.NomUtilisateur);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.NomUtilisateur.Contains(itemToSearch.NomUtilisateur));
                      break;
              }
          }                               
                 
          if (itemToSearch.InfoSearchIdProfil.Consider)
          {
              switch (itemToSearch.InfoSearchIdProfil.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdProfil != itemToSearch.IdProfil);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdProfil < itemToSearch.IdProfil);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdProfil <= itemToSearch.IdProfil);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdProfil > itemToSearch.IdProfil);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdProfil >= itemToSearch.IdProfil);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.IdProfil >= itemToSearch.InfoSearchIdProfil.Intervalle.Debut && obj.IdProfil <= itemToSearch.InfoSearchIdProfil.Intervalle.Fin);                         
                      break;        
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdProfil == itemToSearch.IdProfil);                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchIntitule.Consider)
          {
              switch (itemToSearch.InfoSearchIntitule.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.Intitule != itemToSearch.Intitule);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.Intitule.StartsWith(itemToSearch.Intitule));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.Intitule.EndsWith(itemToSearch.Intitule));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.Intitule == itemToSearch.Intitule);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.Intitule.Contains(itemToSearch.Intitule));
                      break;
              }
          }                               
                 
          if (itemToSearch.InfoSearchDateValiditeDebut.Consider)
          {
              switch (itemToSearch.InfoSearchDateValiditeDebut.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And
                            (obj => 
                                !(obj.DateValiditeDebut.Value.Year == itemToSearch.DateValiditeDebut.Value.Year
                                && obj.DateValiditeDebut.Value.Month == itemToSearch.DateValiditeDebut.Value.Month
                                && obj.DateValiditeDebut.Value.Day == itemToSearch.DateValiditeDebut.Value.Day)
                             );
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateValiditeDebut.Value, itemToSearch.DateValiditeDebut.Value) < 0);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateValiditeDebut.Value, itemToSearch.DateValiditeDebut.Value) <= 0);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateValiditeDebut.Value, itemToSearch.DateValiditeDebut.Value) > 0);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateValiditeDebut.Value, itemToSearch.DateValiditeDebut.Value) >= 0);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      var debut = itemToSearch.InfoSearchDateValiditeDebut.Intervalle.Debut.Date;
                      var fin = itemToSearch.InfoSearchDateValiditeDebut.Intervalle.Fin.AddDays(1).Date;
                      expi = expi.And(obj => obj.DateValiditeDebut >= debut && obj.DateValiditeDebut < fin);                  
                      break;         
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And
                            (
                              obj => obj.DateValiditeDebut.Value.Year == itemToSearch.DateValiditeDebut.Value.Year
                              && obj.DateValiditeDebut.Value.Month == itemToSearch.DateValiditeDebut.Value.Month
                              && obj.DateValiditeDebut.Value.Day == itemToSearch.DateValiditeDebut.Value.Day
                             );                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchDateValiditeFin.Consider)
          {
              switch (itemToSearch.InfoSearchDateValiditeFin.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And
                            (obj => 
                                !(obj.DateValiditeFin.Value.Year == itemToSearch.DateValiditeFin.Value.Year
                                && obj.DateValiditeFin.Value.Month == itemToSearch.DateValiditeFin.Value.Month
                                && obj.DateValiditeFin.Value.Day == itemToSearch.DateValiditeFin.Value.Day)
                             );
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateValiditeFin.Value, itemToSearch.DateValiditeFin.Value) < 0);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateValiditeFin.Value, itemToSearch.DateValiditeFin.Value) <= 0);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateValiditeFin.Value, itemToSearch.DateValiditeFin.Value) > 0);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateValiditeFin.Value, itemToSearch.DateValiditeFin.Value) >= 0);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      var debut = itemToSearch.InfoSearchDateValiditeFin.Intervalle.Debut.Date;
                      var fin = itemToSearch.InfoSearchDateValiditeFin.Intervalle.Fin.AddDays(1).Date;
                      expi = expi.And(obj => obj.DateValiditeFin >= debut && obj.DateValiditeFin < fin);                  
                      break;         
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And
                            (
                              obj => obj.DateValiditeFin.Value.Year == itemToSearch.DateValiditeFin.Value.Year
                              && obj.DateValiditeFin.Value.Month == itemToSearch.DateValiditeFin.Value.Month
                              && obj.DateValiditeFin.Value.Day == itemToSearch.DateValiditeFin.Value.Day
                             );                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchDateFinIndeterminer.Consider)
          {
              switch (itemToSearch.InfoSearchDateFinIndeterminer.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.DateFinIndeterminer != itemToSearch.DateFinIndeterminer);
                      break;                      
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.DateFinIndeterminer == itemToSearch.DateFinIndeterminer);                  
                      break;
              }
          }            

                  if (itemToSearch.InfoSearchAutorise.Consider)
          {
              switch (itemToSearch.InfoSearchAutorise.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.Autorise != itemToSearch.Autorise);
                      break;                      
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.Autorise == itemToSearch.Autorise);                  
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

          return expi == exprinit ? null : expi;
      }

      /// <summary>
        /// Générer une expression lambda à partir d'un DTO
        /// </summary>
        /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
        /// <returns>Expression lambda</returns>
      private static Expression<Func<ViewUtilisateurProfil, object>> GenerateOrderByExpression(ViewUtilisateurProfilDto itemToSearch)
      {
          Expression<Func<ViewUtilisateurProfil, object>> exp = null;

          if (itemToSearch.InfoSearchIdUtilisateurProfil.IsOrderByField)
            exp = (o => o.IdUtilisateurProfil);
          if (itemToSearch.InfoSearchIdUtilisateur.IsOrderByField)
            exp = (o => o.IdUtilisateur);
          if (itemToSearch.InfoSearchNomUtilisateur.IsOrderByField)
            exp = (o => o.NomUtilisateur);
          if (itemToSearch.InfoSearchIdProfil.IsOrderByField)
            exp = (o => o.IdProfil);
          if (itemToSearch.InfoSearchIntitule.IsOrderByField)
            exp = (o => o.Intitule);
          if (itemToSearch.InfoSearchDateValiditeDebut.IsOrderByField)
            exp = (o => o.DateValiditeDebut);
          if (itemToSearch.InfoSearchDateValiditeFin.IsOrderByField)
            exp = (o => o.DateValiditeFin);
          if (itemToSearch.InfoSearchDateFinIndeterminer.IsOrderByField)
            exp = (o => o.DateFinIndeterminer);
          if (itemToSearch.InfoSearchAutorise.IsOrderByField)
            exp = (o => o.Autorise);
          if (itemToSearch.InfoSearchIsDeleted.IsOrderByField)
            exp = (o => o.IsDeleted);
          if (itemToSearch.InfoSearchCreatedBy.IsOrderByField)
            exp = (o => o.CreatedBy);
          if (itemToSearch.InfoSearchModifiedBy.IsOrderByField)
            exp = (o => o.ModifiedBy);
          if (itemToSearch.InfoSearchDateCreation.IsOrderByField)
            exp = (o => o.DateCreation);
          if (itemToSearch.InfoSearchDateMaj.IsOrderByField)
            exp = (o => o.DateMaj);

          return exp ?? (obj => obj.IdUtilisateurProfil);
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private static Expression<Func<ViewUtilisateurProfil, decimal?>> GenerateSumExpression(ViewUtilisateurProfilDto itemToSearch)
      {
          Expression<Func<ViewUtilisateurProfil, decimal?>> exp = null;

          if (itemToSearch.InfoSearchCreatedBy.IsSumField)
            exp = (o => o.CreatedBy);                                         
            if (itemToSearch.InfoSearchModifiedBy.IsSumField)
            exp = (o => o.ModifiedBy);                                         
  
          //return exp ?? (obj => obj.IdUtilisateurProfil);
          return exp;
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private ViewUtilisateurProfilDto GenerateSum(Expression<Func<ViewUtilisateurProfil, bool>> criteria, ViewUtilisateurProfilDto itemToSearch, ViewUtilisateurProfilDto itemToReturn)
      {
          if (itemToSearch.InfoSearchCreatedBy.IsSumField)
            itemToReturn.InfoSearchCreatedBy.Sum = _repositoryViewUtilisateurProfil.GetSum(criteria, o => o.CreatedBy);
                                        
            if (itemToSearch.InfoSearchModifiedBy.IsSumField)
            itemToReturn.InfoSearchModifiedBy.Sum = _repositoryViewUtilisateurProfil.GetSum(criteria, o => o.ModifiedBy);
                                        
  
          return itemToReturn;
      }
  }
}

