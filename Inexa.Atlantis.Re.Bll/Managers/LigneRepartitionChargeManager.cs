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
    public partial class LigneRepartitionChargeManager
    {
        LigneRepartitionChargeProvider _LigneRepartitionChargeProvider;
        #region Singleton

        public LigneRepartitionChargeManager() {
           _LigneRepartitionChargeProvider   = new LigneRepartitionChargeProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<LigneRepartitionChargeDto>> GetLigneRepartitionChargeById(object id)
        {
            return await _LigneRepartitionChargeProvider.GetLigneRepartitionChargeById(id);
        }
        public async Task<BusinessResponse<LigneRepartitionChargeDto>> GetLigneRepartitionChargesByCriteria(BusinessRequest<LigneRepartitionChargeDto> request)
        {
            return await _LigneRepartitionChargeProvider.GetLigneRepartitionChargesByCriteria(request);
        }

        public async Task<BusinessResponse<LigneRepartitionChargeDto>> SaveLigneRepartitionCharges(BusinessRequest<LigneRepartitionChargeDto> request)
        {
            var response = new BusinessResponse<LigneRepartitionChargeDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _LigneRepartitionChargeProvider.SaveLigneRepartitionCharges(request);

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

