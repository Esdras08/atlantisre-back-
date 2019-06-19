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
    public partial class TypePieceIdentiteManager
    {
        TypePieceIdentiteProvider _TypePieceIdentiteProvider;
        #region Singleton

        public TypePieceIdentiteManager() {
           _TypePieceIdentiteProvider   = new TypePieceIdentiteProvider();

		}
      
      
        #endregion

        public async Task<BusinessResponse<TypePieceIdentiteDto>> GetTypePieceIdentiteById(object id)
        {
            return await _TypePieceIdentiteProvider.GetTypePieceIdentiteById(id);
        }
        public async Task<BusinessResponse<TypePieceIdentiteDto>> GetTypePieceIdentitesByCriteria(BusinessRequest<TypePieceIdentiteDto> request)
        {
            return await _TypePieceIdentiteProvider.GetTypePieceIdentitesByCriteria(request);
        }

        public async Task<BusinessResponse<TypePieceIdentiteDto>> SaveTypePieceIdentites(BusinessRequest<TypePieceIdentiteDto> request)
        {
            var response = new BusinessResponse<TypePieceIdentiteDto>();
            
            {
                    TransactionQueueManager.BeginWork(request, response);                    

                    try
                    {
                        /*** Commencer la logique ici ***/                                                    

                        response =await _TypePieceIdentiteProvider.SaveTypePieceIdentites(request);

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

