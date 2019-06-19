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
    public partial class UtilisateurFonctionnaliteHistoManager
    {
        UtilisateurFonctionnaliteHistoProvider _UtilisateurFonctionnaliteHistoProvider;
        #region Singleton

        public UtilisateurFonctionnaliteHistoManager() {
           _UtilisateurFonctionnaliteHistoProvider   = new UtilisateurFonctionnaliteHistoProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<UtilisateurFonctionnaliteHistoDto>> GetUtilisateurFonctionnaliteHistoById(object id)
        {
            return await _UtilisateurFonctionnaliteHistoProvider.GetUtilisateurFonctionnaliteHistoById(id);
        }
        public async Task<BusinessResponse<UtilisateurFonctionnaliteHistoDto>> GetUtilisateurFonctionnaliteHistosByCriteria(BusinessRequest<UtilisateurFonctionnaliteHistoDto> request)
        {
            return await _UtilisateurFonctionnaliteHistoProvider.GetUtilisateurFonctionnaliteHistosByCriteria(request);
        }

        public async Task<BusinessResponse<UtilisateurFonctionnaliteHistoDto>> SaveUtilisateurFonctionnaliteHistos(BusinessRequest<UtilisateurFonctionnaliteHistoDto> request)
        {
            var response = new BusinessResponse<UtilisateurFonctionnaliteHistoDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _UtilisateurFonctionnaliteHistoProvider.SaveUtilisateurFonctionnaliteHistos(request);

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

