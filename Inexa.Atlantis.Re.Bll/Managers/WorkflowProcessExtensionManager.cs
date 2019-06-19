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
    public partial class WorkflowProcessExtensionManager
    {

        #region Singleton

        private WorkflowProcessExtensionManager() { }
      
        private static WorkflowProcessExtensionManager _instance;

        public static WorkflowProcessExtensionManager Instance
        {
            get { return _instance ?? (_instance = new WorkflowProcessExtensionManager()); }
        }

        #endregion

        public BusinessResponse<WorkflowProcessExtensionDto> GetWorkflowProcessExtensionById(object id)
        {
            return WorkflowProcessExtensionProvider.Instance.GetWorkflowProcessExtensionById(id);
        }
        public BusinessResponse<WorkflowProcessExtensionDto> GetWorkflowProcessExtensionsByCriteria(BusinessRequest<WorkflowProcessExtensionDto> request)
        {
            return WorkflowProcessExtensionProvider.Instance.GetWorkflowProcessExtensionsByCriteria(request);
        }

        public BusinessResponse<WorkflowProcessExtensionDto> SaveWorkflowProcessExtensions(BusinessRequest<WorkflowProcessExtensionDto> request)
        {
            var response = new BusinessResponse<WorkflowProcessExtensionDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response = WorkflowProcessExtensionProvider.Instance.SaveWorkflowProcessExtensions(request);

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

