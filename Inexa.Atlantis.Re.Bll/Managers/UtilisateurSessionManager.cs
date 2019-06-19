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
    public partial class UtilisateurSessionManager
    {
        UtilisateurSessionProvider _UtilisateurSessionProvider;
        #region Singleton

        public UtilisateurSessionManager() {
           _UtilisateurSessionProvider   = new UtilisateurSessionProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<UtilisateurSessionDto>> GetUtilisateurSessionById(object id)
        {
            return await _UtilisateurSessionProvider.GetUtilisateurSessionById(id);
        }
        public async Task<BusinessResponse<UtilisateurSessionDto>> GetUtilisateurSessionsByCriteria(BusinessRequest<UtilisateurSessionDto> request)
        {
            return await _UtilisateurSessionProvider.GetUtilisateurSessionsByCriteria(request);
        }

        public async Task<BusinessResponse<UtilisateurSessionDto>> SaveUtilisateurSessions(BusinessRequest<UtilisateurSessionDto> request)
        {
            var response = new BusinessResponse<UtilisateurSessionDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _UtilisateurSessionProvider.SaveUtilisateurSessions(request);

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

