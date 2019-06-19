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
    public partial class StructureManager
    {
        StructureProvider _StructureProvider;
        #region Singleton

        public StructureManager() {
           _StructureProvider   = new StructureProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<StructureDto>> GetStructureById(object id)
        {
            return await _StructureProvider.GetStructureById(id);
        }
        public async Task<BusinessResponse<StructureDto>> GetStructuresByCriteria(BusinessRequest<StructureDto> request)
        {
            return await _StructureProvider.GetStructuresByCriteria(request);
        }

        public async Task<BusinessResponse<StructureDto>> SaveStructures(BusinessRequest<StructureDto> request)
        {
            var response = new BusinessResponse<StructureDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                    /*** Commencer la logique ici ***/

                    BusinessRequest<StructureDto> itemToSave = new BusinessRequest<StructureDto>();
                    foreach (var item in request.ItemsToSave)
                    {

                        var responseToVerif = await _StructureProvider.GetStructuresByCriteria(
                                                    new BusinessRequest<StructureDto>()
                                                    {
                                                        ItemToSearch = new StructureDto()
                                                        {
                                                            RaisonSocialeStructure = item.RaisonSocialeStructure,
                                                            InfoSearchRaisonSocialeStructure = new InfoSearch<string>()
                                                            {
                                                                Consider = true,
                                                                OperatorToUse = OperatorEnum.EQUAL
                                                            }
                                                        }
                                                    });
                        if (responseToVerif.Items.Any())
                        {
                            if (item.IdStructure == 0 || item.IdStructure != responseToVerif.Items[0].IdStructure)
                                throw new Exception(string.Format("La structure {0} existe déjà", item.RaisonSocialeStructure));
                        }
                        else
                        {

                            if (item.IdStructure == 0) item.CreatedBy = request.IdCurrentUser;
                            else item.ModifiedBy = request.IdCurrentUser;

                            itemToSave.ItemsToSave.Add(item);
                        }

                        request.IdCurrentStructure = (short)item.IdStructure;
                    }
                   
                    response =await _StructureProvider.SaveStructures(request);

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

