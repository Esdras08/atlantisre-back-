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
    public partial class ViewUtilisateurProfilManager
    {
        ViewUtilisateurProfilProvider _ViewUtilisateurProfilProvider;
        #region Singleton

        public ViewUtilisateurProfilManager() {
           _ViewUtilisateurProfilProvider   = new ViewUtilisateurProfilProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<ViewUtilisateurProfilDto>> GetViewUtilisateurProfilsByCriteria(BusinessRequest<ViewUtilisateurProfilDto> request)
        {
            return await _ViewUtilisateurProfilProvider.GetViewUtilisateurProfilsByCriteria(request);
        }

        public async Task<BusinessResponse<ViewUtilisateurProfilDto>> SaveViewUtilisateurProfils(BusinessRequest<ViewUtilisateurProfilDto> request)
        {
            var response = new BusinessResponse<ViewUtilisateurProfilDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    



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

