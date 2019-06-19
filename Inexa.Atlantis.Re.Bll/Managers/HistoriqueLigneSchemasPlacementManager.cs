using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inexa.Atlantis.Re.Dal.Providers;
using Inexa.Atlantis.Re.Commons.Dtos;
using Inexa.Atlantis.Re.Commons.Infras.Domains;
using Inexa.Atlantis.Re.Commons.Infras.Enums;
using Inexa.Atlantis.Re.Commons.Infras.Helpers;


namespace Inexa.Atlantis.Re.Bll.Managers
{
    public partial class HistoriqueLigneSchemasPlacementManager
    {
        HistoriqueLigneSchemasPlacementProvider _HistoriqueLigneSchemasPlacementProvider;
        #region Singleton

        public HistoriqueLigneSchemasPlacementManager() {
           _HistoriqueLigneSchemasPlacementProvider   = new HistoriqueLigneSchemasPlacementProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<HistoriqueLigneSchemasPlacementDto>> GetHistoriqueLigneSchemasPlacementById(object id)
        {
            return await _HistoriqueLigneSchemasPlacementProvider.GetHistoriqueLigneSchemasPlacementById(id);
        }
        public async Task<BusinessResponse<HistoriqueLigneSchemasPlacementDto>> GetHistoriqueLigneSchemasPlacementsByCriteria(BusinessRequest<HistoriqueLigneSchemasPlacementDto> request)
        {
            return await _HistoriqueLigneSchemasPlacementProvider.GetHistoriqueLigneSchemasPlacementsByCriteria(request);
        }

        public async Task<BusinessResponse<HistoriqueLigneSchemasPlacementDto>> SaveHistoriqueLigneSchemasPlacements(BusinessRequest<HistoriqueLigneSchemasPlacementDto> request)
        {
            var response = new BusinessResponse<HistoriqueLigneSchemasPlacementDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _HistoriqueLigneSchemasPlacementProvider.SaveHistoriqueLigneSchemasPlacements(request);

                        /*** Finir la logique ici ***/
                    }
                    catch (Exception ex)
                    {
                        CustomException.Write(request, response, ex);                       
                    }
                    finally
                    {
                        TransactionQueueManager.FinishWork(request, response);
                    }
            }            

            return response;
        }     
    }
}

