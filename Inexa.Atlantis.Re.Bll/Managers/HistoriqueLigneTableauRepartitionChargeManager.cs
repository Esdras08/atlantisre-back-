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
    public partial class HistoriqueLigneTableauRepartitionChargeManager
    {
        HistoriqueLigneTableauRepartitionChargeProvider _HistoriqueLigneTableauRepartitionChargeProvider;
        #region Singleton

        public HistoriqueLigneTableauRepartitionChargeManager() {
           _HistoriqueLigneTableauRepartitionChargeProvider   = new HistoriqueLigneTableauRepartitionChargeProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<HistoriqueLigneTableauRepartitionChargeDto>> GetHistoriqueLigneTableauRepartitionChargeById(object id)
        {
            return await _HistoriqueLigneTableauRepartitionChargeProvider.GetHistoriqueLigneTableauRepartitionChargeById(id);
        }
        public async Task<BusinessResponse<HistoriqueLigneTableauRepartitionChargeDto>> GetHistoriqueLigneTableauRepartitionChargesByCriteria(BusinessRequest<HistoriqueLigneTableauRepartitionChargeDto> request)
        {
            return await _HistoriqueLigneTableauRepartitionChargeProvider.GetHistoriqueLigneTableauRepartitionChargesByCriteria(request);
        }

        public async Task<BusinessResponse<HistoriqueLigneTableauRepartitionChargeDto>> SaveHistoriqueLigneTableauRepartitionCharges(BusinessRequest<HistoriqueLigneTableauRepartitionChargeDto> request)
        {
            var response = new BusinessResponse<HistoriqueLigneTableauRepartitionChargeDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _HistoriqueLigneTableauRepartitionChargeProvider.SaveHistoriqueLigneTableauRepartitionCharges(request);

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

