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
  public class DemandePlacementFacultativeProvider
  {

      #region Singleton
      
      private static DemandePlacementFacultativeProvider _instance;

      public static DemandePlacementFacultativeProvider Instance
      {
          get { return _instance ?? (_instance = new DemandePlacementFacultativeProvider()); }
      }

      #endregion

      private readonly IRepository<DemandePlacementFacultative> _repositoryDemandePlacementFacultative;
     
      public  DemandePlacementFacultativeProvider()
      {
          _repositoryDemandePlacementFacultative = new Repository<DemandePlacementFacultative>();
      }

      private DemandePlacementFacultativeProvider(IRepository<DemandePlacementFacultative> repository)
      {    
          _repositoryDemandePlacementFacultative = repository;
      }

      public async Task<BusinessResponse<DemandePlacementFacultativeDto>> GetDemandePlacementFacultativeById(object id)
      {
          var response = new BusinessResponse<DemandePlacementFacultativeDto>();

          try
          {
              var item = _repositoryDemandePlacementFacultative[id];
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
      public async Task<BusinessResponse<DemandePlacementFacultativeDto>> GetDemandePlacementFacultativesByCriteria(BusinessRequest<DemandePlacementFacultativeDto> request)
      {
          var response = new BusinessResponse<DemandePlacementFacultativeDto>();

          try
          {
              request.ToFinalRequest();

              // générer expression lambda de l'objet de recherche principal
              var mainexp = GenerateCriteria(request.ItemToSearch);                  
              mainexp = (mainexp == null) ? obj => obj.IsDeleted == false : mainexp.And(obj => obj.IsDeleted == false);                   

              // générer expression lambda des objets de recherche sécondaires
              if (request.ItemsToSearch == null) 
                request.ItemsToSearch = new List<DemandePlacementFacultativeDto>();                                                    
 
              var exps = new List<Expression<Func<DemandePlacementFacultative, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<DemandePlacementFacultative, bool>> exp = null;                  
              foreach (var expi in exps)
              {
                  if(request.NotIn)
                      exp = (exp == null) ? expi : exp.And(expi);
                  else
                      exp = (exp == null) ? expi : exp.Or(expi); 
              }

                  // concaténer les critères principaux et sécondaires
                  exp = (exp == null) ? mainexp : mainexp.And(exp);                  
                  
                  Expression<Func<DemandePlacementFacultative, object>> orderByExp = GenerateOrderByExpression(request.ItemToSearch);
                  if (string.IsNullOrEmpty(request.SortOrder) || (request.SortOrder != MySortOrder.Ascending && request.SortOrder != MySortOrder.Descending))
                    request.SortOrder = MySortOrder.Descending;

                  int totalRecord;
                  IEnumerable<DemandePlacementFacultative> items = _repositoryDemandePlacementFacultative.GetMany(exp, request.Index, request.Size, out totalRecord, orderByExp, request.SortOrder);
  
                  response.Count = totalRecord;
                  items = items as IList<DemandePlacementFacultative> ?? items.ToList();
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
      public BusinessResponse<decimal> GetSumDemandePlacementFacultativesByCriteria(BusinessRequest<DemandePlacementFacultativeDto> request)
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
                request.ItemsToSearch = new List<DemandePlacementFacultativeDto>();                                                    
 
              var exps = new List<Expression<Func<DemandePlacementFacultative, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<DemandePlacementFacultative, bool>> exp = null;                  
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

                  var sum = _repositoryDemandePlacementFacultative.GetSum(exp, sumExp);
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
  		public async Task<BusinessResponse<DemandePlacementFacultativeDto>> SaveDemandePlacementFacultatives(BusinessRequest<DemandePlacementFacultativeDto> request)
  		{
        var response = new BusinessResponse<DemandePlacementFacultativeDto>();
  
              try
              {
                  foreach (var item in request.ItemsToSave.TakeWhile(BusinessRequest<DemandePlacementFacultativeDto>.ValidateRequiredProperties))
                  {
                     var itemToSave = item.ToEntity();
                     DemandePlacementFacultative itemSaved = null;

                     itemToSave.DateMAJ = Utilities.CurrentDate; 
                     itemToSave.ModifiedBy = request.IdCurrentUser; 
                     if (itemToSave.IdDemandePlacementFacultative == 0)  
                     {  
                        itemToSave.DateCreation = Utilities.CurrentDate;   
                        itemToSave.CreatedBy = request.IdCurrentUser;   

                        itemToSave.IsDeleted = false;
                        itemSaved = _repositoryDemandePlacementFacultative.Add(itemToSave);
                     }
                     else //dans le cas d'une modification
                     {
                         itemSaved = _repositoryDemandePlacementFacultative.Update(itemToSave, p => p.IdDemandePlacementFacultative == itemToSave.IdDemandePlacementFacultative);
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
      private static Expression<Func<DemandePlacementFacultative, bool>> GenerateCriteria(DemandePlacementFacultativeDto itemToSearch) 
      {
          Expression<Func<DemandePlacementFacultative, bool>> exprinit = obj => true;
          Expression<Func<DemandePlacementFacultative, bool>> expi = exprinit;
                                                             
          if (itemToSearch.InfoSearchIdDemandePlacementFacultative.Consider)
          {
              switch (itemToSearch.InfoSearchIdDemandePlacementFacultative.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdDemandePlacementFacultative != itemToSearch.IdDemandePlacementFacultative);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdDemandePlacementFacultative < itemToSearch.IdDemandePlacementFacultative);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdDemandePlacementFacultative <= itemToSearch.IdDemandePlacementFacultative);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdDemandePlacementFacultative > itemToSearch.IdDemandePlacementFacultative);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdDemandePlacementFacultative >= itemToSearch.IdDemandePlacementFacultative);                  
                      break;  
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdDemandePlacementFacultative == itemToSearch.IdDemandePlacementFacultative);                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchDateConsultation.Consider)
          {
              switch (itemToSearch.InfoSearchDateConsultation.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And
                              (obj => 
                                !(obj.DateConsultation.Year == itemToSearch.DateConsultation.Year
                                  && obj.DateConsultation.Month == itemToSearch.DateConsultation.Month
                                  && obj.DateConsultation.Day == itemToSearch.DateConsultation.Day)
                              );
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.DateConsultation.CompareTo(itemToSearch.DateConsultation) < 0);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.DateConsultation.CompareTo(itemToSearch.DateConsultation) <= 0);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.DateConsultation.CompareTo(itemToSearch.DateConsultation) > 0);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.DateConsultation.CompareTo(itemToSearch.DateConsultation)>= 0);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      var debut = itemToSearch.InfoSearchDateConsultation.Intervalle.Debut.Date;
                      var fin = itemToSearch.InfoSearchDateConsultation.Intervalle.Fin.AddDays(1).Date;
                      expi = expi.And(obj => obj.DateConsultation >= debut && obj.DateConsultation < fin);                  
                      break;         
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And
                            (
                              obj => obj.DateConsultation.Year == itemToSearch.DateConsultation.Year
                              && obj.DateConsultation.Month == itemToSearch.DateConsultation.Month
                              && obj.DateConsultation.Day == itemToSearch.DateConsultation.Day
                            );                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchGarantieCedee.Consider)
          {
              switch (itemToSearch.InfoSearchGarantieCedee.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.GarantieCedee != itemToSearch.GarantieCedee);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.GarantieCedee < itemToSearch.GarantieCedee);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.GarantieCedee <= itemToSearch.GarantieCedee);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.GarantieCedee > itemToSearch.GarantieCedee);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.GarantieCedee >= itemToSearch.GarantieCedee);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.GarantieCedee >= itemToSearch.InfoSearchGarantieCedee.Intervalle.Debut && obj.GarantieCedee <= itemToSearch.InfoSearchGarantieCedee.Intervalle.Fin);                         
                      break;        
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.GarantieCedee == itemToSearch.GarantieCedee);                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchPrime.Consider)
          {
              switch (itemToSearch.InfoSearchPrime.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.Prime != itemToSearch.Prime);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.Prime < itemToSearch.Prime);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.Prime <= itemToSearch.Prime);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.Prime > itemToSearch.Prime);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.Prime >= itemToSearch.Prime);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.Prime >= itemToSearch.InfoSearchPrime.Intervalle.Debut && obj.Prime <= itemToSearch.InfoSearchPrime.Intervalle.Fin);                         
                      break;        
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.Prime == itemToSearch.Prime);                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchDateEffet.Consider)
          {
              switch (itemToSearch.InfoSearchDateEffet.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And
                              (obj => 
                                !(obj.DateEffet.Year == itemToSearch.DateEffet.Year
                                  && obj.DateEffet.Month == itemToSearch.DateEffet.Month
                                  && obj.DateEffet.Day == itemToSearch.DateEffet.Day)
                              );
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.DateEffet.CompareTo(itemToSearch.DateEffet) < 0);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.DateEffet.CompareTo(itemToSearch.DateEffet) <= 0);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.DateEffet.CompareTo(itemToSearch.DateEffet) > 0);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.DateEffet.CompareTo(itemToSearch.DateEffet)>= 0);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      var debut = itemToSearch.InfoSearchDateEffet.Intervalle.Debut.Date;
                      var fin = itemToSearch.InfoSearchDateEffet.Intervalle.Fin.AddDays(1).Date;
                      expi = expi.And(obj => obj.DateEffet >= debut && obj.DateEffet < fin);                  
                      break;         
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And
                            (
                              obj => obj.DateEffet.Year == itemToSearch.DateEffet.Year
                              && obj.DateEffet.Month == itemToSearch.DateEffet.Month
                              && obj.DateEffet.Day == itemToSearch.DateEffet.Day
                            );                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchDateEcheance.Consider)
          {
              switch (itemToSearch.InfoSearchDateEcheance.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And
                              (obj => 
                                !(obj.DateEcheance.Year == itemToSearch.DateEcheance.Year
                                  && obj.DateEcheance.Month == itemToSearch.DateEcheance.Month
                                  && obj.DateEcheance.Day == itemToSearch.DateEcheance.Day)
                              );
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.DateEcheance.CompareTo(itemToSearch.DateEcheance) < 0);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.DateEcheance.CompareTo(itemToSearch.DateEcheance) <= 0);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.DateEcheance.CompareTo(itemToSearch.DateEcheance) > 0);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.DateEcheance.CompareTo(itemToSearch.DateEcheance)>= 0);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      var debut = itemToSearch.InfoSearchDateEcheance.Intervalle.Debut.Date;
                      var fin = itemToSearch.InfoSearchDateEcheance.Intervalle.Fin.AddDays(1).Date;
                      expi = expi.And(obj => obj.DateEcheance >= debut && obj.DateEcheance < fin);                  
                      break;         
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And
                            (
                              obj => obj.DateEcheance.Year == itemToSearch.DateEcheance.Year
                              && obj.DateEcheance.Month == itemToSearch.DateEcheance.Month
                              && obj.DateEcheance.Day == itemToSearch.DateEcheance.Day
                            );                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchEnCours.Consider)
          {
              switch (itemToSearch.InfoSearchEnCours.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.EnCours != itemToSearch.EnCours);
                      break;                      
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.EnCours == itemToSearch.EnCours);                  
                      break;
              }
          }            

                  if (itemToSearch.InfoSearchSMP.Consider)
          {
              switch (itemToSearch.InfoSearchSMP.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.SMP != itemToSearch.SMP);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.SMP < itemToSearch.SMP);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.SMP <= itemToSearch.SMP);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.SMP > itemToSearch.SMP);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.SMP >= itemToSearch.SMP);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.SMP >= itemToSearch.InfoSearchSMP.Intervalle.Debut && obj.SMP <= itemToSearch.InfoSearchSMP.Intervalle.Fin);                         
                      break;        
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.SMP == itemToSearch.SMP);                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchPartFiliale.Consider)
          {
              switch (itemToSearch.InfoSearchPartFiliale.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.PartFiliale != itemToSearch.PartFiliale);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.PartFiliale < itemToSearch.PartFiliale);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.PartFiliale <= itemToSearch.PartFiliale);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.PartFiliale > itemToSearch.PartFiliale);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.PartFiliale >= itemToSearch.PartFiliale);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.PartFiliale >= itemToSearch.InfoSearchPartFiliale.Intervalle.Debut && obj.PartFiliale <= itemToSearch.InfoSearchPartFiliale.Intervalle.Fin);                         
                      break;        
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.PartFiliale == itemToSearch.PartFiliale);                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchVersementAuTraite.Consider)
          {
              switch (itemToSearch.InfoSearchVersementAuTraite.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.VersementAuTraite != itemToSearch.VersementAuTraite);
                      break;                      
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.VersementAuTraite == itemToSearch.VersementAuTraite);                  
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
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdProcessus == itemToSearch.IdProcessus);                  
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
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdPays == itemToSearch.IdPays);                  
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

          return expi == exprinit ? null : expi;
      }

      /// <summary>
        /// Générer une expression lambda à partir d'un DTO
        /// </summary>
        /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
        /// <returns>Expression lambda</returns>
      private static Expression<Func<DemandePlacementFacultative, object>> GenerateOrderByExpression(DemandePlacementFacultativeDto itemToSearch)
      {
          Expression<Func<DemandePlacementFacultative, object>> exp = null;

          if (itemToSearch.InfoSearchIdDemandePlacementFacultative.IsOrderByField)
            exp = (o => o.IdDemandePlacementFacultative);
          if (itemToSearch.InfoSearchDateConsultation.IsOrderByField)
            exp = (o => o.DateConsultation);
          if (itemToSearch.InfoSearchGarantieCedee.IsOrderByField)
            exp = (o => o.GarantieCedee);
          if (itemToSearch.InfoSearchPrime.IsOrderByField)
            exp = (o => o.Prime);
          if (itemToSearch.InfoSearchDateEffet.IsOrderByField)
            exp = (o => o.DateEffet);
          if (itemToSearch.InfoSearchDateEcheance.IsOrderByField)
            exp = (o => o.DateEcheance);
          if (itemToSearch.InfoSearchEnCours.IsOrderByField)
            exp = (o => o.EnCours);
          if (itemToSearch.InfoSearchSMP.IsOrderByField)
            exp = (o => o.SMP);
          if (itemToSearch.InfoSearchPartFiliale.IsOrderByField)
            exp = (o => o.PartFiliale);
          if (itemToSearch.InfoSearchVersementAuTraite.IsOrderByField)
            exp = (o => o.VersementAuTraite);
          if (itemToSearch.InfoSearchIdProcessus.IsOrderByField)
            exp = (o => o.IdProcessus);
          if (itemToSearch.InfoSearchIdPays.IsOrderByField)
            exp = (o => o.IdPays);
          if (itemToSearch.InfoSearchIsDeleted.IsOrderByField)
            exp = (o => o.IsDeleted);
          if (itemToSearch.InfoSearchDateCreation.IsOrderByField)
            exp = (o => o.DateCreation);
          if (itemToSearch.InfoSearchDateMAJ.IsOrderByField)
            exp = (o => o.DateMAJ);
          if (itemToSearch.InfoSearchDataKey.IsOrderByField)
            exp = (o => o.DataKey);
          if (itemToSearch.InfoSearchCreatedBy.IsOrderByField)
            exp = (o => o.CreatedBy);
          if (itemToSearch.InfoSearchModifiedBy.IsOrderByField)
            exp = (o => o.ModifiedBy);

          return exp ?? (obj => obj.IdDemandePlacementFacultative);
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private static Expression<Func<DemandePlacementFacultative, decimal?>> GenerateSumExpression(DemandePlacementFacultativeDto itemToSearch)
      {
          Expression<Func<DemandePlacementFacultative, decimal?>> exp = null;

          if (itemToSearch.InfoSearchGarantieCedee.IsSumField)
            exp = (o => o.GarantieCedee);                                         
            if (itemToSearch.InfoSearchPrime.IsSumField)
            exp = (o => o.Prime);                                         
            if (itemToSearch.InfoSearchSMP.IsSumField)
            exp = (o => o.SMP);                                         
            if (itemToSearch.InfoSearchPartFiliale.IsSumField)
            exp = (o => o.PartFiliale);                                         
            if (itemToSearch.InfoSearchCreatedBy.IsSumField)
            exp = (o => o.CreatedBy.Value);                     
            if (itemToSearch.InfoSearchModifiedBy.IsSumField)
            exp = (o => o.ModifiedBy.Value);                     
  
          //return exp ?? (obj => obj.IdDemandePlacementFacultative);
          return exp;
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private DemandePlacementFacultativeDto GenerateSum(Expression<Func<DemandePlacementFacultative, bool>> criteria, DemandePlacementFacultativeDto itemToSearch, DemandePlacementFacultativeDto itemToReturn)
      {
          if (itemToSearch.InfoSearchGarantieCedee.IsSumField)
            itemToReturn.InfoSearchGarantieCedee.Sum = _repositoryDemandePlacementFacultative.GetSum(criteria, o => o.GarantieCedee);
                                        
            if (itemToSearch.InfoSearchPrime.IsSumField)
            itemToReturn.InfoSearchPrime.Sum = _repositoryDemandePlacementFacultative.GetSum(criteria, o => o.Prime);
                                        
            if (itemToSearch.InfoSearchSMP.IsSumField)
            itemToReturn.InfoSearchSMP.Sum = _repositoryDemandePlacementFacultative.GetSum(criteria, o => o.SMP);
                                        
            if (itemToSearch.InfoSearchPartFiliale.IsSumField)
            itemToReturn.InfoSearchPartFiliale.Sum = _repositoryDemandePlacementFacultative.GetSum(criteria, o => o.PartFiliale);
                                        
            if (itemToSearch.InfoSearchCreatedBy.IsSumField)
            itemToReturn.InfoSearchCreatedBy.Sum = _repositoryDemandePlacementFacultative.GetSum(criteria, o => o.CreatedBy.Value);     
               
            if (itemToSearch.InfoSearchModifiedBy.IsSumField)
            itemToReturn.InfoSearchModifiedBy.Sum = _repositoryDemandePlacementFacultative.GetSum(criteria, o => o.ModifiedBy.Value);     
               
  
          return itemToReturn;
      }
  }
}

