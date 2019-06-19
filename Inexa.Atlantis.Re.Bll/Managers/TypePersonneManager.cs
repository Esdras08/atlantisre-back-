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
    public partial class TypePersonneManager
    {
        TypePersonneProvider _TypePersonneProvider;
        #region Singleton

        public TypePersonneManager() {
           _TypePersonneProvider   = new TypePersonneProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<TypePersonneDto>> GetTypePersonneById(object id)
        {
            return await _TypePersonneProvider.GetTypePersonneById(id);
        }
        public async Task<BusinessResponse<TypePersonneDto>> GetTypePersonnesByCriteria(BusinessRequest<TypePersonneDto> request)
        {
            return await _TypePersonneProvider.GetTypePersonnesByCriteria(request);
        }

        public async Task<BusinessResponse<TypePersonneDto>> SaveTypePersonnes(BusinessRequest<TypePersonneDto> request)
        {
            var response = new BusinessResponse<TypePersonneDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _TypePersonneProvider.SaveTypePersonnes(request);

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

