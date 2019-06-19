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
    public partial class WorkflowCircuitManager
    {

        #region Singleton

        private WorkflowCircuitManager() { }
      
        private static WorkflowCircuitManager _instance;

        public static WorkflowCircuitManager Instance
        {
            get { return _instance ?? (_instance = new WorkflowCircuitManager()); }
        }

        #endregion

        public BusinessResponse<WorkflowCircuitDto> GetWorkflowCircuitById(object id)
        {
            return WorkflowCircuitProvider.Instance.GetWorkflowCircuitById(id);
        }
        public BusinessResponse<WorkflowCircuitDto> GetWorkflowCircuitsByCriteria(BusinessRequest<WorkflowCircuitDto> request)
        {
            return WorkflowCircuitProvider.Instance.GetWorkflowCircuitsByCriteria(request);
        }

        public BusinessResponse<WorkflowCircuitDto> SaveWorkflowCircuits(BusinessRequest<WorkflowCircuitDto> request)
        {
            var response = new BusinessResponse<WorkflowCircuitDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response = WorkflowCircuitProvider.Instance.SaveWorkflowCircuits(request);

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

