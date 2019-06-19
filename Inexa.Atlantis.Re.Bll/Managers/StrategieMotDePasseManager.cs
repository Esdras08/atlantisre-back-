using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inexa.Atlantis.Re.Dal.Providers;
using Inexa.Atlantis.Re.Commons.Dtos;
using Inexa.Atlantis.Re.Commons.Infras.Domains;
using Inexa.Atlantis.Re.Commons.Infras.Enums;
using Inexa.Atlantis.Re.Commons.Infras.Helpers;

namespace Inexa.Atlantis.Re.Bll.Managers
{
    public partial class StrategieMotDePasseManager
    {

        #region Singleton

        private StrategieMotDePasseManager() { }
      
        private static StrategieMotDePasseManager _instance;

        public static StrategieMotDePasseManager Instance
        {
            get { return _instance ?? (_instance = new StrategieMotDePasseManager()); }
        }

        #endregion

        public BusinessResponse<StrategieMotDePasseDto> GetStrategieMotDePasseById(object id)
        {
            return StrategieMotDePasseProvider.Instance.GetStrategieMotDePasseById(id);
        }
        public BusinessResponse<StrategieMotDePasseDto> GetStrategieMotDePassesByCriteria(BusinessRequest<StrategieMotDePasseDto> request)
        {
            return StrategieMotDePasseProvider.Instance.GetStrategieMotDePassesByCriteria(request);
        }

        public BusinessResponse<StrategieMotDePasseDto> SaveStrategieMotDePasses(BusinessRequest<StrategieMotDePasseDto> request)
        {
            var response = new BusinessResponse<StrategieMotDePasseDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response = StrategieMotDePasseProvider.Instance.SaveStrategieMotDePasses(request);

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

