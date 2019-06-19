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
    public partial class MailParametreManager
    {
        MailParametreProvider _MailParametreProvider;
        #region Singleton

        public MailParametreManager() {
           _MailParametreProvider   = new MailParametreProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<MailParametreDto>> GetMailParametreById(object id)
        {
            return await _MailParametreProvider.GetMailParametreById(id);
        }
        public async Task<BusinessResponse<MailParametreDto>> GetMailParametresByCriteria(BusinessRequest<MailParametreDto> request)
        {
            return await _MailParametreProvider.GetMailParametresByCriteria(request);
        }

        public async Task<BusinessResponse<MailParametreDto>> SaveMailParametres(BusinessRequest<MailParametreDto> request)
        {
            var response = new BusinessResponse<MailParametreDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _MailParametreProvider.SaveMailParametres(request);

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

