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
    public partial class CategorieProcessuManager
    {
        CategorieProcessuProvider _CategorieProcessuProvider;
        #region Singleton

        public CategorieProcessuManager() {
           _CategorieProcessuProvider   = new CategorieProcessuProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<CategorieProcessuDto>> GetCategorieProcessuById(object id)
        {
            return await _CategorieProcessuProvider.GetCategorieProcessuById(id);
        }
        public async Task<BusinessResponse<CategorieProcessuDto>> GetCategorieProcessusByCriteria(BusinessRequest<CategorieProcessuDto> request)
        {
            return await _CategorieProcessuProvider.GetCategorieProcessusByCriteria(request);
        }

        public async Task<BusinessResponse<CategorieProcessuDto>> SaveCategorieProcessus(BusinessRequest<CategorieProcessuDto> request)
        {
            var response = new BusinessResponse<CategorieProcessuDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _CategorieProcessuProvider.SaveCategorieProcessus(request);

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

