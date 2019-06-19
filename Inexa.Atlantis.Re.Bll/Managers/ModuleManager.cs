using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inexa.Atlantis.Re.Dal.Providers;
using Inexa.Atlantis.Re.Commons.Dtos;
using Inexa.Atlantis.Re.Commons.Infras.Domains;
using Inexa.Atlantis.Re.Commons.Infras.Enums;
using Inexa.Atlantis.Re.Commons.Infras.Helpers;

namespace Inexa.Atlantis.Re.Bll.Managers
{
    public partial class ModuleManager
    {

        #region Singleton

        private ModuleManager() { }
      
        private static ModuleManager _instance;

        public static ModuleManager Instance
        {
            get { return _instance ?? (_instance = new ModuleManager()); }
        }

        #endregion

        public BusinessResponse<ModuleDto> GetModuleById(object id)
        {
            return ModuleProvider.Instance.GetModuleById(id);
        }
        public BusinessResponse<ModuleDto> GetModulesByCriteria(BusinessRequest<ModuleDto> request)
        {
            return ModuleProvider.Instance.GetModulesByCriteria(request);
        }

        public BusinessResponse<ModuleDto> SaveModules(BusinessRequest<ModuleDto> request)
        {
            var response = new BusinessResponse<ModuleDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response = ModuleProvider.Instance.SaveModules(request);

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

