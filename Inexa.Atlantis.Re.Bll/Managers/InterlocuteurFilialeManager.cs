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
    public partial class InterlocuteurFilialeManager
    {
        InterlocuteurFilialeProvider _InterlocuteurFilialeProvider;
        #region Singleton

        public InterlocuteurFilialeManager() {
           _InterlocuteurFilialeProvider   = new InterlocuteurFilialeProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<InterlocuteurFilialeDto>> GetInterlocuteurFilialeById(object id)
        {
            return await _InterlocuteurFilialeProvider.GetInterlocuteurFilialeById(id);
        }
        public async Task<BusinessResponse<InterlocuteurFilialeDto>> GetInterlocuteurFilialesByCriteria(BusinessRequest<InterlocuteurFilialeDto> request)
        {
            return await _InterlocuteurFilialeProvider.GetInterlocuteurFilialesByCriteria(request);
        }

        public async Task<BusinessResponse<InterlocuteurFilialeDto>> SaveInterlocuteurFiliales(BusinessRequest<InterlocuteurFilialeDto> request)
        {
            var response = new BusinessResponse<InterlocuteurFilialeDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _InterlocuteurFilialeProvider.SaveInterlocuteurFiliales(request);

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

