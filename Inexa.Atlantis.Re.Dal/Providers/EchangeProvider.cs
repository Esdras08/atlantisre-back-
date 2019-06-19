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
  public class EchangeProvider
  {

      #region Singleton
      
      private static EchangeProvider _instance;

      public static EchangeProvider Instance
      {
          get { return _instance ?? (_instance = new EchangeProvider()); }
      }

      #endregion

      private readonly IRepository<Echange> _repositoryEchange;
     
      public  EchangeProvider()
      {
          _repositoryEchange = new Repository<Echange>();
      }

      private EchangeProvider(IRepository<Echange> repository)
      {    
          _repositoryEchange = repository;
      }

      public async Task<BusinessResponse<EchangeDto>> GetEchangeById(object id)
      {
          var response = new BusinessResponse<EchangeDto>();

          try
          {
              var item = _repositoryEchange[id];
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
      public async Task<BusinessResponse<EchangeDto>> GetEchangesByCriteria(BusinessRequest<EchangeDto> request)
      {
          var response = new BusinessResponse<EchangeDto>();

          try
          {
              request.ToFinalRequest();

              // générer expression lambda de l'objet de recherche principal
              var mainexp = GenerateCriteria(request.ItemToSearch);                  
              mainexp = (mainexp == null) ? obj => obj.IsDeleted == false : mainexp.And(obj => obj.IsDeleted == false);                   

              // générer expression lambda des objets de recherche sécondaires
              if (request.ItemsToSearch == null) 
                request.ItemsToSearch = new List<EchangeDto>();                                                    
 
              var exps = new List<Expression<Func<Echange, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<Echange, bool>> exp = null;                  
              foreach (var expi in exps)
              {
                  if(request.NotIn)
                      exp = (exp == null) ? expi : exp.And(expi);
                  else
                      exp = (exp == null) ? expi : exp.Or(expi); 
              }

                  // concaténer les critères principaux et sécondaires
                  exp = (exp == null) ? mainexp : mainexp.And(exp);                  
                  
                  Expression<Func<Echange, object>> orderByExp = GenerateOrderByExpression(request.ItemToSearch);
                  if (string.IsNullOrEmpty(request.SortOrder) || (request.SortOrder != MySortOrder.Ascending && request.SortOrder != MySortOrder.Descending))
                    request.SortOrder = MySortOrder.Descending;

                  int totalRecord;
                  IEnumerable<Echange> items = _repositoryEchange.GetMany(exp, request.Index, request.Size, out totalRecord, orderByExp, request.SortOrder);
  
                  response.Count = totalRecord;
                  items = items as IList<Echange> ?? items.ToList();
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
      public BusinessResponse<decimal> GetSumEchangesByCriteria(BusinessRequest<EchangeDto> request)
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
                request.ItemsToSearch = new List<EchangeDto>();                                                    
 
              var exps = new List<Expression<Func<Echange, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<Echange, bool>> exp = null;                  
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

                  var sum = _repositoryEchange.GetSum(exp, sumExp);
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
  		public async Task<BusinessResponse<EchangeDto>> SaveEchanges(BusinessRequest<EchangeDto> request)
  		{
        var response = new BusinessResponse<EchangeDto>();
  
              try
              {
                  foreach (var item in request.ItemsToSave.TakeWhile(BusinessRequest<EchangeDto>.ValidateRequiredProperties))
                  {
                     var itemToSave = item.ToEntity();
                     Echange itemSaved = null;

                     itemToSave.DateMAJ = Utilities.CurrentDate; 
                     itemToSave.ModifiedBy = request.IdCurrentUser; 
                     if (itemToSave.IdEchange == 0)  
                     {  
                        itemToSave.CreatedBy = request.IdCurrentUser;   
                        itemToSave.DateCreation = Utilities.CurrentDate;   

                        itemToSave.IsDeleted = false;
                        itemSaved = _repositoryEchange.Add(itemToSave);
                     }
                     else //dans le cas d'une modification
                     {
                         itemSaved = _repositoryEchange.Update(itemToSave, p => p.IdEchange == itemToSave.IdEchange);
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
      private static Expression<Func<Echange, bool>> GenerateCriteria(EchangeDto itemToSearch) 
      {
          Expression<Func<Echange, bool>> exprinit = obj => true;
          Expression<Func<Echange, bool>> expi = exprinit;
                                                             
          if (itemToSearch.InfoSearchIdEchange.Consider)
          {
              switch (itemToSearch.InfoSearchIdEchange.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdEchange != itemToSearch.IdEchange);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdEchange < itemToSearch.IdEchange);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdEchange <= itemToSearch.IdEchange);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdEchange > itemToSearch.IdEchange);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdEchange >= itemToSearch.IdEchange);                  
                      break;  
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdEchange == itemToSearch.IdEchange);                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchDateEchange.Consider)
          {
              switch (itemToSearch.InfoSearchDateEchange.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.DateEchange != itemToSearch.DateEchange);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.DateEchange < itemToSearch.DateEchange);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.DateEchange <= itemToSearch.DateEchange);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.DateEchange > itemToSearch.DateEchange);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.DateEchange >= itemToSearch.DateEchange);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.DateEchange >= itemToSearch.InfoSearchDateEchange.Intervalle.Debut && obj.DateEchange <= itemToSearch.InfoSearchDateEchange.Intervalle.Fin);                         
                      break;        
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.DateEchange == itemToSearch.DateEchange);                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchIdEmetteur.Consider)
          {
              switch (itemToSearch.InfoSearchIdEmetteur.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdEmetteur != itemToSearch.IdEmetteur);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdEmetteur < itemToSearch.IdEmetteur);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdEmetteur <= itemToSearch.IdEmetteur);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdEmetteur > itemToSearch.IdEmetteur);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdEmetteur >= itemToSearch.IdEmetteur);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.IdEmetteur >= itemToSearch.InfoSearchIdEmetteur.Intervalle.Debut && obj.IdEmetteur <= itemToSearch.InfoSearchIdEmetteur.Intervalle.Fin);                         
                      break;        
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdEmetteur == itemToSearch.IdEmetteur);                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchIdDestinataire.Consider)
          {
              switch (itemToSearch.InfoSearchIdDestinataire.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdDestinataire != itemToSearch.IdDestinataire);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdDestinataire < itemToSearch.IdDestinataire);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdDestinataire <= itemToSearch.IdDestinataire);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdDestinataire > itemToSearch.IdDestinataire);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdDestinataire >= itemToSearch.IdDestinataire);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.IdDestinataire >= itemToSearch.InfoSearchIdDestinataire.Intervalle.Debut && obj.IdDestinataire <= itemToSearch.InfoSearchIdDestinataire.Intervalle.Fin);                         
                      break;        
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdDestinataire == itemToSearch.IdDestinataire);                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchNatureEmetteur.Consider)
          {
              switch (itemToSearch.InfoSearchNatureEmetteur.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.NatureEmetteur != itemToSearch.NatureEmetteur);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.NatureEmetteur < itemToSearch.NatureEmetteur);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.NatureEmetteur <= itemToSearch.NatureEmetteur);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.NatureEmetteur > itemToSearch.NatureEmetteur);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.NatureEmetteur >= itemToSearch.NatureEmetteur);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.NatureEmetteur >= itemToSearch.InfoSearchNatureEmetteur.Intervalle.Debut && obj.NatureEmetteur <= itemToSearch.InfoSearchNatureEmetteur.Intervalle.Fin);                         
                      break;        
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.NatureEmetteur == itemToSearch.NatureEmetteur);                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchNatureDestinataire.Consider)
          {
              switch (itemToSearch.InfoSearchNatureDestinataire.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.NatureDestinataire != itemToSearch.NatureDestinataire);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.NatureDestinataire < itemToSearch.NatureDestinataire);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.NatureDestinataire <= itemToSearch.NatureDestinataire);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.NatureDestinataire > itemToSearch.NatureDestinataire);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.NatureDestinataire >= itemToSearch.NatureDestinataire);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.NatureDestinataire >= itemToSearch.InfoSearchNatureDestinataire.Intervalle.Debut && obj.NatureDestinataire <= itemToSearch.InfoSearchNatureDestinataire.Intervalle.Fin);                         
                      break;        
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.NatureDestinataire == itemToSearch.NatureDestinataire);                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchIdProcessus.Consider)
          {
              switch (itemToSearch.InfoSearchIdProcessus.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdProcessus != itemToSearch.IdProcessus);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdProcessus < itemToSearch.IdProcessus);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdProcessus <= itemToSearch.IdProcessus);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdProcessus > itemToSearch.IdProcessus);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdProcessus >= itemToSearch.IdProcessus);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.IdProcessus >= itemToSearch.InfoSearchIdProcessus.Intervalle.Debut && obj.IdProcessus <= itemToSearch.InfoSearchIdProcessus.Intervalle.Fin);                         
                      break;        
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdProcessus == itemToSearch.IdProcessus);                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchIdTypeEchange.Consider)
          {
              switch (itemToSearch.InfoSearchIdTypeEchange.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdTypeEchange != itemToSearch.IdTypeEchange);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdTypeEchange < itemToSearch.IdTypeEchange);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdTypeEchange <= itemToSearch.IdTypeEchange);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdTypeEchange > itemToSearch.IdTypeEchange);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdTypeEchange >= itemToSearch.IdTypeEchange);                  
                      break;  
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdTypeEchange == itemToSearch.IdTypeEchange);                  
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

          return expi == exprinit ? null : expi;
      }

      /// <summary>
        /// Générer une expression lambda à partir d'un DTO
        /// </summary>
        /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
        /// <returns>Expression lambda</returns>
      private static Expression<Func<Echange, object>> GenerateOrderByExpression(EchangeDto itemToSearch)
      {
          Expression<Func<Echange, object>> exp = null;

          if (itemToSearch.InfoSearchIdEchange.IsOrderByField)
            exp = (o => o.IdEchange);
          if (itemToSearch.InfoSearchDateEchange.IsOrderByField)
            exp = (o => o.DateEchange);
          if (itemToSearch.InfoSearchIdEmetteur.IsOrderByField)
            exp = (o => o.IdEmetteur);
          if (itemToSearch.InfoSearchIdDestinataire.IsOrderByField)
            exp = (o => o.IdDestinataire);
          if (itemToSearch.InfoSearchNatureEmetteur.IsOrderByField)
            exp = (o => o.NatureEmetteur);
          if (itemToSearch.InfoSearchNatureDestinataire.IsOrderByField)
            exp = (o => o.NatureDestinataire);
          if (itemToSearch.InfoSearchIdProcessus.IsOrderByField)
            exp = (o => o.IdProcessus);
          if (itemToSearch.InfoSearchIdTypeEchange.IsOrderByField)
            exp = (o => o.IdTypeEchange);
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

          return exp ?? (obj => obj.IdEchange);
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private static Expression<Func<Echange, decimal?>> GenerateSumExpression(EchangeDto itemToSearch)
      {
          Expression<Func<Echange, decimal?>> exp = null;

          if (itemToSearch.InfoSearchDateEchange.IsSumField)
            exp = (o => o.DateEchange);                                         
            if (itemToSearch.InfoSearchNatureEmetteur.IsSumField)
            exp = (o => o.NatureEmetteur);                                         
            if (itemToSearch.InfoSearchNatureDestinataire.IsSumField)
            exp = (o => o.NatureDestinataire);                                         
            if (itemToSearch.InfoSearchCreatedBy.IsSumField)
            exp = (o => o.CreatedBy.Value);                     
            if (itemToSearch.InfoSearchModifiedBy.IsSumField)
            exp = (o => o.ModifiedBy.Value);                     
  
          //return exp ?? (obj => obj.IdEchange);
          return exp;
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private EchangeDto GenerateSum(Expression<Func<Echange, bool>> criteria, EchangeDto itemToSearch, EchangeDto itemToReturn)
      {
          if (itemToSearch.InfoSearchDateEchange.IsSumField)
            itemToReturn.InfoSearchDateEchange.Sum = _repositoryEchange.GetSum(criteria, o => o.DateEchange);
                                        
            if (itemToSearch.InfoSearchNatureEmetteur.IsSumField)
            itemToReturn.InfoSearchNatureEmetteur.Sum = _repositoryEchange.GetSum(criteria, o => o.NatureEmetteur);
                                        
            if (itemToSearch.InfoSearchNatureDestinataire.IsSumField)
            itemToReturn.InfoSearchNatureDestinataire.Sum = _repositoryEchange.GetSum(criteria, o => o.NatureDestinataire);
                                        
            if (itemToSearch.InfoSearchCreatedBy.IsSumField)
            itemToReturn.InfoSearchCreatedBy.Sum = _repositoryEchange.GetSum(criteria, o => o.CreatedBy.Value);     
               
            if (itemToSearch.InfoSearchModifiedBy.IsSumField)
            itemToReturn.InfoSearchModifiedBy.Sum = _repositoryEchange.GetSum(criteria, o => o.ModifiedBy.Value);     
               
  
          return itemToReturn;
      }
  }
}

