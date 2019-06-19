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
    public partial class SysComplexiteMotDePasseManager
    {
        SysComplexiteMotDePasseProvider _SysComplexiteMotDePasseProvider;
        #region Singleton

        public SysComplexiteMotDePasseManager() {
           _SysComplexiteMotDePasseProvider   = new SysComplexiteMotDePasseProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<SysComplexiteMotDePasseDto>> GetSysComplexiteMotDePasseById(object id)
        {
            return await _SysComplexiteMotDePasseProvider.GetSysComplexiteMotDePasseById(id);
        }
        public async Task<BusinessResponse<SysComplexiteMotDePasseDto>> GetSysComplexiteMotDePassesByCriteria(BusinessRequest<SysComplexiteMotDePasseDto> request)
        {
            return await _SysComplexiteMotDePasseProvider.GetSysComplexiteMotDePassesByCriteria(request);
        }

        public async Task<BusinessResponse<SysComplexiteMotDePasseDto>> SaveSysComplexiteMotDePasses(BusinessRequest<SysComplexiteMotDePasseDto> request)
        {
            var response = new BusinessResponse<SysComplexiteMotDePasseDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _SysComplexiteMotDePasseProvider.SaveSysComplexiteMotDePasses(request);

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

