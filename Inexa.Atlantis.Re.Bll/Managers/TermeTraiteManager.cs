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
    public partial class TermeTraiteManager
    {
        TermeTraiteProvider _TermeTraiteProvider;
        #region Singleton

        public TermeTraiteManager() {
           _TermeTraiteProvider   = new TermeTraiteProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<TermeTraiteDto>> GetTermeTraiteById(object id)
        {
            return await _TermeTraiteProvider.GetTermeTraiteById(id);
        }
        public async Task<BusinessResponse<TermeTraiteDto>> GetTermeTraitesByCriteria(BusinessRequest<TermeTraiteDto> request)
        {
            return await _TermeTraiteProvider.GetTermeTraitesByCriteria(request);
        }

        public async Task<BusinessResponse<TermeTraiteDto>> SaveTermeTraites(BusinessRequest<TermeTraiteDto> request)
        {
            var response = new BusinessResponse<TermeTraiteDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _TermeTraiteProvider.SaveTermeTraites(request);

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

