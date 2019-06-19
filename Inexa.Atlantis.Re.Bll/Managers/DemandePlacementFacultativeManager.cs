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
    public partial class DemandePlacementFacultativeManager
    {
        DemandePlacementFacultativeProvider _DemandePlacementFacultativeProvider;
        #region Singleton

        public DemandePlacementFacultativeManager() {
           _DemandePlacementFacultativeProvider   = new DemandePlacementFacultativeProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<DemandePlacementFacultativeDto>> GetDemandePlacementFacultativeById(object id)
        {
            return await _DemandePlacementFacultativeProvider.GetDemandePlacementFacultativeById(id);
        }
        public async Task<BusinessResponse<DemandePlacementFacultativeDto>> GetDemandePlacementFacultativesByCriteria(BusinessRequest<DemandePlacementFacultativeDto> request)
        {
            return await _DemandePlacementFacultativeProvider.GetDemandePlacementFacultativesByCriteria(request);
        }

        public async Task<BusinessResponse<DemandePlacementFacultativeDto>> SaveDemandePlacementFacultatives(BusinessRequest<DemandePlacementFacultativeDto> request)
        {
            var response = new BusinessResponse<DemandePlacementFacultativeDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _DemandePlacementFacultativeProvider.SaveDemandePlacementFacultatives(request);

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

