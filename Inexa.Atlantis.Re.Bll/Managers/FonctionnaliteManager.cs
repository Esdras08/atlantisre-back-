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
    public partial class FonctionnaliteManager
    {
        FonctionnaliteProvider _FonctionnaliteProvider;
        #region Singleton

        public FonctionnaliteManager() {
           _FonctionnaliteProvider   = new FonctionnaliteProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<FonctionnaliteDto>> GetFonctionnaliteById(object id)
        {
            return await _FonctionnaliteProvider.GetFonctionnaliteById(id);
        }
        public async Task<BusinessResponse<FonctionnaliteDto>> GetFonctionnalitesByCriteria(BusinessRequest<FonctionnaliteDto> request)
        {
            return await _FonctionnaliteProvider.GetFonctionnalitesByCriteria(request);
        }

        public async Task<BusinessResponse<FonctionnaliteDto>> SaveFonctionnalites(BusinessRequest<FonctionnaliteDto> request)
        {
            var response = new BusinessResponse<FonctionnaliteDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _FonctionnaliteProvider.SaveFonctionnalites(request);

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

