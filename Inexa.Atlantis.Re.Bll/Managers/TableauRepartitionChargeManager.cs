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
    public partial class TableauRepartitionChargeManager
    {
        TableauRepartitionChargeProvider _TableauRepartitionChargeProvider;
        #region Singleton

        public TableauRepartitionChargeManager() {
           _TableauRepartitionChargeProvider   = new TableauRepartitionChargeProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<TableauRepartitionChargeDto>> GetTableauRepartitionChargeById(object id)
        {
            return await _TableauRepartitionChargeProvider.GetTableauRepartitionChargeById(id);
        }
        public async Task<BusinessResponse<TableauRepartitionChargeDto>> GetTableauRepartitionChargesByCriteria(BusinessRequest<TableauRepartitionChargeDto> request)
        {
            return await _TableauRepartitionChargeProvider.GetTableauRepartitionChargesByCriteria(request);
        }

        public async Task<BusinessResponse<TableauRepartitionChargeDto>> SaveTableauRepartitionCharges(BusinessRequest<TableauRepartitionChargeDto> request)
        {
            var response = new BusinessResponse<TableauRepartitionChargeDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _TableauRepartitionChargeProvider.SaveTableauRepartitionCharges(request);

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

