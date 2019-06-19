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
    public partial class WorkflowNoteManager
    {

        #region Singleton

        private WorkflowNoteManager() { }
      
        private static WorkflowNoteManager _instance;

        public static WorkflowNoteManager Instance
        {
            get { return _instance ?? (_instance = new WorkflowNoteManager()); }
        }

        #endregion

        public BusinessResponse<WorkflowNoteDto> GetWorkflowNoteById(object id)
        {
            return WorkflowNoteProvider.Instance.GetWorkflowNoteById(id);
        }
        public BusinessResponse<WorkflowNoteDto> GetWorkflowNotesByCriteria(BusinessRequest<WorkflowNoteDto> request)
        {
            return WorkflowNoteProvider.Instance.GetWorkflowNotesByCriteria(request);
        }

        public BusinessResponse<WorkflowNoteDto> SaveWorkflowNotes(BusinessRequest<WorkflowNoteDto> request)
        {
            var response = new BusinessResponse<WorkflowNoteDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response = WorkflowNoteProvider.Instance.SaveWorkflowNotes(request);

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

