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
    public partial class SysStrategieMotDePasseHistoManager
    {
        SysStrategieMotDePasseHistoProvider _SysStrategieMotDePasseHistoProvider;
        #region Singleton

        public SysStrategieMotDePasseHistoManager() {
           _SysStrategieMotDePasseHistoProvider   = new SysStrategieMotDePasseHistoProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<SysStrategieMotDePasseHistoDto>> GetSysStrategieMotDePasseHistoById(object id)
        {
            return await _SysStrategieMotDePasseHistoProvider.GetSysStrategieMotDePasseHistoById(id);
        }
        public async Task<BusinessResponse<SysStrategieMotDePasseHistoDto>> GetSysStrategieMotDePasseHistosByCriteria(BusinessRequest<SysStrategieMotDePasseHistoDto> request)
        {
            return await _SysStrategieMotDePasseHistoProvider.GetSysStrategieMotDePasseHistosByCriteria(request);
        }

        public async Task<BusinessResponse<SysStrategieMotDePasseHistoDto>> SaveSysStrategieMotDePasseHistos(BusinessRequest<SysStrategieMotDePasseHistoDto> request)
        {
            var response = new BusinessResponse<SysStrategieMotDePasseHistoDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _SysStrategieMotDePasseHistoProvider.SaveSysStrategieMotDePasseHistos(request);

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

