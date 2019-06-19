﻿using System;
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
  public class ControlProvider
  {

      #region Singleton
      
      private static ControlProvider _instance;

      public static ControlProvider Instance
      {
          get { return _instance ?? (_instance = new ControlProvider()); }
      }

      #endregion

      private readonly IRepository<Control> _repositoryControl;
     
      private ControlProvider()
      {
          _repositoryControl = new Repository<Control>();
      }

      private ControlProvider(IRepository<Control> repository)
      {    
          _repositoryControl = repository;
      }

      public BusinessResponse<ControlDto> GetControlById(object id)
      {
          var response = new BusinessResponse<ControlDto>();

          try
          {
              var item = _repositoryControl[id];
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
      public BusinessResponse<ControlDto> GetControlsByCriteria(BusinessRequest<ControlDto> request)
      {
          var response = new BusinessResponse<ControlDto>();

          try
          {
              request.ToFinalRequest();

              // générer expression lambda de l'objet de recherche principal
              var mainexp = GenerateCriteria(request.ItemToSearch);                  
              mainexp = (mainexp == null) ? obj => obj.IsDeleted == false : mainexp.And(obj => obj.IsDeleted == false);                   

              // générer expression lambda des objets de recherche sécondaires
              if (request.ItemsToSearch == null) 
                request.ItemsToSearch = new List<ControlDto>();                                                    
 
              var exps = new List<Expression<Func<Control, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<Control, bool>> exp = null;                  
              foreach (var expi in exps)
              {
                  if(request.NotIn)
                      exp = (exp == null) ? expi : exp.And(expi);
                  else
                      exp = (exp == null) ? expi : exp.Or(expi); 
              }

                  // concaténer les critères principaux et sécondaires
                  exp = (exp == null) ? mainexp : mainexp.And(exp);                  
                  
                  Expression<Func<Control, object>> orderByExp = GenerateOrderByExpression(request.ItemToSearch);
                  if (string.IsNullOrEmpty(request.SortOrder) || (request.SortOrder != MySortOrder.Ascending && request.SortOrder != MySortOrder.Descending))
                    request.SortOrder = MySortOrder.Descending;

                  int totalRecord;
                  IEnumerable<Control> items = _repositoryControl.GetMany(exp, request.Index, request.Size, out totalRecord, orderByExp, request.SortOrder);
  
                  response.Count = totalRecord;
                  items = items as IList<Control> ?? items.ToList();
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
      public BusinessResponse<decimal> GetSumControlsByCriteria(BusinessRequest<ControlDto> request)
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
                request.ItemsToSearch = new List<ControlDto>();                                                    
 
              var exps = new List<Expression<Func<Control, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<Control, bool>> exp = null;                  
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

                  var sum = _repositoryControl.GetSum(exp, sumExp);
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
  		public BusinessResponse<ControlDto> SaveControls(BusinessRequest<ControlDto> request)
  		{
        var response = new BusinessResponse<ControlDto>();
  
              try
              {
                  foreach (var item in request.ItemsToSave.TakeWhile(BusinessRequest<ControlDto>.ValidateRequiredProperties))
                  {
                     var itemToSave = item.ToEntity();
                     Control itemSaved = null;

                     itemToSave.ModifiedBy = request.IdCurrentUser; 
                     if (itemToSave.IdControl == 0)  
                     {  
                        itemToSave.CreatedBy = request.IdCurrentUser;   

                        itemToSave.IsDeleted = false;
                        itemSaved = _repositoryControl.Add(itemToSave);
                     }
                     else //dans le cas d'une modification
                     {
                         itemSaved = _repositoryControl.Update(itemToSave, p => p.IdControl == itemToSave.IdControl);
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
      private static Expression<Func<Control, bool>> GenerateCriteria(ControlDto itemToSearch) 
      {
          Expression<Func<Control, bool>> exprinit = obj => true;
          Expression<Func<Control, bool>> expi = exprinit;
                                                             
          if (itemToSearch.InfoSearchIdControl.Consider)
          {
              switch (itemToSearch.InfoSearchIdControl.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdControl != itemToSearch.IdControl);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdControl < itemToSearch.IdControl);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdControl <= itemToSearch.IdControl);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdControl > itemToSearch.IdControl);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdControl >= itemToSearch.IdControl);                  
                      break;  
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdControl == itemToSearch.IdControl);                  
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
                 
          if (itemToSearch.InfoSearchIdControlParent.Consider)
          {
              switch (itemToSearch.InfoSearchIdControlParent.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdControlParent != itemToSearch.IdControlParent);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdControlParent < itemToSearch.IdControlParent);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdControlParent <= itemToSearch.IdControlParent);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdControlParent > itemToSearch.IdControlParent);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdControlParent >= itemToSearch.IdControlParent);                  
                      break;  
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdControlParent == itemToSearch.IdControlParent);                  
                      break;
              }
          }            

          if (itemToSearch.InfoSearchFormName.Consider)
          {
              switch (itemToSearch.InfoSearchFormName.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.FormName != itemToSearch.FormName);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.FormName.StartsWith(itemToSearch.FormName));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.FormName.EndsWith(itemToSearch.FormName));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.FormName == itemToSearch.FormName);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.FormName.Contains(itemToSearch.FormName));
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
      private static Expression<Func<Control, object>> GenerateOrderByExpression(ControlDto itemToSearch)
      {
          Expression<Func<Control, object>> exp = null;

          if (itemToSearch.InfoSearchIdControl.IsOrderByField)
            exp = (o => o.IdControl);
          if (itemToSearch.InfoSearchCode.IsOrderByField)
            exp = (o => o.Code);
          if (itemToSearch.InfoSearchIdControlParent.IsOrderByField)
            exp = (o => o.IdControlParent);
          if (itemToSearch.InfoSearchFormName.IsOrderByField)
            exp = (o => o.FormName);
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

          return exp ?? (obj => obj.IdControl);
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private static Expression<Func<Control, decimal?>> GenerateSumExpression(ControlDto itemToSearch)
      {
          Expression<Func<Control, decimal?>> exp = null;

          if (itemToSearch.InfoSearchCreateBy.IsSumField)
            exp = (o => o.CreateBy.Value);                     
            if (itemToSearch.InfoSearchModifiedBy.IsSumField)
            exp = (o => o.ModifiedBy.Value);                     
            if (itemToSearch.InfoSearchCreatedBy.IsSumField)
            exp = (o => o.CreatedBy.Value);                     
  
          //return exp ?? (obj => obj.IdControl);
          return exp;
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private ControlDto GenerateSum(Expression<Func<Control, bool>> criteria, ControlDto itemToSearch, ControlDto itemToReturn)
      {
          if (itemToSearch.InfoSearchCreateBy.IsSumField)
            itemToReturn.InfoSearchCreateBy.Sum = _repositoryControl.GetSum(criteria, o => o.CreateBy.Value);     
               
            if (itemToSearch.InfoSearchModifiedBy.IsSumField)
            itemToReturn.InfoSearchModifiedBy.Sum = _repositoryControl.GetSum(criteria, o => o.ModifiedBy.Value);     
               
            if (itemToSearch.InfoSearchCreatedBy.IsSumField)
            itemToReturn.InfoSearchCreatedBy.Sum = _repositoryControl.GetSum(criteria, o => o.CreatedBy.Value);     
               
  
          return itemToReturn;
      }
  }
}

