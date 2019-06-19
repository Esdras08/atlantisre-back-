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
    public partial class CiviliteManager
    {
        CiviliteProvider _CiviliteProvider;
        #region Singleton

        public CiviliteManager() {
           _CiviliteProvider   = new CiviliteProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<CiviliteDto>> GetCiviliteById(object id)
        {
            return await _CiviliteProvider.GetCiviliteById(id);
        }
        public async Task<BusinessResponse<CiviliteDto>> GetCivilitesByCriteria(BusinessRequest<CiviliteDto> request)
        {
            return await _CiviliteProvider.GetCivilitesByCriteria(request);
        }

        public async Task<BusinessResponse<CiviliteDto>> SaveCivilites(BusinessRequest<CiviliteDto> request)
        {
            var response = new BusinessResponse<CiviliteDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _CiviliteProvider.SaveCivilites(request);

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

