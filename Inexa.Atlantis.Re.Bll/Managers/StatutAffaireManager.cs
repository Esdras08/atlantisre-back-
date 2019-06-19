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
    public partial class StatutAffaireManager
    {
        StatutAffaireProvider _StatutAffaireProvider;
        #region Singleton

        public StatutAffaireManager() {
           _StatutAffaireProvider   = new StatutAffaireProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<StatutAffaireDto>> GetStatutAffaireById(object id)
        {
            return await _StatutAffaireProvider.GetStatutAffaireById(id);
        }
        public async Task<BusinessResponse<StatutAffaireDto>> GetStatutAffairesByCriteria(BusinessRequest<StatutAffaireDto> request)
        {
            return await _StatutAffaireProvider.GetStatutAffairesByCriteria(request);
        }

        public async Task<BusinessResponse<StatutAffaireDto>> SaveStatutAffaires(BusinessRequest<StatutAffaireDto> request)
        {
            var response = new BusinessResponse<StatutAffaireDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _StatutAffaireProvider.SaveStatutAffaires(request);

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

