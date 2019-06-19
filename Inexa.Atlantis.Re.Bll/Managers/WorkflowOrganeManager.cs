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
    public partial class WorkflowOrganeManager
    {

        #region Singleton

        private WorkflowOrganeManager() { }
      
        private static WorkflowOrganeManager _instance;

        public static WorkflowOrganeManager Instance
        {
            get { return _instance ?? (_instance = new WorkflowOrganeManager()); }
        }

        #endregion

        public BusinessResponse<WorkflowOrganeDto> GetWorkflowOrganeById(object id)
        {
            return WorkflowOrganeProvider.Instance.GetWorkflowOrganeById(id);
        }
        public BusinessResponse<WorkflowOrganeDto> GetWorkflowOrganesByCriteria(BusinessRequest<WorkflowOrganeDto> request)
        {
            return WorkflowOrganeProvider.Instance.GetWorkflowOrganesByCriteria(request);
        }

        public BusinessResponse<WorkflowOrganeDto> SaveWorkflowOrganes(BusinessRequest<WorkflowOrganeDto> request)
        {
            var response = new BusinessResponse<WorkflowOrganeDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response = WorkflowOrganeProvider.Instance.SaveWorkflowOrganes(request);

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

