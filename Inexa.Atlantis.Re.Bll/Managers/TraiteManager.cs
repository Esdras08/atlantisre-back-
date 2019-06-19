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
    public partial class TraiteManager
    {
        TraiteProvider _TraiteProvider;
        #region Singleton

        public TraiteManager() {
           _TraiteProvider   = new TraiteProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<TraiteDto>> GetTraiteById(object id)
        {
            return await _TraiteProvider.GetTraiteById(id);
        }
        public async Task<BusinessResponse<TraiteDto>> GetTraitesByCriteria(BusinessRequest<TraiteDto> request)
        {
            return await _TraiteProvider.GetTraitesByCriteria(request);
        }

        public async Task<BusinessResponse<TraiteDto>> SaveTraites(BusinessRequest<TraiteDto> request)
        {
            var response = new BusinessResponse<TraiteDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _TraiteProvider.SaveTraites(request);

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

