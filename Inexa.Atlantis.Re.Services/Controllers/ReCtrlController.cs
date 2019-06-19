using System;
using System.Web.Http;
using System.Threading.Tasks;
using Inexa.Atlantis.Re.Bll.Managers;
using Inexa.Atlantis.Re.Commons.Dtos;
using Inexa.Atlantis.Re.Commons.Infras.Domains;

namespace Inexa.Atlantis.Re.Services.Controllers
{
    public class ReCtrlController: ApiController
    {

        #region Pays

        [ActionName("GetPayById")]
        public async Task<BusinessResponse<PayDto>> GetPayById(int id)
        {
            var response = new BusinessResponse<PayDto>();

            try
            {
                response = await new PayManager().GetPayById(id);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }


        [ActionName("GetPaysByCriteria")]
        public async Task<BusinessResponse<PayDto>> PostPaysByCriteria([FromBody]BusinessRequest<PayDto> request)
        {
            var response = new BusinessResponse<PayDto>();

            try
            {
                response = await new PayManager().GetPaysByCriteria(request);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }

        [ActionName("SavePays")]
        public async Task<BusinessResponse<PayDto>> PostSavePays([FromBody]BusinessRequest<PayDto> request)
        {
            var response = new BusinessResponse<PayDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new PayManager().SavePays(request);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }

        #endregion

        #region Devises

        [ActionName("GetDeviseById")]
        public async Task<BusinessResponse<DeviseDto>> GetDeviseById(int id)
        {
            var response = new BusinessResponse<DeviseDto>();

            try
            {
                response = await new DeviseManager().GetDeviseById(id);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }


        [ActionName("GetDevisesByCriteria")]
        public async Task<BusinessResponse<DeviseDto>> PostDevisesByCriteria([FromBody]BusinessRequest<DeviseDto> request)
        {
            var response = new BusinessResponse<DeviseDto>();

            try
            {
                response = await new DeviseManager().GetDevisesByCriteria(request);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }



        [ActionName("SaveDevises")]
        public async Task<BusinessResponse<DeviseDto>> PostSaveDevises([FromBody]BusinessRequest<DeviseDto> request)
        {
            var response = new BusinessResponse<DeviseDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new DeviseManager().SaveDevises(request);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }

        #endregion

        #region Professions

        [ActionName("GetProfessionById")]
        public async Task<BusinessResponse<ProfessionDto>> GetProfessionById(int id)
        {
            var response = new BusinessResponse<ProfessionDto>();

            try
            {
                response = await new ProfessionManager().GetProfessionById(id);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }


        [ActionName("GetProfessionsByCriteria")]
        public async Task<BusinessResponse<ProfessionDto>> PostProfessionsByCriteria([FromBody]BusinessRequest<ProfessionDto> request)
        {
            var response = new BusinessResponse<ProfessionDto>();

            try
            {
                response = await new ProfessionManager().GetProfessionsByCriteria(request);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }



        [ActionName("SaveProfessions")]
        public async Task<BusinessResponse<ProfessionDto>> PostSaveProfessions([FromBody]BusinessRequest<ProfessionDto> request)
        {
            var response = new BusinessResponse<ProfessionDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new ProfessionManager().SaveProfessions(request);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }

        #endregion

        #region SecteurActivites

        [ActionName("GetSecteurActiviteById")]
        public async Task<BusinessResponse<SecteurActiviteDto>> GetSecteurActiviteById(int id)
        {
            var response = new BusinessResponse<SecteurActiviteDto>();

            try
            {
                response = await new SecteurActiviteManager().GetSecteurActiviteById(id);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }


        [ActionName("GetSecteurActivitesByCriteria")]
        public async Task<BusinessResponse<SecteurActiviteDto>> PostSecteurActivitesByCriteria([FromBody]BusinessRequest<SecteurActiviteDto> request)
        {
            var response = new BusinessResponse<SecteurActiviteDto>();

            try
            {
                response = await new SecteurActiviteManager().GetSecteurActivitesByCriteria(request);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }



        [ActionName("SaveSecteurActivites")]
        public async Task<BusinessResponse<SecteurActiviteDto>> PostSaveSecteurActivites([FromBody]BusinessRequest<SecteurActiviteDto> request)
        {
            var response = new BusinessResponse<SecteurActiviteDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new SecteurActiviteManager().SaveSecteurActivites(request);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }

        #endregion

        #region Structures

        [ActionName("GetStructureById")]
        public async Task<BusinessResponse<StructureDto>> GetStructureById(int id)
        {
            var response = new BusinessResponse<StructureDto>();

            try
            {
                response = await new StructureManager().GetStructureById(id);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }


        [ActionName("GetStructuresByCriteria")]
        public async Task<BusinessResponse<StructureDto>> PostStructuresByCriteria([FromBody]BusinessRequest<StructureDto> request)
        {
            var response = new BusinessResponse<StructureDto>();

            try
            {
                response = await new StructureManager().GetStructuresByCriteria(request);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }



        [ActionName("SaveStructures")]
        public async Task<BusinessResponse<StructureDto>> PostSaveStructures([FromBody]BusinessRequest<StructureDto> request)
        {
            var response = new BusinessResponse<StructureDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new StructureManager().SaveStructures(request);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }

        #endregion

        #region EtapeProcessus

                [ActionName("GetEtapeProcessuById")]
                public async Task<BusinessResponse<EtapeProcessuDto>> GetEtapeProcessuById(int id)
                {
                    var response = new BusinessResponse<EtapeProcessuDto>();

                    try
                    {
                        response = await new EtapeProcessuManager().GetEtapeProcessuById(id);
                        if (response.HasError) throw new Exception(response.Message);
                    }
                    catch (Exception ex)
                    {
                        response.HasError = true;
                        response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    }

                    return response;
                }
                [ActionName("GetEtapeProcessusByCriteria")]
                public async Task<BusinessResponse<EtapeProcessuDto>> PostEtapeProcessusByCriteria([FromBody]BusinessRequest<EtapeProcessuDto> request)
                {
                    var response = new BusinessResponse<EtapeProcessuDto>();

                    try
                    {
                        response = await new EtapeProcessuManager().GetEtapeProcessusByCriteria(request);
                        if (response.HasError) throw new Exception(response.Message);
                    }
                    catch (Exception ex)
                    {
                        response.HasError = true;
                        response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    }

                    return response;
                }


                [ActionName("SaveEtapeProcessus")]
                public async Task<BusinessResponse<EtapeProcessuDto>> PostSaveEtapeProcessus([FromBody]BusinessRequest<EtapeProcessuDto> request)
                {
                    var response = new BusinessResponse<EtapeProcessuDto>();
                    request.CanApplyTransaction = true;

                    try
                    {
                        response = await new EtapeProcessuManager().SaveEtapeProcessus(request);
                        if (response.HasError) throw new Exception(response.Message);
                    }
                    catch (Exception ex)
                    {
                        response.HasError = true;
                        response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    }

                    return response;
                }

        #endregion

        #region Echanges
        
                [ActionName("GetEchangeById")]
                public async Task<BusinessResponse<EchangeDto>> GetEchangeById(int id)
                {
                    var response = new BusinessResponse<EchangeDto>();

                    try
                    {
                        response = await new EchangeManager().GetEchangeById(id);
                        if (response.HasError) throw new Exception(response.Message);
                    }
                    catch (Exception ex)
                    {
                        response.HasError = true;
                        response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    }

                    return response;
                }

                [ActionName("GetEchangesByCriteria")]
                public async Task<BusinessResponse<EchangeDto>> PostEchangesByCriteria([FromBody]BusinessRequest<EchangeDto> request)
                {
                    var response = new BusinessResponse<EchangeDto>();

                    try
                    {
                        response = await new EchangeManager().GetEchangesByCriteria(request);
                        if (response.HasError) throw new Exception(response.Message);
                    }
                    catch (Exception ex)
                    {
                        response.HasError = true;
                        response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    }

                    return response;
                }
       
                [ActionName("SaveEchanges")]
                public async Task<BusinessResponse<EchangeDto>> PostSaveEchanges([FromBody]BusinessRequest<EchangeDto> request)
                {
                    var response = new BusinessResponse<EchangeDto>();
                    request.CanApplyTransaction = true;

                    try
                    {
                        response = await new EchangeManager().SaveEchanges(request);
                        if (response.HasError) throw new Exception(response.Message);
                    }
                    catch (Exception ex)
                    {
                        response.HasError = true;
                        response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    }

                    return response;
                }

        #endregion

        #region Affaires
        
                [ActionName("GetAffaireById")]
                public async Task<BusinessResponse<AffaireDto>> GetAffaireById(int id)
                {
                    var response = new BusinessResponse<AffaireDto>();

                    try
                    {
                        response = await new AffaireManager().GetAffaireById(id);
                        if (response.HasError) throw new Exception(response.Message);
                    }
                    catch (Exception ex)
                    {
                        response.HasError = true;
                        response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    }

                    return response;
                }

                [ActionName("GetAffairesByCriteria")]
                public async Task<BusinessResponse<AffaireDto>> PostAffairesByCriteria([FromBody]BusinessRequest<AffaireDto> request)
                {
                    var response = new BusinessResponse<AffaireDto>();

                    try
                    {
                        response = await new AffaireManager().GetAffairesByCriteria(request);
                        if (response.HasError) throw new Exception(response.Message);
                    }
                    catch (Exception ex)
                    {
                        response.HasError = true;
                        response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    }

                    return response;
                }
       
                [ActionName("SaveAffaires")]
                public async Task<BusinessResponse<AffaireDto>> PostSaveAffaires([FromBody]BusinessRequest<AffaireDto> request)
                {
                    var response = new BusinessResponse<AffaireDto>();
                    request.CanApplyTransaction = true;

                    try
                    {
                        response = await new AffaireManager().SaveAffaires(request);
                        if (response.HasError) throw new Exception(response.Message);
                    }
                    catch (Exception ex)
                    {
                        response.HasError = true;
                        response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    }

                    return response;
                }

        #endregion

        #region Processus
        
                [ActionName("GetProcessuById")]
                public async Task<BusinessResponse<ProcessuDto>> GetProcessuById(int id)
                {
                    var response = new BusinessResponse<ProcessuDto>();

                    try
                    {
                        response = await new ProcessuManager().GetProcessuById(id);
                        if (response.HasError) throw new Exception(response.Message);
                    }
                    catch (Exception ex)
                    {
                        response.HasError = true;
                        response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    }

                    return response;
                }

                [ActionName("GetProcessusByCriteria")]
                public async Task<BusinessResponse<ProcessuDto>> PostProcessusByCriteria([FromBody]BusinessRequest<ProcessuDto> request)
                {
                    var response = new BusinessResponse<ProcessuDto>();

                    try
                    {
                        response = await new ProcessuManager().GetProcessusByCriteria(request);
                        if (response.HasError) throw new Exception(response.Message);
                    }
                    catch (Exception ex)
                    {
                        response.HasError = true;
                        response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    }

                    return response;
                }

        [ActionName("SaveProcessus")]
                public async Task<BusinessResponse<ProcessuDto>> PostSaveProcessus([FromBody]BusinessRequest<ProcessuDto> request)
                {
                    var response = new BusinessResponse<ProcessuDto>();
                    request.CanApplyTransaction = true;

                    try
                    {
                        response = await new ProcessuManager().SaveProcessus(request);
                        if (response.HasError) throw new Exception(response.Message);
                    }
                    catch (Exception ex)
                    {
                        response.HasError = true;
                        response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    }

                    return response;
                }

        #endregion

        #region StatutAffaires
        
                [ActionName("GetStatutAffaireById")]
                public async Task<BusinessResponse<StatutAffaireDto>> GetStatutAffaireById(int id)
                {
                    var response = new BusinessResponse<StatutAffaireDto>();

                    try
                    {
                        response = await new StatutAffaireManager().GetStatutAffaireById(id);
                        if (response.HasError) throw new Exception(response.Message);
                    }
                    catch (Exception ex)
                    {
                        response.HasError = true;
                        response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    }

                    return response;
                }

                [ActionName("GetStatutAffairesByCriteria")]
                public async Task<BusinessResponse<StatutAffaireDto>> PostStatutAffairesByCriteria([FromBody]BusinessRequest<StatutAffaireDto> request)
                {
                    var response = new BusinessResponse<StatutAffaireDto>();

                    try
                    {
                        response = await new StatutAffaireManager().GetStatutAffairesByCriteria(request);
                        if (response.HasError) throw new Exception(response.Message);
                    }
                    catch (Exception ex)
                    {
                        response.HasError = true;
                        response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    }

                    return response;
                }

                [ActionName("SaveStatutAffaires")]
                public async Task<BusinessResponse<StatutAffaireDto>> PostSaveStatutAffaires([FromBody]BusinessRequest<StatutAffaireDto> request)
                {
                    var response = new BusinessResponse<StatutAffaireDto>();
                    request.CanApplyTransaction = true;

                    try
                    {
                        response = await new StatutAffaireManager().SaveStatutAffaires(request);
                        if (response.HasError) throw new Exception(response.Message);
                    }
                    catch (Exception ex)
                    {
                        response.HasError = true;
                        response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    }

                    return response;
                }
        
        #endregion

        #region StatutMouvements
        
                [ActionName("GetStatutMouvementById")]
                public async Task<BusinessResponse<StatutMouvementDto>> GetStatutMouvementById(int id)
                {
                    var response = new BusinessResponse<StatutMouvementDto>();

                    try
                    {
                        response = await new StatutMouvementManager().GetStatutMouvementById(id);
                        if (response.HasError) throw new Exception(response.Message);
                    }
                    catch (Exception ex)
                    {
                        response.HasError = true;
                        response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    }

                    return response;
                }

                [ActionName("GetStatutMouvementsByCriteria")]
                public async Task<BusinessResponse<StatutMouvementDto>> PostStatutMouvementsByCriteria([FromBody]BusinessRequest<StatutMouvementDto> request)
                {
                    var response = new BusinessResponse<StatutMouvementDto>();

                    try
                    {
                        response = await new StatutMouvementManager().GetStatutMouvementsByCriteria(request);
                        if (response.HasError) throw new Exception(response.Message);
                    }
                    catch (Exception ex)
                    {
                        response.HasError = true;
                        response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    }

                    return response;
                }
        
                [ActionName("SaveStatutMouvements")]
                public async Task<BusinessResponse<StatutMouvementDto>> PostSaveStatutMouvements([FromBody]BusinessRequest<StatutMouvementDto> request)
                {
                    var response = new BusinessResponse<StatutMouvementDto>();
                    request.CanApplyTransaction = true;

                    try
                    {
                        response = await new StatutMouvementManager().SaveStatutMouvements(request);
                        if (response.HasError) throw new Exception(response.Message);
                    }
                    catch (Exception ex)
                    {
                        response.HasError = true;
                        response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    }

                    return response;
                }
        
        #endregion

        #region SuiviProcessus
        
                [ActionName("GetSuiviProcessuById")]
                public async Task<BusinessResponse<SuiviProcessuDto>> GetSuiviProcessuById(int id)
                {
                    var response = new BusinessResponse<SuiviProcessuDto>();

                    try
                    {
                        response = await new SuiviProcessuManager().GetSuiviProcessuById(id);
                        if (response.HasError) throw new Exception(response.Message);
                    }
                    catch (Exception ex)
                    {
                        response.HasError = true;
                        response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    }

                    return response;
                }

                [ActionName("GetSuiviProcessusByCriteria")]
                public async Task<BusinessResponse<SuiviProcessuDto>> PostSuiviProcessusByCriteria([FromBody]BusinessRequest<SuiviProcessuDto> request)
                {
                    var response = new BusinessResponse<SuiviProcessuDto>();

                    try
                    {
                        response = await new SuiviProcessuManager().GetSuiviProcessusByCriteria(request);
                        if (response.HasError) throw new Exception(response.Message);
                    }
                    catch (Exception ex)
                    {
                        response.HasError = true;
                        response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    }

                    return response;
                }
        
                [ActionName("SaveSuiviProcessus")]
                public async Task<BusinessResponse<SuiviProcessuDto>> PostSaveSuiviProcessus([FromBody]BusinessRequest<SuiviProcessuDto> request)
                {
                    var response = new BusinessResponse<SuiviProcessuDto>();
                    request.CanApplyTransaction = true;

                    try
                    {
                        response = await new SuiviProcessuManager().SaveSuiviProcessus(request);
                        if (response.HasError) throw new Exception(response.Message);
                    }
                    catch (Exception ex)
                    {
                        response.HasError = true;
                        response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    }

                    return response;
                }

        #endregion

        #region TypeEchanges
        
                [ActionName("GetTypeEchangeById")]
                public async Task<BusinessResponse<TypeEchangeDto>> GetTypeEchangeById(int id)
                {
                    var response = new BusinessResponse<TypeEchangeDto>();

                    try
                    {
                        response = await new TypeEchangeManager().GetTypeEchangeById(id);
                        if (response.HasError) throw new Exception(response.Message);
                    }
                    catch (Exception ex)
                    {
                        response.HasError = true;
                        response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    }

                    return response;
                }
                
                [ActionName("GetTypeEchangesByCriteria")]
                public async Task<BusinessResponse<TypeEchangeDto>> PostTypeEchangesByCriteria([FromBody]BusinessRequest<TypeEchangeDto> request)
                {
                    var response = new BusinessResponse<TypeEchangeDto>();

                    try
                    {
                        response = await new TypeEchangeManager().GetTypeEchangesByCriteria(request);
                        if (response.HasError) throw new Exception(response.Message);
                    }
                    catch (Exception ex)
                    {
                        response.HasError = true;
                        response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    }

                    return response;
                }
        
                [ActionName("SaveTypeEchanges")]
                public async Task<BusinessResponse<TypeEchangeDto>> PostSaveTypeEchanges([FromBody]BusinessRequest<TypeEchangeDto> request)
                {
                    var response = new BusinessResponse<TypeEchangeDto>();
                    request.CanApplyTransaction = true;

                    try
                    {
                        response = await new TypeEchangeManager().SaveTypeEchanges(request);
                        if (response.HasError) throw new Exception(response.Message);
                    }
                    catch (Exception ex)
                    {
                        response.HasError = true;
                        response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    }

                    return response;
                }
        
        #endregion

        #region TypeProcessus
        
                [ActionName("GetTypeProcessuById")]
                public async Task<BusinessResponse<TypeProcessuDto>> GetTypeProcessuById(int id)
                {
                    var response = new BusinessResponse<TypeProcessuDto>();

                    try
                    {
                        response = await new TypeProcessuManager().GetTypeProcessuById(id);
                        if (response.HasError) throw new Exception(response.Message);
                    }
                    catch (Exception ex)
                    {
                        response.HasError = true;
                        response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    }

                    return response;
                }

                [ActionName("GetTypeProcessusByCriteria")]
                public async Task<BusinessResponse<TypeProcessuDto>> PostTypeProcessusByCriteria([FromBody]BusinessRequest<TypeProcessuDto> request)
                {
                    var response = new BusinessResponse<TypeProcessuDto>();

                    try
                    {
                        response = await new TypeProcessuManager().GetTypeProcessusByCriteria(request);
                        if (response.HasError) throw new Exception(response.Message);
                    }
                    catch (Exception ex)
                    {
                        response.HasError = true;
                        response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    }

                    return response;
                }

                [ActionName("SaveTypeProcessus")]
                public async Task<BusinessResponse<TypeProcessuDto>> PostSaveTypeProcessus([FromBody]BusinessRequest<TypeProcessuDto> request)
                {
                    var response = new BusinessResponse<TypeProcessuDto>();
                    request.CanApplyTransaction = true;

                    try
                    {
                        response = await new TypeProcessuManager().SaveTypeProcessus(request);
                        if (response.HasError) throw new Exception(response.Message);
                    }
                    catch (Exception ex)
                    {
                        response.HasError = true;
                        response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    }

                    return response;
                }
        
        #endregion

        #region Assures

        [ActionName("GetAssureById")]
        public async Task<BusinessResponse<AssureDto>> GetAssureById(int id)
        {
            var response = new BusinessResponse<AssureDto>();

            try
            {
                response = await new AssureManager().GetAssureById(id);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }


        [ActionName("GetAssuresByCriteria")]
        public async Task<BusinessResponse<AssureDto>> PostAssuresByCriteria([FromBody]BusinessRequest<AssureDto> request)
        {
            var response = new BusinessResponse<AssureDto>();

            try
            {
                response = await new AssureManager().GetAssuresByCriteria(request);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }

        [ActionName("SaveAssures")]
        public async Task<BusinessResponse<AssureDto>> PostSaveAssures([FromBody]BusinessRequest<AssureDto> request)
        {
            var response = new BusinessResponse<AssureDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new AssureManager().SaveAssures(request);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }

        #endregion

        #region Branches

        [ActionName("GetBrancheById")]
        public async Task<BusinessResponse<BrancheDto>> GetBrancheById(int id)
        {
            var response = new BusinessResponse<BrancheDto>();

            try
            {
                response = await new BrancheManager().GetBrancheById(id);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }


        [ActionName("GetBranchesByCriteria")]
        public async Task<BusinessResponse<BrancheDto>> PostBranchesByCriteria([FromBody]BusinessRequest<BrancheDto> request)
        {
            var response = new BusinessResponse<BrancheDto>();

            try
            {
                response = await new BrancheManager().GetBranchesByCriteria(request);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }



        [ActionName("SaveBranches")]
        public async Task<BusinessResponse<BrancheDto>> PostSaveBranches([FromBody]BusinessRequest<BrancheDto> request)
        {
            var response = new BusinessResponse<BrancheDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new BrancheManager().SaveBranches(request);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }

        #endregion

        #region Filiales

        [ActionName("GetFilialeById")]
        public async Task<BusinessResponse<FilialeDto>> GetFilialeById(int id)
        {
            var response = new BusinessResponse<FilialeDto>();

            try
            {
                response = await new FilialeManager().GetFilialeById(id);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }


        [ActionName("GetFilialesByCriteria")]
        public async Task<BusinessResponse<FilialeDto>> PostFilialesByCriteria([FromBody]BusinessRequest<FilialeDto> request)
        {
            var response = new BusinessResponse<FilialeDto>();

            try
            {
                response = await new FilialeManager().GetFilialesByCriteria(request);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }



        [ActionName("SaveFiliales")]
        public async Task<BusinessResponse<FilialeDto>> PostSaveFiliales([FromBody]BusinessRequest<FilialeDto> request)
        {
            var response = new BusinessResponse<FilialeDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new FilialeManager().SaveFiliales(request);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }

        #endregion

        #region Reassureurs

        [ActionName("GetReassureurById")]
        public async Task<BusinessResponse<ReassureurDto>> GetReassureurById(int id)
        {
            var response = new BusinessResponse<ReassureurDto>();

            try
            {
                response = await new ReassureurManager().GetReassureurById(id);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }


        [ActionName("GetReassureursByCriteria")]
        public async Task<BusinessResponse<ReassureurDto>> PostReassureursByCriteria([FromBody]BusinessRequest<ReassureurDto> request)
        {
            var response = new BusinessResponse<ReassureurDto>();

            try
            {
                response = await new ReassureurManager().GetReassureursByCriteria(request);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }



        [ActionName("SaveReassureurs")]
        public async Task<BusinessResponse<ReassureurDto>> PostSaveReassureurs([FromBody]BusinessRequest<ReassureurDto> request)
        {
            var response = new BusinessResponse<ReassureurDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new ReassureurManager().SaveReassureurs(request);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }

        #endregion

        #region Traites

        [ActionName("GetTraiteById")]
        public async Task<BusinessResponse<TraiteDto>> GetTraiteById(int id)
        {
            var response = new BusinessResponse<TraiteDto>();

            try
            {
                response = await new TraiteManager().GetTraiteById(id);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }


        [ActionName("GetTraitesByCriteria")]
        public async Task<BusinessResponse<TraiteDto>> PostTraitesByCriteria([FromBody]BusinessRequest<TraiteDto> request)
        {
            var response = new BusinessResponse<TraiteDto>();

            try
            {
                response = await new TraiteManager().GetTraitesByCriteria(request);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }



        [ActionName("SaveTraites")]
        public async Task<BusinessResponse<TraiteDto>> PostSaveTraites([FromBody]BusinessRequest<TraiteDto> request)
        {
            var response = new BusinessResponse<TraiteDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new TraiteManager().SaveTraites(request);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }

        #endregion

        #region Profils
        
                [ActionName("GetProfilById")]
                public async Task<BusinessResponse<ProfilDto>> GetProfilById(int id)
                {
                    var response = new BusinessResponse<ProfilDto>();

                    try
                    {
                        response = await new ProfilManager().GetProfilById(id);
                        if (response.HasError) throw new Exception(response.Message);
                    }
                    catch (Exception ex)
                    {
                        response.HasError = true;
                        response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    }

                    return response;
                }

                [ActionName("GetProfilsByCriteria")]
                public async Task<BusinessResponse<ProfilDto>> PostProfilsByCriteria([FromBody]BusinessRequest<ProfilDto> request)
                {
                    var response = new BusinessResponse<ProfilDto>();

                    try
                    {
                        response = await new ProfilManager().GetProfilsByCriteria(request);
                        if (response.HasError) throw new Exception(response.Message);
                    }
                    catch (Exception ex)
                    {
                        response.HasError = true;
                        response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    }

                    return response;
                }
        

       
                [ActionName("SaveProfils")]
                public async Task<BusinessResponse<ProfilDto>> PostSaveProfils([FromBody]BusinessRequest<ProfilDto> request)
                {
                    var response = new BusinessResponse<ProfilDto>();
                    request.CanApplyTransaction = true;

                    try
                    {
                        response = await new ProfilManager().SaveProfils(request);
                        if (response.HasError) throw new Exception(response.Message);
                    }
                    catch (Exception ex)
                    {
                        response.HasError = true;
                        response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    }

                    return response;
                }

        #endregion

        #region Fonctionnalites
        
                [ActionName("GetFonctionnaliteById")]
                public async Task<BusinessResponse<FonctionnaliteDto>> GetFonctionnaliteById(int id)
                {
                    var response = new BusinessResponse<FonctionnaliteDto>();

                    try
                    {
                        response = await new FonctionnaliteManager().GetFonctionnaliteById(id);
                        if (response.HasError) throw new Exception(response.Message);
                    }
                    catch (Exception ex)
                    {
                        response.HasError = true;
                        response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    }

                    return response;
                }

                [ActionName("GetFonctionnalitesByCriteria")]
                public async Task<BusinessResponse<FonctionnaliteDto>> PostFonctionnalitesByCriteria([FromBody]BusinessRequest<FonctionnaliteDto> request)
                {
                    var response = new BusinessResponse<FonctionnaliteDto>();

                    try
                    {
                        response = await new FonctionnaliteManager().GetFonctionnalitesByCriteria(request);
                        if (response.HasError) throw new Exception(response.Message);
                    }
                    catch (Exception ex)
                    {
                        response.HasError = true;
                        response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    }

                    return response;
                }
        
                [ActionName("SaveFonctionnalites")]
                public async Task<BusinessResponse<FonctionnaliteDto>> PostSaveFonctionnalites([FromBody]BusinessRequest<FonctionnaliteDto> request)
                {
                    var response = new BusinessResponse<FonctionnaliteDto>();
                    request.CanApplyTransaction = true;

                    try
                    {
                        response = await new FonctionnaliteManager().SaveFonctionnalites(request);
                        if (response.HasError) throw new Exception(response.Message);
                    }
                    catch (Exception ex)
                    {
                        response.HasError = true;
                        response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    }

                    return response;
                }
        
        #endregion

        #region Utilisateurs

        [ActionName("GetUtilisateurById")]
                public async Task<BusinessResponse<UtilisateurDto>> GetUtilisateurById(int id)
                {
                    var response = new BusinessResponse<UtilisateurDto>();

                    try
                    {
                        response = await new UtilisateurManager().GetUtilisateurById(id);
                        if (response.HasError) throw new Exception(response.Message);
                    }
                    catch (Exception ex)
                    {
                        response.HasError = true;
                        response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    }

                    return response;
                }

                [ActionName("GetUtilisateursByCriteria")]
                public async Task<BusinessResponse<UtilisateurDto>> PostUtilisateursByCriteria([FromBody]BusinessRequest<UtilisateurDto> request)
                {
                    var response = new BusinessResponse<UtilisateurDto>();

                    try
                    {
                        response = await new UtilisateurManager().GetUtilisateursByCriteria(request);
                        if (response.HasError) throw new Exception(response.Message);
                    }
                    catch (Exception ex)
                    {
                        response.HasError = true;
                        response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    }

                    return response;
                }
        
                [ActionName("SaveUtilisateurs")]
                public async Task<BusinessResponse<UtilisateurDto>> PostSaveUtilisateurs([FromBody]BusinessRequest<UtilisateurDto> request)
                {
                    var response = new BusinessResponse<UtilisateurDto>();
                    request.CanApplyTransaction = true;

                    try
                    {
                        response = await new UtilisateurManager().SaveUtilisateurs(request);
                        if (response.HasError) throw new Exception(response.Message);
                    }
                    catch (Exception ex)
                    {
                        response.HasError = true;
                        response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    }

                    return response;
                }
        
        #endregion

        #region UtilisateurFonctionnalites

        [ActionName("GetUtilisateurFonctionnaliteById")]
        public async Task<BusinessResponse<UtilisateurFonctionnaliteDto>> GetUtilisateurFonctionnaliteById(int id)
        {
            var response = new BusinessResponse<UtilisateurFonctionnaliteDto>();

            try
            {
                response = await new UtilisateurFonctionnaliteManager().GetUtilisateurFonctionnaliteById(id);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }

        [ActionName("GetUtilisateurFonctionnalitesByCriteria")]
        public async Task<BusinessResponse<UtilisateurFonctionnaliteDto>> PostUtilisateurFonctionnalitesByCriteria([FromBody]BusinessRequest<UtilisateurFonctionnaliteDto> request)
        {
            var response = new BusinessResponse<UtilisateurFonctionnaliteDto>();

            try
            {
                response = await new UtilisateurFonctionnaliteManager().GetUtilisateurFonctionnalitesByCriteria(request);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }

        [ActionName("SaveUtilisateurFonctionnalites")]
        public async Task<BusinessResponse<UtilisateurFonctionnaliteDto>> PostSaveUtilisateurFonctionnalites([FromBody]BusinessRequest<UtilisateurFonctionnaliteDto> request)
        {
            var response = new BusinessResponse<UtilisateurFonctionnaliteDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new UtilisateurFonctionnaliteManager().SaveUtilisateurFonctionnalites(request);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }

        #endregion

        #region ProfilFonctionnalites
        
                [ActionName("GetProfilFonctionnaliteById")]
                public async Task<BusinessResponse<ProfilFonctionnaliteDto>> GetProfilFonctionnaliteById(int id)
                {
                    var response = new BusinessResponse<ProfilFonctionnaliteDto>();

                    try
                    {
                        response = await new ProfilFonctionnaliteManager().GetProfilFonctionnaliteById(id);
                        if (response.HasError) throw new Exception(response.Message);
                    }
                    catch (Exception ex)
                    {
                        response.HasError = true;
                        response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    }

                    return response;
                }

                [ActionName("GetProfilFonctionnalitesByCriteria")]
                public async Task<BusinessResponse<ProfilFonctionnaliteDto>> PostProfilFonctionnalitesByCriteria([FromBody]BusinessRequest<ProfilFonctionnaliteDto> request)
                {
                    var response = new BusinessResponse<ProfilFonctionnaliteDto>();

                    try
                    {
                        response = await new ProfilFonctionnaliteManager().GetProfilFonctionnalitesByCriteria(request);
                        if (response.HasError) throw new Exception(response.Message);
                    }
                    catch (Exception ex)
                    {
                        response.HasError = true;
                        response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    }

                    return response;
                }
        
                [ActionName("SaveProfilFonctionnalites")]
                public async Task<BusinessResponse<ProfilFonctionnaliteDto>> PostSaveProfilFonctionnalites([FromBody]BusinessRequest<ProfilFonctionnaliteDto> request)
                {
                    var response = new BusinessResponse<ProfilFonctionnaliteDto>();
                    request.CanApplyTransaction = true;

                    try
                    {
                        response = await new ProfilFonctionnaliteManager().SaveProfilFonctionnalites(request);
                        if (response.HasError) throw new Exception(response.Message);
                    }
                    catch (Exception ex)
                    {
                        response.HasError = true;
                        response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    }

                    return response;
                }

        #endregion

        #region UtilisateurProfils

        [ActionName("GetUtilisateurProfilById")]
        public async Task<BusinessResponse<UtilisateurProfilDto>> GetUtilisateurProfilById(int id)
        {
            var response = new BusinessResponse<UtilisateurProfilDto>();

            try
            {
                response = await new UtilisateurProfilManager().GetUtilisateurProfilById(id);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }

        [ActionName("GetUtilisateurProfilsByCriteria")]
        public async Task<BusinessResponse<UtilisateurProfilDto>> PostUtilisateurProfilsByCriteria([FromBody]BusinessRequest<UtilisateurProfilDto> request)
        {
            var response = new BusinessResponse<UtilisateurProfilDto>();

            try
            {
                response = await new UtilisateurProfilManager().GetUtilisateurProfilsByCriteria(request);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }

        [ActionName("SaveUtilisateurProfils")]
        public async Task<BusinessResponse<UtilisateurProfilDto>> PostSaveUtilisateurProfils([FromBody]BusinessRequest<UtilisateurProfilDto> request)
        {
            var response = new BusinessResponse<UtilisateurProfilDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new UtilisateurProfilManager().SaveUtilisateurProfils(request);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }

        #endregion

        #region Personnes

        [ActionName("GetPersonneById")]
        public async Task<BusinessResponse<PersonneDto>> GetPersonneById(int id)
        {
            var response = new BusinessResponse<PersonneDto>();

            try
            {
                response = await new PersonneManager().GetPersonneById(id);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }


        [ActionName("GetPersonnesByCriteria")]
        public async Task<BusinessResponse<PersonneDto>> PostPersonnesByCriteria([FromBody]BusinessRequest<PersonneDto> request)
        {
            var response = new BusinessResponse<PersonneDto>();

            try
            {
                response = await new PersonneManager().GetPersonnesByCriteria(request);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }

        [ActionName("SavePersonnes")]
        public async Task<BusinessResponse<PersonneDto>> PostSavePersonnes([FromBody]BusinessRequest<PersonneDto> request)
        {
            var response = new BusinessResponse<PersonneDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new PersonneManager().SavePersonnes(request);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }

        #endregion

        #region TypePersonnes

        [ActionName("GetTypePersonneById")]
        public async Task<BusinessResponse<TypePersonneDto>> GetTypePersonneById(int id)
        {
            var response = new BusinessResponse<TypePersonneDto>();

            try
            {
                response = await new TypePersonneManager().GetTypePersonneById(id);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }


        [ActionName("GetTypePersonnesByCriteria")]
        public async Task<BusinessResponse<TypePersonneDto>> PostTypePersonnesByCriteria([FromBody]BusinessRequest<TypePersonneDto> request)
        {
            var response = new BusinessResponse<TypePersonneDto>();

            try
            {
                response = await new TypePersonneManager().GetTypePersonnesByCriteria(request);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }


        /*
                [ActionName("SaveTypePersonnes")]
                public async Task<BusinessResponse<TypePersonneDto>> PostSaveTypePersonnes([FromBody]BusinessRequest<TypePersonneDto> request)
                {
                    var response = new BusinessResponse<TypePersonneDto>();
                    request.CanApplyTransaction = true;

                    try
                    {
                        response = await new TypePersonneManager().SaveTypePersonnes(request);
                        if (response.HasError) throw new Exception(response.Message);
                    }
                    catch (Exception ex)
                    {
                        response.HasError = true;
                        response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    }

                    return response;
                }
        */
        #endregion

        #region Civilites

        [ActionName("GetCiviliteById")]
        public async Task<BusinessResponse<CiviliteDto>> GetCiviliteById(int id)
        {
            var response = new BusinessResponse<CiviliteDto>();

            try
            {
                response = await new CiviliteManager().GetCiviliteById(id);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }


        [ActionName("GetCivilitesByCriteria")]
        public async Task<BusinessResponse<CiviliteDto>> PostCivilitesByCriteria([FromBody]BusinessRequest<CiviliteDto> request)
        {
            var response = new BusinessResponse<CiviliteDto>();

            try
            {
                response = await new CiviliteManager().GetCivilitesByCriteria(request);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }


        /*
                [ActionName("SaveCivilites")]
                public async Task<BusinessResponse<CiviliteDto>> PostSaveCivilites([FromBody]BusinessRequest<CiviliteDto> request)
                {
                    var response = new BusinessResponse<CiviliteDto>();
                    request.CanApplyTransaction = true;

                    try
                    {
                        response = await new CiviliteManager().SaveCivilites(request);
                        if (response.HasError) throw new Exception(response.Message);
                    }
                    catch (Exception ex)
                    {
                        response.HasError = true;
                        response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    }

                    return response;
                }
        */
        #endregion

        #region FormeJuridiques

        [ActionName("GetFormeJuridiqueById")]
        public async Task<BusinessResponse<FormeJuridiqueDto>> GetFormeJuridiqueById(int id)
        {
            var response = new BusinessResponse<FormeJuridiqueDto>();

            try
            {
                response = await new FormeJuridiqueManager().GetFormeJuridiqueById(id);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }


        [ActionName("GetFormeJuridiquesByCriteria")]
        public async Task<BusinessResponse<FormeJuridiqueDto>> PostFormeJuridiquesByCriteria([FromBody]BusinessRequest<FormeJuridiqueDto> request)
        {
            var response = new BusinessResponse<FormeJuridiqueDto>();

            try
            {
                response = await new FormeJuridiqueManager().GetFormeJuridiquesByCriteria(request);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }



        [ActionName("SaveFormeJuridiques")]
        public async Task<BusinessResponse<FormeJuridiqueDto>> PostSaveFormeJuridiques([FromBody]BusinessRequest<FormeJuridiqueDto> request)
        {
            var response = new BusinessResponse<FormeJuridiqueDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new FormeJuridiqueManager().SaveFormeJuridiques(request);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }

        #endregion

        #region TypePieceIdentites

        [ActionName("GetTypePieceIdentiteById")]
        public async Task<BusinessResponse<TypePieceIdentiteDto>> GetTypePieceIdentiteById(int id)
        {
            var response = new BusinessResponse<TypePieceIdentiteDto>();

            try
            {
                response = await new TypePieceIdentiteManager().GetTypePieceIdentiteById(id);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }


        [ActionName("GetTypePieceIdentitesByCriteria")]
        public async Task<BusinessResponse<TypePieceIdentiteDto>> PostTypePieceIdentitesByCriteria([FromBody]BusinessRequest<TypePieceIdentiteDto> request)
        {
            var response = new BusinessResponse<TypePieceIdentiteDto>();

            try
            {
                response = await new TypePieceIdentiteManager().GetTypePieceIdentitesByCriteria(request);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }



        [ActionName("SaveTypePieceIdentites")]
        public async Task<BusinessResponse<TypePieceIdentiteDto>> PostSaveTypePieceIdentites([FromBody]BusinessRequest<TypePieceIdentiteDto> request)
        {
            var response = new BusinessResponse<TypePieceIdentiteDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new TypePieceIdentiteManager().SaveTypePieceIdentites(request);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }

        #endregion

        #region DemandePlacementFacultatives

        [ActionName("GetDemandePlacementFacultativeById")]
        public async Task<BusinessResponse<DemandePlacementFacultativeDto>> GetDemandePlacementFacultativeById(int id)
        {
            var response = new BusinessResponse<DemandePlacementFacultativeDto>();

            try
            {
                response = await new DemandePlacementFacultativeManager().GetDemandePlacementFacultativeById(id);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }


        [ActionName("GetDemandePlacementFacultativesByCriteria")]
        public async Task<BusinessResponse<DemandePlacementFacultativeDto>> PostDemandePlacementFacultativesByCriteria([FromBody]BusinessRequest<DemandePlacementFacultativeDto> request)
        {
            var response = new BusinessResponse<DemandePlacementFacultativeDto>();

            try
            {
                response = await new DemandePlacementFacultativeManager().GetDemandePlacementFacultativesByCriteria(request);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }


        [ActionName("SaveDemandePlacementFacultatives")]
        public async Task<BusinessResponse<DemandePlacementFacultativeDto>> PostSaveDemandePlacementFacultatives([FromBody]BusinessRequest<DemandePlacementFacultativeDto> request)
        {
            var response = new BusinessResponse<DemandePlacementFacultativeDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new DemandePlacementFacultativeManager().SaveDemandePlacementFacultatives(request);
                if (response.HasError) throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }

            return response;
        }

        #endregion

        #region CategorieProcessus
        
                [ActionName("GetCategorieProcessuById")]
                public async Task<BusinessResponse<CategorieProcessuDto>> GetCategorieProcessuById(int id)
                {
                    var response = new BusinessResponse<CategorieProcessuDto>();

                    try
                    {
                        response = await new CategorieProcessuManager().GetCategorieProcessuById(id);
                        if (response.HasError) throw new Exception(response.Message);
                    }
                    catch (Exception ex)
                    {
                        response.HasError = true;
                        response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    }

                    return response;
                }
                
                [ActionName("GetCategorieProcessusByCriteria")]
                public async Task<BusinessResponse<CategorieProcessuDto>> PostCategorieProcessusByCriteria([FromBody]BusinessRequest<CategorieProcessuDto> request)
                {
                    var response = new BusinessResponse<CategorieProcessuDto>();

                    try
                    {
                        response = await new CategorieProcessuManager().GetCategorieProcessusByCriteria(request);
                        if (response.HasError) throw new Exception(response.Message);
                    }
                    catch (Exception ex)
                    {
                        response.HasError = true;
                        response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    }

                    return response;
                }
   
                [ActionName("SaveCategorieProcessus")]
                public async Task<BusinessResponse<CategorieProcessuDto>> PostSaveCategorieProcessus([FromBody]BusinessRequest<CategorieProcessuDto> request)
                {
                    var response = new BusinessResponse<CategorieProcessuDto>();
                    request.CanApplyTransaction = true;

                    try
                    {
                        response = await new CategorieProcessuManager().SaveCategorieProcessus(request);
                        if (response.HasError) throw new Exception(response.Message);
                    }
                    catch (Exception ex)
                    {
                        response.HasError = true;
                        response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    }

                    return response;
                }
        
        #endregion
    }
}