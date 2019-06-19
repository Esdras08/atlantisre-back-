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
    public partial class HistoriquePasswordManager
    {

        #region Singleton

        private HistoriquePasswordManager() { }
      
        private static HistoriquePasswordManager _instance;

        public static HistoriquePasswordManager Instance
        {
            get { return _instance ?? (_instance = new HistoriquePasswordManager()); }
        }

        #endregion

        public BusinessResponse<HistoriquePasswordDto> GetHistoriquePasswordById(object id)
        {
            return HistoriquePasswordProvider.Instance.GetHistoriquePasswordById(id);
        }
        public BusinessResponse<HistoriquePasswordDto> GetHistoriquePasswordsByCriteria(BusinessRequest<HistoriquePasswordDto> request)
        {
            return HistoriquePasswordProvider.Instance.GetHistoriquePasswordsByCriteria(request);
        }

        public BusinessResponse<HistoriquePasswordDto> SaveHistoriquePasswords(BusinessRequest<HistoriquePasswordDto> request)
        {
            var response = new BusinessResponse<HistoriquePasswordDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response = HistoriquePasswordProvider.Instance.SaveHistoriquePasswords(request);

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

