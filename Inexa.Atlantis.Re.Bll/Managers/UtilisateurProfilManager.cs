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
    public partial class UtilisateurProfilManager
    {
        UtilisateurProfilProvider _UtilisateurProfilProvider;
        #region Singleton

        public UtilisateurProfilManager() {
           _UtilisateurProfilProvider   = new UtilisateurProfilProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<UtilisateurProfilDto>> GetUtilisateurProfilById(object id)
        {
            return await _UtilisateurProfilProvider.GetUtilisateurProfilById(id);
        }
        public async Task<BusinessResponse<UtilisateurProfilDto>> GetUtilisateurProfilsByCriteria(BusinessRequest<UtilisateurProfilDto> request)
        {
            return await _UtilisateurProfilProvider.GetUtilisateurProfilsByCriteria(request);
        }

        public async Task<BusinessResponse<UtilisateurProfilDto>> SaveUtilisateurProfils(BusinessRequest<UtilisateurProfilDto> request)
        {
            var response = new BusinessResponse<UtilisateurProfilDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _UtilisateurProfilProvider.SaveUtilisateurProfils(request);

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

