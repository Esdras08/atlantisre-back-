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
    public partial class DeclarationSinistreManager
    {
        DeclarationSinistreProvider _DeclarationSinistreProvider;
        #region Singleton

        public DeclarationSinistreManager() {
           _DeclarationSinistreProvider   = new DeclarationSinistreProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<DeclarationSinistreDto>> GetDeclarationSinistreById(object id)
        {
            return await _DeclarationSinistreProvider.GetDeclarationSinistreById(id);
        }
        public async Task<BusinessResponse<DeclarationSinistreDto>> GetDeclarationSinistresByCriteria(BusinessRequest<DeclarationSinistreDto> request)
        {
            return await _DeclarationSinistreProvider.GetDeclarationSinistresByCriteria(request);
        }

        public async Task<BusinessResponse<DeclarationSinistreDto>> SaveDeclarationSinistres(BusinessRequest<DeclarationSinistreDto> request)
        {
            var response = new BusinessResponse<DeclarationSinistreDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _DeclarationSinistreProvider.SaveDeclarationSinistres(request);

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

