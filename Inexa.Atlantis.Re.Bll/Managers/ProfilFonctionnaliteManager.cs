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
    public partial class ProfilFonctionnaliteManager
    {
        ProfilFonctionnaliteProvider _ProfilFonctionnaliteProvider;
        #region Singleton

        public ProfilFonctionnaliteManager() {
           _ProfilFonctionnaliteProvider   = new ProfilFonctionnaliteProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<ProfilFonctionnaliteDto>> GetProfilFonctionnaliteById(object id)
        {
            return await _ProfilFonctionnaliteProvider.GetProfilFonctionnaliteById(id);
        }
        public async Task<BusinessResponse<ProfilFonctionnaliteDto>> GetProfilFonctionnalitesByCriteria(BusinessRequest<ProfilFonctionnaliteDto> request)
        {
            return await _ProfilFonctionnaliteProvider.GetProfilFonctionnalitesByCriteria(request);
        }

        public async Task<BusinessResponse<ProfilFonctionnaliteDto>> SaveProfilFonctionnalites(BusinessRequest<ProfilFonctionnaliteDto> request)
        {
            var response = new BusinessResponse<ProfilFonctionnaliteDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _ProfilFonctionnaliteProvider.SaveProfilFonctionnalites(request);

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

