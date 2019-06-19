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
    public partial class SuiviProcessuManager
    {
        SuiviProcessuProvider _SuiviProcessuProvider;
        #region Singleton

        public SuiviProcessuManager() {
           _SuiviProcessuProvider   = new SuiviProcessuProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<SuiviProcessuDto>> GetSuiviProcessuById(object id)
        {
            return await _SuiviProcessuProvider.GetSuiviProcessuById(id);
        }
        public async Task<BusinessResponse<SuiviProcessuDto>> GetSuiviProcessusByCriteria(BusinessRequest<SuiviProcessuDto> request)
        {
            return await _SuiviProcessuProvider.GetSuiviProcessusByCriteria(request);
        }

        public async Task<BusinessResponse<SuiviProcessuDto>> SaveSuiviProcessus(BusinessRequest<SuiviProcessuDto> request)
        {
            var response = new BusinessResponse<SuiviProcessuDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _SuiviProcessuProvider.SaveSuiviProcessus(request);

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

