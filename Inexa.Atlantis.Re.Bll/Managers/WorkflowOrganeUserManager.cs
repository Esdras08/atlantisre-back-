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
    public partial class WorkflowOrganeUserManager
    {

        #region Singleton

        private WorkflowOrganeUserManager() { }
      
        private static WorkflowOrganeUserManager _instance;

        public static WorkflowOrganeUserManager Instance
        {
            get { return _instance ?? (_instance = new WorkflowOrganeUserManager()); }
        }

        #endregion

        public BusinessResponse<WorkflowOrganeUserDto> GetWorkflowOrganeUserById(object id)
        {
            return WorkflowOrganeUserProvider.Instance.GetWorkflowOrganeUserById(id);
        }
        public BusinessResponse<WorkflowOrganeUserDto> GetWorkflowOrganeUsersByCriteria(BusinessRequest<WorkflowOrganeUserDto> request)
        {
            return WorkflowOrganeUserProvider.Instance.GetWorkflowOrganeUsersByCriteria(request);
        }

        public BusinessResponse<WorkflowOrganeUserDto> SaveWorkflowOrganeUsers(BusinessRequest<WorkflowOrganeUserDto> request)
        {
            var response = new BusinessResponse<WorkflowOrganeUserDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response = WorkflowOrganeUserProvider.Instance.SaveWorkflowOrganeUsers(request);

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

