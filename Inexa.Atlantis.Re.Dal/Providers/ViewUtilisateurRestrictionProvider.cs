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
  public class ViewUtilisateurRestrictionProvider
  {

      #region Singleton
      
      private static ViewUtilisateurRestrictionProvider _instance;

      public static ViewUtilisateurRestrictionProvider Instance
      {
          get { return _instance ?? (_instance = new ViewUtilisateurRestrictionProvider()); }
      }

      #endregion

      private readonly IRepository<ViewUtilisateurRestriction> _repositoryViewUtilisateurRestriction;
     
      public  ViewUtilisateurRestrictionProvider()
      {
          _repositoryViewUtilisateurRestriction = new Repository<ViewUtilisateurRestriction>();
      }

      private ViewUtilisateurRestrictionProvider(IRepository<ViewUtilisateurRestriction> repository)
      {    
          _repositoryViewUtilisateurRestriction = repository;
      }

      /// <summary>
      /// Recupere les données de la table
      /// </summary>
      /// <param name="request">La requete contenant les criteres de recherche</param>
      /// <returns></returns>
      public async Task<BusinessResponse<ViewUtilisateurRestrictionDto>> GetViewUtilisateurRestrictionsByCriteria(BusinessRequest<ViewUtilisateurRestrictionDto> request)
      {
          var response = new BusinessResponse<ViewUtilisateurRestrictionDto>();

          try
          {
              request.ToFinalRequest();

              // générer expression lambda de l'objet de recherche principal
              var mainexp = GenerateCriteria(request.ItemToSearch);                  
              mainexp = mainexp ?? (obj => true);                   

              // générer expression lambda des objets de recherche sécondaires
              if (request.ItemsToSearch == null) 
                request.ItemsToSearch = new List<ViewUtilisateurRestrictionDto>();                                                    
 
              var exps = new List<Expression<Func<ViewUtilisateurRestriction, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<ViewUtilisateurRestriction, bool>> exp = null;                  
              foreach (var expi in exps)
              {
                  if(request.NotIn)
                      exp = (exp == null) ? expi : exp.And(expi);
                  else
                      exp = (exp == null) ? expi : exp.Or(expi); 
              }

                  // concaténer les critères principaux et sécondaires
                  exp = (exp == null) ? mainexp : mainexp.And(exp);                  
                  
                  Expression<Func<ViewUtilisateurRestriction, object>> orderByExp = GenerateOrderByExpression(request.ItemToSearch);
                  if (string.IsNullOrEmpty(request.SortOrder) || (request.SortOrder != MySortOrder.Ascending && request.SortOrder != MySortOrder.Descending))
                    request.SortOrder = MySortOrder.Descending;

                  int totalRecord;
                  IEnumerable<ViewUtilisateurRestriction> items = _repositoryViewUtilisateurRestriction.GetMany(exp, request.Index, request.Size, out totalRecord, orderByExp, request.SortOrder);
  
                  response.Count = totalRecord;
                  items = items as IList<ViewUtilisateurRestriction> ?? items.ToList();
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
      public BusinessResponse<decimal> GetSumViewUtilisateurRestrictionsByCriteria(BusinessRequest<ViewUtilisateurRestrictionDto> request)
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
                request.ItemsToSearch = new List<ViewUtilisateurRestrictionDto>();                                                    
 
              var exps = new List<Expression<Func<ViewUtilisateurRestriction, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<ViewUtilisateurRestriction, bool>> exp = null;                  
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

                  var sum = _repositoryViewUtilisateurRestriction.GetSum(exp, sumExp);
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
      private static Expression<Func<ViewUtilisateurRestriction, bool>> GenerateCriteria(ViewUtilisateurRestrictionDto itemToSearch) 
      {
          Expression<Func<ViewUtilisateurRestriction, bool>> exprinit = obj => true;
          Expression<Func<ViewUtilisateurRestriction, bool>> expi = exprinit;
                                                             
          if (itemToSearch.InfoSearchIdUtilisateurRestriction.Consider)
          {
              switch (itemToSearch.InfoSearchIdUtilisateurRestriction.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdUtilisateurRestriction != itemToSearch.IdUtilisateurRestriction);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdUtilisateurRestriction < itemToSearch.IdUtilisateurRestriction);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdUtilisateurRestriction <= itemToSearch.IdUtilisateurRestriction);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdUtilisateurRestriction > itemToSearch.IdUtilisateurRestriction);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdUtilisateurRestriction >= itemToSearch.IdUtilisateurRestriction);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.IdUtilisateurRestriction >= itemToSearch.InfoSearchIdUtilisateurRestriction.Intervalle.Debut && obj.IdUtilisateurRestriction <= itemToSearch.InfoSearchIdUtilisateurRestriction.Intervalle.Fin);                         
                      break;        
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdUtilisateurRestriction == itemToSearch.IdUtilisateurRestriction);                  
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

          if (itemToSearch.InfoSearchLogin.Consider)
          {
              switch (itemToSearch.InfoSearchLogin.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.Login != itemToSearch.Login);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.Login.StartsWith(itemToSearch.Login));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.Login.EndsWith(itemToSearch.Login));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.Login == itemToSearch.Login);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.Login.Contains(itemToSearch.Login));
                      break;
              }
          }                               
                 
          if (itemToSearch.InfoSearchNom.Consider)
          {
              switch (itemToSearch.InfoSearchNom.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.Nom != itemToSearch.Nom);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.Nom.StartsWith(itemToSearch.Nom));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.Nom.EndsWith(itemToSearch.Nom));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.Nom == itemToSearch.Nom);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.Nom.Contains(itemToSearch.Nom));
                      break;
              }
          }                               
                 
          if (itemToSearch.InfoSearchPrenom.Consider)
          {
              switch (itemToSearch.InfoSearchPrenom.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.Prenom != itemToSearch.Prenom);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.Prenom.StartsWith(itemToSearch.Prenom));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.Prenom.EndsWith(itemToSearch.Prenom));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.Prenom == itemToSearch.Prenom);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.Prenom.Contains(itemToSearch.Prenom));
                      break;
              }
          }                               
                 
          if (itemToSearch.InfoSearchIdSysObjet.Consider)
          {
              switch (itemToSearch.InfoSearchIdSysObjet.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdSysObjet != itemToSearch.IdSysObjet);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdSysObjet < itemToSearch.IdSysObjet);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdSysObjet <= itemToSearch.IdSysObjet);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdSysObjet > itemToSearch.IdSysObjet);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdSysObjet >= itemToSearch.IdSysObjet);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.IdSysObjet >= itemToSearch.InfoSearchIdSysObjet.Intervalle.Debut && obj.IdSysObjet <= itemToSearch.InfoSearchIdSysObjet.Intervalle.Fin);                         
                      break;        
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdSysObjet == itemToSearch.IdSysObjet);                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchTypeObjet.Consider)
          {
              switch (itemToSearch.InfoSearchTypeObjet.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.TypeObjet != itemToSearch.TypeObjet);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.TypeObjet.StartsWith(itemToSearch.TypeObjet));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.TypeObjet.EndsWith(itemToSearch.TypeObjet));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.TypeObjet == itemToSearch.TypeObjet);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.TypeObjet.Contains(itemToSearch.TypeObjet));
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
                 
          if (itemToSearch.InfoSearchAutorise.Consider)
          {
              switch (itemToSearch.InfoSearchAutorise.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.Autorise != itemToSearch.Autorise);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.Autorise.StartsWith(itemToSearch.Autorise));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.Autorise.EndsWith(itemToSearch.Autorise));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.Autorise == itemToSearch.Autorise);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.Autorise.Contains(itemToSearch.Autorise));
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
      private static Expression<Func<ViewUtilisateurRestriction, object>> GenerateOrderByExpression(ViewUtilisateurRestrictionDto itemToSearch)
      {
          Expression<Func<ViewUtilisateurRestriction, object>> exp = null;

          if (itemToSearch.InfoSearchIdUtilisateurRestriction.IsOrderByField)
            exp = (o => o.IdUtilisateurRestriction);
          if (itemToSearch.InfoSearchIdUtilisateur.IsOrderByField)
            exp = (o => o.IdUtilisateur);
          if (itemToSearch.InfoSearchLogin.IsOrderByField)
            exp = (o => o.Login);
          if (itemToSearch.InfoSearchNom.IsOrderByField)
            exp = (o => o.Nom);
          if (itemToSearch.InfoSearchPrenom.IsOrderByField)
            exp = (o => o.Prenom);
          if (itemToSearch.InfoSearchIdSysObjet.IsOrderByField)
            exp = (o => o.IdSysObjet);
          if (itemToSearch.InfoSearchTypeObjet.IsOrderByField)
            exp = (o => o.TypeObjet);
          if (itemToSearch.InfoSearchCode.IsOrderByField)
            exp = (o => o.Code);
          if (itemToSearch.InfoSearchLibelle.IsOrderByField)
            exp = (o => o.Libelle);
          if (itemToSearch.InfoSearchDescription.IsOrderByField)
            exp = (o => o.Description);
          if (itemToSearch.InfoSearchAutorise.IsOrderByField)
            exp = (o => o.Autorise);
          if (itemToSearch.InfoSearchDateCreation.IsOrderByField)
            exp = (o => o.DateCreation);
          if (itemToSearch.InfoSearchCreatedBy.IsOrderByField)
            exp = (o => o.CreatedBy);

          return exp ?? (obj => obj.IdUtilisateurRestriction);
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private static Expression<Func<ViewUtilisateurRestriction, decimal?>> GenerateSumExpression(ViewUtilisateurRestrictionDto itemToSearch)
      {
          Expression<Func<ViewUtilisateurRestriction, decimal?>> exp = null;

          if (itemToSearch.InfoSearchCreatedBy.IsSumField)
            exp = (o => o.CreatedBy);                                         
  
          //return exp ?? (obj => obj.IdUtilisateurRestriction);
          return exp;
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private ViewUtilisateurRestrictionDto GenerateSum(Expression<Func<ViewUtilisateurRestriction, bool>> criteria, ViewUtilisateurRestrictionDto itemToSearch, ViewUtilisateurRestrictionDto itemToReturn)
      {
          if (itemToSearch.InfoSearchCreatedBy.IsSumField)
            itemToReturn.InfoSearchCreatedBy.Sum = _repositoryViewUtilisateurRestriction.GetSum(criteria, o => o.CreatedBy);
                                        
  
          return itemToReturn;
      }
  }
}

