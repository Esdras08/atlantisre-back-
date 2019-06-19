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
    public partial class ViewUtilisateurManager
    {
        ViewUtilisateurProvider _ViewUtilisateurProvider;
        #region Singleton

        public ViewUtilisateurManager() {
           _ViewUtilisateurProvider   = new ViewUtilisateurProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<ViewUtilisateurDto>> GetViewUtilisateursByCriteria(BusinessRequest<ViewUtilisateurDto> request)
        {
            return await _ViewUtilisateurProvider.GetViewUtilisateursByCriteria(request);
        }

        public async Task<BusinessResponse<ViewUtilisateurDto>> SaveViewUtilisateurs(BusinessRequest<ViewUtilisateurDto> request)
        {
            var response = new BusinessResponse<ViewUtilisateurDto>();
            
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

