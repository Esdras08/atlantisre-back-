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
    public partial class Fonctionnalite1Manager
    {

        #region Singleton

        private Fonctionnalite1Manager() { }
      
        private static Fonctionnalite1Manager _instance;

        public static Fonctionnalite1Manager Instance
        {
            get { return _instance ?? (_instance = new Fonctionnalite1Manager()); }
        }

        #endregion

        public BusinessResponse<Fonctionnalite1Dto> GetFonctionnalite1ById(object id)
        {
            return Fonctionnalite1Provider.Instance.GetFonctionnalite1ById(id);
        }
        public BusinessResponse<Fonctionnalite1Dto> GetFonctionnalite1sByCriteria(BusinessRequest<Fonctionnalite1Dto> request)
        {
            return Fonctionnalite1Provider.Instance.GetFonctionnalite1sByCriteria(request);
        }

        public BusinessResponse<Fonctionnalite1Dto> SaveFonctionnalite1s(BusinessRequest<Fonctionnalite1Dto> request)
        {
            var response = new BusinessResponse<Fonctionnalite1Dto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response = Fonctionnalite1Provider.Instance.SaveFonctionnalite1s(request);

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

