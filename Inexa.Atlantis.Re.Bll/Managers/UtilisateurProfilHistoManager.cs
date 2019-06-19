﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inexa.Atlantis.Re.Dal.Providers;
using Inexa.Atlantis.Re.Commons.Dtos;
using Inexa.Atlantis.Re.Commons.Infras.Domains;
using Inexa.Atlantis.Re.Commons.Infras.Enums;
using Inexa.Atlantis.Re.Commons.Infras.Helpers;


namespace Inexa.Atlantis.Re.Bll.Managers
{
    public partial class UtilisateurProfilHistoManager
    {
        UtilisateurProfilHistoProvider _UtilisateurProfilHistoProvider;
        #region Singleton

        public UtilisateurProfilHistoManager() {
           _UtilisateurProfilHistoProvider   = new UtilisateurProfilHistoProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<UtilisateurProfilHistoDto>> GetUtilisateurProfilHistoById(object id)
        {
            return await _UtilisateurProfilHistoProvider.GetUtilisateurProfilHistoById(id);
        }
        public async Task<BusinessResponse<UtilisateurProfilHistoDto>> GetUtilisateurProfilHistosByCriteria(BusinessRequest<UtilisateurProfilHistoDto> request)
        {
            return await _UtilisateurProfilHistoProvider.GetUtilisateurProfilHistosByCriteria(request);
        }

        public async Task<BusinessResponse<UtilisateurProfilHistoDto>> SaveUtilisateurProfilHistos(BusinessRequest<UtilisateurProfilHistoDto> request)
        {
            var response = new BusinessResponse<UtilisateurProfilHistoDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _UtilisateurProfilHistoProvider.SaveUtilisateurProfilHistos(request);

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
