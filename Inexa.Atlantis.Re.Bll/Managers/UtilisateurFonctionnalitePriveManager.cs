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
    public partial class UtilisateurFonctionnalitePriveManager
    {
        UtilisateurFonctionnalitePriveProvider _UtilisateurFonctionnalitePriveProvider;
        #region Singleton

        public UtilisateurFonctionnalitePriveManager() {
           _UtilisateurFonctionnalitePriveProvider   = new UtilisateurFonctionnalitePriveProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<UtilisateurFonctionnalitePriveDto>> GetUtilisateurFonctionnalitePriveById(object id)
        {
            return await _UtilisateurFonctionnalitePriveProvider.GetUtilisateurFonctionnalitePriveById(id);
        }
        public async Task<BusinessResponse<UtilisateurFonctionnalitePriveDto>> GetUtilisateurFonctionnalitePrivesByCriteria(BusinessRequest<UtilisateurFonctionnalitePriveDto> request)
        {
            return await _UtilisateurFonctionnalitePriveProvider.GetUtilisateurFonctionnalitePrivesByCriteria(request);
        }

        public async Task<BusinessResponse<UtilisateurFonctionnalitePriveDto>> SaveUtilisateurFonctionnalitePrives(BusinessRequest<UtilisateurFonctionnalitePriveDto> request)
        {
            var response = new BusinessResponse<UtilisateurFonctionnalitePriveDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _UtilisateurFonctionnalitePriveProvider.SaveUtilisateurFonctionnalitePrives(request);

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

