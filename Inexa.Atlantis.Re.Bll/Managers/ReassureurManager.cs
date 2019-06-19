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
    public partial class ReassureurManager
    {
        ReassureurProvider _ReassureurProvider;
        #region Singleton

        public ReassureurManager() {
           _ReassureurProvider   = new ReassureurProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<ReassureurDto>> GetReassureurById(object id)
        {
            return await _ReassureurProvider.GetReassureurById(id);
        }
        public async Task<BusinessResponse<ReassureurDto>> GetReassureursByCriteria(BusinessRequest<ReassureurDto> request)
        {
            return await _ReassureurProvider.GetReassureursByCriteria(request);
        }

        public async Task<BusinessResponse<ReassureurDto>> SaveReassureurs(BusinessRequest<ReassureurDto> request)
        {
            var response = new BusinessResponse<ReassureurDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _ReassureurProvider.SaveReassureurs(request);

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

