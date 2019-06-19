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
    public partial class ViewUtilisateurRestrictionManager
    {
        ViewUtilisateurRestrictionProvider _ViewUtilisateurRestrictionProvider;
        #region Singleton

        public ViewUtilisateurRestrictionManager() {
           _ViewUtilisateurRestrictionProvider   = new ViewUtilisateurRestrictionProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<ViewUtilisateurRestrictionDto>> GetViewUtilisateurRestrictionsByCriteria(BusinessRequest<ViewUtilisateurRestrictionDto> request)
        {
            return await _ViewUtilisateurRestrictionProvider.GetViewUtilisateurRestrictionsByCriteria(request);
        }

        public async Task<BusinessResponse<ViewUtilisateurRestrictionDto>> SaveViewUtilisateurRestrictions(BusinessRequest<ViewUtilisateurRestrictionDto> request)
        {
            var response = new BusinessResponse<ViewUtilisateurRestrictionDto>();
            
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

