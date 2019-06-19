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
    public partial class TraceConformiteManager
    {

        #region Singleton

        private TraceConformiteManager() { }
      
        private static TraceConformiteManager _instance;

        public static TraceConformiteManager Instance
        {
            get { return _instance ?? (_instance = new TraceConformiteManager()); }
        }

        #endregion

        public BusinessResponse<TraceConformiteDto> GetTraceConformiteById(object id)
        {
            return TraceConformiteProvider.Instance.GetTraceConformiteById(id);
        }
        public BusinessResponse<TraceConformiteDto> GetTraceConformitesByCriteria(BusinessRequest<TraceConformiteDto> request)
        {
            return TraceConformiteProvider.Instance.GetTraceConformitesByCriteria(request);
        }

        public BusinessResponse<TraceConformiteDto> SaveTraceConformites(BusinessRequest<TraceConformiteDto> request)
        {
            var response = new BusinessResponse<TraceConformiteDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response = TraceConformiteProvider.Instance.SaveTraceConformites(request);

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

