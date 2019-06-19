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
    public partial class TypeProcessuManager
    {
        TypeProcessuProvider _TypeProcessuProvider;
        #region Singleton

        public TypeProcessuManager() {
           _TypeProcessuProvider   = new TypeProcessuProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<TypeProcessuDto>> GetTypeProcessuById(object id)
        {
            return await _TypeProcessuProvider.GetTypeProcessuById(id);
        }
        public async Task<BusinessResponse<TypeProcessuDto>> GetTypeProcessusByCriteria(BusinessRequest<TypeProcessuDto> request)
        {
            return await _TypeProcessuProvider.GetTypeProcessusByCriteria(request);
        }

        public async Task<BusinessResponse<TypeProcessuDto>> SaveTypeProcessus(BusinessRequest<TypeProcessuDto> request)
        {
            var response = new BusinessResponse<TypeProcessuDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _TypeProcessuProvider.SaveTypeProcessus(request);

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

