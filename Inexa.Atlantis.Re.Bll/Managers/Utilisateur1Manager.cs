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
    public partial class Utilisateur1Manager
    {

        #region Singleton

        private Utilisateur1Manager() { }
      
        private static Utilisateur1Manager _instance;

        public static Utilisateur1Manager Instance
        {
            get { return _instance ?? (_instance = new Utilisateur1Manager()); }
        }

        #endregion

        public BusinessResponse<Utilisateur1Dto> GetUtilisateur1ById(object id)
        {
            return Utilisateur1Provider.Instance.GetUtilisateur1ById(id);
        }
        public BusinessResponse<Utilisateur1Dto> GetUtilisateur1sByCriteria(BusinessRequest<Utilisateur1Dto> request)
        {
            return Utilisateur1Provider.Instance.GetUtilisateur1sByCriteria(request);
        }

        public BusinessResponse<Utilisateur1Dto> SaveUtilisateur1s(BusinessRequest<Utilisateur1Dto> request)
        {
            var response = new BusinessResponse<Utilisateur1Dto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response = Utilisateur1Provider.Instance.SaveUtilisateur1s(request);

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

