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
  public class HabillitationProvider
  {

      #region Singleton
      
      private static HabillitationProvider _instance;

      public static HabillitationProvider Instance
      {
          get { return _instance ?? (_instance = new HabillitationProvider()); }
      }

      #endregion

      private readonly IRepository<Habillitation> _repositoryHabillitation;
     
      private HabillitationProvider()
      {
          _repositoryHabillitation = new Repository<Habillitation>();
      }

      private HabillitationProvider(IRepository<Habillitation> repository)
      {    
          _repositoryHabillitation = repository;
      }

      public BusinessResponse<HabillitationDto> GetHabillitationById(object id)
      {
          var response = new BusinessResponse<HabillitationDto>();

          try
          {
              var item = _repositoryHabillitation[id];
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
      public BusinessResponse<HabillitationDto> GetHabillitationsByCriteria(BusinessRequest<HabillitationDto> request)
      {
          var response = new BusinessResponse<HabillitationDto>();

          try
          {
              request.ToFinalRequest();

              // générer expression lambda de l'objet de recherche principal
              var mainexp = GenerateCriteria(request.ItemToSearch);                  
              mainexp = (mainexp == null) ? obj => obj.IsDeleted == false : mainexp.And(obj => obj.IsDeleted == false);                   

              // générer expression lambda des objets de recherche sécondaires
              if (request.ItemsToSearch == null) 
                request.ItemsToSearch = new List<HabillitationDto>();                                                    
 
              var exps = new List<Expression<Func<Habillitation, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<Habillitation, bool>> exp = null;                  
              foreach (var expi in exps)
              {
                  if(request.NotIn)
                      exp = (exp == null) ? expi : exp.And(expi);
                  else
                      exp = (exp == null) ? expi : exp.Or(expi); 
              }

                  // concaténer les critères principaux et sécondaires
                  exp = (exp == null) ? mainexp : mainexp.And(exp);                  
                  
                  Expression<Func<Habillitation, object>> orderByExp = GenerateOrderByExpression(request.ItemToSearch);
                  if (string.IsNullOrEmpty(request.SortOrder) || (request.SortOrder != MySortOrder.Ascending && request.SortOrder != MySortOrder.Descending))
                    request.SortOrder = MySortOrder.Descending;

                  int totalRecord;
                  IEnumerable<Habillitation> items = _repositoryHabillitation.GetMany(exp, request.Index, request.Size, out totalRecord, orderByExp, request.SortOrder);
  
                  response.Count = totalRecord;
                  items = items as IList<Habillitation> ?? items.ToList();
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
      public BusinessResponse<decimal> GetSumHabillitationsByCriteria(BusinessRequest<HabillitationDto> request)
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
                request.ItemsToSearch = new List<HabillitationDto>();                                                    
 
              var exps = new List<Expression<Func<Habillitation, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<Habillitation, bool>> exp = null;                  
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

                  var sum = _repositoryHabillitation.GetSum(exp, sumExp);
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
  		public BusinessResponse<HabillitationDto> SaveHabillitations(BusinessRequest<HabillitationDto> request)
  		{
        var response = new BusinessResponse<HabillitationDto>();
  
              try
              {
                  foreach (var item in request.ItemsToSave.TakeWhile(BusinessRequest<HabillitationDto>.ValidateRequiredProperties))
                  {
                     var itemToSave = item.ToEntity();
                     Habillitation itemSaved = null;

                     itemToSave.ModifiedBy = request.IdCurrentUser; 
                     if (itemToSave.IdHabillitation == 0)  
                     {  
                        itemToSave.CreatedBy = request.IdCurrentUser;   

                        itemToSave.IsDeleted = false;
                        itemSaved = _repositoryHabillitation.Add(itemToSave);
                     }
                     else //dans le cas d'une modification
                     {
                         itemSaved = _repositoryHabillitation.Update(itemToSave, p => p.IdHabillitation == itemToSave.IdHabillitation);
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
      private static Expression<Func<Habillitation, bool>> GenerateCriteria(HabillitationDto itemToSearch) 
      {
          Expression<Func<Habillitation, bool>> exprinit = obj => true;
          Expression<Func<Habillitation, bool>> expi = exprinit;
                                                             
          if (itemToSearch.InfoSearchIdHabillitation.Consider)
          {
              switch (itemToSearch.InfoSearchIdHabillitation.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdHabillitation != itemToSearch.IdHabillitation);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdHabillitation < itemToSearch.IdHabillitation);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdHabillitation <= itemToSearch.IdHabillitation);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdHabillitation > itemToSearch.IdHabillitation);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdHabillitation >= itemToSearch.IdHabillitation);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.IdHabillitation >= itemToSearch.InfoSearchIdHabillitation.Intervalle.Debut && obj.IdHabillitation <= itemToSearch.InfoSearchIdHabillitation.Intervalle.Fin);                         
                      break;        
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdHabillitation == itemToSearch.IdHabillitation);                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchIdBeneficiaire.Consider)
          {
              switch (itemToSearch.InfoSearchIdBeneficiaire.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdBeneficiaire != itemToSearch.IdBeneficiaire);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdBeneficiaire < itemToSearch.IdBeneficiaire);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdBeneficiaire <= itemToSearch.IdBeneficiaire);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdBeneficiaire > itemToSearch.IdBeneficiaire);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdBeneficiaire >= itemToSearch.IdBeneficiaire);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.IdBeneficiaire >= itemToSearch.InfoSearchIdBeneficiaire.Intervalle.Debut && obj.IdBeneficiaire <= itemToSearch.InfoSearchIdBeneficiaire.Intervalle.Fin);                         
                      break;        
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdBeneficiaire == itemToSearch.IdBeneficiaire);                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchIdNatureBeneficiaire.Consider)
          {
              switch (itemToSearch.InfoSearchIdNatureBeneficiaire.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdNatureBeneficiaire != itemToSearch.IdNatureBeneficiaire);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdNatureBeneficiaire < itemToSearch.IdNatureBeneficiaire);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdNatureBeneficiaire <= itemToSearch.IdNatureBeneficiaire);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdNatureBeneficiaire > itemToSearch.IdNatureBeneficiaire);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdNatureBeneficiaire >= itemToSearch.IdNatureBeneficiaire);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.IdNatureBeneficiaire >= itemToSearch.InfoSearchIdNatureBeneficiaire.Intervalle.Debut && obj.IdNatureBeneficiaire <= itemToSearch.InfoSearchIdNatureBeneficiaire.Intervalle.Fin);                         
                      break;        
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdNatureBeneficiaire == itemToSearch.IdNatureBeneficiaire);                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchIdFonctionnalite.Consider)
          {
              switch (itemToSearch.InfoSearchIdFonctionnalite.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdFonctionnalite != itemToSearch.IdFonctionnalite);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdFonctionnalite < itemToSearch.IdFonctionnalite);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdFonctionnalite <= itemToSearch.IdFonctionnalite);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdFonctionnalite > itemToSearch.IdFonctionnalite);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdFonctionnalite >= itemToSearch.IdFonctionnalite);                  
                      break;  
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdFonctionnalite == itemToSearch.IdFonctionnalite);                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchAction.Consider)
          {
              switch (itemToSearch.InfoSearchAction.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.Action != itemToSearch.Action);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.Action < itemToSearch.Action);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.Action <= itemToSearch.Action);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.Action > itemToSearch.Action);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.Action >= itemToSearch.Action);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.Action >= itemToSearch.InfoSearchAction.Intervalle.Debut && obj.Action <= itemToSearch.InfoSearchAction.Intervalle.Fin);                         
                      break;    
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.Action == itemToSearch.Action);                  
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
      private static Expression<Func<Habillitation, object>> GenerateOrderByExpression(HabillitationDto itemToSearch)
      {
          Expression<Func<Habillitation, object>> exp = null;

          if (itemToSearch.InfoSearchIdHabillitation.IsOrderByField)
            exp = (o => o.IdHabillitation);
          if (itemToSearch.InfoSearchIdBeneficiaire.IsOrderByField)
            exp = (o => o.IdBeneficiaire);
          if (itemToSearch.InfoSearchIdNatureBeneficiaire.IsOrderByField)
            exp = (o => o.IdNatureBeneficiaire);
          if (itemToSearch.InfoSearchIdFonctionnalite.IsOrderByField)
            exp = (o => o.IdFonctionnalite);
          if (itemToSearch.InfoSearchAction.IsOrderByField)
            exp = (o => o.Action);
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

          return exp ?? (obj => obj.IdHabillitation);
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private static Expression<Func<Habillitation, decimal?>> GenerateSumExpression(HabillitationDto itemToSearch)
      {
          Expression<Func<Habillitation, decimal?>> exp = null;

          if (itemToSearch.InfoSearchAction.IsSumField)
            exp = (o => o.Action.Value);                     
            if (itemToSearch.InfoSearchCreateBy.IsSumField)
            exp = (o => o.CreateBy.Value);                     
            if (itemToSearch.InfoSearchModifiedBy.IsSumField)
            exp = (o => o.ModifiedBy.Value);                     
            if (itemToSearch.InfoSearchCreatedBy.IsSumField)
            exp = (o => o.CreatedBy.Value);                     
  
          //return exp ?? (obj => obj.IdHabillitation);
          return exp;
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private HabillitationDto GenerateSum(Expression<Func<Habillitation, bool>> criteria, HabillitationDto itemToSearch, HabillitationDto itemToReturn)
      {
          if (itemToSearch.InfoSearchAction.IsSumField)
            itemToReturn.InfoSearchAction.Sum = _repositoryHabillitation.GetSum(criteria, o => o.Action.Value);     
               
            if (itemToSearch.InfoSearchCreateBy.IsSumField)
            itemToReturn.InfoSearchCreateBy.Sum = _repositoryHabillitation.GetSum(criteria, o => o.CreateBy.Value);     
               
            if (itemToSearch.InfoSearchModifiedBy.IsSumField)
            itemToReturn.InfoSearchModifiedBy.Sum = _repositoryHabillitation.GetSum(criteria, o => o.ModifiedBy.Value);     
               
            if (itemToSearch.InfoSearchCreatedBy.IsSumField)
            itemToReturn.InfoSearchCreatedBy.Sum = _repositoryHabillitation.GetSum(criteria, o => o.CreatedBy.Value);     
               
  
          return itemToReturn;
      }
  }
}

