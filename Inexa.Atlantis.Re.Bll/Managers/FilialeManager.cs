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
    public partial class FilialeManager
    {
        FilialeProvider _FilialeProvider;
        #region Singleton

        public FilialeManager() {
           _FilialeProvider   = new FilialeProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<FilialeDto>> GetFilialeById(object id)
        {
            return await _FilialeProvider.GetFilialeById(id);
        }
        public async Task<BusinessResponse<FilialeDto>> GetFilialesByCriteria(BusinessRequest<FilialeDto> request)
        {
            return await _FilialeProvider.GetFilialesByCriteria(request);
        }

        public async Task<BusinessResponse<FilialeDto>> SaveFiliales(BusinessRequest<FilialeDto> request)
        {
            var response = new BusinessResponse<FilialeDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _FilialeProvider.SaveFiliales(request);

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

