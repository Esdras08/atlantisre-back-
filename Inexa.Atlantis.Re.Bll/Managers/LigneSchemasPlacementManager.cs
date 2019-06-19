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
    public partial class LigneSchemasPlacementManager
    {
        LigneSchemasPlacementProvider _LigneSchemasPlacementProvider;
        #region Singleton

        public LigneSchemasPlacementManager() {
           _LigneSchemasPlacementProvider   = new LigneSchemasPlacementProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<LigneSchemasPlacementDto>> GetLigneSchemasPlacementById(object id)
        {
            return await _LigneSchemasPlacementProvider.GetLigneSchemasPlacementById(id);
        }
        public async Task<BusinessResponse<LigneSchemasPlacementDto>> GetLigneSchemasPlacementsByCriteria(BusinessRequest<LigneSchemasPlacementDto> request)
        {
            return await _LigneSchemasPlacementProvider.GetLigneSchemasPlacementsByCriteria(request);
        }

        public async Task<BusinessResponse<LigneSchemasPlacementDto>> SaveLigneSchemasPlacements(BusinessRequest<LigneSchemasPlacementDto> request)
        {
            var response = new BusinessResponse<LigneSchemasPlacementDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _LigneSchemasPlacementProvider.SaveLigneSchemasPlacements(request);

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

