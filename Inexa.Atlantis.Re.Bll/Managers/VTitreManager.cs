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
    public partial class VTitreManager
    {

        #region Singleton

        private VTitreManager() { }
      
        private static VTitreManager _instance;

        public static VTitreManager Instance
        {
            get { return _instance ?? (_instance = new VTitreManager()); }
        }

        #endregion

        public BusinessResponse<VTitreDto> GetVTitreById(object id)
        {
            return VTitreProvider.Instance.GetVTitreById(id);
        }
        public BusinessResponse<VTitreDto> GetVTitresByCriteria(BusinessRequest<VTitreDto> request)
        {
            return VTitreProvider.Instance.GetVTitresByCriteria(request);
        }

        public BusinessResponse<VTitreDto> SaveVTitres(BusinessRequest<VTitreDto> request)
        {
            var response = new BusinessResponse<VTitreDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response = VTitreProvider.Instance.SaveVTitres(request);

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

