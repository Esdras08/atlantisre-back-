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
    public partial class TypeEchangeManager
    {
        TypeEchangeProvider _TypeEchangeProvider;
        #region Singleton

        public TypeEchangeManager() {
           _TypeEchangeProvider   = new TypeEchangeProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<TypeEchangeDto>> GetTypeEchangeById(object id)
        {
            return await _TypeEchangeProvider.GetTypeEchangeById(id);
        }
        public async Task<BusinessResponse<TypeEchangeDto>> GetTypeEchangesByCriteria(BusinessRequest<TypeEchangeDto> request)
        {
            return await _TypeEchangeProvider.GetTypeEchangesByCriteria(request);
        }

        public async Task<BusinessResponse<TypeEchangeDto>> SaveTypeEchanges(BusinessRequest<TypeEchangeDto> request)
        {
            var response = new BusinessResponse<TypeEchangeDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _TypeEchangeProvider.SaveTypeEchanges(request);

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

