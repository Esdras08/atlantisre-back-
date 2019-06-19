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
    public partial class MouvementTresorerieManager
    {
        MouvementTresorerieProvider _MouvementTresorerieProvider;
        #region Singleton

        public MouvementTresorerieManager() {
           _MouvementTresorerieProvider   = new MouvementTresorerieProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<MouvementTresorerieDto>> GetMouvementTresorerieById(object id)
        {
            return await _MouvementTresorerieProvider.GetMouvementTresorerieById(id);
        }
        public async Task<BusinessResponse<MouvementTresorerieDto>> GetMouvementTresoreriesByCriteria(BusinessRequest<MouvementTresorerieDto> request)
        {
            return await _MouvementTresorerieProvider.GetMouvementTresoreriesByCriteria(request);
        }

        public async Task<BusinessResponse<MouvementTresorerieDto>> SaveMouvementTresoreries(BusinessRequest<MouvementTresorerieDto> request)
        {
            var response = new BusinessResponse<MouvementTresorerieDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _MouvementTresorerieProvider.SaveMouvementTresoreries(request);

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

