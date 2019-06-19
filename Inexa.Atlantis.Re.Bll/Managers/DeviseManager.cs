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
    public partial class DeviseManager
    {
        DeviseProvider _DeviseProvider;
        #region Singleton

        public DeviseManager() {
           _DeviseProvider   = new DeviseProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<DeviseDto>> GetDeviseById(object id)
        {
            return await _DeviseProvider.GetDeviseById(id);
        }
        public async Task<BusinessResponse<DeviseDto>> GetDevisesByCriteria(BusinessRequest<DeviseDto> request)
        {
            return await _DeviseProvider.GetDevisesByCriteria(request);
        }

        public async Task<BusinessResponse<DeviseDto>> SaveDevises(BusinessRequest<DeviseDto> request)
        {
            var response = new BusinessResponse<DeviseDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _DeviseProvider.SaveDevises(request);

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

