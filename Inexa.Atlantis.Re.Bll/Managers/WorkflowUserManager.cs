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
    public partial class WorkflowUserManager
    {

        #region Singleton

        private WorkflowUserManager() { }
      
        private static WorkflowUserManager _instance;

        public static WorkflowUserManager Instance
        {
            get { return _instance ?? (_instance = new WorkflowUserManager()); }
        }

        #endregion

        public BusinessResponse<WorkflowUserDto> GetWorkflowUserById(object id)
        {
            return WorkflowUserProvider.Instance.GetWorkflowUserById(id);
        }
        public BusinessResponse<WorkflowUserDto> GetWorkflowUsersByCriteria(BusinessRequest<WorkflowUserDto> request)
        {
            return WorkflowUserProvider.Instance.GetWorkflowUsersByCriteria(request);
        }

        public BusinessResponse<WorkflowUserDto> SaveWorkflowUsers(BusinessRequest<WorkflowUserDto> request)
        {
            var response = new BusinessResponse<WorkflowUserDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response = WorkflowUserProvider.Instance.SaveWorkflowUsers(request);

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

