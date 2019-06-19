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
    public partial class ProfessionManager
    {
        ProfessionProvider _ProfessionProvider;
        #region Singleton

        public ProfessionManager() {
           _ProfessionProvider   = new ProfessionProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<ProfessionDto>> GetProfessionById(object id)
        {
            return await _ProfessionProvider.GetProfessionById(id);
        }
        public async Task<BusinessResponse<ProfessionDto>> GetProfessionsByCriteria(BusinessRequest<ProfessionDto> request)
        {
            return await _ProfessionProvider.GetProfessionsByCriteria(request);
        }

        public async Task<BusinessResponse<ProfessionDto>> SaveProfessions(BusinessRequest<ProfessionDto> request)
        {
            var response = new BusinessResponse<ProfessionDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                    /*** Commencer la logique ici ***/
                    BusinessRequest<ProfessionDto> itemToSave = new BusinessRequest<ProfessionDto>();
                    foreach (var item in request.ItemsToSave)
                    {

                        var responseToVerif = await _ProfessionProvider.GetProfessionsByCriteria(
                                                    new BusinessRequest<ProfessionDto>()
                                                    {
                                                        ItemToSearch = new ProfessionDto()
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
                            if (item.IdProfession == 0 || item.IdProfession != responseToVerif.Items[0].IdProfession)
                                throw new Exception(string.Format("La profession {0} existe déjà", item.Libelle));
                        }
                        else
                        {

                            if (item.IdProfession == 0) item.CreatedBy = request.IdCurrentUser;
                            else item.ModifiedBy = request.IdCurrentUser;

                            itemToSave.ItemsToSave.Add(item);
                        }


                    }


                    response = await _ProfessionProvider.SaveProfessions(request);

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

