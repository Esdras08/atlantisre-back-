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
    public partial class UtilisateurFonctionnaliteManager
    {
        UtilisateurFonctionnaliteProvider _UtilisateurFonctionnaliteProvider;
        #region Singleton

        public UtilisateurFonctionnaliteManager() {
           _UtilisateurFonctionnaliteProvider   = new UtilisateurFonctionnaliteProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<UtilisateurFonctionnaliteDto>> GetUtilisateurFonctionnaliteById(object id)
        {
            return await _UtilisateurFonctionnaliteProvider.GetUtilisateurFonctionnaliteById(id);
        }
        public async Task<BusinessResponse<UtilisateurFonctionnaliteDto>> GetUtilisateurFonctionnalitesByCriteria(BusinessRequest<UtilisateurFonctionnaliteDto> request)
        {
            return await _UtilisateurFonctionnaliteProvider.GetUtilisateurFonctionnalitesByCriteria(request);
        }

        public async Task<BusinessResponse<UtilisateurFonctionnaliteDto>> SaveUtilisateurFonctionnalites(BusinessRequest<UtilisateurFonctionnaliteDto> request)
        {
            var response = new BusinessResponse<UtilisateurFonctionnaliteDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _UtilisateurFonctionnaliteProvider.SaveUtilisateurFonctionnalites(request);

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

