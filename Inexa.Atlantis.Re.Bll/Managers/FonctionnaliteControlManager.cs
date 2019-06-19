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
    public partial class FonctionnaliteControlManager
    {

        #region Singleton

        private FonctionnaliteControlManager() { }
      
        private static FonctionnaliteControlManager _instance;

        public static FonctionnaliteControlManager Instance
        {
            get { return _instance ?? (_instance = new FonctionnaliteControlManager()); }
        }

        #endregion

        public BusinessResponse<FonctionnaliteControlDto> GetFonctionnaliteControlById(object id)
        {
            return FonctionnaliteControlProvider.Instance.GetFonctionnaliteControlById(id);
        }
        public BusinessResponse<FonctionnaliteControlDto> GetFonctionnaliteControlsByCriteria(BusinessRequest<FonctionnaliteControlDto> request)
        {
            return FonctionnaliteControlProvider.Instance.GetFonctionnaliteControlsByCriteria(request);
        }

        public BusinessResponse<FonctionnaliteControlDto> SaveFonctionnaliteControls(BusinessRequest<FonctionnaliteControlDto> request)
        {
            var response = new BusinessResponse<FonctionnaliteControlDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response = FonctionnaliteControlProvider.Instance.SaveFonctionnaliteControls(request);

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

