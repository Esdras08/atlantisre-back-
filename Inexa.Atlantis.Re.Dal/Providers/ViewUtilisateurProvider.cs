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
  public class ViewUtilisateurProvider
  {

      #region Singleton
      
      private static ViewUtilisateurProvider _instance;

      public static ViewUtilisateurProvider Instance
      {
          get { return _instance ?? (_instance = new ViewUtilisateurProvider()); }
      }

      #endregion

      private readonly IRepository<ViewUtilisateur> _repositoryViewUtilisateur;
     
      public  ViewUtilisateurProvider()
      {
          _repositoryViewUtilisateur = new Repository<ViewUtilisateur>();
      }

      private ViewUtilisateurProvider(IRepository<ViewUtilisateur> repository)
      {    
          _repositoryViewUtilisateur = repository;
      }

      /// <summary>
      /// Recupere les données de la table
      /// </summary>
      /// <param name="request">La requete contenant les criteres de recherche</param>
      /// <returns></returns>
      public async Task<BusinessResponse<ViewUtilisateurDto>> GetViewUtilisateursByCriteria(BusinessRequest<ViewUtilisateurDto> request)
      {
          var response = new BusinessResponse<ViewUtilisateurDto>();

          try
          {
              request.ToFinalRequest();

              // générer expression lambda de l'objet de recherche principal
              var mainexp = GenerateCriteria(request.ItemToSearch);                  
              mainexp = (mainexp == null) ? obj => obj.IsDeleted == false : mainexp.And(obj => obj.IsDeleted == false);                   
			  // rechercher en fonction de la structure courante 
              if (request.CanFilterByStruct)
                mainexp = (mainexp == null) ? obj => obj.IdStructure == request.IdCurrentStructure : mainexp.And(obj => obj.IdStructure == request.IdCurrentStructure);                   

              // générer expression lambda des objets de recherche sécondaires
              if (request.ItemsToSearch == null) 
                request.ItemsToSearch = new List<ViewUtilisateurDto>();                                                    
 
              var exps = new List<Expression<Func<ViewUtilisateur, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<ViewUtilisateur, bool>> exp = null;                  
              foreach (var expi in exps)
              {
                  if(request.NotIn)
                      exp = (exp == null) ? expi : exp.And(expi);
                  else
                      exp = (exp == null) ? expi : exp.Or(expi); 
              }

                  // concaténer les critères principaux et sécondaires
                  exp = (exp == null) ? mainexp : mainexp.And(exp);                  
                  
                  Expression<Func<ViewUtilisateur, object>> orderByExp = GenerateOrderByExpression(request.ItemToSearch);
                  if (string.IsNullOrEmpty(request.SortOrder) || (request.SortOrder != MySortOrder.Ascending && request.SortOrder != MySortOrder.Descending))
                    request.SortOrder = MySortOrder.Descending;

                  int totalRecord;
                  IEnumerable<ViewUtilisateur> items = _repositoryViewUtilisateur.GetMany(exp, request.Index, request.Size, out totalRecord, orderByExp, request.SortOrder);
  
                  response.Count = totalRecord;
                  items = items as IList<ViewUtilisateur> ?? items.ToList();
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
      public BusinessResponse<decimal> GetSumViewUtilisateursByCriteria(BusinessRequest<ViewUtilisateurDto> request)
      {
          var response = new BusinessResponse<decimal>();

          try
          {
              request.ToFinalRequest();

              // générer expression lambda de l'objet de recherche principal
              var mainexp = GenerateCriteria(request.ItemToSearch);                  
              mainexp = (mainexp == null) ? obj => obj.IsDeleted == false : mainexp.And(obj => obj.IsDeleted == false);                   
			  // rechercher en fonction de la structure courante
              mainexp = (mainexp == null) ? obj => obj.IdStructure == request.IdCurrentStructure : mainexp.And(obj => obj.IdStructure == request.IdCurrentStructure);                   

              // générer expression lambda des objets de recherche sécondaires
              if (request.ItemsToSearch == null) 
                request.ItemsToSearch = new List<ViewUtilisateurDto>();                                                    
 
              var exps = new List<Expression<Func<ViewUtilisateur, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<ViewUtilisateur, bool>> exp = null;                  
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

                  var sum = _repositoryViewUtilisateur.GetSum(exp, sumExp);
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
      private static Expression<Func<ViewUtilisateur, bool>> GenerateCriteria(ViewUtilisateurDto itemToSearch) 
      {
          Expression<Func<ViewUtilisateur, bool>> exprinit = obj => true;
          Expression<Func<ViewUtilisateur, bool>> expi = exprinit;
                                                             
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
                 
          if (itemToSearch.InfoSearchPassword.Consider)
          {
              switch (itemToSearch.InfoSearchPassword.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.Password != itemToSearch.Password);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.Password.StartsWith(itemToSearch.Password));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.Password.EndsWith(itemToSearch.Password));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.Password == itemToSearch.Password);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.Password.Contains(itemToSearch.Password));
                      break;
              }
          }                               
                 
          if (itemToSearch.InfoSearchTimeSession.Consider)
          {
              switch (itemToSearch.InfoSearchTimeSession.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.TimeSession != itemToSearch.TimeSession);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.TimeSession < itemToSearch.TimeSession);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.TimeSession <= itemToSearch.TimeSession);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.TimeSession > itemToSearch.TimeSession);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.TimeSession >= itemToSearch.TimeSession);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.TimeSession >= itemToSearch.InfoSearchTimeSession.Intervalle.Debut && obj.TimeSession <= itemToSearch.InfoSearchTimeSession.Intervalle.Fin);                         
                      break;    
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.TimeSession == itemToSearch.TimeSession);                  
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
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.Actif.StartsWith(itemToSearch.Actif));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.Actif.EndsWith(itemToSearch.Actif));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.Actif == itemToSearch.Actif);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.Actif.Contains(itemToSearch.Actif));
                      break;
              }
          }                               
                 
          if (itemToSearch.InfoSearchIsWindowAccount.Consider)
          {
              switch (itemToSearch.InfoSearchIsWindowAccount.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IsWindowAccount != itemToSearch.IsWindowAccount);
                      break;                      
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IsWindowAccount == itemToSearch.IsWindowAccount);                  
                      break;
              }
          }            

                  if (itemToSearch.InfoSearchCanApplySingleSession.Consider)
          {
              switch (itemToSearch.InfoSearchCanApplySingleSession.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.CanApplySingleSession != itemToSearch.CanApplySingleSession);
                      break;                      
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.CanApplySingleSession == itemToSearch.CanApplySingleSession);                  
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

                  if (itemToSearch.InfoSearchDateMaj.Consider)
          {
              switch (itemToSearch.InfoSearchDateMaj.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And
                            (obj => 
                                !(obj.DateMaj.Value.Year == itemToSearch.DateMaj.Value.Year
                                && obj.DateMaj.Value.Month == itemToSearch.DateMaj.Value.Month
                                && obj.DateMaj.Value.Day == itemToSearch.DateMaj.Value.Day)
                             );
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateMaj.Value, itemToSearch.DateMaj.Value) < 0);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateMaj.Value, itemToSearch.DateMaj.Value) <= 0);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateMaj.Value, itemToSearch.DateMaj.Value) > 0);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateMaj.Value, itemToSearch.DateMaj.Value) >= 0);                  
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
                              obj => obj.DateMaj.Value.Year == itemToSearch.DateMaj.Value.Year
                              && obj.DateMaj.Value.Month == itemToSearch.DateMaj.Value.Month
                              && obj.DateMaj.Value.Day == itemToSearch.DateMaj.Value.Day
                             );                  
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

          if (itemToSearch.InfoSearchAdresse.Consider)
          {
              switch (itemToSearch.InfoSearchAdresse.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.Adresse != itemToSearch.Adresse);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.Adresse.StartsWith(itemToSearch.Adresse));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.Adresse.EndsWith(itemToSearch.Adresse));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.Adresse == itemToSearch.Adresse);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.Adresse.Contains(itemToSearch.Adresse));
                      break;
              }
          }                               
                 
          if (itemToSearch.InfoSearchEmail.Consider)
          {
              switch (itemToSearch.InfoSearchEmail.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.Email != itemToSearch.Email);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.Email.StartsWith(itemToSearch.Email));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.Email.EndsWith(itemToSearch.Email));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.Email == itemToSearch.Email);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.Email.Contains(itemToSearch.Email));
                      break;
              }
          }                               
                 
          if (itemToSearch.InfoSearchTelephone.Consider)
          {
              switch (itemToSearch.InfoSearchTelephone.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.Telephone != itemToSearch.Telephone);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.Telephone.StartsWith(itemToSearch.Telephone));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.Telephone.EndsWith(itemToSearch.Telephone));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.Telephone == itemToSearch.Telephone);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.Telephone.Contains(itemToSearch.Telephone));
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
                 
          if (itemToSearch.InfoSearchVoirCompteSensible.Consider)
          {
              switch (itemToSearch.InfoSearchVoirCompteSensible.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.VoirCompteSensible != itemToSearch.VoirCompteSensible);
                      break;                      
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.VoirCompteSensible == itemToSearch.VoirCompteSensible);                  
                      break;
              }
          }            

                  if (itemToSearch.InfoSearchIsOldUser.Consider)
          {
              switch (itemToSearch.InfoSearchIsOldUser.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IsOldUser != itemToSearch.IsOldUser);
                      break;                      
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IsOldUser == itemToSearch.IsOldUser);                  
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
                 
          if (itemToSearch.InfoSearchIsConnected.Consider)
          {
              switch (itemToSearch.InfoSearchIsConnected.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IsConnected != itemToSearch.IsConnected);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.IsConnected.StartsWith(itemToSearch.IsConnected));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.IsConnected.EndsWith(itemToSearch.IsConnected));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.IsConnected == itemToSearch.IsConnected);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.IsConnected.Contains(itemToSearch.IsConnected));
                      break;
              }
          }                               
                 
          if (itemToSearch.InfoSearchComptePermanent.Consider)
          {
              switch (itemToSearch.InfoSearchComptePermanent.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.ComptePermanent != itemToSearch.ComptePermanent);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.ComptePermanent.StartsWith(itemToSearch.ComptePermanent));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.ComptePermanent.EndsWith(itemToSearch.ComptePermanent));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.ComptePermanent == itemToSearch.ComptePermanent);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.ComptePermanent.Contains(itemToSearch.ComptePermanent));
                      break;
              }
          }                               
                 
          if (itemToSearch.InfoSearchDateDebutValidite.Consider)
          {
              switch (itemToSearch.InfoSearchDateDebutValidite.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And
                            (obj => 
                                !(obj.DateDebutValidite.Value.Year == itemToSearch.DateDebutValidite.Value.Year
                                && obj.DateDebutValidite.Value.Month == itemToSearch.DateDebutValidite.Value.Month
                                && obj.DateDebutValidite.Value.Day == itemToSearch.DateDebutValidite.Value.Day)
                             );
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateDebutValidite.Value, itemToSearch.DateDebutValidite.Value) < 0);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateDebutValidite.Value, itemToSearch.DateDebutValidite.Value) <= 0);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateDebutValidite.Value, itemToSearch.DateDebutValidite.Value) > 0);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateDebutValidite.Value, itemToSearch.DateDebutValidite.Value) >= 0);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      var debut = itemToSearch.InfoSearchDateDebutValidite.Intervalle.Debut.Date;
                      var fin = itemToSearch.InfoSearchDateDebutValidite.Intervalle.Fin.AddDays(1).Date;
                      expi = expi.And(obj => obj.DateDebutValidite >= debut && obj.DateDebutValidite < fin);                  
                      break;         
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And
                            (
                              obj => obj.DateDebutValidite.Value.Year == itemToSearch.DateDebutValidite.Value.Year
                              && obj.DateDebutValidite.Value.Month == itemToSearch.DateDebutValidite.Value.Month
                              && obj.DateDebutValidite.Value.Day == itemToSearch.DateDebutValidite.Value.Day
                             );                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchDateFinValidite.Consider)
          {
              switch (itemToSearch.InfoSearchDateFinValidite.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And
                            (obj => 
                                !(obj.DateFinValidite.Value.Year == itemToSearch.DateFinValidite.Value.Year
                                && obj.DateFinValidite.Value.Month == itemToSearch.DateFinValidite.Value.Month
                                && obj.DateFinValidite.Value.Day == itemToSearch.DateFinValidite.Value.Day)
                             );
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateFinValidite.Value, itemToSearch.DateFinValidite.Value) < 0);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateFinValidite.Value, itemToSearch.DateFinValidite.Value) <= 0);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateFinValidite.Value, itemToSearch.DateFinValidite.Value) > 0);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateFinValidite.Value, itemToSearch.DateFinValidite.Value) >= 0);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      var debut = itemToSearch.InfoSearchDateFinValidite.Intervalle.Debut.Date;
                      var fin = itemToSearch.InfoSearchDateFinValidite.Intervalle.Fin.AddDays(1).Date;
                      expi = expi.And(obj => obj.DateFinValidite >= debut && obj.DateFinValidite < fin);                  
                      break;         
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And
                            (
                              obj => obj.DateFinValidite.Value.Year == itemToSearch.DateFinValidite.Value.Year
                              && obj.DateFinValidite.Value.Month == itemToSearch.DateFinValidite.Value.Month
                              && obj.DateFinValidite.Value.Day == itemToSearch.DateFinValidite.Value.Day
                             );                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchChangeLogin.Consider)
          {
              switch (itemToSearch.InfoSearchChangeLogin.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.ChangeLogin != itemToSearch.ChangeLogin);
                      break;                      
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.ChangeLogin == itemToSearch.ChangeLogin);                  
                      break;
              }
          }            

                  if (itemToSearch.InfoSearchChangePassword.Consider)
          {
              switch (itemToSearch.InfoSearchChangePassword.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.ChangePassword != itemToSearch.ChangePassword);
                      break;                      
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.ChangePassword == itemToSearch.ChangePassword);                  
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
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdProfil == itemToSearch.IdProfil);                  
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

          if (itemToSearch.InfoSearchRecevoirMail.Consider)
          {
              switch (itemToSearch.InfoSearchRecevoirMail.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.RecevoirMail != itemToSearch.RecevoirMail);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.RecevoirMail.StartsWith(itemToSearch.RecevoirMail));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.RecevoirMail.EndsWith(itemToSearch.RecevoirMail));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.RecevoirMail == itemToSearch.RecevoirMail);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.RecevoirMail.Contains(itemToSearch.RecevoirMail));
                      break;
              }
          }                               
                 
          if (itemToSearch.InfoSearchRecevoirMailSysteme.Consider)
          {
              switch (itemToSearch.InfoSearchRecevoirMailSysteme.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.RecevoirMailSysteme != itemToSearch.RecevoirMailSysteme);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.RecevoirMailSysteme.StartsWith(itemToSearch.RecevoirMailSysteme));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.RecevoirMailSysteme.EndsWith(itemToSearch.RecevoirMailSysteme));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.RecevoirMailSysteme == itemToSearch.RecevoirMailSysteme);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.RecevoirMailSysteme.Contains(itemToSearch.RecevoirMailSysteme));
                      break;
              }
          }                               
                 
          if (itemToSearch.InfoSearchTentativeConnexion.Consider)
          {
              switch (itemToSearch.InfoSearchTentativeConnexion.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.TentativeConnexion != itemToSearch.TentativeConnexion);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.TentativeConnexion < itemToSearch.TentativeConnexion);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.TentativeConnexion <= itemToSearch.TentativeConnexion);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.TentativeConnexion > itemToSearch.TentativeConnexion);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.TentativeConnexion >= itemToSearch.TentativeConnexion);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.TentativeConnexion >= itemToSearch.InfoSearchTentativeConnexion.Intervalle.Debut && obj.TentativeConnexion <= itemToSearch.InfoSearchTentativeConnexion.Intervalle.Fin);                         
                      break;    
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.TentativeConnexion == itemToSearch.TentativeConnexion);                  
                      break;
              }
          }            

          if (itemToSearch.InfoSearchDateDebutValiditePassword.Consider)
          {
              switch (itemToSearch.InfoSearchDateDebutValiditePassword.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And
                            (obj => 
                                !(obj.DateDebutValiditePassword.Value.Year == itemToSearch.DateDebutValiditePassword.Value.Year
                                && obj.DateDebutValiditePassword.Value.Month == itemToSearch.DateDebutValiditePassword.Value.Month
                                && obj.DateDebutValiditePassword.Value.Day == itemToSearch.DateDebutValiditePassword.Value.Day)
                             );
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateDebutValiditePassword.Value, itemToSearch.DateDebutValiditePassword.Value) < 0);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateDebutValiditePassword.Value, itemToSearch.DateDebutValiditePassword.Value) <= 0);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateDebutValiditePassword.Value, itemToSearch.DateDebutValiditePassword.Value) > 0);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateDebutValiditePassword.Value, itemToSearch.DateDebutValiditePassword.Value) >= 0);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      var debut = itemToSearch.InfoSearchDateDebutValiditePassword.Intervalle.Debut.Date;
                      var fin = itemToSearch.InfoSearchDateDebutValiditePassword.Intervalle.Fin.AddDays(1).Date;
                      expi = expi.And(obj => obj.DateDebutValiditePassword >= debut && obj.DateDebutValiditePassword < fin);                  
                      break;         
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And
                            (
                              obj => obj.DateDebutValiditePassword.Value.Year == itemToSearch.DateDebutValiditePassword.Value.Year
                              && obj.DateDebutValiditePassword.Value.Month == itemToSearch.DateDebutValiditePassword.Value.Month
                              && obj.DateDebutValiditePassword.Value.Day == itemToSearch.DateDebutValiditePassword.Value.Day
                             );                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchDateFinValiditePassword.Consider)
          {
              switch (itemToSearch.InfoSearchDateFinValiditePassword.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And
                            (obj => 
                                !(obj.DateFinValiditePassword.Value.Year == itemToSearch.DateFinValiditePassword.Value.Year
                                && obj.DateFinValiditePassword.Value.Month == itemToSearch.DateFinValiditePassword.Value.Month
                                && obj.DateFinValiditePassword.Value.Day == itemToSearch.DateFinValiditePassword.Value.Day)
                             );
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateFinValiditePassword.Value, itemToSearch.DateFinValiditePassword.Value) < 0);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateFinValiditePassword.Value, itemToSearch.DateFinValiditePassword.Value) <= 0);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateFinValiditePassword.Value, itemToSearch.DateFinValiditePassword.Value) > 0);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateFinValiditePassword.Value, itemToSearch.DateFinValiditePassword.Value) >= 0);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      var debut = itemToSearch.InfoSearchDateFinValiditePassword.Intervalle.Debut.Date;
                      var fin = itemToSearch.InfoSearchDateFinValiditePassword.Intervalle.Fin.AddDays(1).Date;
                      expi = expi.And(obj => obj.DateFinValiditePassword >= debut && obj.DateFinValiditePassword < fin);                  
                      break;         
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And
                            (
                              obj => obj.DateFinValiditePassword.Value.Year == itemToSearch.DateFinValiditePassword.Value.Year
                              && obj.DateFinValiditePassword.Value.Month == itemToSearch.DateFinValiditePassword.Value.Month
                              && obj.DateFinValiditePassword.Value.Day == itemToSearch.DateFinValiditePassword.Value.Day
                             );                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchUserSageCode.Consider)
          {
              switch (itemToSearch.InfoSearchUserSageCode.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.UserSageCode != itemToSearch.UserSageCode);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.UserSageCode.StartsWith(itemToSearch.UserSageCode));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.UserSageCode.EndsWith(itemToSearch.UserSageCode));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.UserSageCode == itemToSearch.UserSageCode);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.UserSageCode.Contains(itemToSearch.UserSageCode));
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
      private static Expression<Func<ViewUtilisateur, object>> GenerateOrderByExpression(ViewUtilisateurDto itemToSearch)
      {
          Expression<Func<ViewUtilisateur, object>> exp = null;

          if (itemToSearch.InfoSearchIdUtilisateur.IsOrderByField)
            exp = (o => o.IdUtilisateur);
          if (itemToSearch.InfoSearchLogin.IsOrderByField)
            exp = (o => o.Login);
          if (itemToSearch.InfoSearchPassword.IsOrderByField)
            exp = (o => o.Password);
          if (itemToSearch.InfoSearchTimeSession.IsOrderByField)
            exp = (o => o.TimeSession);
          if (itemToSearch.InfoSearchActif.IsOrderByField)
            exp = (o => o.Actif);
          if (itemToSearch.InfoSearchIsWindowAccount.IsOrderByField)
            exp = (o => o.IsWindowAccount);
          if (itemToSearch.InfoSearchCanApplySingleSession.IsOrderByField)
            exp = (o => o.CanApplySingleSession);
          if (itemToSearch.InfoSearchIsDeleted.IsOrderByField)
            exp = (o => o.IsDeleted);
          if (itemToSearch.InfoSearchDateMaj.IsOrderByField)
            exp = (o => o.DateMaj);
          if (itemToSearch.InfoSearchDateCreation.IsOrderByField)
            exp = (o => o.DateCreation);
          if (itemToSearch.InfoSearchCreatedBy.IsOrderByField)
            exp = (o => o.CreatedBy);
          if (itemToSearch.InfoSearchModifiedBy.IsOrderByField)
            exp = (o => o.ModifiedBy);
          if (itemToSearch.InfoSearchAdresse.IsOrderByField)
            exp = (o => o.Adresse);
          if (itemToSearch.InfoSearchEmail.IsOrderByField)
            exp = (o => o.Email);
          if (itemToSearch.InfoSearchTelephone.IsOrderByField)
            exp = (o => o.Telephone);
          if (itemToSearch.InfoSearchNomUtilisateur.IsOrderByField)
            exp = (o => o.NomUtilisateur);
          if (itemToSearch.InfoSearchPhoto.IsOrderByField)
            exp = (o => o.Photo);
          if (itemToSearch.InfoSearchVoirCompteSensible.IsOrderByField)
            exp = (o => o.VoirCompteSensible);
          if (itemToSearch.InfoSearchIsOldUser.IsOrderByField)
            exp = (o => o.IsOldUser);
          if (itemToSearch.InfoSearchNom.IsOrderByField)
            exp = (o => o.Nom);
          if (itemToSearch.InfoSearchPrenom.IsOrderByField)
            exp = (o => o.Prenom);
          if (itemToSearch.InfoSearchIsConnected.IsOrderByField)
            exp = (o => o.IsConnected);
          if (itemToSearch.InfoSearchComptePermanent.IsOrderByField)
            exp = (o => o.ComptePermanent);
          if (itemToSearch.InfoSearchDateDebutValidite.IsOrderByField)
            exp = (o => o.DateDebutValidite);
          if (itemToSearch.InfoSearchDateFinValidite.IsOrderByField)
            exp = (o => o.DateFinValidite);
          if (itemToSearch.InfoSearchChangeLogin.IsOrderByField)
            exp = (o => o.ChangeLogin);
          if (itemToSearch.InfoSearchChangePassword.IsOrderByField)
            exp = (o => o.ChangePassword);
          if (itemToSearch.InfoSearchIdProfil.IsOrderByField)
            exp = (o => o.IdProfil);
          if (itemToSearch.InfoSearchIdStructure.IsOrderByField)
            exp = (o => o.IdStructure);
          if (itemToSearch.InfoSearchRecevoirMail.IsOrderByField)
            exp = (o => o.RecevoirMail);
          if (itemToSearch.InfoSearchRecevoirMailSysteme.IsOrderByField)
            exp = (o => o.RecevoirMailSysteme);
          if (itemToSearch.InfoSearchTentativeConnexion.IsOrderByField)
            exp = (o => o.TentativeConnexion);
          if (itemToSearch.InfoSearchDateDebutValiditePassword.IsOrderByField)
            exp = (o => o.DateDebutValiditePassword);
          if (itemToSearch.InfoSearchDateFinValiditePassword.IsOrderByField)
            exp = (o => o.DateFinValiditePassword);
          if (itemToSearch.InfoSearchUserSageCode.IsOrderByField)
            exp = (o => o.UserSageCode);

          return exp ?? (obj => obj.IdUtilisateur);
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private static Expression<Func<ViewUtilisateur, decimal?>> GenerateSumExpression(ViewUtilisateurDto itemToSearch)
      {
          Expression<Func<ViewUtilisateur, decimal?>> exp = null;

          if (itemToSearch.InfoSearchTimeSession.IsSumField)
            exp = (o => o.TimeSession.Value);                     
            if (itemToSearch.InfoSearchCreatedBy.IsSumField)
            exp = (o => o.CreatedBy);                                         
            if (itemToSearch.InfoSearchModifiedBy.IsSumField)
            exp = (o => o.ModifiedBy.Value);                     
            if (itemToSearch.InfoSearchTentativeConnexion.IsSumField)
            exp = (o => o.TentativeConnexion.Value);                     
  
          //return exp ?? (obj => obj.IdUtilisateur);
          return exp;
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private ViewUtilisateurDto GenerateSum(Expression<Func<ViewUtilisateur, bool>> criteria, ViewUtilisateurDto itemToSearch, ViewUtilisateurDto itemToReturn)
      {
          if (itemToSearch.InfoSearchTimeSession.IsSumField)
            itemToReturn.InfoSearchTimeSession.Sum = _repositoryViewUtilisateur.GetSum(criteria, o => o.TimeSession.Value);     
               
            if (itemToSearch.InfoSearchCreatedBy.IsSumField)
            itemToReturn.InfoSearchCreatedBy.Sum = _repositoryViewUtilisateur.GetSum(criteria, o => o.CreatedBy);
                                        
            if (itemToSearch.InfoSearchModifiedBy.IsSumField)
            itemToReturn.InfoSearchModifiedBy.Sum = _repositoryViewUtilisateur.GetSum(criteria, o => o.ModifiedBy.Value);     
               
            if (itemToSearch.InfoSearchTentativeConnexion.IsSumField)
            itemToReturn.InfoSearchTentativeConnexion.Sum = _repositoryViewUtilisateur.GetSum(criteria, o => o.TentativeConnexion.Value);     
               
  
          return itemToReturn;
      }
  }
}

