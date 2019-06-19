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
    public partial class ViewSysStrategieMotDePasseManager
    {
        ViewSysStrategieMotDePasseProvider _ViewSysStrategieMotDePasseProvider;
        #region Singleton

        public ViewSysStrategieMotDePasseManager() {
           _ViewSysStrategieMotDePasseProvider   = new ViewSysStrategieMotDePasseProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<ViewSysStrategieMotDePasseDto>> GetViewSysStrategieMotDePassesByCriteria(BusinessRequest<ViewSysStrategieMotDePasseDto> request)
        {
            return await _ViewSysStrategieMotDePasseProvider.GetViewSysStrategieMotDePassesByCriteria(request);
        }

        public async Task<BusinessResponse<ViewSysStrategieMotDePasseDto>> SaveViewSysStrategieMotDePasses(BusinessRequest<ViewSysStrategieMotDePasseDto> request)
        {
            var response = new BusinessResponse<ViewSysStrategieMotDePasseDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    



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

