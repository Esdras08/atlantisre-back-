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
    public partial class ProfilManager
    {
        ProfilProvider _ProfilProvider;
        #region Singleton

        public ProfilManager() {
           _ProfilProvider   = new ProfilProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<ProfilDto>> GetProfilById(object id)
        {
            return await _ProfilProvider.GetProfilById(id);
        }
        public async Task<BusinessResponse<ProfilDto>> GetProfilsByCriteria(BusinessRequest<ProfilDto> request)
        {
            return await _ProfilProvider.GetProfilsByCriteria(request);
        }

        public async Task<BusinessResponse<ProfilDto>> SaveProfils(BusinessRequest<ProfilDto> request)
        {
            var response = new BusinessResponse<ProfilDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _ProfilProvider.SaveProfils(request);

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

