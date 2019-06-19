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
    public partial class UtilisateurHistoManager
    {
        UtilisateurHistoProvider _UtilisateurHistoProvider;
        #region Singleton

        public UtilisateurHistoManager() {
           _UtilisateurHistoProvider   = new UtilisateurHistoProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<UtilisateurHistoDto>> GetUtilisateurHistoById(object id)
        {
            return await _UtilisateurHistoProvider.GetUtilisateurHistoById(id);
        }
        public async Task<BusinessResponse<UtilisateurHistoDto>> GetUtilisateurHistosByCriteria(BusinessRequest<UtilisateurHistoDto> request)
        {
            return await _UtilisateurHistoProvider.GetUtilisateurHistosByCriteria(request);
        }

        public async Task<BusinessResponse<UtilisateurHistoDto>> SaveUtilisateurHistos(BusinessRequest<UtilisateurHistoDto> request)
        {
            var response = new BusinessResponse<UtilisateurHistoDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _UtilisateurHistoProvider.SaveUtilisateurHistos(request);

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

