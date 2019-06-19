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
    public partial class UtilisateurFonctionnalitePriveHistoManager
    {
        UtilisateurFonctionnalitePriveHistoProvider _UtilisateurFonctionnalitePriveHistoProvider;
        #region Singleton

        public UtilisateurFonctionnalitePriveHistoManager() {
           _UtilisateurFonctionnalitePriveHistoProvider   = new UtilisateurFonctionnalitePriveHistoProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<UtilisateurFonctionnalitePriveHistoDto>> GetUtilisateurFonctionnalitePriveHistoById(object id)
        {
            return await _UtilisateurFonctionnalitePriveHistoProvider.GetUtilisateurFonctionnalitePriveHistoById(id);
        }
        public async Task<BusinessResponse<UtilisateurFonctionnalitePriveHistoDto>> GetUtilisateurFonctionnalitePriveHistosByCriteria(BusinessRequest<UtilisateurFonctionnalitePriveHistoDto> request)
        {
            return await _UtilisateurFonctionnalitePriveHistoProvider.GetUtilisateurFonctionnalitePriveHistosByCriteria(request);
        }

        public async Task<BusinessResponse<UtilisateurFonctionnalitePriveHistoDto>> SaveUtilisateurFonctionnalitePriveHistos(BusinessRequest<UtilisateurFonctionnalitePriveHistoDto> request)
        {
            var response = new BusinessResponse<UtilisateurFonctionnalitePriveHistoDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _UtilisateurFonctionnalitePriveHistoProvider.SaveUtilisateurFonctionnalitePriveHistos(request);

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

