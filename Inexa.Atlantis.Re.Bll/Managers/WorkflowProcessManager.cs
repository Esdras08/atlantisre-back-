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
    public partial class WorkflowProcessManager
    {

        #region Singleton

        private WorkflowProcessManager() { }
      
        private static WorkflowProcessManager _instance;

        public static WorkflowProcessManager Instance
        {
            get { return _instance ?? (_instance = new WorkflowProcessManager()); }
        }

        #endregion

        public BusinessResponse<WorkflowProcessDto> GetWorkflowProcessById(object id)
        {
            return WorkflowProcessProvider.Instance.GetWorkflowProcessById(id);
        }
        public BusinessResponse<WorkflowProcessDto> GetWorkflowProcesssByCriteria(BusinessRequest<WorkflowProcessDto> request)
        {
            return WorkflowProcessProvider.Instance.GetWorkflowProcesssByCriteria(request);
        }

        public BusinessResponse<WorkflowProcessDto> SaveWorkflowProcesss(BusinessRequest<WorkflowProcessDto> request)
        {
            var response = new BusinessResponse<WorkflowProcessDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response = WorkflowProcessProvider.Instance.SaveWorkflowProcesss(request);

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

