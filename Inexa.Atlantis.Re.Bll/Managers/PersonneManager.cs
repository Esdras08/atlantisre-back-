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
    public partial class PersonneManager
    {
        PersonneProvider _PersonneProvider;
        #region Singleton

        public PersonneManager() {
           _PersonneProvider   = new PersonneProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<PersonneDto>> GetPersonneById(object id)
        {
            return await _PersonneProvider.GetPersonneById(id);
        }
        public async Task<BusinessResponse<PersonneDto>> GetPersonnesByCriteria(BusinessRequest<PersonneDto> request)
        {
            return await _PersonneProvider.GetPersonnesByCriteria(request);
        }

        public async Task<BusinessResponse<PersonneDto>> SavePersonnes(BusinessRequest<PersonneDto> request)
        {
            var response = new BusinessResponse<PersonneDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _PersonneProvider.SavePersonnes(request);

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

