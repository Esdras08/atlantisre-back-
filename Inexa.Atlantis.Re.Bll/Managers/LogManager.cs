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
    public partial class LogManager
    {

        #region Singleton

        private LogManager() { }
      
        private static LogManager _instance;

        public static LogManager Instance
        {
            get { return _instance ?? (_instance = new LogManager()); }
        }

        #endregion

        public BusinessResponse<LogDto> GetLogById(object id)
        {
            return LogProvider.Instance.GetLogById(id);
        }
        public BusinessResponse<LogDto> GetLogsByCriteria(BusinessRequest<LogDto> request)
        {
            return LogProvider.Instance.GetLogsByCriteria(request);
        }

        public BusinessResponse<LogDto> SaveLogs(BusinessRequest<LogDto> request)
        {
            var response = new BusinessResponse<LogDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response = LogProvider.Instance.SaveLogs(request);

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

