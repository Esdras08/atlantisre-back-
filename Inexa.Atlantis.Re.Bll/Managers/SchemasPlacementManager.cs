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
    public partial class SchemasPlacementManager
    {
        SchemasPlacementProvider _SchemasPlacementProvider;
        #region Singleton

        public SchemasPlacementManager() {
           _SchemasPlacementProvider   = new SchemasPlacementProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<SchemasPlacementDto>> GetSchemasPlacementById(object id)
        {
            return await _SchemasPlacementProvider.GetSchemasPlacementById(id);
        }
        public async Task<BusinessResponse<SchemasPlacementDto>> GetSchemasPlacementsByCriteria(BusinessRequest<SchemasPlacementDto> request)
        {
            return await _SchemasPlacementProvider.GetSchemasPlacementsByCriteria(request);
        }

        public async Task<BusinessResponse<SchemasPlacementDto>> SaveSchemasPlacements(BusinessRequest<SchemasPlacementDto> request)
        {
            var response = new BusinessResponse<SchemasPlacementDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _SchemasPlacementProvider.SaveSchemasPlacements(request);

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

