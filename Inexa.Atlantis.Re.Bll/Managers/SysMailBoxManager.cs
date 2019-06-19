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
    public partial class SysMailBoxManager
    {
        SysMailBoxProvider _SysMailBoxProvider;
        #region Singleton

        public SysMailBoxManager() {
           _SysMailBoxProvider   = new SysMailBoxProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<SysMailBoxDto>> GetSysMailBoxById(object id)
        {
            return await _SysMailBoxProvider.GetSysMailBoxById(id);
        }
        public async Task<BusinessResponse<SysMailBoxDto>> GetSysMailBoxsByCriteria(BusinessRequest<SysMailBoxDto> request)
        {
            return await _SysMailBoxProvider.GetSysMailBoxsByCriteria(request);
        }

        public async Task<BusinessResponse<SysMailBoxDto>> SaveSysMailBoxs(BusinessRequest<SysMailBoxDto> request)
        {
            var response = new BusinessResponse<SysMailBoxDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _SysMailBoxProvider.SaveSysMailBoxs(request);

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

