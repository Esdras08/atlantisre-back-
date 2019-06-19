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
    public partial class EtapeProcessuManager
    {
        EtapeProcessuProvider _EtapeProcessuProvider;
        #region Singleton

        public EtapeProcessuManager() {
           _EtapeProcessuProvider   = new EtapeProcessuProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<EtapeProcessuDto>> GetEtapeProcessuById(object id)
        {
            return await _EtapeProcessuProvider.GetEtapeProcessuById(id);
        }
        public async Task<BusinessResponse<EtapeProcessuDto>> GetEtapeProcessusByCriteria(BusinessRequest<EtapeProcessuDto> request)
        {
            return await _EtapeProcessuProvider.GetEtapeProcessusByCriteria(request);
        }

        public async Task<BusinessResponse<EtapeProcessuDto>> SaveEtapeProcessus(BusinessRequest<EtapeProcessuDto> request)
        {
            var response = new BusinessResponse<EtapeProcessuDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _EtapeProcessuProvider.SaveEtapeProcessus(request);

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

