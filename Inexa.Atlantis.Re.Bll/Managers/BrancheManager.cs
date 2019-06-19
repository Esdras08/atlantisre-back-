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
    public partial class BrancheManager
    {
        BrancheProvider _BrancheProvider;
        #region Singleton

        public BrancheManager() {
           _BrancheProvider   = new BrancheProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<BrancheDto>> GetBrancheById(object id)
        {
            return await _BrancheProvider.GetBrancheById(id);
        }
        public async Task<BusinessResponse<BrancheDto>> GetBranchesByCriteria(BusinessRequest<BrancheDto> request)
        {
            return await _BrancheProvider.GetBranchesByCriteria(request);
        }

        public async Task<BusinessResponse<BrancheDto>> SaveBranches(BusinessRequest<BrancheDto> request)
        {
            var response = new BusinessResponse<BrancheDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _BrancheProvider.SaveBranches(request);

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

