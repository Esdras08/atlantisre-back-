using System;
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
    public partial class SecteurActiviteManager
    {
        SecteurActiviteProvider _SecteurActiviteProvider;
        #region Singleton

        public SecteurActiviteManager() {
           _SecteurActiviteProvider   = new SecteurActiviteProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<SecteurActiviteDto>> GetSecteurActiviteById(object id)
        {
            return await _SecteurActiviteProvider.GetSecteurActiviteById(id);
        }
        public async Task<BusinessResponse<SecteurActiviteDto>> GetSecteurActivitesByCriteria(BusinessRequest<SecteurActiviteDto> request)
        {
            return await _SecteurActiviteProvider.GetSecteurActivitesByCriteria(request);
        }

        public async Task<BusinessResponse<SecteurActiviteDto>> SaveSecteurActivites(BusinessRequest<SecteurActiviteDto> request)
        {
            var response = new BusinessResponse<SecteurActiviteDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                    /*** Commencer la logique ici ***/
                    BusinessRequest<SecteurActiviteDto> itemToSave = new BusinessRequest<SecteurActiviteDto>();
                    foreach (var item in request.ItemsToSave)
                    {

                        var responseToVerif = await _SecteurActiviteProvider.GetSecteurActivitesByCriteria(
                                                    new BusinessRequest<SecteurActiviteDto>()
                                                    {
                                                        ItemToSearch = new SecteurActiviteDto()
                                                        {
                                                            Libelle = item.Libelle,
                                                            InfoSearchLibelle = new InfoSearch<string>()
                                                            {
                                                                Consider = true,
                                                                OperatorToUse = OperatorEnum.EQUAL
                                                            }
                                                        }
                                                    });
                        if (responseToVerif.Items.Any())
                        {
                            if (item.IdDomaineActivite == 0 || item.IdDomaineActivite != responseToVerif.Items[0].IdDomaineActivite)
                                throw new Exception(string.Format("Le secteur  {0} existe déjà", item.Libelle));
                        }
                        else
                        {

                            if (item.IdDomaineActivite == 0)
                            {
                                item.CreatedBy = request.IdCurrentUser;
                                
                            }

                            else {
                                item.ModifiedBy = request.IdCurrentUser;
                            } 

                            itemToSave.ItemsToSave.Add(item);
                        }


                    }

                    response =await _SecteurActiviteProvider.SaveSecteurActivites(request);

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

