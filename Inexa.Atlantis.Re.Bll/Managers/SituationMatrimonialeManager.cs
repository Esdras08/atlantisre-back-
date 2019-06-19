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
    public partial class SituationMatrimonialeManager
    {
        SituationMatrimonialeProvider _SituationMatrimonialeProvider;
        #region Singleton

        public SituationMatrimonialeManager() {
           _SituationMatrimonialeProvider   = new SituationMatrimonialeProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<SituationMatrimonialeDto>> GetSituationMatrimonialeById(object id)
        {
            return await _SituationMatrimonialeProvider.GetSituationMatrimonialeById(id);
        }
        public async Task<BusinessResponse<SituationMatrimonialeDto>> GetSituationMatrimonialesByCriteria(BusinessRequest<SituationMatrimonialeDto> request)
        {
            return await _SituationMatrimonialeProvider.GetSituationMatrimonialesByCriteria(request);
        }

        public async Task<BusinessResponse<SituationMatrimonialeDto>> SaveSituationMatrimoniales(BusinessRequest<SituationMatrimonialeDto> request)
        {
            var response = new BusinessResponse<SituationMatrimonialeDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _SituationMatrimonialeProvider.SaveSituationMatrimoniales(request);

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

