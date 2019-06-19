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
    public partial class TermeManager
    {
        TermeProvider _TermeProvider;
        #region Singleton

        public TermeManager() {
           _TermeProvider   = new TermeProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<TermeDto>> GetTermeById(object id)
        {
            return await _TermeProvider.GetTermeById(id);
        }
        public async Task<BusinessResponse<TermeDto>> GetTermesByCriteria(BusinessRequest<TermeDto> request)
        {
            return await _TermeProvider.GetTermesByCriteria(request);
        }

        public async Task<BusinessResponse<TermeDto>> SaveTermes(BusinessRequest<TermeDto> request)
        {
            var response = new BusinessResponse<TermeDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _TermeProvider.SaveTermes(request);

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

