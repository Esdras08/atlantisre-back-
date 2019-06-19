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
    public partial class ProcessuManager
    {
        ProcessuProvider _ProcessuProvider;
        #region Singleton

        public ProcessuManager() {
           _ProcessuProvider   = new ProcessuProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<ProcessuDto>> GetProcessuById(object id)
        {
            return await _ProcessuProvider.GetProcessuById(id);
        }
        public async Task<BusinessResponse<ProcessuDto>> GetProcessusByCriteria(BusinessRequest<ProcessuDto> request)
        {
            return await _ProcessuProvider.GetProcessusByCriteria(request);
        }

        public async Task<BusinessResponse<ProcessuDto>> SaveProcessus(BusinessRequest<ProcessuDto> request)
        {
            var response = new BusinessResponse<ProcessuDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _ProcessuProvider.SaveProcessus(request);

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

