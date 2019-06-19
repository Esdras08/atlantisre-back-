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
    public partial class SysLogManager
    {
        SysLogProvider _SysLogProvider;
        #region Singleton

        public SysLogManager() {
           _SysLogProvider   = new SysLogProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<SysLogDto>> GetSysLogById(object id)
        {
            return await _SysLogProvider.GetSysLogById(id);
        }
        public async Task<BusinessResponse<SysLogDto>> GetSysLogsByCriteria(BusinessRequest<SysLogDto> request)
        {
            return await _SysLogProvider.GetSysLogsByCriteria(request);
        }

        public async Task<BusinessResponse<SysLogDto>> SaveSysLogs(BusinessRequest<SysLogDto> request)
        {
            var response = new BusinessResponse<SysLogDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _SysLogProvider.SaveSysLogs(request);

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

