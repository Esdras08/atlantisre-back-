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
    public partial class ControlManager
    {

        #region Singleton

        private ControlManager() { }
      
        private static ControlManager _instance;

        public static ControlManager Instance
        {
            get { return _instance ?? (_instance = new ControlManager()); }
        }

        #endregion

        public BusinessResponse<ControlDto> GetControlById(object id)
        {
            return ControlProvider.Instance.GetControlById(id);
        }
        public BusinessResponse<ControlDto> GetControlsByCriteria(BusinessRequest<ControlDto> request)
        {
            return ControlProvider.Instance.GetControlsByCriteria(request);
        }

        public BusinessResponse<ControlDto> SaveControls(BusinessRequest<ControlDto> request)
        {
            var response = new BusinessResponse<ControlDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response = ControlProvider.Instance.SaveControls(request);

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

