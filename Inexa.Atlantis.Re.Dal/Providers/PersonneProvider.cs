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
  public class PersonneProvider
  {

      #region Singleton
      
      private static PersonneProvider _instance;

      public static PersonneProvider Instance
      {
          get { return _instance ?? (_instance = new PersonneProvider()); }
      }

      #endregion

      private readonly IRepository<Personne> _repositoryPersonne;
     
      public  PersonneProvider()
      {
          _repositoryPersonne = new Repository<Personne>();
      }

      private PersonneProvider(IRepository<Personne> repository)
      {    
          _repositoryPersonne = repository;
      }

      public async Task<BusinessResponse<PersonneDto>> GetPersonneById(object id)
      {
          var response = new BusinessResponse<PersonneDto>();

          try
          {
              var item = _repositoryPersonne[id];
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
      public async Task<BusinessResponse<PersonneDto>> GetPersonnesByCriteria(BusinessRequest<PersonneDto> request)
      {
          var response = new BusinessResponse<PersonneDto>();

          try
          {
              request.ToFinalRequest();

              // générer expression lambda de l'objet de recherche principal
              var mainexp = GenerateCriteria(request.ItemToSearch);                  
              mainexp = (mainexp == null) ? obj => obj.IsDeleted == false : mainexp.And(obj => obj.IsDeleted == false);                   

              // générer expression lambda des objets de recherche sécondaires
              if (request.ItemsToSearch == null) 
                request.ItemsToSearch = new List<PersonneDto>();                                                    
 
              var exps = new List<Expression<Func<Personne, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<Personne, bool>> exp = null;                  
              foreach (var expi in exps)
              {
                  if(request.NotIn)
                      exp = (exp == null) ? expi : exp.And(expi);
                  else
                      exp = (exp == null) ? expi : exp.Or(expi); 
              }

                  // concaténer les critères principaux et sécondaires
                  exp = (exp == null) ? mainexp : mainexp.And(exp);                  
                  
                  Expression<Func<Personne, object>> orderByExp = GenerateOrderByExpression(request.ItemToSearch);
                  if (string.IsNullOrEmpty(request.SortOrder) || (request.SortOrder != MySortOrder.Ascending && request.SortOrder != MySortOrder.Descending))
                    request.SortOrder = MySortOrder.Descending;

                  int totalRecord;
                  IEnumerable<Personne> items = _repositoryPersonne.GetMany(exp, request.Index, request.Size, out totalRecord, orderByExp, request.SortOrder);
  
                  response.Count = totalRecord;
                  items = items as IList<Personne> ?? items.ToList();
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
      public BusinessResponse<decimal> GetSumPersonnesByCriteria(BusinessRequest<PersonneDto> request)
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
                request.ItemsToSearch = new List<PersonneDto>();                                                    
 
              var exps = new List<Expression<Func<Personne, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<Personne, bool>> exp = null;                  
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

                  var sum = _repositoryPersonne.GetSum(exp, sumExp);
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
  		public async Task<BusinessResponse<PersonneDto>> SavePersonnes(BusinessRequest<PersonneDto> request)
  		{
        var response = new BusinessResponse<PersonneDto>();
  
              try
              {
                  foreach (var item in request.ItemsToSave.TakeWhile(BusinessRequest<PersonneDto>.ValidateRequiredProperties))
                  {
                     var itemToSave = item.ToEntity();
                     Personne itemSaved = null;

                     itemToSave.DateMaj = Utilities.CurrentDate; 
                     itemToSave.ModifiedBy = request.IdCurrentUser; 
                     if (itemToSave.IdPersonne == 0)  
                     {  
                        itemToSave.DateCreation = Utilities.CurrentDate;   
                        itemToSave.CreatedBy = request.IdCurrentUser;   

                        itemToSave.IsDeleted = false;
                        itemSaved = _repositoryPersonne.Add(itemToSave);
                     }
                     else //dans le cas d'une modification
                     {
                         itemSaved = _repositoryPersonne.Update(itemToSave, p => p.IdPersonne == itemToSave.IdPersonne);
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
      private static Expression<Func<Personne, bool>> GenerateCriteria(PersonneDto itemToSearch) 
      {
          Expression<Func<Personne, bool>> exprinit = obj => true;
          Expression<Func<Personne, bool>> expi = exprinit;
                                                             
          if (itemToSearch.InfoSearchIdPersonne.Consider)
          {
              switch (itemToSearch.InfoSearchIdPersonne.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdPersonne != itemToSearch.IdPersonne);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdPersonne < itemToSearch.IdPersonne);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdPersonne <= itemToSearch.IdPersonne);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdPersonne > itemToSearch.IdPersonne);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdPersonne >= itemToSearch.IdPersonne);                  
                      break;  
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdPersonne == itemToSearch.IdPersonne);                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchTypePersonne.Consider)
          {
              switch (itemToSearch.InfoSearchTypePersonne.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.TypePersonne != itemToSearch.TypePersonne);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.TypePersonne.StartsWith(itemToSearch.TypePersonne));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.TypePersonne.EndsWith(itemToSearch.TypePersonne));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.TypePersonne == itemToSearch.TypePersonne);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.TypePersonne.Contains(itemToSearch.TypePersonne));
                      break;
              }
          }                               
                 
          if (itemToSearch.InfoSearchIdCivilite.Consider)
          {
              switch (itemToSearch.InfoSearchIdCivilite.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdCivilite != itemToSearch.IdCivilite);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.IdCivilite.StartsWith(itemToSearch.IdCivilite));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.IdCivilite.EndsWith(itemToSearch.IdCivilite));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.IdCivilite == itemToSearch.IdCivilite);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.IdCivilite.Contains(itemToSearch.IdCivilite));
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
                 
          if (itemToSearch.InfoSearchDateNaissance.Consider)
          {
              switch (itemToSearch.InfoSearchDateNaissance.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And
                            (obj => 
                                !(obj.DateNaissance.Value.Year == itemToSearch.DateNaissance.Value.Year
                                && obj.DateNaissance.Value.Month == itemToSearch.DateNaissance.Value.Month
                                && obj.DateNaissance.Value.Day == itemToSearch.DateNaissance.Value.Day)
                             );
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateNaissance.Value, itemToSearch.DateNaissance.Value) < 0);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateNaissance.Value, itemToSearch.DateNaissance.Value) <= 0);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateNaissance.Value, itemToSearch.DateNaissance.Value) > 0);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateNaissance.Value, itemToSearch.DateNaissance.Value) >= 0);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      var debut = itemToSearch.InfoSearchDateNaissance.Intervalle.Debut.Date;
                      var fin = itemToSearch.InfoSearchDateNaissance.Intervalle.Fin.AddDays(1).Date;
                      expi = expi.And(obj => obj.DateNaissance >= debut && obj.DateNaissance < fin);                  
                      break;         
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And
                            (
                              obj => obj.DateNaissance.Value.Year == itemToSearch.DateNaissance.Value.Year
                              && obj.DateNaissance.Value.Month == itemToSearch.DateNaissance.Value.Month
                              && obj.DateNaissance.Value.Day == itemToSearch.DateNaissance.Value.Day
                             );                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchLieuNaissance.Consider)
          {
              switch (itemToSearch.InfoSearchLieuNaissance.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.LieuNaissance != itemToSearch.LieuNaissance);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.LieuNaissance.StartsWith(itemToSearch.LieuNaissance));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.LieuNaissance.EndsWith(itemToSearch.LieuNaissance));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.LieuNaissance == itemToSearch.LieuNaissance);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.LieuNaissance.Contains(itemToSearch.LieuNaissance));
                      break;
              }
          }                               
                 
          if (itemToSearch.InfoSearchIdTypePieceIdentite.Consider)
          {
              switch (itemToSearch.InfoSearchIdTypePieceIdentite.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdTypePieceIdentite != itemToSearch.IdTypePieceIdentite);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdTypePieceIdentite < itemToSearch.IdTypePieceIdentite);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdTypePieceIdentite <= itemToSearch.IdTypePieceIdentite);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdTypePieceIdentite > itemToSearch.IdTypePieceIdentite);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdTypePieceIdentite >= itemToSearch.IdTypePieceIdentite);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.IdTypePieceIdentite >= itemToSearch.InfoSearchIdTypePieceIdentite.Intervalle.Debut && obj.IdTypePieceIdentite <= itemToSearch.InfoSearchIdTypePieceIdentite.Intervalle.Fin);                         
                      break;    
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdTypePieceIdentite == itemToSearch.IdTypePieceIdentite);                  
                      break;
              }
          }            

          if (itemToSearch.InfoSearchNumeroPieceIdentite.Consider)
          {
              switch (itemToSearch.InfoSearchNumeroPieceIdentite.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.NumeroPieceIdentite != itemToSearch.NumeroPieceIdentite);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.NumeroPieceIdentite.StartsWith(itemToSearch.NumeroPieceIdentite));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.NumeroPieceIdentite.EndsWith(itemToSearch.NumeroPieceIdentite));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.NumeroPieceIdentite == itemToSearch.NumeroPieceIdentite);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.NumeroPieceIdentite.Contains(itemToSearch.NumeroPieceIdentite));
                      break;
              }
          }                               
                 
          if (itemToSearch.InfoSearchIdPays.Consider)
          {
              switch (itemToSearch.InfoSearchIdPays.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdPays != itemToSearch.IdPays);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdPays < itemToSearch.IdPays);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdPays <= itemToSearch.IdPays);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdPays > itemToSearch.IdPays);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdPays >= itemToSearch.IdPays);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.IdPays >= itemToSearch.InfoSearchIdPays.Intervalle.Debut && obj.IdPays <= itemToSearch.InfoSearchIdPays.Intervalle.Fin);                         
                      break;    
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdPays == itemToSearch.IdPays);                  
                      break;
              }
          }            

          if (itemToSearch.InfoSearchDatePieceIdentite.Consider)
          {
              switch (itemToSearch.InfoSearchDatePieceIdentite.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And
                            (obj => 
                                !(obj.DatePieceIdentite.Value.Year == itemToSearch.DatePieceIdentite.Value.Year
                                && obj.DatePieceIdentite.Value.Month == itemToSearch.DatePieceIdentite.Value.Month
                                && obj.DatePieceIdentite.Value.Day == itemToSearch.DatePieceIdentite.Value.Day)
                             );
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DatePieceIdentite.Value, itemToSearch.DatePieceIdentite.Value) < 0);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DatePieceIdentite.Value, itemToSearch.DatePieceIdentite.Value) <= 0);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DatePieceIdentite.Value, itemToSearch.DatePieceIdentite.Value) > 0);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DatePieceIdentite.Value, itemToSearch.DatePieceIdentite.Value) >= 0);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      var debut = itemToSearch.InfoSearchDatePieceIdentite.Intervalle.Debut.Date;
                      var fin = itemToSearch.InfoSearchDatePieceIdentite.Intervalle.Fin.AddDays(1).Date;
                      expi = expi.And(obj => obj.DatePieceIdentite >= debut && obj.DatePieceIdentite < fin);                  
                      break;         
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And
                            (
                              obj => obj.DatePieceIdentite.Value.Year == itemToSearch.DatePieceIdentite.Value.Year
                              && obj.DatePieceIdentite.Value.Month == itemToSearch.DatePieceIdentite.Value.Month
                              && obj.DatePieceIdentite.Value.Day == itemToSearch.DatePieceIdentite.Value.Day
                             );                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchDateValiditePieceIdentite.Consider)
          {
              switch (itemToSearch.InfoSearchDateValiditePieceIdentite.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And
                            (obj => 
                                !(obj.DateValiditePieceIdentite.Value.Year == itemToSearch.DateValiditePieceIdentite.Value.Year
                                && obj.DateValiditePieceIdentite.Value.Month == itemToSearch.DateValiditePieceIdentite.Value.Month
                                && obj.DateValiditePieceIdentite.Value.Day == itemToSearch.DateValiditePieceIdentite.Value.Day)
                             );
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateValiditePieceIdentite.Value, itemToSearch.DateValiditePieceIdentite.Value) < 0);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateValiditePieceIdentite.Value, itemToSearch.DateValiditePieceIdentite.Value) <= 0);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateValiditePieceIdentite.Value, itemToSearch.DateValiditePieceIdentite.Value) > 0);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateValiditePieceIdentite.Value, itemToSearch.DateValiditePieceIdentite.Value) >= 0);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      var debut = itemToSearch.InfoSearchDateValiditePieceIdentite.Intervalle.Debut.Date;
                      var fin = itemToSearch.InfoSearchDateValiditePieceIdentite.Intervalle.Fin.AddDays(1).Date;
                      expi = expi.And(obj => obj.DateValiditePieceIdentite >= debut && obj.DateValiditePieceIdentite < fin);                  
                      break;         
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And
                            (
                              obj => obj.DateValiditePieceIdentite.Value.Year == itemToSearch.DateValiditePieceIdentite.Value.Year
                              && obj.DateValiditePieceIdentite.Value.Month == itemToSearch.DateValiditePieceIdentite.Value.Month
                              && obj.DateValiditePieceIdentite.Value.Day == itemToSearch.DateValiditePieceIdentite.Value.Day
                             );                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchIdFormeJuridique.Consider)
          {
              switch (itemToSearch.InfoSearchIdFormeJuridique.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdFormeJuridique != itemToSearch.IdFormeJuridique);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdFormeJuridique < itemToSearch.IdFormeJuridique);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdFormeJuridique <= itemToSearch.IdFormeJuridique);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdFormeJuridique > itemToSearch.IdFormeJuridique);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdFormeJuridique >= itemToSearch.IdFormeJuridique);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.IdFormeJuridique >= itemToSearch.InfoSearchIdFormeJuridique.Intervalle.Debut && obj.IdFormeJuridique <= itemToSearch.InfoSearchIdFormeJuridique.Intervalle.Fin);                         
                      break;    
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdFormeJuridique == itemToSearch.IdFormeJuridique);                  
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

          return expi == exprinit ? null : expi;
      }

      /// <summary>
        /// Générer une expression lambda à partir d'un DTO
        /// </summary>
        /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
        /// <returns>Expression lambda</returns>
      private static Expression<Func<Personne, object>> GenerateOrderByExpression(PersonneDto itemToSearch)
      {
          Expression<Func<Personne, object>> exp = null;

          if (itemToSearch.InfoSearchIdPersonne.IsOrderByField)
            exp = (o => o.IdPersonne);
          if (itemToSearch.InfoSearchTypePersonne.IsOrderByField)
            exp = (o => o.TypePersonne);
          if (itemToSearch.InfoSearchIdCivilite.IsOrderByField)
            exp = (o => o.IdCivilite);
          if (itemToSearch.InfoSearchNom.IsOrderByField)
            exp = (o => o.Nom);
          if (itemToSearch.InfoSearchPrenom.IsOrderByField)
            exp = (o => o.Prenom);
          if (itemToSearch.InfoSearchDateNaissance.IsOrderByField)
            exp = (o => o.DateNaissance);
          if (itemToSearch.InfoSearchLieuNaissance.IsOrderByField)
            exp = (o => o.LieuNaissance);
          if (itemToSearch.InfoSearchIdTypePieceIdentite.IsOrderByField)
            exp = (o => o.IdTypePieceIdentite);
          if (itemToSearch.InfoSearchNumeroPieceIdentite.IsOrderByField)
            exp = (o => o.NumeroPieceIdentite);
          if (itemToSearch.InfoSearchIdPays.IsOrderByField)
            exp = (o => o.IdPays);
          if (itemToSearch.InfoSearchDatePieceIdentite.IsOrderByField)
            exp = (o => o.DatePieceIdentite);
          if (itemToSearch.InfoSearchDateValiditePieceIdentite.IsOrderByField)
            exp = (o => o.DateValiditePieceIdentite);
          if (itemToSearch.InfoSearchIdFormeJuridique.IsOrderByField)
            exp = (o => o.IdFormeJuridique);
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
          if (itemToSearch.InfoSearchDataKey.IsOrderByField)
            exp = (o => o.DataKey);
          if (itemToSearch.InfoSearchModifiedBy.IsOrderByField)
            exp = (o => o.ModifiedBy);

          return exp ?? (obj => obj.IdPersonne);
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private static Expression<Func<Personne, decimal?>> GenerateSumExpression(PersonneDto itemToSearch)
      {
          Expression<Func<Personne, decimal?>> exp = null;

          if (itemToSearch.InfoSearchCreatedBy.IsSumField)
            exp = (o => o.CreatedBy);                                         
            if (itemToSearch.InfoSearchModifiedBy.IsSumField)
            exp = (o => o.ModifiedBy.Value);                     
  
          //return exp ?? (obj => obj.IdPersonne);
          return exp;
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private PersonneDto GenerateSum(Expression<Func<Personne, bool>> criteria, PersonneDto itemToSearch, PersonneDto itemToReturn)
      {
          if (itemToSearch.InfoSearchCreatedBy.IsSumField)
            itemToReturn.InfoSearchCreatedBy.Sum = _repositoryPersonne.GetSum(criteria, o => o.CreatedBy);
                                        
            if (itemToSearch.InfoSearchModifiedBy.IsSumField)
            itemToReturn.InfoSearchModifiedBy.Sum = _repositoryPersonne.GetSum(criteria, o => o.ModifiedBy.Value);     
               
  
          return itemToReturn;
      }
  }
}

