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
    public partial class UtilisateurManager
    {
        UtilisateurProvider _UtilisateurProvider;
        #region Singleton

        public UtilisateurManager() {
           _UtilisateurProvider   = new UtilisateurProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<UtilisateurDto>> GetUtilisateurById(object id)
        {
            return await _UtilisateurProvider.GetUtilisateurById(id);
        }
        public async Task<BusinessResponse<UtilisateurDto>> GetUtilisateursByCriteria(BusinessRequest<UtilisateurDto> request)
        {
            return await _UtilisateurProvider.GetUtilisateursByCriteria(request);
        }

        public async Task<BusinessResponse<UtilisateurDto>> SaveUtilisateurs(BusinessRequest<UtilisateurDto> request)
        {
            var response = new BusinessResponse<UtilisateurDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _UtilisateurProvider.SaveUtilisateurs(request);

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

