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
    public partial class Trace1Manager
    {

        #region Singleton

        private Trace1Manager() { }
      
        private static Trace1Manager _instance;

        public static Trace1Manager Instance
        {
            get { return _instance ?? (_instance = new Trace1Manager()); }
        }

        #endregion

        public BusinessResponse<Trace1Dto> GetTrace1ById(object id)
        {
            return Trace1Provider.Instance.GetTrace1ById(id);
        }
        public BusinessResponse<Trace1Dto> GetTrace1sByCriteria(BusinessRequest<Trace1Dto> request)
        {
            return Trace1Provider.Instance.GetTrace1sByCriteria(request);
        }

        public BusinessResponse<Trace1Dto> SaveTrace1s(BusinessRequest<Trace1Dto> request)
        {
            var response = new BusinessResponse<Trace1Dto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response = Trace1Provider.Instance.SaveTrace1s(request);

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

