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
  public class ProfilFonctionnaliteHistoProvider
  {

      #region Singleton
      
      private static ProfilFonctionnaliteHistoProvider _instance;

      public static ProfilFonctionnaliteHistoProvider Instance
      {
          get { return _instance ?? (_instance = new ProfilFonctionnaliteHistoProvider()); }
      }

      #endregion

      private readonly IRepository<ProfilFonctionnaliteHisto> _repositoryProfilFonctionnaliteHisto;
     
      public  ProfilFonctionnaliteHistoProvider()
      {
          _repositoryProfilFonctionnaliteHisto = new Repository<ProfilFonctionnaliteHisto>();
      }

      private ProfilFonctionnaliteHistoProvider(IRepository<ProfilFonctionnaliteHisto> repository)
      {    
          _repositoryProfilFonctionnaliteHisto = repository;
      }

      public async Task<BusinessResponse<ProfilFonctionnaliteHistoDto>> GetProfilFonctionnaliteHistoById(object id)
      {
          var response = new BusinessResponse<ProfilFonctionnaliteHistoDto>();

          try
          {
              var item = _repositoryProfilFonctionnaliteHisto[id];
              if (item != null)                  
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
      public async Task<BusinessResponse<ProfilFonctionnaliteHistoDto>> GetProfilFonctionnaliteHistosByCriteria(BusinessRequest<ProfilFonctionnaliteHistoDto> request)
      {
          var response = new BusinessResponse<ProfilFonctionnaliteHistoDto>();

          try
          {
              request.ToFinalRequest();

              // générer expression lambda de l'objet de recherche principal
              var mainexp = GenerateCriteria(request.ItemToSearch);                  
              mainexp = mainexp ?? (obj => true);                   

              // générer expression lambda des objets de recherche sécondaires
              if (request.ItemsToSearch == null) 
                request.ItemsToSearch = new List<ProfilFonctionnaliteHistoDto>();                                                    
 
              var exps = new List<Expression<Func<ProfilFonctionnaliteHisto, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<ProfilFonctionnaliteHisto, bool>> exp = null;                  
              foreach (var expi in exps)
              {
                  if(request.NotIn)
                      exp = (exp == null) ? expi : exp.And(expi);
                  else
                      exp = (exp == null) ? expi : exp.Or(expi); 
              }

                  // concaténer les critères principaux et sécondaires
                  exp = (exp == null) ? mainexp : mainexp.And(exp);                  
                  
                  Expression<Func<ProfilFonctionnaliteHisto, object>> orderByExp = GenerateOrderByExpression(request.ItemToSearch);
                  if (string.IsNullOrEmpty(request.SortOrder) || (request.SortOrder != MySortOrder.Ascending && request.SortOrder != MySortOrder.Descending))
                    request.SortOrder = MySortOrder.Descending;

                  int totalRecord;
                  IEnumerable<ProfilFonctionnaliteHisto> items = _repositoryProfilFonctionnaliteHisto.GetMany(exp, request.Index, request.Size, out totalRecord, orderByExp, request.SortOrder);
  
                  response.Count = totalRecord;
                  items = items as IList<ProfilFonctionnaliteHisto> ?? items.ToList();
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
      public BusinessResponse<decimal> GetSumProfilFonctionnaliteHistosByCriteria(BusinessRequest<ProfilFonctionnaliteHistoDto> request)
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
                request.ItemsToSearch = new List<ProfilFonctionnaliteHistoDto>();                                                    
 
              var exps = new List<Expression<Func<ProfilFonctionnaliteHisto, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<ProfilFonctionnaliteHisto, bool>> exp = null;                  
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

                  var sum = _repositoryProfilFonctionnaliteHisto.GetSum(exp, sumExp);
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
  		public async Task<BusinessResponse<ProfilFonctionnaliteHistoDto>> SaveProfilFonctionnaliteHistos(BusinessRequest<ProfilFonctionnaliteHistoDto> request)
  		{
        var response = new BusinessResponse<ProfilFonctionnaliteHistoDto>();
  
              try
              {
                  foreach (var item in request.ItemsToSave.TakeWhile(BusinessRequest<ProfilFonctionnaliteHistoDto>.ValidateRequiredProperties))
                  {
                     var itemToSave = item.ToEntity();
                     ProfilFonctionnaliteHisto itemSaved = null;

                     itemToSave.DateMAJ = Utilities.CurrentDate; 
                     if (itemToSave.IdProfilFonctionnalite == 0)  
                     {  
                        itemToSave.DateCreation = Utilities.CurrentDate;   
                        itemSaved = _repositoryProfilFonctionnaliteHisto.Add(itemToSave);
                     }
                     else //dans le cas d'une modification
                     {
                         itemSaved = _repositoryProfilFonctionnaliteHisto.Update(itemToSave, p => p.IdProfilFonctionnalite == itemToSave.IdProfilFonctionnalite);
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
      private static Expression<Func<ProfilFonctionnaliteHisto, bool>> GenerateCriteria(ProfilFonctionnaliteHistoDto itemToSearch) 
      {
          Expression<Func<ProfilFonctionnaliteHisto, bool>> exprinit = obj => true;
          Expression<Func<ProfilFonctionnaliteHisto, bool>> expi = exprinit;
                                                             
          if (itemToSearch.InfoSearchIdProfilFonctionnalite.Consider)
          {
              switch (itemToSearch.InfoSearchIdProfilFonctionnalite.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdProfilFonctionnalite != itemToSearch.IdProfilFonctionnalite);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdProfilFonctionnalite < itemToSearch.IdProfilFonctionnalite);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdProfilFonctionnalite <= itemToSearch.IdProfilFonctionnalite);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdProfilFonctionnalite > itemToSearch.IdProfilFonctionnalite);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdProfilFonctionnalite >= itemToSearch.IdProfilFonctionnalite);                  
                      break;  
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdProfilFonctionnalite == itemToSearch.IdProfilFonctionnalite);                  
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

          if (itemToSearch.InfoSearchCodeFonctionnalite.Consider)
          {
              switch (itemToSearch.InfoSearchCodeFonctionnalite.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.CodeFonctionnalite != itemToSearch.CodeFonctionnalite);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.CodeFonctionnalite.StartsWith(itemToSearch.CodeFonctionnalite));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.CodeFonctionnalite.EndsWith(itemToSearch.CodeFonctionnalite));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.CodeFonctionnalite == itemToSearch.CodeFonctionnalite);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.CodeFonctionnalite.Contains(itemToSearch.CodeFonctionnalite));
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
                 
          return expi == exprinit ? null : expi;
      }

      /// <summary>
        /// Générer une expression lambda à partir d'un DTO
        /// </summary>
        /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
        /// <returns>Expression lambda</returns>
      private static Expression<Func<ProfilFonctionnaliteHisto, object>> GenerateOrderByExpression(ProfilFonctionnaliteHistoDto itemToSearch)
      {
          Expression<Func<ProfilFonctionnaliteHisto, object>> exp = null;

          if (itemToSearch.InfoSearchIdProfilFonctionnalite.IsOrderByField)
            exp = (o => o.IdProfilFonctionnalite);
          if (itemToSearch.InfoSearchIdProfil.IsOrderByField)
            exp = (o => o.IdProfil);
          if (itemToSearch.InfoSearchCodeFonctionnalite.IsOrderByField)
            exp = (o => o.CodeFonctionnalite);
          if (itemToSearch.InfoSearchAutorise.IsOrderByField)
            exp = (o => o.Autorise);
          if (itemToSearch.InfoSearchDateCreation.IsOrderByField)
            exp = (o => o.DateCreation);
          if (itemToSearch.InfoSearchDateMAJ.IsOrderByField)
            exp = (o => o.DateMAJ);
          if (itemToSearch.InfoSearchDataKey.IsOrderByField)
            exp = (o => o.DataKey);

          return exp ?? (obj => obj.IdProfilFonctionnalite);
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private static Expression<Func<ProfilFonctionnaliteHisto, decimal?>> GenerateSumExpression(ProfilFonctionnaliteHistoDto itemToSearch)
      {
          Expression<Func<ProfilFonctionnaliteHisto, decimal?>> exp = null;


          //return exp ?? (obj => obj.IdProfilFonctionnalite);
          return exp;
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private ProfilFonctionnaliteHistoDto GenerateSum(Expression<Func<ProfilFonctionnaliteHisto, bool>> criteria, ProfilFonctionnaliteHistoDto itemToSearch, ProfilFonctionnaliteHistoDto itemToReturn)
      {

          return itemToReturn;
      }
  }
}

