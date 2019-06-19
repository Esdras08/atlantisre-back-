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
    public partial class PayManager
    {
        PayProvider _PayProvider;
        #region Singleton

        public PayManager() {
           _PayProvider   = new PayProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<PayDto>> GetPayById(object id)
        {
            return await _PayProvider.GetPayById(id);
        }
        public async Task<BusinessResponse<PayDto>> GetPaysByCriteria(BusinessRequest<PayDto> request)
        {
            return await _PayProvider.GetPaysByCriteria(request);
        }

        public async Task<BusinessResponse<PayDto>> SavePays(BusinessRequest<PayDto> request)
        {
            var response = new BusinessResponse<PayDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _PayProvider.SavePays(request);

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

