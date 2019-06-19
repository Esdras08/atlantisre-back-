using System;
using System.Web.Http;
using System.Threading.Tasks;
using Inexa.Atlantis.Re.Bll.Managers;
using Inexa.Atlantis.Re.Commons.Dtos;
using Inexa.Atlantis.Re.Commons.Infras.Domains;


namespace Inexa.Atlantis.Re.Services.Controllers
{
    public class ReGeneratedCtrlController : ApiController
    {
	
	

        #region Affaires
/*
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
		*/
/*
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
*/

/*
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
*/
        #endregion

        #region Assures
/*
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
		*/
/*
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
*/

/*
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
*/
        #endregion

        #region Branches
/*
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
		*/
/*
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
*/

/*
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
*/
        #endregion

        #region DeclarationSinistres
/*
        [ActionName("GetDeclarationSinistreById")]
        public async Task<BusinessResponse<DeclarationSinistreDto>> GetDeclarationSinistreById(int id)
        {
            var response = new BusinessResponse<DeclarationSinistreDto>();

            try
            {
                response = await new DeclarationSinistreManager().GetDeclarationSinistreById(id);
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
/*
        [ActionName("GetDeclarationSinistresByCriteria")]
        public async Task<BusinessResponse<DeclarationSinistreDto>> PostDeclarationSinistresByCriteria([FromBody]BusinessRequest<DeclarationSinistreDto> request)
        {
            var response = new BusinessResponse<DeclarationSinistreDto>();

            try
            {
                response = await new DeclarationSinistreManager().GetDeclarationSinistresByCriteria(request);
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

/*
        [ActionName("SaveDeclarationSinistres")]
        public async Task<BusinessResponse<DeclarationSinistreDto>> PostSaveDeclarationSinistres([FromBody]BusinessRequest<DeclarationSinistreDto> request)
        {
            var response = new BusinessResponse<DeclarationSinistreDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new DeclarationSinistreManager().SaveDeclarationSinistres(request);
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

        #region DemandePlacementFacultatives
/*
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
		*/
/*
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
*/

/*
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
*/
        #endregion

        #region Devises
/*
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
		*/
/*
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
*/

/*
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
*/
        #endregion

        #region DocumentEchanges
/*
        [ActionName("GetDocumentEchangeById")]
        public async Task<BusinessResponse<DocumentEchangeDto>> GetDocumentEchangeById(int id)
        {
            var response = new BusinessResponse<DocumentEchangeDto>();

            try
            {
                response = await new DocumentEchangeManager().GetDocumentEchangeById(id);
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
/*
        [ActionName("GetDocumentEchangesByCriteria")]
        public async Task<BusinessResponse<DocumentEchangeDto>> PostDocumentEchangesByCriteria([FromBody]BusinessRequest<DocumentEchangeDto> request)
        {
            var response = new BusinessResponse<DocumentEchangeDto>();

            try
            {
                response = await new DocumentEchangeManager().GetDocumentEchangesByCriteria(request);
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

/*
        [ActionName("SaveDocumentEchanges")]
        public async Task<BusinessResponse<DocumentEchangeDto>> PostSaveDocumentEchanges([FromBody]BusinessRequest<DocumentEchangeDto> request)
        {
            var response = new BusinessResponse<DocumentEchangeDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new DocumentEchangeManager().SaveDocumentEchanges(request);
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

        #region DomaineActivites
/*
        [ActionName("GetDomaineActiviteById")]
        public async Task<BusinessResponse<DomaineActiviteDto>> GetDomaineActiviteById(int id)
        {
            var response = new BusinessResponse<DomaineActiviteDto>();

            try
            {
                response = await new DomaineActiviteManager().GetDomaineActiviteById(id);
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
/*
        [ActionName("GetDomaineActivitesByCriteria")]
        public async Task<BusinessResponse<DomaineActiviteDto>> PostDomaineActivitesByCriteria([FromBody]BusinessRequest<DomaineActiviteDto> request)
        {
            var response = new BusinessResponse<DomaineActiviteDto>();

            try
            {
                response = await new DomaineActiviteManager().GetDomaineActivitesByCriteria(request);
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

/*
        [ActionName("SaveDomaineActivites")]
        public async Task<BusinessResponse<DomaineActiviteDto>> PostSaveDomaineActivites([FromBody]BusinessRequest<DomaineActiviteDto> request)
        {
            var response = new BusinessResponse<DomaineActiviteDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new DomaineActiviteManager().SaveDomaineActivites(request);
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

        #region Echanges
/*
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
		*/
/*
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
*/

/*
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
*/
        #endregion

        #region EtapeProcessus
/*
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
		*/
/*
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
*/

/*
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
*/
        #endregion

        #region Filiales
/*
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
		*/
/*
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
*/

/*
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
*/
        #endregion

        #region Fonctionnalites
/*
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
		*/
/*
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
*/

/*
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
*/
        #endregion

        #region FormeJuridiques
/*
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
		*/
/*
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
*/

/*
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
*/
        #endregion

        #region HistoriqueLigneSchemasPlacements
/*
        [ActionName("GetHistoriqueLigneSchemasPlacementById")]
        public async Task<BusinessResponse<HistoriqueLigneSchemasPlacementDto>> GetHistoriqueLigneSchemasPlacementById(int id)
        {
            var response = new BusinessResponse<HistoriqueLigneSchemasPlacementDto>();

            try
            {
                response = await new HistoriqueLigneSchemasPlacementManager().GetHistoriqueLigneSchemasPlacementById(id);
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
/*
        [ActionName("GetHistoriqueLigneSchemasPlacementsByCriteria")]
        public async Task<BusinessResponse<HistoriqueLigneSchemasPlacementDto>> PostHistoriqueLigneSchemasPlacementsByCriteria([FromBody]BusinessRequest<HistoriqueLigneSchemasPlacementDto> request)
        {
            var response = new BusinessResponse<HistoriqueLigneSchemasPlacementDto>();

            try
            {
                response = await new HistoriqueLigneSchemasPlacementManager().GetHistoriqueLigneSchemasPlacementsByCriteria(request);
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

/*
        [ActionName("SaveHistoriqueLigneSchemasPlacements")]
        public async Task<BusinessResponse<HistoriqueLigneSchemasPlacementDto>> PostSaveHistoriqueLigneSchemasPlacements([FromBody]BusinessRequest<HistoriqueLigneSchemasPlacementDto> request)
        {
            var response = new BusinessResponse<HistoriqueLigneSchemasPlacementDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new HistoriqueLigneSchemasPlacementManager().SaveHistoriqueLigneSchemasPlacements(request);
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

        #region HistoriqueLigneTableauRepartitionCharges
/*
        [ActionName("GetHistoriqueLigneTableauRepartitionChargeById")]
        public async Task<BusinessResponse<HistoriqueLigneTableauRepartitionChargeDto>> GetHistoriqueLigneTableauRepartitionChargeById(int id)
        {
            var response = new BusinessResponse<HistoriqueLigneTableauRepartitionChargeDto>();

            try
            {
                response = await new HistoriqueLigneTableauRepartitionChargeManager().GetHistoriqueLigneTableauRepartitionChargeById(id);
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
/*
        [ActionName("GetHistoriqueLigneTableauRepartitionChargesByCriteria")]
        public async Task<BusinessResponse<HistoriqueLigneTableauRepartitionChargeDto>> PostHistoriqueLigneTableauRepartitionChargesByCriteria([FromBody]BusinessRequest<HistoriqueLigneTableauRepartitionChargeDto> request)
        {
            var response = new BusinessResponse<HistoriqueLigneTableauRepartitionChargeDto>();

            try
            {
                response = await new HistoriqueLigneTableauRepartitionChargeManager().GetHistoriqueLigneTableauRepartitionChargesByCriteria(request);
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

/*
        [ActionName("SaveHistoriqueLigneTableauRepartitionCharges")]
        public async Task<BusinessResponse<HistoriqueLigneTableauRepartitionChargeDto>> PostSaveHistoriqueLigneTableauRepartitionCharges([FromBody]BusinessRequest<HistoriqueLigneTableauRepartitionChargeDto> request)
        {
            var response = new BusinessResponse<HistoriqueLigneTableauRepartitionChargeDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new HistoriqueLigneTableauRepartitionChargeManager().SaveHistoriqueLigneTableauRepartitionCharges(request);
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

        #region InterlocuteurFiliales
/*
        [ActionName("GetInterlocuteurFilialeById")]
        public async Task<BusinessResponse<InterlocuteurFilialeDto>> GetInterlocuteurFilialeById(int id)
        {
            var response = new BusinessResponse<InterlocuteurFilialeDto>();

            try
            {
                response = await new InterlocuteurFilialeManager().GetInterlocuteurFilialeById(id);
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
/*
        [ActionName("GetInterlocuteurFilialesByCriteria")]
        public async Task<BusinessResponse<InterlocuteurFilialeDto>> PostInterlocuteurFilialesByCriteria([FromBody]BusinessRequest<InterlocuteurFilialeDto> request)
        {
            var response = new BusinessResponse<InterlocuteurFilialeDto>();

            try
            {
                response = await new InterlocuteurFilialeManager().GetInterlocuteurFilialesByCriteria(request);
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

/*
        [ActionName("SaveInterlocuteurFiliales")]
        public async Task<BusinessResponse<InterlocuteurFilialeDto>> PostSaveInterlocuteurFiliales([FromBody]BusinessRequest<InterlocuteurFilialeDto> request)
        {
            var response = new BusinessResponse<InterlocuteurFilialeDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new InterlocuteurFilialeManager().SaveInterlocuteurFiliales(request);
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

        #region InterlocuteurReassureurs
/*
        [ActionName("GetInterlocuteurReassureurById")]
        public async Task<BusinessResponse<InterlocuteurReassureurDto>> GetInterlocuteurReassureurById(int id)
        {
            var response = new BusinessResponse<InterlocuteurReassureurDto>();

            try
            {
                response = await new InterlocuteurReassureurManager().GetInterlocuteurReassureurById(id);
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
/*
        [ActionName("GetInterlocuteurReassureursByCriteria")]
        public async Task<BusinessResponse<InterlocuteurReassureurDto>> PostInterlocuteurReassureursByCriteria([FromBody]BusinessRequest<InterlocuteurReassureurDto> request)
        {
            var response = new BusinessResponse<InterlocuteurReassureurDto>();

            try
            {
                response = await new InterlocuteurReassureurManager().GetInterlocuteurReassureursByCriteria(request);
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

/*
        [ActionName("SaveInterlocuteurReassureurs")]
        public async Task<BusinessResponse<InterlocuteurReassureurDto>> PostSaveInterlocuteurReassureurs([FromBody]BusinessRequest<InterlocuteurReassureurDto> request)
        {
            var response = new BusinessResponse<InterlocuteurReassureurDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new InterlocuteurReassureurManager().SaveInterlocuteurReassureurs(request);
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

        #region LigneRepartitionCharges
/*
        [ActionName("GetLigneRepartitionChargeById")]
        public async Task<BusinessResponse<LigneRepartitionChargeDto>> GetLigneRepartitionChargeById(int id)
        {
            var response = new BusinessResponse<LigneRepartitionChargeDto>();

            try
            {
                response = await new LigneRepartitionChargeManager().GetLigneRepartitionChargeById(id);
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
/*
        [ActionName("GetLigneRepartitionChargesByCriteria")]
        public async Task<BusinessResponse<LigneRepartitionChargeDto>> PostLigneRepartitionChargesByCriteria([FromBody]BusinessRequest<LigneRepartitionChargeDto> request)
        {
            var response = new BusinessResponse<LigneRepartitionChargeDto>();

            try
            {
                response = await new LigneRepartitionChargeManager().GetLigneRepartitionChargesByCriteria(request);
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

/*
        [ActionName("SaveLigneRepartitionCharges")]
        public async Task<BusinessResponse<LigneRepartitionChargeDto>> PostSaveLigneRepartitionCharges([FromBody]BusinessRequest<LigneRepartitionChargeDto> request)
        {
            var response = new BusinessResponse<LigneRepartitionChargeDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new LigneRepartitionChargeManager().SaveLigneRepartitionCharges(request);
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

        #region LigneSchemasPlacements
/*
        [ActionName("GetLigneSchemasPlacementById")]
        public async Task<BusinessResponse<LigneSchemasPlacementDto>> GetLigneSchemasPlacementById(int id)
        {
            var response = new BusinessResponse<LigneSchemasPlacementDto>();

            try
            {
                response = await new LigneSchemasPlacementManager().GetLigneSchemasPlacementById(id);
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
/*
        [ActionName("GetLigneSchemasPlacementsByCriteria")]
        public async Task<BusinessResponse<LigneSchemasPlacementDto>> PostLigneSchemasPlacementsByCriteria([FromBody]BusinessRequest<LigneSchemasPlacementDto> request)
        {
            var response = new BusinessResponse<LigneSchemasPlacementDto>();

            try
            {
                response = await new LigneSchemasPlacementManager().GetLigneSchemasPlacementsByCriteria(request);
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

/*
        [ActionName("SaveLigneSchemasPlacements")]
        public async Task<BusinessResponse<LigneSchemasPlacementDto>> PostSaveLigneSchemasPlacements([FromBody]BusinessRequest<LigneSchemasPlacementDto> request)
        {
            var response = new BusinessResponse<LigneSchemasPlacementDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new LigneSchemasPlacementManager().SaveLigneSchemasPlacements(request);
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

        #region MailParametres
/*
        [ActionName("GetMailParametreById")]
        public async Task<BusinessResponse<MailParametreDto>> GetMailParametreById(int id)
        {
            var response = new BusinessResponse<MailParametreDto>();

            try
            {
                response = await new MailParametreManager().GetMailParametreById(id);
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
/*
        [ActionName("GetMailParametresByCriteria")]
        public async Task<BusinessResponse<MailParametreDto>> PostMailParametresByCriteria([FromBody]BusinessRequest<MailParametreDto> request)
        {
            var response = new BusinessResponse<MailParametreDto>();

            try
            {
                response = await new MailParametreManager().GetMailParametresByCriteria(request);
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

/*
        [ActionName("SaveMailParametres")]
        public async Task<BusinessResponse<MailParametreDto>> PostSaveMailParametres([FromBody]BusinessRequest<MailParametreDto> request)
        {
            var response = new BusinessResponse<MailParametreDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new MailParametreManager().SaveMailParametres(request);
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

        #region MouvementTresoreries
/*
        [ActionName("GetMouvementTresorerieById")]
        public async Task<BusinessResponse<MouvementTresorerieDto>> GetMouvementTresorerieById(int id)
        {
            var response = new BusinessResponse<MouvementTresorerieDto>();

            try
            {
                response = await new MouvementTresorerieManager().GetMouvementTresorerieById(id);
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
/*
        [ActionName("GetMouvementTresoreriesByCriteria")]
        public async Task<BusinessResponse<MouvementTresorerieDto>> PostMouvementTresoreriesByCriteria([FromBody]BusinessRequest<MouvementTresorerieDto> request)
        {
            var response = new BusinessResponse<MouvementTresorerieDto>();

            try
            {
                response = await new MouvementTresorerieManager().GetMouvementTresoreriesByCriteria(request);
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

/*
        [ActionName("SaveMouvementTresoreries")]
        public async Task<BusinessResponse<MouvementTresorerieDto>> PostSaveMouvementTresoreries([FromBody]BusinessRequest<MouvementTresorerieDto> request)
        {
            var response = new BusinessResponse<MouvementTresorerieDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new MouvementTresorerieManager().SaveMouvementTresoreries(request);
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

        #region Pays
/*
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
		*/
/*
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
*/

/*
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
*/
        #endregion

        #region Personnes
/*
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
		*/
/*
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
*/

/*
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
*/
        #endregion

        #region Processus
/*
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
		*/
/*
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
*/

/*
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
*/
        #endregion

        #region Professions
/*
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
		*/
/*
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
*/

/*
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
*/
        #endregion

        #region Profils
/*
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
		*/
/*
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
*/

/*
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
*/
        #endregion

        #region ProfilFonctionnalites
/*
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
		*/
/*
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
*/

/*
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
*/
        #endregion

        #region ProfilFonctionnaliteHistos
/*
        [ActionName("GetProfilFonctionnaliteHistoById")]
        public async Task<BusinessResponse<ProfilFonctionnaliteHistoDto>> GetProfilFonctionnaliteHistoById(int id)
        {
            var response = new BusinessResponse<ProfilFonctionnaliteHistoDto>();

            try
            {
                response = await new ProfilFonctionnaliteHistoManager().GetProfilFonctionnaliteHistoById(id);
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
/*
        [ActionName("GetProfilFonctionnaliteHistosByCriteria")]
        public async Task<BusinessResponse<ProfilFonctionnaliteHistoDto>> PostProfilFonctionnaliteHistosByCriteria([FromBody]BusinessRequest<ProfilFonctionnaliteHistoDto> request)
        {
            var response = new BusinessResponse<ProfilFonctionnaliteHistoDto>();

            try
            {
                response = await new ProfilFonctionnaliteHistoManager().GetProfilFonctionnaliteHistosByCriteria(request);
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

/*
        [ActionName("SaveProfilFonctionnaliteHistos")]
        public async Task<BusinessResponse<ProfilFonctionnaliteHistoDto>> PostSaveProfilFonctionnaliteHistos([FromBody]BusinessRequest<ProfilFonctionnaliteHistoDto> request)
        {
            var response = new BusinessResponse<ProfilFonctionnaliteHistoDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new ProfilFonctionnaliteHistoManager().SaveProfilFonctionnaliteHistos(request);
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

        #region Reassureurs
/*
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
		*/
/*
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
*/

/*
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
*/
        #endregion

        #region SchemasPlacements
/*
        [ActionName("GetSchemasPlacementById")]
        public async Task<BusinessResponse<SchemasPlacementDto>> GetSchemasPlacementById(int id)
        {
            var response = new BusinessResponse<SchemasPlacementDto>();

            try
            {
                response = await new SchemasPlacementManager().GetSchemasPlacementById(id);
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
/*
        [ActionName("GetSchemasPlacementsByCriteria")]
        public async Task<BusinessResponse<SchemasPlacementDto>> PostSchemasPlacementsByCriteria([FromBody]BusinessRequest<SchemasPlacementDto> request)
        {
            var response = new BusinessResponse<SchemasPlacementDto>();

            try
            {
                response = await new SchemasPlacementManager().GetSchemasPlacementsByCriteria(request);
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

/*
        [ActionName("SaveSchemasPlacements")]
        public async Task<BusinessResponse<SchemasPlacementDto>> PostSaveSchemasPlacements([FromBody]BusinessRequest<SchemasPlacementDto> request)
        {
            var response = new BusinessResponse<SchemasPlacementDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new SchemasPlacementManager().SaveSchemasPlacements(request);
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

        #region SecteurActivites
/*
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
		*/
/*
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
*/

/*
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
*/
        #endregion

        #region SituationMatrimoniales
/*
        [ActionName("GetSituationMatrimonialeById")]
        public async Task<BusinessResponse<SituationMatrimonialeDto>> GetSituationMatrimonialeById(int id)
        {
            var response = new BusinessResponse<SituationMatrimonialeDto>();

            try
            {
                response = await new SituationMatrimonialeManager().GetSituationMatrimonialeById(id);
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
/*
        [ActionName("GetSituationMatrimonialesByCriteria")]
        public async Task<BusinessResponse<SituationMatrimonialeDto>> PostSituationMatrimonialesByCriteria([FromBody]BusinessRequest<SituationMatrimonialeDto> request)
        {
            var response = new BusinessResponse<SituationMatrimonialeDto>();

            try
            {
                response = await new SituationMatrimonialeManager().GetSituationMatrimonialesByCriteria(request);
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

/*
        [ActionName("SaveSituationMatrimoniales")]
        public async Task<BusinessResponse<SituationMatrimonialeDto>> PostSaveSituationMatrimoniales([FromBody]BusinessRequest<SituationMatrimonialeDto> request)
        {
            var response = new BusinessResponse<SituationMatrimonialeDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new SituationMatrimonialeManager().SaveSituationMatrimoniales(request);
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

        #region StatutAffaires
/*
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
		*/
/*
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
*/

/*
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
*/
        #endregion

        #region StatutMouvements
/*
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
		*/
/*
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
*/

/*
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
*/
        #endregion

        #region SuiviProcessus
/*
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
		*/
/*
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
*/

/*
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
*/
        #endregion

        #region SysComplexiteMotDePasses
/*
        [ActionName("GetSysComplexiteMotDePasseById")]
        public async Task<BusinessResponse<SysComplexiteMotDePasseDto>> GetSysComplexiteMotDePasseById(int id)
        {
            var response = new BusinessResponse<SysComplexiteMotDePasseDto>();

            try
            {
                response = await new SysComplexiteMotDePasseManager().GetSysComplexiteMotDePasseById(id);
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
/*
        [ActionName("GetSysComplexiteMotDePassesByCriteria")]
        public async Task<BusinessResponse<SysComplexiteMotDePasseDto>> PostSysComplexiteMotDePassesByCriteria([FromBody]BusinessRequest<SysComplexiteMotDePasseDto> request)
        {
            var response = new BusinessResponse<SysComplexiteMotDePasseDto>();

            try
            {
                response = await new SysComplexiteMotDePasseManager().GetSysComplexiteMotDePassesByCriteria(request);
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

/*
        [ActionName("SaveSysComplexiteMotDePasses")]
        public async Task<BusinessResponse<SysComplexiteMotDePasseDto>> PostSaveSysComplexiteMotDePasses([FromBody]BusinessRequest<SysComplexiteMotDePasseDto> request)
        {
            var response = new BusinessResponse<SysComplexiteMotDePasseDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new SysComplexiteMotDePasseManager().SaveSysComplexiteMotDePasses(request);
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

        #region SysLogs
/*
        [ActionName("GetSysLogById")]
        public async Task<BusinessResponse<SysLogDto>> GetSysLogById(int id)
        {
            var response = new BusinessResponse<SysLogDto>();

            try
            {
                response = await new SysLogManager().GetSysLogById(id);
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
/*
        [ActionName("GetSysLogsByCriteria")]
        public async Task<BusinessResponse<SysLogDto>> PostSysLogsByCriteria([FromBody]BusinessRequest<SysLogDto> request)
        {
            var response = new BusinessResponse<SysLogDto>();

            try
            {
                response = await new SysLogManager().GetSysLogsByCriteria(request);
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

/*
        [ActionName("SaveSysLogs")]
        public async Task<BusinessResponse<SysLogDto>> PostSaveSysLogs([FromBody]BusinessRequest<SysLogDto> request)
        {
            var response = new BusinessResponse<SysLogDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new SysLogManager().SaveSysLogs(request);
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

        #region SysMailBoxs
/*
        [ActionName("GetSysMailBoxById")]
        public async Task<BusinessResponse<SysMailBoxDto>> GetSysMailBoxById(int id)
        {
            var response = new BusinessResponse<SysMailBoxDto>();

            try
            {
                response = await new SysMailBoxManager().GetSysMailBoxById(id);
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
/*
        [ActionName("GetSysMailBoxsByCriteria")]
        public async Task<BusinessResponse<SysMailBoxDto>> PostSysMailBoxsByCriteria([FromBody]BusinessRequest<SysMailBoxDto> request)
        {
            var response = new BusinessResponse<SysMailBoxDto>();

            try
            {
                response = await new SysMailBoxManager().GetSysMailBoxsByCriteria(request);
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

/*
        [ActionName("SaveSysMailBoxs")]
        public async Task<BusinessResponse<SysMailBoxDto>> PostSaveSysMailBoxs([FromBody]BusinessRequest<SysMailBoxDto> request)
        {
            var response = new BusinessResponse<SysMailBoxDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new SysMailBoxManager().SaveSysMailBoxs(request);
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

        #region SysNotifications
/*
        [ActionName("GetSysNotificationById")]
        public async Task<BusinessResponse<SysNotificationDto>> GetSysNotificationById(int id)
        {
            var response = new BusinessResponse<SysNotificationDto>();

            try
            {
                response = await new SysNotificationManager().GetSysNotificationById(id);
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
/*
        [ActionName("GetSysNotificationsByCriteria")]
        public async Task<BusinessResponse<SysNotificationDto>> PostSysNotificationsByCriteria([FromBody]BusinessRequest<SysNotificationDto> request)
        {
            var response = new BusinessResponse<SysNotificationDto>();

            try
            {
                response = await new SysNotificationManager().GetSysNotificationsByCriteria(request);
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

/*
        [ActionName("SaveSysNotifications")]
        public async Task<BusinessResponse<SysNotificationDto>> PostSaveSysNotifications([FromBody]BusinessRequest<SysNotificationDto> request)
        {
            var response = new BusinessResponse<SysNotificationDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new SysNotificationManager().SaveSysNotifications(request);
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

        #region SysObjets
/*
        [ActionName("GetSysObjetById")]
        public async Task<BusinessResponse<SysObjetDto>> GetSysObjetById(int id)
        {
            var response = new BusinessResponse<SysObjetDto>();

            try
            {
                response = await new SysObjetManager().GetSysObjetById(id);
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
/*
        [ActionName("GetSysObjetsByCriteria")]
        public async Task<BusinessResponse<SysObjetDto>> PostSysObjetsByCriteria([FromBody]BusinessRequest<SysObjetDto> request)
        {
            var response = new BusinessResponse<SysObjetDto>();

            try
            {
                response = await new SysObjetManager().GetSysObjetsByCriteria(request);
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

/*
        [ActionName("SaveSysObjets")]
        public async Task<BusinessResponse<SysObjetDto>> PostSaveSysObjets([FromBody]BusinessRequest<SysObjetDto> request)
        {
            var response = new BusinessResponse<SysObjetDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new SysObjetManager().SaveSysObjets(request);
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

        #region SysStrategieMotDePasses
/*
        [ActionName("GetSysStrategieMotDePasseById")]
        public async Task<BusinessResponse<SysStrategieMotDePasseDto>> GetSysStrategieMotDePasseById(int id)
        {
            var response = new BusinessResponse<SysStrategieMotDePasseDto>();

            try
            {
                response = await new SysStrategieMotDePasseManager().GetSysStrategieMotDePasseById(id);
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
/*
        [ActionName("GetSysStrategieMotDePassesByCriteria")]
        public async Task<BusinessResponse<SysStrategieMotDePasseDto>> PostSysStrategieMotDePassesByCriteria([FromBody]BusinessRequest<SysStrategieMotDePasseDto> request)
        {
            var response = new BusinessResponse<SysStrategieMotDePasseDto>();

            try
            {
                response = await new SysStrategieMotDePasseManager().GetSysStrategieMotDePassesByCriteria(request);
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

/*
        [ActionName("SaveSysStrategieMotDePasses")]
        public async Task<BusinessResponse<SysStrategieMotDePasseDto>> PostSaveSysStrategieMotDePasses([FromBody]BusinessRequest<SysStrategieMotDePasseDto> request)
        {
            var response = new BusinessResponse<SysStrategieMotDePasseDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new SysStrategieMotDePasseManager().SaveSysStrategieMotDePasses(request);
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

        #region SysStrategieMotDePasseHistos
/*
        [ActionName("GetSysStrategieMotDePasseHistoById")]
        public async Task<BusinessResponse<SysStrategieMotDePasseHistoDto>> GetSysStrategieMotDePasseHistoById(int id)
        {
            var response = new BusinessResponse<SysStrategieMotDePasseHistoDto>();

            try
            {
                response = await new SysStrategieMotDePasseHistoManager().GetSysStrategieMotDePasseHistoById(id);
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
/*
        [ActionName("GetSysStrategieMotDePasseHistosByCriteria")]
        public async Task<BusinessResponse<SysStrategieMotDePasseHistoDto>> PostSysStrategieMotDePasseHistosByCriteria([FromBody]BusinessRequest<SysStrategieMotDePasseHistoDto> request)
        {
            var response = new BusinessResponse<SysStrategieMotDePasseHistoDto>();

            try
            {
                response = await new SysStrategieMotDePasseHistoManager().GetSysStrategieMotDePasseHistosByCriteria(request);
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

/*
        [ActionName("SaveSysStrategieMotDePasseHistos")]
        public async Task<BusinessResponse<SysStrategieMotDePasseHistoDto>> PostSaveSysStrategieMotDePasseHistos([FromBody]BusinessRequest<SysStrategieMotDePasseHistoDto> request)
        {
            var response = new BusinessResponse<SysStrategieMotDePasseHistoDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new SysStrategieMotDePasseHistoManager().SaveSysStrategieMotDePasseHistos(request);
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

        #region TableauRepartitionCharges
/*
        [ActionName("GetTableauRepartitionChargeById")]
        public async Task<BusinessResponse<TableauRepartitionChargeDto>> GetTableauRepartitionChargeById(int id)
        {
            var response = new BusinessResponse<TableauRepartitionChargeDto>();

            try
            {
                response = await new TableauRepartitionChargeManager().GetTableauRepartitionChargeById(id);
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
/*
        [ActionName("GetTableauRepartitionChargesByCriteria")]
        public async Task<BusinessResponse<TableauRepartitionChargeDto>> PostTableauRepartitionChargesByCriteria([FromBody]BusinessRequest<TableauRepartitionChargeDto> request)
        {
            var response = new BusinessResponse<TableauRepartitionChargeDto>();

            try
            {
                response = await new TableauRepartitionChargeManager().GetTableauRepartitionChargesByCriteria(request);
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

/*
        [ActionName("SaveTableauRepartitionCharges")]
        public async Task<BusinessResponse<TableauRepartitionChargeDto>> PostSaveTableauRepartitionCharges([FromBody]BusinessRequest<TableauRepartitionChargeDto> request)
        {
            var response = new BusinessResponse<TableauRepartitionChargeDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new TableauRepartitionChargeManager().SaveTableauRepartitionCharges(request);
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

        #region Termes
/*
        [ActionName("GetTermeById")]
        public async Task<BusinessResponse<TermeDto>> GetTermeById(int id)
        {
            var response = new BusinessResponse<TermeDto>();

            try
            {
                response = await new TermeManager().GetTermeById(id);
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
/*
        [ActionName("GetTermesByCriteria")]
        public async Task<BusinessResponse<TermeDto>> PostTermesByCriteria([FromBody]BusinessRequest<TermeDto> request)
        {
            var response = new BusinessResponse<TermeDto>();

            try
            {
                response = await new TermeManager().GetTermesByCriteria(request);
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

/*
        [ActionName("SaveTermes")]
        public async Task<BusinessResponse<TermeDto>> PostSaveTermes([FromBody]BusinessRequest<TermeDto> request)
        {
            var response = new BusinessResponse<TermeDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new TermeManager().SaveTermes(request);
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

        #region TermeTraites
/*
        [ActionName("GetTermeTraiteById")]
        public async Task<BusinessResponse<TermeTraiteDto>> GetTermeTraiteById(int id)
        {
            var response = new BusinessResponse<TermeTraiteDto>();

            try
            {
                response = await new TermeTraiteManager().GetTermeTraiteById(id);
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
/*
        [ActionName("GetTermeTraitesByCriteria")]
        public async Task<BusinessResponse<TermeTraiteDto>> PostTermeTraitesByCriteria([FromBody]BusinessRequest<TermeTraiteDto> request)
        {
            var response = new BusinessResponse<TermeTraiteDto>();

            try
            {
                response = await new TermeTraiteManager().GetTermeTraitesByCriteria(request);
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

/*
        [ActionName("SaveTermeTraites")]
        public async Task<BusinessResponse<TermeTraiteDto>> PostSaveTermeTraites([FromBody]BusinessRequest<TermeTraiteDto> request)
        {
            var response = new BusinessResponse<TermeTraiteDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new TermeTraiteManager().SaveTermeTraites(request);
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

        #region Traces
/*
        [ActionName("GetTraceById")]
        public async Task<BusinessResponse<TraceDto>> GetTraceById(int id)
        {
            var response = new BusinessResponse<TraceDto>();

            try
            {
                response = await new TraceManager().GetTraceById(id);
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
/*
        [ActionName("GetTracesByCriteria")]
        public async Task<BusinessResponse<TraceDto>> PostTracesByCriteria([FromBody]BusinessRequest<TraceDto> request)
        {
            var response = new BusinessResponse<TraceDto>();

            try
            {
                response = await new TraceManager().GetTracesByCriteria(request);
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

/*
        [ActionName("SaveTraces")]
        public async Task<BusinessResponse<TraceDto>> PostSaveTraces([FromBody]BusinessRequest<TraceDto> request)
        {
            var response = new BusinessResponse<TraceDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new TraceManager().SaveTraces(request);
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

        #region Traites
/*
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
		*/
/*
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
*/

/*
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
*/
        #endregion

        #region TypeEchanges
/*
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
		*/
/*
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
*/

/*
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
*/
        #endregion

        #region TypePersonnes
/*
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
		*/
/*
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
*/

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

        #region TypeProcessus
/*
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
		*/
/*
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
*/

/*
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
*/
        #endregion

        #region Utilisateurs
/*
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
		*/
/*
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
*/

/*
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
*/
        #endregion

        #region UtilisateurFonctionnalites
/*
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
		*/
/*
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
*/

/*
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
*/
        #endregion

        #region UtilisateurFonctionnaliteHistos
/*
        [ActionName("GetUtilisateurFonctionnaliteHistoById")]
        public async Task<BusinessResponse<UtilisateurFonctionnaliteHistoDto>> GetUtilisateurFonctionnaliteHistoById(int id)
        {
            var response = new BusinessResponse<UtilisateurFonctionnaliteHistoDto>();

            try
            {
                response = await new UtilisateurFonctionnaliteHistoManager().GetUtilisateurFonctionnaliteHistoById(id);
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
/*
        [ActionName("GetUtilisateurFonctionnaliteHistosByCriteria")]
        public async Task<BusinessResponse<UtilisateurFonctionnaliteHistoDto>> PostUtilisateurFonctionnaliteHistosByCriteria([FromBody]BusinessRequest<UtilisateurFonctionnaliteHistoDto> request)
        {
            var response = new BusinessResponse<UtilisateurFonctionnaliteHistoDto>();

            try
            {
                response = await new UtilisateurFonctionnaliteHistoManager().GetUtilisateurFonctionnaliteHistosByCriteria(request);
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

/*
        [ActionName("SaveUtilisateurFonctionnaliteHistos")]
        public async Task<BusinessResponse<UtilisateurFonctionnaliteHistoDto>> PostSaveUtilisateurFonctionnaliteHistos([FromBody]BusinessRequest<UtilisateurFonctionnaliteHistoDto> request)
        {
            var response = new BusinessResponse<UtilisateurFonctionnaliteHistoDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new UtilisateurFonctionnaliteHistoManager().SaveUtilisateurFonctionnaliteHistos(request);
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

        #region UtilisateurFonctionnalitePrives
/*
        [ActionName("GetUtilisateurFonctionnalitePriveById")]
        public async Task<BusinessResponse<UtilisateurFonctionnalitePriveDto>> GetUtilisateurFonctionnalitePriveById(int id)
        {
            var response = new BusinessResponse<UtilisateurFonctionnalitePriveDto>();

            try
            {
                response = await new UtilisateurFonctionnalitePriveManager().GetUtilisateurFonctionnalitePriveById(id);
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
/*
        [ActionName("GetUtilisateurFonctionnalitePrivesByCriteria")]
        public async Task<BusinessResponse<UtilisateurFonctionnalitePriveDto>> PostUtilisateurFonctionnalitePrivesByCriteria([FromBody]BusinessRequest<UtilisateurFonctionnalitePriveDto> request)
        {
            var response = new BusinessResponse<UtilisateurFonctionnalitePriveDto>();

            try
            {
                response = await new UtilisateurFonctionnalitePriveManager().GetUtilisateurFonctionnalitePrivesByCriteria(request);
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

/*
        [ActionName("SaveUtilisateurFonctionnalitePrives")]
        public async Task<BusinessResponse<UtilisateurFonctionnalitePriveDto>> PostSaveUtilisateurFonctionnalitePrives([FromBody]BusinessRequest<UtilisateurFonctionnalitePriveDto> request)
        {
            var response = new BusinessResponse<UtilisateurFonctionnalitePriveDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new UtilisateurFonctionnalitePriveManager().SaveUtilisateurFonctionnalitePrives(request);
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

        #region UtilisateurFonctionnalitePriveHistos
/*
        [ActionName("GetUtilisateurFonctionnalitePriveHistoById")]
        public async Task<BusinessResponse<UtilisateurFonctionnalitePriveHistoDto>> GetUtilisateurFonctionnalitePriveHistoById(int id)
        {
            var response = new BusinessResponse<UtilisateurFonctionnalitePriveHistoDto>();

            try
            {
                response = await new UtilisateurFonctionnalitePriveHistoManager().GetUtilisateurFonctionnalitePriveHistoById(id);
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
/*
        [ActionName("GetUtilisateurFonctionnalitePriveHistosByCriteria")]
        public async Task<BusinessResponse<UtilisateurFonctionnalitePriveHistoDto>> PostUtilisateurFonctionnalitePriveHistosByCriteria([FromBody]BusinessRequest<UtilisateurFonctionnalitePriveHistoDto> request)
        {
            var response = new BusinessResponse<UtilisateurFonctionnalitePriveHistoDto>();

            try
            {
                response = await new UtilisateurFonctionnalitePriveHistoManager().GetUtilisateurFonctionnalitePriveHistosByCriteria(request);
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

/*
        [ActionName("SaveUtilisateurFonctionnalitePriveHistos")]
        public async Task<BusinessResponse<UtilisateurFonctionnalitePriveHistoDto>> PostSaveUtilisateurFonctionnalitePriveHistos([FromBody]BusinessRequest<UtilisateurFonctionnalitePriveHistoDto> request)
        {
            var response = new BusinessResponse<UtilisateurFonctionnalitePriveHistoDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new UtilisateurFonctionnalitePriveHistoManager().SaveUtilisateurFonctionnalitePriveHistos(request);
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

        #region UtilisateurHistos
/*
        [ActionName("GetUtilisateurHistoById")]
        public async Task<BusinessResponse<UtilisateurHistoDto>> GetUtilisateurHistoById(int id)
        {
            var response = new BusinessResponse<UtilisateurHistoDto>();

            try
            {
                response = await new UtilisateurHistoManager().GetUtilisateurHistoById(id);
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
/*
        [ActionName("GetUtilisateurHistosByCriteria")]
        public async Task<BusinessResponse<UtilisateurHistoDto>> PostUtilisateurHistosByCriteria([FromBody]BusinessRequest<UtilisateurHistoDto> request)
        {
            var response = new BusinessResponse<UtilisateurHistoDto>();

            try
            {
                response = await new UtilisateurHistoManager().GetUtilisateurHistosByCriteria(request);
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

/*
        [ActionName("SaveUtilisateurHistos")]
        public async Task<BusinessResponse<UtilisateurHistoDto>> PostSaveUtilisateurHistos([FromBody]BusinessRequest<UtilisateurHistoDto> request)
        {
            var response = new BusinessResponse<UtilisateurHistoDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new UtilisateurHistoManager().SaveUtilisateurHistos(request);
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

        #region UtilisateurProfils
/*
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
		*/
/*
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
*/

/*
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
*/
        #endregion

        #region UtilisateurProfilHistos
/*
        [ActionName("GetUtilisateurProfilHistoById")]
        public async Task<BusinessResponse<UtilisateurProfilHistoDto>> GetUtilisateurProfilHistoById(int id)
        {
            var response = new BusinessResponse<UtilisateurProfilHistoDto>();

            try
            {
                response = await new UtilisateurProfilHistoManager().GetUtilisateurProfilHistoById(id);
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
/*
        [ActionName("GetUtilisateurProfilHistosByCriteria")]
        public async Task<BusinessResponse<UtilisateurProfilHistoDto>> PostUtilisateurProfilHistosByCriteria([FromBody]BusinessRequest<UtilisateurProfilHistoDto> request)
        {
            var response = new BusinessResponse<UtilisateurProfilHistoDto>();

            try
            {
                response = await new UtilisateurProfilHistoManager().GetUtilisateurProfilHistosByCriteria(request);
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

/*
        [ActionName("SaveUtilisateurProfilHistos")]
        public async Task<BusinessResponse<UtilisateurProfilHistoDto>> PostSaveUtilisateurProfilHistos([FromBody]BusinessRequest<UtilisateurProfilHistoDto> request)
        {
            var response = new BusinessResponse<UtilisateurProfilHistoDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new UtilisateurProfilHistoManager().SaveUtilisateurProfilHistos(request);
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

        #region UtilisateurRestrictions
/*
        [ActionName("GetUtilisateurRestrictionById")]
        public async Task<BusinessResponse<UtilisateurRestrictionDto>> GetUtilisateurRestrictionById(int id)
        {
            var response = new BusinessResponse<UtilisateurRestrictionDto>();

            try
            {
                response = await new UtilisateurRestrictionManager().GetUtilisateurRestrictionById(id);
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
/*
        [ActionName("GetUtilisateurRestrictionsByCriteria")]
        public async Task<BusinessResponse<UtilisateurRestrictionDto>> PostUtilisateurRestrictionsByCriteria([FromBody]BusinessRequest<UtilisateurRestrictionDto> request)
        {
            var response = new BusinessResponse<UtilisateurRestrictionDto>();

            try
            {
                response = await new UtilisateurRestrictionManager().GetUtilisateurRestrictionsByCriteria(request);
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

/*
        [ActionName("SaveUtilisateurRestrictions")]
        public async Task<BusinessResponse<UtilisateurRestrictionDto>> PostSaveUtilisateurRestrictions([FromBody]BusinessRequest<UtilisateurRestrictionDto> request)
        {
            var response = new BusinessResponse<UtilisateurRestrictionDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new UtilisateurRestrictionManager().SaveUtilisateurRestrictions(request);
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

        #region UtilisateurRestrictionHistos
/*
        [ActionName("GetUtilisateurRestrictionHistoById")]
        public async Task<BusinessResponse<UtilisateurRestrictionHistoDto>> GetUtilisateurRestrictionHistoById(int id)
        {
            var response = new BusinessResponse<UtilisateurRestrictionHistoDto>();

            try
            {
                response = await new UtilisateurRestrictionHistoManager().GetUtilisateurRestrictionHistoById(id);
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
/*
        [ActionName("GetUtilisateurRestrictionHistosByCriteria")]
        public async Task<BusinessResponse<UtilisateurRestrictionHistoDto>> PostUtilisateurRestrictionHistosByCriteria([FromBody]BusinessRequest<UtilisateurRestrictionHistoDto> request)
        {
            var response = new BusinessResponse<UtilisateurRestrictionHistoDto>();

            try
            {
                response = await new UtilisateurRestrictionHistoManager().GetUtilisateurRestrictionHistosByCriteria(request);
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

/*
        [ActionName("SaveUtilisateurRestrictionHistos")]
        public async Task<BusinessResponse<UtilisateurRestrictionHistoDto>> PostSaveUtilisateurRestrictionHistos([FromBody]BusinessRequest<UtilisateurRestrictionHistoDto> request)
        {
            var response = new BusinessResponse<UtilisateurRestrictionHistoDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new UtilisateurRestrictionHistoManager().SaveUtilisateurRestrictionHistos(request);
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

        #region UtilisateurSessions
/*
        [ActionName("GetUtilisateurSessionById")]
        public async Task<BusinessResponse<UtilisateurSessionDto>> GetUtilisateurSessionById(int id)
        {
            var response = new BusinessResponse<UtilisateurSessionDto>();

            try
            {
                response = await new UtilisateurSessionManager().GetUtilisateurSessionById(id);
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
/*
        [ActionName("GetUtilisateurSessionsByCriteria")]
        public async Task<BusinessResponse<UtilisateurSessionDto>> PostUtilisateurSessionsByCriteria([FromBody]BusinessRequest<UtilisateurSessionDto> request)
        {
            var response = new BusinessResponse<UtilisateurSessionDto>();

            try
            {
                response = await new UtilisateurSessionManager().GetUtilisateurSessionsByCriteria(request);
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

/*
        [ActionName("SaveUtilisateurSessions")]
        public async Task<BusinessResponse<UtilisateurSessionDto>> PostSaveUtilisateurSessions([FromBody]BusinessRequest<UtilisateurSessionDto> request)
        {
            var response = new BusinessResponse<UtilisateurSessionDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new UtilisateurSessionManager().SaveUtilisateurSessions(request);
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

        #region ViewSysStrategieMotDePasses
/*
        [ActionName("GetViewSysStrategieMotDePassesByCriteria")]
        public async Task<BusinessResponse<ViewSysStrategieMotDePasseDto>> PostViewSysStrategieMotDePassesByCriteria([FromBody]BusinessRequest<ViewSysStrategieMotDePasseDto> request)
        {
            var response = new BusinessResponse<ViewSysStrategieMotDePasseDto>();

            try
            {
                response = await new ViewSysStrategieMotDePasseManager().GetViewSysStrategieMotDePassesByCriteria(request);
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

/*
        [ActionName("SaveViewSysStrategieMotDePasses")]
        public async Task<BusinessResponse<ViewSysStrategieMotDePasseDto>> PostSaveViewSysStrategieMotDePasses([FromBody]BusinessRequest<ViewSysStrategieMotDePasseDto> request)
        {
            var response = new BusinessResponse<ViewSysStrategieMotDePasseDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new ViewSysStrategieMotDePasseManager().SaveViewSysStrategieMotDePasses(request);
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

        #region ViewUtilisateurs
/*
        [ActionName("GetViewUtilisateursByCriteria")]
        public async Task<BusinessResponse<ViewUtilisateurDto>> PostViewUtilisateursByCriteria([FromBody]BusinessRequest<ViewUtilisateurDto> request)
        {
            var response = new BusinessResponse<ViewUtilisateurDto>();

            try
            {
                response = await new ViewUtilisateurManager().GetViewUtilisateursByCriteria(request);
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

/*
        [ActionName("SaveViewUtilisateurs")]
        public async Task<BusinessResponse<ViewUtilisateurDto>> PostSaveViewUtilisateurs([FromBody]BusinessRequest<ViewUtilisateurDto> request)
        {
            var response = new BusinessResponse<ViewUtilisateurDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new ViewUtilisateurManager().SaveViewUtilisateurs(request);
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

        #region ViewUtilisateurProfils
/*
        [ActionName("GetViewUtilisateurProfilsByCriteria")]
        public async Task<BusinessResponse<ViewUtilisateurProfilDto>> PostViewUtilisateurProfilsByCriteria([FromBody]BusinessRequest<ViewUtilisateurProfilDto> request)
        {
            var response = new BusinessResponse<ViewUtilisateurProfilDto>();

            try
            {
                response = await new ViewUtilisateurProfilManager().GetViewUtilisateurProfilsByCriteria(request);
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

/*
        [ActionName("SaveViewUtilisateurProfils")]
        public async Task<BusinessResponse<ViewUtilisateurProfilDto>> PostSaveViewUtilisateurProfils([FromBody]BusinessRequest<ViewUtilisateurProfilDto> request)
        {
            var response = new BusinessResponse<ViewUtilisateurProfilDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new ViewUtilisateurProfilManager().SaveViewUtilisateurProfils(request);
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

        #region ViewUtilisateurRestrictions
/*
        [ActionName("GetViewUtilisateurRestrictionsByCriteria")]
        public async Task<BusinessResponse<ViewUtilisateurRestrictionDto>> PostViewUtilisateurRestrictionsByCriteria([FromBody]BusinessRequest<ViewUtilisateurRestrictionDto> request)
        {
            var response = new BusinessResponse<ViewUtilisateurRestrictionDto>();

            try
            {
                response = await new ViewUtilisateurRestrictionManager().GetViewUtilisateurRestrictionsByCriteria(request);
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

/*
        [ActionName("SaveViewUtilisateurRestrictions")]
        public async Task<BusinessResponse<ViewUtilisateurRestrictionDto>> PostSaveViewUtilisateurRestrictions([FromBody]BusinessRequest<ViewUtilisateurRestrictionDto> request)
        {
            var response = new BusinessResponse<ViewUtilisateurRestrictionDto>();
            request.CanApplyTransaction = true;

            try
            {
                response = await new ViewUtilisateurRestrictionManager().SaveViewUtilisateurRestrictions(request);
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

        #region Structures
/*
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
		*/
/*
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
*/

/*
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
*/
        #endregion

        #region Civilites
/*
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
		*/
/*
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
*/

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

        #region TypePieceIdentites
/*
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
		*/
/*
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
*/

/*
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
*/
        #endregion

        #region CategorieProcessus
/*
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
		*/
/*
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
*/

/*
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
*/
        #endregion
    }
}
