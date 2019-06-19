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
    public partial class InterlocuteurReassureurManager
    {
        InterlocuteurReassureurProvider _InterlocuteurReassureurProvider;
        #region Singleton

        public InterlocuteurReassureurManager() {
           _InterlocuteurReassureurProvider   = new InterlocuteurReassureurProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<InterlocuteurReassureurDto>> GetInterlocuteurReassureurById(object id)
        {
            return await _InterlocuteurReassureurProvider.GetInterlocuteurReassureurById(id);
        }
        public async Task<BusinessResponse<InterlocuteurReassureurDto>> GetInterlocuteurReassureursByCriteria(BusinessRequest<InterlocuteurReassureurDto> request)
        {
            return await _InterlocuteurReassureurProvider.GetInterlocuteurReassureursByCriteria(request);
        }

        public async Task<BusinessResponse<InterlocuteurReassureurDto>> SaveInterlocuteurReassureurs(BusinessRequest<InterlocuteurReassureurDto> request)
        {
            var response = new BusinessResponse<InterlocuteurReassureurDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _InterlocuteurReassureurProvider.SaveInterlocuteurReassureurs(request);

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

