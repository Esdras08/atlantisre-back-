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
    public partial class AssureManager
    {
        AssureProvider _AssureProvider;
        #region Singleton

        public AssureManager() {
           _AssureProvider   = new AssureProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<AssureDto>> GetAssureById(object id)
        {
            return await _AssureProvider.GetAssureById(id);
        }
        public async Task<BusinessResponse<AssureDto>> GetAssuresByCriteria(BusinessRequest<AssureDto> request)
        {
            return await _AssureProvider.GetAssuresByCriteria(request);
        }

        public async Task<BusinessResponse<AssureDto>> SaveAssures(BusinessRequest<AssureDto> request)
        {
            var response = new BusinessResponse<AssureDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _AssureProvider.SaveAssures(request);

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

