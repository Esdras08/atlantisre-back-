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
    public partial class DomaineActiviteManager
    {
        DomaineActiviteProvider _DomaineActiviteProvider;
        #region Singleton

        public DomaineActiviteManager() {
           _DomaineActiviteProvider   = new DomaineActiviteProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<DomaineActiviteDto>> GetDomaineActiviteById(object id)
        {
            return await _DomaineActiviteProvider.GetDomaineActiviteById(id);
        }
        public async Task<BusinessResponse<DomaineActiviteDto>> GetDomaineActivitesByCriteria(BusinessRequest<DomaineActiviteDto> request)
        {
            return await _DomaineActiviteProvider.GetDomaineActivitesByCriteria(request);
        }

        public async Task<BusinessResponse<DomaineActiviteDto>> SaveDomaineActivites(BusinessRequest<DomaineActiviteDto> request)
        {
            var response = new BusinessResponse<DomaineActiviteDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _DomaineActiviteProvider.SaveDomaineActivites(request);

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

