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
    public partial class AffaireManager
    {
        AffaireProvider _AffaireProvider;
        #region Singleton

        public AffaireManager() {
           _AffaireProvider   = new AffaireProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<AffaireDto>> GetAffaireById(object id)
        {
            return await _AffaireProvider.GetAffaireById(id);
        }
        public async Task<BusinessResponse<AffaireDto>> GetAffairesByCriteria(BusinessRequest<AffaireDto> request)
        {
            return await _AffaireProvider.GetAffairesByCriteria(request);
        }

        public async Task<BusinessResponse<AffaireDto>> SaveAffaires(BusinessRequest<AffaireDto> request)
        {
            var response = new BusinessResponse<AffaireDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _AffaireProvider.SaveAffaires(request);

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

