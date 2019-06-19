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
    public partial class FormeJuridiqueManager
    {
        FormeJuridiqueProvider _FormeJuridiqueProvider;
        #region Singleton

        public FormeJuridiqueManager() {
           _FormeJuridiqueProvider   = new FormeJuridiqueProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<FormeJuridiqueDto>> GetFormeJuridiqueById(object id)
        {
            return await _FormeJuridiqueProvider.GetFormeJuridiqueById(id);
        }
        public async Task<BusinessResponse<FormeJuridiqueDto>> GetFormeJuridiquesByCriteria(BusinessRequest<FormeJuridiqueDto> request)
        {
            return await _FormeJuridiqueProvider.GetFormeJuridiquesByCriteria(request);
        }

        public async Task<BusinessResponse<FormeJuridiqueDto>> SaveFormeJuridiques(BusinessRequest<FormeJuridiqueDto> request)
        {
            var response = new BusinessResponse<FormeJuridiqueDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _FormeJuridiqueProvider.SaveFormeJuridiques(request);

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

