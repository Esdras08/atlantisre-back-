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
    public partial class HabillitationManager
    {

        #region Singleton

        private HabillitationManager() { }
      
        private static HabillitationManager _instance;

        public static HabillitationManager Instance
        {
            get { return _instance ?? (_instance = new HabillitationManager()); }
        }

        #endregion

        public BusinessResponse<HabillitationDto> GetHabillitationById(object id)
        {
            return HabillitationProvider.Instance.GetHabillitationById(id);
        }
        public BusinessResponse<HabillitationDto> GetHabillitationsByCriteria(BusinessRequest<HabillitationDto> request)
        {
            return HabillitationProvider.Instance.GetHabillitationsByCriteria(request);
        }

        public BusinessResponse<HabillitationDto> SaveHabillitations(BusinessRequest<HabillitationDto> request)
        {
            var response = new BusinessResponse<HabillitationDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response = HabillitationProvider.Instance.SaveHabillitations(request);

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

