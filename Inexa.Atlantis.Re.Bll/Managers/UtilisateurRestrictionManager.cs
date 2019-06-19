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
    public partial class UtilisateurRestrictionManager
    {
        UtilisateurRestrictionProvider _UtilisateurRestrictionProvider;
        #region Singleton

        public UtilisateurRestrictionManager() {
           _UtilisateurRestrictionProvider   = new UtilisateurRestrictionProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<UtilisateurRestrictionDto>> GetUtilisateurRestrictionById(object id)
        {
            return await _UtilisateurRestrictionProvider.GetUtilisateurRestrictionById(id);
        }
        public async Task<BusinessResponse<UtilisateurRestrictionDto>> GetUtilisateurRestrictionsByCriteria(BusinessRequest<UtilisateurRestrictionDto> request)
        {
            return await _UtilisateurRestrictionProvider.GetUtilisateurRestrictionsByCriteria(request);
        }

        public async Task<BusinessResponse<UtilisateurRestrictionDto>> SaveUtilisateurRestrictions(BusinessRequest<UtilisateurRestrictionDto> request)
        {
            var response = new BusinessResponse<UtilisateurRestrictionDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _UtilisateurRestrictionProvider.SaveUtilisateurRestrictions(request);

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

