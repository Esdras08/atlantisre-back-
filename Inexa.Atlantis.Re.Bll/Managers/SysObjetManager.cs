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
    public partial class SysObjetManager
    {
        SysObjetProvider _SysObjetProvider;
        #region Singleton

        public SysObjetManager() {
           _SysObjetProvider   = new SysObjetProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<SysObjetDto>> GetSysObjetById(object id)
        {
            return await _SysObjetProvider.GetSysObjetById(id);
        }
        public async Task<BusinessResponse<SysObjetDto>> GetSysObjetsByCriteria(BusinessRequest<SysObjetDto> request)
        {
            return await _SysObjetProvider.GetSysObjetsByCriteria(request);
        }

        public async Task<BusinessResponse<SysObjetDto>> SaveSysObjets(BusinessRequest<SysObjetDto> request)
        {
            var response = new BusinessResponse<SysObjetDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _SysObjetProvider.SaveSysObjets(request);

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

