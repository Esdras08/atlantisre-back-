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
    public partial class EchangeManager
    {
        EchangeProvider _EchangeProvider;
        #region Singleton

        public EchangeManager() {
           _EchangeProvider   = new EchangeProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<EchangeDto>> GetEchangeById(object id)
        {
            return await _EchangeProvider.GetEchangeById(id);
        }
        public async Task<BusinessResponse<EchangeDto>> GetEchangesByCriteria(BusinessRequest<EchangeDto> request)
        {
            return await _EchangeProvider.GetEchangesByCriteria(request);
        }

        public async Task<BusinessResponse<EchangeDto>> SaveEchanges(BusinessRequest<EchangeDto> request)
        {
            var response = new BusinessResponse<EchangeDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _EchangeProvider.SaveEchanges(request);

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

