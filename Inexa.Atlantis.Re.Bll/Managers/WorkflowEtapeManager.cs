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
    public partial class WorkflowEtapeManager
    {

        #region Singleton

        private WorkflowEtapeManager() { }
      
        private static WorkflowEtapeManager _instance;

        public static WorkflowEtapeManager Instance
        {
            get { return _instance ?? (_instance = new WorkflowEtapeManager()); }
        }

        #endregion

        public BusinessResponse<WorkflowEtapeDto> GetWorkflowEtapeById(object id)
        {
            return WorkflowEtapeProvider.Instance.GetWorkflowEtapeById(id);
        }
        public BusinessResponse<WorkflowEtapeDto> GetWorkflowEtapesByCriteria(BusinessRequest<WorkflowEtapeDto> request)
        {
            return WorkflowEtapeProvider.Instance.GetWorkflowEtapesByCriteria(request);
        }

        public BusinessResponse<WorkflowEtapeDto> SaveWorkflowEtapes(BusinessRequest<WorkflowEtapeDto> request)
        {
            var response = new BusinessResponse<WorkflowEtapeDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response = WorkflowEtapeProvider.Instance.SaveWorkflowEtapes(request);

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

