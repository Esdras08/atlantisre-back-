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
  public class DeclarationSinistreProvider
  {

      #region Singleton
      
      private static DeclarationSinistreProvider _instance;

      public static DeclarationSinistreProvider Instance
      {
          get { return _instance ?? (_instance = new DeclarationSinistreProvider()); }
      }

      #endregion

      private readonly IRepository<DeclarationSinistre> _repositoryDeclarationSinistre;
     
      public  DeclarationSinistreProvider()
      {
          _repositoryDeclarationSinistre = new Repository<DeclarationSinistre>();
      }

      private DeclarationSinistreProvider(IRepository<DeclarationSinistre> repository)
      {    
          _repositoryDeclarationSinistre = repository;
      }

      public async Task<BusinessResponse<DeclarationSinistreDto>> GetDeclarationSinistreById(object id)
      {
          var response = new BusinessResponse<DeclarationSinistreDto>();

          try
          {
              var item = _repositoryDeclarationSinistre[id];
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
      public async Task<BusinessResponse<DeclarationSinistreDto>> GetDeclarationSinistresByCriteria(BusinessRequest<DeclarationSinistreDto> request)
      {
          var response = new BusinessResponse<DeclarationSinistreDto>();

          try
          {
              request.ToFinalRequest();

              // générer expression lambda de l'objet de recherche principal
              var mainexp = GenerateCriteria(request.ItemToSearch);                  
              mainexp = (mainexp == null) ? obj => obj.IsDeleted == false : mainexp.And(obj => obj.IsDeleted == false);                   

              // générer expression lambda des objets de recherche sécondaires
              if (request.ItemsToSearch == null) 
                request.ItemsToSearch = new List<DeclarationSinistreDto>();                                                    
 
              var exps = new List<Expression<Func<DeclarationSinistre, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<DeclarationSinistre, bool>> exp = null;                  
              foreach (var expi in exps)
              {
                  if(request.NotIn)
                      exp = (exp == null) ? expi : exp.And(expi);
                  else
                      exp = (exp == null) ? expi : exp.Or(expi); 
              }

                  // concaténer les critères principaux et sécondaires
                  exp = (exp == null) ? mainexp : mainexp.And(exp);                  
                  
                  Expression<Func<DeclarationSinistre, object>> orderByExp = GenerateOrderByExpression(request.ItemToSearch);
                  if (string.IsNullOrEmpty(request.SortOrder) || (request.SortOrder != MySortOrder.Ascending && request.SortOrder != MySortOrder.Descending))
                    request.SortOrder = MySortOrder.Descending;

                  int totalRecord;
                  IEnumerable<DeclarationSinistre> items = _repositoryDeclarationSinistre.GetMany(exp, request.Index, request.Size, out totalRecord, orderByExp, request.SortOrder);
  
                  response.Count = totalRecord;
                  items = items as IList<DeclarationSinistre> ?? items.ToList();
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
      public BusinessResponse<decimal> GetSumDeclarationSinistresByCriteria(BusinessRequest<DeclarationSinistreDto> request)
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
                request.ItemsToSearch = new List<DeclarationSinistreDto>();                                                    
 
              var exps = new List<Expression<Func<DeclarationSinistre, bool>>>();                    
              foreach (var itemToSearch in request.ItemsToSearch)
              {
                  var expi = GenerateCriteria(itemToSearch);

                  if (expi != null) exps.Add(expi);                    
              }                       
   
              Expression<Func<DeclarationSinistre, bool>> exp = null;                  
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

                  var sum = _repositoryDeclarationSinistre.GetSum(exp, sumExp);
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
  		public async Task<BusinessResponse<DeclarationSinistreDto>> SaveDeclarationSinistres(BusinessRequest<DeclarationSinistreDto> request)
  		{
        var response = new BusinessResponse<DeclarationSinistreDto>();
  
              try
              {
                  foreach (var item in request.ItemsToSave.TakeWhile(BusinessRequest<DeclarationSinistreDto>.ValidateRequiredProperties))
                  {
                     var itemToSave = item.ToEntity();
                     DeclarationSinistre itemSaved = null;

                     itemToSave.DateMAJ = Utilities.CurrentDate; 
                     itemToSave.ModifiedBy = request.IdCurrentUser; 
                     if (itemToSave.IdDeclarationSinistre == 0)  
                     {  
                        itemToSave.DateCreation = Utilities.CurrentDate;   
                        itemToSave.CreatedBy = request.IdCurrentUser;   

                        itemToSave.IsDeleted = false;
                        itemSaved = _repositoryDeclarationSinistre.Add(itemToSave);
                     }
                     else //dans le cas d'une modification
                     {
                         itemSaved = _repositoryDeclarationSinistre.Update(itemToSave, p => p.IdDeclarationSinistre == itemToSave.IdDeclarationSinistre);
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
      private static Expression<Func<DeclarationSinistre, bool>> GenerateCriteria(DeclarationSinistreDto itemToSearch) 
      {
          Expression<Func<DeclarationSinistre, bool>> exprinit = obj => true;
          Expression<Func<DeclarationSinistre, bool>> expi = exprinit;
                                                             
          if (itemToSearch.InfoSearchIdDeclarationSinistre.Consider)
          {
              switch (itemToSearch.InfoSearchIdDeclarationSinistre.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.IdDeclarationSinistre != itemToSearch.IdDeclarationSinistre);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.IdDeclarationSinistre < itemToSearch.IdDeclarationSinistre);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.IdDeclarationSinistre <= itemToSearch.IdDeclarationSinistre);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.IdDeclarationSinistre > itemToSearch.IdDeclarationSinistre);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.IdDeclarationSinistre >= itemToSearch.IdDeclarationSinistre);                  
                      break;  
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.IdDeclarationSinistre == itemToSearch.IdDeclarationSinistre);                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchDateSurvenance.Consider)
          {
              switch (itemToSearch.InfoSearchDateSurvenance.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And
                            (obj => 
                                !(obj.DateSurvenance.Value.Year == itemToSearch.DateSurvenance.Value.Year
                                && obj.DateSurvenance.Value.Month == itemToSearch.DateSurvenance.Value.Month
                                && obj.DateSurvenance.Value.Day == itemToSearch.DateSurvenance.Value.Day)
                             );
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateSurvenance.Value, itemToSearch.DateSurvenance.Value) < 0);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateSurvenance.Value, itemToSearch.DateSurvenance.Value) <= 0);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateSurvenance.Value, itemToSearch.DateSurvenance.Value) > 0);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateSurvenance.Value, itemToSearch.DateSurvenance.Value) >= 0);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      var debut = itemToSearch.InfoSearchDateSurvenance.Intervalle.Debut.Date;
                      var fin = itemToSearch.InfoSearchDateSurvenance.Intervalle.Fin.AddDays(1).Date;
                      expi = expi.And(obj => obj.DateSurvenance >= debut && obj.DateSurvenance < fin);                  
                      break;         
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And
                            (
                              obj => obj.DateSurvenance.Value.Year == itemToSearch.DateSurvenance.Value.Year
                              && obj.DateSurvenance.Value.Month == itemToSearch.DateSurvenance.Value.Month
                              && obj.DateSurvenance.Value.Day == itemToSearch.DateSurvenance.Value.Day
                             );                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchDateDeclaration.Consider)
          {
              switch (itemToSearch.InfoSearchDateDeclaration.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And
                            (obj => 
                                !(obj.DateDeclaration.Value.Year == itemToSearch.DateDeclaration.Value.Year
                                && obj.DateDeclaration.Value.Month == itemToSearch.DateDeclaration.Value.Month
                                && obj.DateDeclaration.Value.Day == itemToSearch.DateDeclaration.Value.Day)
                             );
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateDeclaration.Value, itemToSearch.DateDeclaration.Value) < 0);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateDeclaration.Value, itemToSearch.DateDeclaration.Value) <= 0);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateDeclaration.Value, itemToSearch.DateDeclaration.Value) > 0);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => Nullable.Compare<DateTime>(obj.DateDeclaration.Value, itemToSearch.DateDeclaration.Value) >= 0);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      var debut = itemToSearch.InfoSearchDateDeclaration.Intervalle.Debut.Date;
                      var fin = itemToSearch.InfoSearchDateDeclaration.Intervalle.Fin.AddDays(1).Date;
                      expi = expi.And(obj => obj.DateDeclaration >= debut && obj.DateDeclaration < fin);                  
                      break;         
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And
                            (
                              obj => obj.DateDeclaration.Value.Year == itemToSearch.DateDeclaration.Value.Year
                              && obj.DateDeclaration.Value.Month == itemToSearch.DateDeclaration.Value.Month
                              && obj.DateDeclaration.Value.Day == itemToSearch.DateDeclaration.Value.Day
                             );                  
                      break;
              }
          }              

          if (itemToSearch.InfoSearchNatureSinistre.Consider)
          {
              switch (itemToSearch.InfoSearchNatureSinistre.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.NatureSinistre != itemToSearch.NatureSinistre);
                      break;
                  case OperatorEnum.STARSTWITH :
                      expi = expi.And(obj => obj.NatureSinistre.StartsWith(itemToSearch.NatureSinistre));
                      break;
                  case OperatorEnum.ENDSWITH :
                      expi = expi.And(obj => obj.NatureSinistre.EndsWith(itemToSearch.NatureSinistre));
                      break;
                  case OperatorEnum.EQUAL :
                      expi = expi.And(obj => obj.NatureSinistre == itemToSearch.NatureSinistre);                  
                      break;              
                  default:
                      // opérateur par défaut : contains
                      expi = expi.And(obj => obj.NatureSinistre.Contains(itemToSearch.NatureSinistre));
                      break;
              }
          }                               
                 
          if (itemToSearch.InfoSearchEvaluation.Consider)
          {
              switch (itemToSearch.InfoSearchEvaluation.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.Evaluation != itemToSearch.Evaluation);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.Evaluation < itemToSearch.Evaluation);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.Evaluation <= itemToSearch.Evaluation);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.Evaluation > itemToSearch.Evaluation);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.Evaluation >= itemToSearch.Evaluation);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.Evaluation >= itemToSearch.InfoSearchEvaluation.Intervalle.Debut && obj.Evaluation <= itemToSearch.InfoSearchEvaluation.Intervalle.Fin);                         
                      break;    
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.Evaluation == itemToSearch.Evaluation);                  
                      break;
              }
          }            

          if (itemToSearch.InfoSearchHonoraireExperts.Consider)
          {
              switch (itemToSearch.InfoSearchHonoraireExperts.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.HonoraireExperts != itemToSearch.HonoraireExperts);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.HonoraireExperts < itemToSearch.HonoraireExperts);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.HonoraireExperts <= itemToSearch.HonoraireExperts);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.HonoraireExperts > itemToSearch.HonoraireExperts);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.HonoraireExperts >= itemToSearch.HonoraireExperts);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.HonoraireExperts >= itemToSearch.InfoSearchHonoraireExperts.Intervalle.Debut && obj.HonoraireExperts <= itemToSearch.InfoSearchHonoraireExperts.Intervalle.Fin);                         
                      break;    
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.HonoraireExperts == itemToSearch.HonoraireExperts);                  
                      break;
              }
          }            

          if (itemToSearch.InfoSearchMontantPaye.Consider)
          {
              switch (itemToSearch.InfoSearchMontantPaye.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.MontantPaye != itemToSearch.MontantPaye);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.MontantPaye < itemToSearch.MontantPaye);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.MontantPaye <= itemToSearch.MontantPaye);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.MontantPaye > itemToSearch.MontantPaye);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.MontantPaye >= itemToSearch.MontantPaye);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.MontantPaye >= itemToSearch.InfoSearchMontantPaye.Intervalle.Debut && obj.MontantPaye <= itemToSearch.InfoSearchMontantPaye.Intervalle.Fin);                         
                      break;    
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.MontantPaye == itemToSearch.MontantPaye);                  
                      break;
              }
          }            

          if (itemToSearch.InfoSearchMontantRestant.Consider)
          {
              switch (itemToSearch.InfoSearchMontantRestant.OperatorToUse)
              {
                  case OperatorEnum.NOTEQUAL:
                      expi = expi.And(obj => obj.MontantRestant != itemToSearch.MontantRestant);
                      break;
                  case OperatorEnum.LESS :
                      expi = expi.And(obj => obj.MontantRestant < itemToSearch.MontantRestant);                  
                      break; 
                  case OperatorEnum.LESSOREQUAL :
                      expi = expi.And(obj => obj.MontantRestant <= itemToSearch.MontantRestant);                  
                      break;  
                  case OperatorEnum.MORE :
                      expi = expi.And(obj => obj.MontantRestant > itemToSearch.MontantRestant);                  
                      break; 
                  case OperatorEnum.MOREOREQUAL :
                      expi = expi.And(obj => obj.MontantRestant >= itemToSearch.MontantRestant);                  
                      break;  
                  case OperatorEnum.BETWEEN :
                      expi = expi.And(obj => obj.MontantRestant >= itemToSearch.InfoSearchMontantRestant.Intervalle.Debut && obj.MontantRestant <= itemToSearch.InfoSearchMontantRestant.Intervalle.Fin);                         
                      break;    
                  default:
                      // opérateur par défaut : equal
                      expi = expi.And(obj => obj.MontantRestant == itemToSearch.MontantRestant);                  
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
      private static Expression<Func<DeclarationSinistre, object>> GenerateOrderByExpression(DeclarationSinistreDto itemToSearch)
      {
          Expression<Func<DeclarationSinistre, object>> exp = null;

          if (itemToSearch.InfoSearchIdDeclarationSinistre.IsOrderByField)
            exp = (o => o.IdDeclarationSinistre);
          if (itemToSearch.InfoSearchDateSurvenance.IsOrderByField)
            exp = (o => o.DateSurvenance);
          if (itemToSearch.InfoSearchDateDeclaration.IsOrderByField)
            exp = (o => o.DateDeclaration);
          if (itemToSearch.InfoSearchNatureSinistre.IsOrderByField)
            exp = (o => o.NatureSinistre);
          if (itemToSearch.InfoSearchEvaluation.IsOrderByField)
            exp = (o => o.Evaluation);
          if (itemToSearch.InfoSearchHonoraireExperts.IsOrderByField)
            exp = (o => o.HonoraireExperts);
          if (itemToSearch.InfoSearchMontantPaye.IsOrderByField)
            exp = (o => o.MontantPaye);
          if (itemToSearch.InfoSearchMontantRestant.IsOrderByField)
            exp = (o => o.MontantRestant);
          if (itemToSearch.InfoSearchIdProcessus.IsOrderByField)
            exp = (o => o.IdProcessus);
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

          return exp ?? (obj => obj.IdDeclarationSinistre);
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private static Expression<Func<DeclarationSinistre, decimal?>> GenerateSumExpression(DeclarationSinistreDto itemToSearch)
      {
          Expression<Func<DeclarationSinistre, decimal?>> exp = null;

          if (itemToSearch.InfoSearchEvaluation.IsSumField)
            exp = (o => o.Evaluation.Value);                     
            if (itemToSearch.InfoSearchHonoraireExperts.IsSumField)
            exp = (o => o.HonoraireExperts.Value);                     
            if (itemToSearch.InfoSearchMontantPaye.IsSumField)
            exp = (o => o.MontantPaye.Value);                     
            if (itemToSearch.InfoSearchMontantRestant.IsSumField)
            exp = (o => o.MontantRestant.Value);                     
            if (itemToSearch.InfoSearchCreatedBy.IsSumField)
            exp = (o => o.CreatedBy.Value);                     
            if (itemToSearch.InfoSearchModifiedBy.IsSumField)
            exp = (o => o.ModifiedBy.Value);                     
  
          //return exp ?? (obj => obj.IdDeclarationSinistre);
          return exp;
      }

      /// <summary>
      /// Générer une expression lambda à partir d'un DTO
      /// </summary>
      /// <param name="itemToSearch">objet DTO contenant les champs de recherche</param>
      /// <returns>Expression lambda</returns>
      private DeclarationSinistreDto GenerateSum(Expression<Func<DeclarationSinistre, bool>> criteria, DeclarationSinistreDto itemToSearch, DeclarationSinistreDto itemToReturn)
      {
          if (itemToSearch.InfoSearchEvaluation.IsSumField)
            itemToReturn.InfoSearchEvaluation.Sum = _repositoryDeclarationSinistre.GetSum(criteria, o => o.Evaluation.Value);     
               
            if (itemToSearch.InfoSearchHonoraireExperts.IsSumField)
            itemToReturn.InfoSearchHonoraireExperts.Sum = _repositoryDeclarationSinistre.GetSum(criteria, o => o.HonoraireExperts.Value);     
               
            if (itemToSearch.InfoSearchMontantPaye.IsSumField)
            itemToReturn.InfoSearchMontantPaye.Sum = _repositoryDeclarationSinistre.GetSum(criteria, o => o.MontantPaye.Value);     
               
            if (itemToSearch.InfoSearchMontantRestant.IsSumField)
            itemToReturn.InfoSearchMontantRestant.Sum = _repositoryDeclarationSinistre.GetSum(criteria, o => o.MontantRestant.Value);     
               
            if (itemToSearch.InfoSearchCreatedBy.IsSumField)
            itemToReturn.InfoSearchCreatedBy.Sum = _repositoryDeclarationSinistre.GetSum(criteria, o => o.CreatedBy.Value);     
               
            if (itemToSearch.InfoSearchModifiedBy.IsSumField)
            itemToReturn.InfoSearchModifiedBy.Sum = _repositoryDeclarationSinistre.GetSum(criteria, o => o.ModifiedBy.Value);     
               
  
          return itemToReturn;
      }
  }
}

