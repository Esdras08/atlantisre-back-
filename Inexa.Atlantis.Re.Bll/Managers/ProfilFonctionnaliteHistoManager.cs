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
    public partial class ProfilFonctionnaliteHistoManager
    {
        ProfilFonctionnaliteHistoProvider _ProfilFonctionnaliteHistoProvider;
        #region Singleton

        public ProfilFonctionnaliteHistoManager() {
           _ProfilFonctionnaliteHistoProvider   = new ProfilFonctionnaliteHistoProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<ProfilFonctionnaliteHistoDto>> GetProfilFonctionnaliteHistoById(object id)
        {
            return await _ProfilFonctionnaliteHistoProvider.GetProfilFonctionnaliteHistoById(id);
        }
        public async Task<BusinessResponse<ProfilFonctionnaliteHistoDto>> GetProfilFonctionnaliteHistosByCriteria(BusinessRequest<ProfilFonctionnaliteHistoDto> request)
        {
            return await _ProfilFonctionnaliteHistoProvider.GetProfilFonctionnaliteHistosByCriteria(request);
        }

        public async Task<BusinessResponse<ProfilFonctionnaliteHistoDto>> SaveProfilFonctionnaliteHistos(BusinessRequest<ProfilFonctionnaliteHistoDto> request)
        {
            var response = new BusinessResponse<ProfilFonctionnaliteHistoDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _ProfilFonctionnaliteHistoProvider.SaveProfilFonctionnaliteHistos(request);

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

