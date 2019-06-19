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
    public partial class WorkflowEtudeManager
    {

        #region Singleton

        private WorkflowEtudeManager() { }
      
        private static WorkflowEtudeManager _instance;

        public static WorkflowEtudeManager Instance
        {
            get { return _instance ?? (_instance = new WorkflowEtudeManager()); }
        }

        #endregion

        public BusinessResponse<WorkflowEtudeDto> GetWorkflowEtudeById(object id)
        {
            return WorkflowEtudeProvider.Instance.GetWorkflowEtudeById(id);
        }
        public BusinessResponse<WorkflowEtudeDto> GetWorkflowEtudesByCriteria(BusinessRequest<WorkflowEtudeDto> request)
        {
            return WorkflowEtudeProvider.Instance.GetWorkflowEtudesByCriteria(request);
        }

        public BusinessResponse<WorkflowEtudeDto> SaveWorkflowEtudes(BusinessRequest<WorkflowEtudeDto> request)
        {
            var response = new BusinessResponse<WorkflowEtudeDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response = WorkflowEtudeProvider.Instance.SaveWorkflowEtudes(request);

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

