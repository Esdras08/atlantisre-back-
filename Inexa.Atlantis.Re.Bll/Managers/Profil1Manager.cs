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
    public partial class Profil1Manager
    {

        #region Singleton

        private Profil1Manager() { }
      
        private static Profil1Manager _instance;

        public static Profil1Manager Instance
        {
            get { return _instance ?? (_instance = new Profil1Manager()); }
        }

        #endregion

        public BusinessResponse<Profil1Dto> GetProfil1ById(object id)
        {
            return Profil1Provider.Instance.GetProfil1ById(id);
        }
        public BusinessResponse<Profil1Dto> GetProfil1sByCriteria(BusinessRequest<Profil1Dto> request)
        {
            return Profil1Provider.Instance.GetProfil1sByCriteria(request);
        }

        public BusinessResponse<Profil1Dto> SaveProfil1s(BusinessRequest<Profil1Dto> request)
        {
            var response = new BusinessResponse<Profil1Dto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response = Profil1Provider.Instance.SaveProfil1s(request);

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

