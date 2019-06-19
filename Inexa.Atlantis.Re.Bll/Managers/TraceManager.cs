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
    public partial class TraceManager
    {
        TraceProvider _TraceProvider;
        #region Singleton

        public TraceManager() {
           _TraceProvider   = new TraceProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<TraceDto>> GetTraceById(object id)
        {
            return await _TraceProvider.GetTraceById(id);
        }
        public async Task<BusinessResponse<TraceDto>> GetTracesByCriteria(BusinessRequest<TraceDto> request)
        {
            return await _TraceProvider.GetTracesByCriteria(request);
        }

        public async Task<BusinessResponse<TraceDto>> SaveTraces(BusinessRequest<TraceDto> request)
        {
            var response = new BusinessResponse<TraceDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _TraceProvider.SaveTraces(request);

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

