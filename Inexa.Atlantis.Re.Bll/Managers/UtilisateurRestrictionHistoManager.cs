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
    public partial class UtilisateurRestrictionHistoManager
    {
        UtilisateurRestrictionHistoProvider _UtilisateurRestrictionHistoProvider;
        #region Singleton

        public UtilisateurRestrictionHistoManager() {
           _UtilisateurRestrictionHistoProvider   = new UtilisateurRestrictionHistoProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<UtilisateurRestrictionHistoDto>> GetUtilisateurRestrictionHistoById(object id)
        {
            return await _UtilisateurRestrictionHistoProvider.GetUtilisateurRestrictionHistoById(id);
        }
        public async Task<BusinessResponse<UtilisateurRestrictionHistoDto>> GetUtilisateurRestrictionHistosByCriteria(BusinessRequest<UtilisateurRestrictionHistoDto> request)
        {
            return await _UtilisateurRestrictionHistoProvider.GetUtilisateurRestrictionHistosByCriteria(request);
        }

        public async Task<BusinessResponse<UtilisateurRestrictionHistoDto>> SaveUtilisateurRestrictionHistos(BusinessRequest<UtilisateurRestrictionHistoDto> request)
        {
            var response = new BusinessResponse<UtilisateurRestrictionHistoDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _UtilisateurRestrictionHistoProvider.SaveUtilisateurRestrictionHistos(request);

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

