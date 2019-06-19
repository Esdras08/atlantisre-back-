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
    public partial class SessionManager
    {

        #region Singleton

        private SessionManager() { }
      
        private static SessionManager _instance;

        public static SessionManager Instance
        {
            get { return _instance ?? (_instance = new SessionManager()); }
        }

        #endregion

        public BusinessResponse<SessionDto> GetSessionById(object id)
        {
            return SessionProvider.Instance.GetSessionById(id);
        }
        public BusinessResponse<SessionDto> GetSessionsByCriteria(BusinessRequest<SessionDto> request)
        {
            return SessionProvider.Instance.GetSessionsByCriteria(request);
        }

        public BusinessResponse<SessionDto> SaveSessions(BusinessRequest<SessionDto> request)
        {
            var response = new BusinessResponse<SessionDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response = SessionProvider.Instance.SaveSessions(request);

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

