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
    public partial class StatutMouvementManager
    {
        StatutMouvementProvider _StatutMouvementProvider;
        #region Singleton

        public StatutMouvementManager() {
           _StatutMouvementProvider   = new StatutMouvementProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<StatutMouvementDto>> GetStatutMouvementById(object id)
        {
            return await _StatutMouvementProvider.GetStatutMouvementById(id);
        }
        public async Task<BusinessResponse<StatutMouvementDto>> GetStatutMouvementsByCriteria(BusinessRequest<StatutMouvementDto> request)
        {
            return await _StatutMouvementProvider.GetStatutMouvementsByCriteria(request);
        }

        public async Task<BusinessResponse<StatutMouvementDto>> SaveStatutMouvements(BusinessRequest<StatutMouvementDto> request)
        {
            var response = new BusinessResponse<StatutMouvementDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _StatutMouvementProvider.SaveStatutMouvements(request);

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

