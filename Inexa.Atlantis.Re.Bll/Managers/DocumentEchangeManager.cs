using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inexa.Atlantis.Re.Dal.Providers;
using Inexa.Atlantis.Re.Commons.Dtos;
using Inexa.Atlantis.Re.Commons.Infras.Domains;
using Inexa.Atlantis.Re.Commons.Infras.Enums;
using Inexa.Atlantis.Re.Commons.Infras.Helpers;


namespace Inexa.Atlantis.Re.Bll.Managers
{
    public partial class DocumentEchangeManager
    {
        DocumentEchangeProvider _DocumentEchangeProvider;
        #region Singleton

        public DocumentEchangeManager() {
           _DocumentEchangeProvider   = new DocumentEchangeProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<DocumentEchangeDto>> GetDocumentEchangeById(object id)
        {
            return await _DocumentEchangeProvider.GetDocumentEchangeById(id);
        }
        public async Task<BusinessResponse<DocumentEchangeDto>> GetDocumentEchangesByCriteria(BusinessRequest<DocumentEchangeDto> request)
        {
            return await _DocumentEchangeProvider.GetDocumentEchangesByCriteria(request);
        }

        public async Task<BusinessResponse<DocumentEchangeDto>> SaveDocumentEchanges(BusinessRequest<DocumentEchangeDto> request)
        {
            var response = new BusinessResponse<DocumentEchangeDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _DocumentEchangeProvider.SaveDocumentEchanges(request);

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

