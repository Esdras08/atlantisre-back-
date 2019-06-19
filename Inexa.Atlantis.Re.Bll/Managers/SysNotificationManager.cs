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
    public partial class SysNotificationManager
    {
        SysNotificationProvider _SysNotificationProvider;
        #region Singleton

        public SysNotificationManager() {
           _SysNotificationProvider   = new SysNotificationProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<SysNotificationDto>> GetSysNotificationById(object id)
        {
            return await _SysNotificationProvider.GetSysNotificationById(id);
        }
        public async Task<BusinessResponse<SysNotificationDto>> GetSysNotificationsByCriteria(BusinessRequest<SysNotificationDto> request)
        {
            return await _SysNotificationProvider.GetSysNotificationsByCriteria(request);
        }

        public async Task<BusinessResponse<SysNotificationDto>> SaveSysNotifications(BusinessRequest<SysNotificationDto> request)
        {
            var response = new BusinessResponse<SysNotificationDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _SysNotificationProvider.SaveSysNotifications(request);

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

