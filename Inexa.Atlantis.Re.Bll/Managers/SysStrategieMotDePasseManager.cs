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
    public partial class SysStrategieMotDePasseManager
    {
        SysStrategieMotDePasseProvider _SysStrategieMotDePasseProvider;
        #region Singleton

        public SysStrategieMotDePasseManager() {
           _SysStrategieMotDePasseProvider   = new SysStrategieMotDePasseProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<SysStrategieMotDePasseDto>> GetSysStrategieMotDePasseById(object id)
        {
            return await _SysStrategieMotDePasseProvider.GetSysStrategieMotDePasseById(id);
        }
        public async Task<BusinessResponse<SysStrategieMotDePasseDto>> GetSysStrategieMotDePassesByCriteria(BusinessRequest<SysStrategieMotDePasseDto> request)
        {
            return await _SysStrategieMotDePasseProvider.GetSysStrategieMotDePassesByCriteria(request);
        }

        public async Task<BusinessResponse<SysStrategieMotDePasseDto>> SaveSysStrategieMotDePasses(BusinessRequest<SysStrategieMotDePasseDto> request)
        {
            var response = new BusinessResponse<SysStrategieMotDePasseDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _SysStrategieMotDePasseProvider.SaveSysStrategieMotDePasses(request);

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

