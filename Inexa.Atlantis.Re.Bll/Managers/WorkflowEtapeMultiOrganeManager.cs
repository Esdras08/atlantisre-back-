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
    public partial class WorkflowEtapeMultiOrganeManager
    {

        #region Singleton

        private WorkflowEtapeMultiOrganeManager() { }
      
        private static WorkflowEtapeMultiOrganeManager _instance;

        public static WorkflowEtapeMultiOrganeManager Instance
        {
            get { return _instance ?? (_instance = new WorkflowEtapeMultiOrganeManager()); }
        }

        #endregion

        public BusinessResponse<WorkflowEtapeMultiOrganeDto> GetWorkflowEtapeMultiOrganeById(object id)
        {
            return WorkflowEtapeMultiOrganeProvider.Instance.GetWorkflowEtapeMultiOrganeById(id);
        }
        public BusinessResponse<WorkflowEtapeMultiOrganeDto> GetWorkflowEtapeMultiOrganesByCriteria(BusinessRequest<WorkflowEtapeMultiOrganeDto> request)
        {
            return WorkflowEtapeMultiOrganeProvider.Instance.GetWorkflowEtapeMultiOrganesByCriteria(request);
        }

        public BusinessResponse<WorkflowEtapeMultiOrganeDto> SaveWorkflowEtapeMultiOrganes(BusinessRequest<WorkflowEtapeMultiOrganeDto> request)
        {
            var response = new BusinessResponse<WorkflowEtapeMultiOrganeDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response = WorkflowEtapeMultiOrganeProvider.Instance.SaveWorkflowEtapeMultiOrganes(request);

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

