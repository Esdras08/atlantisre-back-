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
    public partial class WorkflowEtapeEntiteManager
    {

        #region Singleton

        private WorkflowEtapeEntiteManager() { }
      
        private static WorkflowEtapeEntiteManager _instance;

        public static WorkflowEtapeEntiteManager Instance
        {
            get { return _instance ?? (_instance = new WorkflowEtapeEntiteManager()); }
        }

        #endregion

        public BusinessResponse<WorkflowEtapeEntiteDto> GetWorkflowEtapeEntiteById(object id)
        {
            return WorkflowEtapeEntiteProvider.Instance.GetWorkflowEtapeEntiteById(id);
        }
        public BusinessResponse<WorkflowEtapeEntiteDto> GetWorkflowEtapeEntitesByCriteria(BusinessRequest<WorkflowEtapeEntiteDto> request)
        {
            return WorkflowEtapeEntiteProvider.Instance.GetWorkflowEtapeEntitesByCriteria(request);
        }

        public BusinessResponse<WorkflowEtapeEntiteDto> SaveWorkflowEtapeEntites(BusinessRequest<WorkflowEtapeEntiteDto> request)
        {
            var response = new BusinessResponse<WorkflowEtapeEntiteDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response = WorkflowEtapeEntiteProvider.Instance.SaveWorkflowEtapeEntites(request);

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

