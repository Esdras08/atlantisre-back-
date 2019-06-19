  /*


         // Affaire
         router.get('/api/ReGeneratedCtrl/GetAffaireById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetAffaireById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetAffairesByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetAffairesByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveAffaires', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveAffaires', 'POST', param, response);
         });

         // Assure
         router.get('/api/ReGeneratedCtrl/GetAssureById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetAssureById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetAssuresByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetAssuresByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveAssures', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveAssures', 'POST', param, response);
         });

         // Branche
         router.get('/api/ReGeneratedCtrl/GetBrancheById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetBrancheById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetBranchesByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetBranchesByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveBranches', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveBranches', 'POST', param, response);
         });

         // DeclarationSinistre
         router.get('/api/ReGeneratedCtrl/GetDeclarationSinistreById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetDeclarationSinistreById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetDeclarationSinistresByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetDeclarationSinistresByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveDeclarationSinistres', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveDeclarationSinistres', 'POST', param, response);
         });

         // DemandePlacementFacultative
         router.get('/api/ReGeneratedCtrl/GetDemandePlacementFacultativeById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetDemandePlacementFacultativeById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetDemandePlacementFacultativesByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetDemandePlacementFacultativesByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveDemandePlacementFacultatives', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveDemandePlacementFacultatives', 'POST', param, response);
         });

         // Devise
         router.get('/api/ReGeneratedCtrl/GetDeviseById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetDeviseById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetDevisesByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetDevisesByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveDevises', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveDevises', 'POST', param, response);
         });

         // DocumentEchange
         router.get('/api/ReGeneratedCtrl/GetDocumentEchangeById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetDocumentEchangeById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetDocumentEchangesByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetDocumentEchangesByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveDocumentEchanges', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveDocumentEchanges', 'POST', param, response);
         });

         // DomaineActivite
         router.get('/api/ReGeneratedCtrl/GetDomaineActiviteById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetDomaineActiviteById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetDomaineActivitesByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetDomaineActivitesByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveDomaineActivites', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveDomaineActivites', 'POST', param, response);
         });

         // Echange
         router.get('/api/ReGeneratedCtrl/GetEchangeById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetEchangeById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetEchangesByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetEchangesByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveEchanges', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveEchanges', 'POST', param, response);
         });

         // EtapeProcessu
         router.get('/api/ReGeneratedCtrl/GetEtapeProcessuById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetEtapeProcessuById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetEtapeProcessusByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetEtapeProcessusByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveEtapeProcessus', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveEtapeProcessus', 'POST', param, response);
         });

         // Filiale
         router.get('/api/ReGeneratedCtrl/GetFilialeById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetFilialeById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetFilialesByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetFilialesByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveFiliales', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveFiliales', 'POST', param, response);
         });

         // Fonctionnalite
         router.get('/api/ReGeneratedCtrl/GetFonctionnaliteById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetFonctionnaliteById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetFonctionnalitesByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetFonctionnalitesByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveFonctionnalites', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveFonctionnalites', 'POST', param, response);
         });

         // FormeJuridique
         router.get('/api/ReGeneratedCtrl/GetFormeJuridiqueById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetFormeJuridiqueById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetFormeJuridiquesByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetFormeJuridiquesByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveFormeJuridiques', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveFormeJuridiques', 'POST', param, response);
         });

         // HistoriqueLigneSchemasPlacement
         router.get('/api/ReGeneratedCtrl/GetHistoriqueLigneSchemasPlacementById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetHistoriqueLigneSchemasPlacementById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetHistoriqueLigneSchemasPlacementsByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetHistoriqueLigneSchemasPlacementsByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveHistoriqueLigneSchemasPlacements', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveHistoriqueLigneSchemasPlacements', 'POST', param, response);
         });

         // HistoriqueLigneTableauRepartitionCharge
         router.get('/api/ReGeneratedCtrl/GetHistoriqueLigneTableauRepartitionChargeById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetHistoriqueLigneTableauRepartitionChargeById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetHistoriqueLigneTableauRepartitionChargesByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetHistoriqueLigneTableauRepartitionChargesByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveHistoriqueLigneTableauRepartitionCharges', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveHistoriqueLigneTableauRepartitionCharges', 'POST', param, response);
         });

         // InterlocuteurFiliale
         router.get('/api/ReGeneratedCtrl/GetInterlocuteurFilialeById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetInterlocuteurFilialeById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetInterlocuteurFilialesByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetInterlocuteurFilialesByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveInterlocuteurFiliales', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveInterlocuteurFiliales', 'POST', param, response);
         });

         // InterlocuteurReassureur
         router.get('/api/ReGeneratedCtrl/GetInterlocuteurReassureurById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetInterlocuteurReassureurById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetInterlocuteurReassureursByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetInterlocuteurReassureursByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveInterlocuteurReassureurs', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveInterlocuteurReassureurs', 'POST', param, response);
         });

         // LigneRepartitionCharge
         router.get('/api/ReGeneratedCtrl/GetLigneRepartitionChargeById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetLigneRepartitionChargeById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetLigneRepartitionChargesByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetLigneRepartitionChargesByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveLigneRepartitionCharges', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveLigneRepartitionCharges', 'POST', param, response);
         });

         // LigneSchemasPlacement
         router.get('/api/ReGeneratedCtrl/GetLigneSchemasPlacementById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetLigneSchemasPlacementById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetLigneSchemasPlacementsByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetLigneSchemasPlacementsByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveLigneSchemasPlacements', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveLigneSchemasPlacements', 'POST', param, response);
         });

         // MailParametre
         router.get('/api/ReGeneratedCtrl/GetMailParametreById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetMailParametreById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetMailParametresByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetMailParametresByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveMailParametres', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveMailParametres', 'POST', param, response);
         });

         // MouvementTresorerie
         router.get('/api/ReGeneratedCtrl/GetMouvementTresorerieById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetMouvementTresorerieById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetMouvementTresoreriesByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetMouvementTresoreriesByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveMouvementTresoreries', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveMouvementTresoreries', 'POST', param, response);
         });

         // Pay
         router.get('/api/ReGeneratedCtrl/GetPayById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetPayById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetPaysByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetPaysByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SavePays', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SavePays', 'POST', param, response);
         });

         // Personne
         router.get('/api/ReGeneratedCtrl/GetPersonneById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetPersonneById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetPersonnesByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetPersonnesByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SavePersonnes', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SavePersonnes', 'POST', param, response);
         });

         // Processu
         router.get('/api/ReGeneratedCtrl/GetProcessuById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetProcessuById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetProcessusByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetProcessusByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveProcessus', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveProcessus', 'POST', param, response);
         });

         // Profession
         router.get('/api/ReGeneratedCtrl/GetProfessionById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetProfessionById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetProfessionsByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetProfessionsByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveProfessions', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveProfessions', 'POST', param, response);
         });

         // Profil
         router.get('/api/ReGeneratedCtrl/GetProfilById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetProfilById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetProfilsByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetProfilsByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveProfils', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveProfils', 'POST', param, response);
         });

         // ProfilFonctionnalite
         router.get('/api/ReGeneratedCtrl/GetProfilFonctionnaliteById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetProfilFonctionnaliteById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetProfilFonctionnalitesByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetProfilFonctionnalitesByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveProfilFonctionnalites', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveProfilFonctionnalites', 'POST', param, response);
         });

         // ProfilFonctionnaliteHisto
         router.get('/api/ReGeneratedCtrl/GetProfilFonctionnaliteHistoById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetProfilFonctionnaliteHistoById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetProfilFonctionnaliteHistosByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetProfilFonctionnaliteHistosByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveProfilFonctionnaliteHistos', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveProfilFonctionnaliteHistos', 'POST', param, response);
         });

         // Reassureur
         router.get('/api/ReGeneratedCtrl/GetReassureurById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetReassureurById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetReassureursByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetReassureursByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveReassureurs', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveReassureurs', 'POST', param, response);
         });

         // SchemasPlacement
         router.get('/api/ReGeneratedCtrl/GetSchemasPlacementById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetSchemasPlacementById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetSchemasPlacementsByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetSchemasPlacementsByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveSchemasPlacements', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveSchemasPlacements', 'POST', param, response);
         });

         // SecteurActivite
         router.get('/api/ReGeneratedCtrl/GetSecteurActiviteById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetSecteurActiviteById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetSecteurActivitesByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetSecteurActivitesByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveSecteurActivites', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveSecteurActivites', 'POST', param, response);
         });

         // SituationMatrimoniale
         router.get('/api/ReGeneratedCtrl/GetSituationMatrimonialeById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetSituationMatrimonialeById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetSituationMatrimonialesByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetSituationMatrimonialesByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveSituationMatrimoniales', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveSituationMatrimoniales', 'POST', param, response);
         });

         // StatutAffaire
         router.get('/api/ReGeneratedCtrl/GetStatutAffaireById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetStatutAffaireById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetStatutAffairesByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetStatutAffairesByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveStatutAffaires', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveStatutAffaires', 'POST', param, response);
         });

         // StatutMouvement
         router.get('/api/ReGeneratedCtrl/GetStatutMouvementById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetStatutMouvementById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetStatutMouvementsByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetStatutMouvementsByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveStatutMouvements', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveStatutMouvements', 'POST', param, response);
         });

         // SuiviProcessu
         router.get('/api/ReGeneratedCtrl/GetSuiviProcessuById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetSuiviProcessuById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetSuiviProcessusByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetSuiviProcessusByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveSuiviProcessus', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveSuiviProcessus', 'POST', param, response);
         });

         // SysComplexiteMotDePasse
         router.get('/api/ReGeneratedCtrl/GetSysComplexiteMotDePasseById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetSysComplexiteMotDePasseById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetSysComplexiteMotDePassesByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetSysComplexiteMotDePassesByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveSysComplexiteMotDePasses', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveSysComplexiteMotDePasses', 'POST', param, response);
         });

         // SysLog
         router.get('/api/ReGeneratedCtrl/GetSysLogById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetSysLogById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetSysLogsByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetSysLogsByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveSysLogs', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveSysLogs', 'POST', param, response);
         });

         // SysMailBox
         router.get('/api/ReGeneratedCtrl/GetSysMailBoxById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetSysMailBoxById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetSysMailBoxsByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetSysMailBoxsByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveSysMailBoxs', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveSysMailBoxs', 'POST', param, response);
         });

         // SysNotification
         router.get('/api/ReGeneratedCtrl/GetSysNotificationById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetSysNotificationById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetSysNotificationsByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetSysNotificationsByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveSysNotifications', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveSysNotifications', 'POST', param, response);
         });

         // SysObjet
         router.get('/api/ReGeneratedCtrl/GetSysObjetById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetSysObjetById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetSysObjetsByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetSysObjetsByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveSysObjets', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveSysObjets', 'POST', param, response);
         });

         // SysStrategieMotDePasse
         router.get('/api/ReGeneratedCtrl/GetSysStrategieMotDePasseById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetSysStrategieMotDePasseById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetSysStrategieMotDePassesByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetSysStrategieMotDePassesByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveSysStrategieMotDePasses', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveSysStrategieMotDePasses', 'POST', param, response);
         });

         // SysStrategieMotDePasseHisto
         router.get('/api/ReGeneratedCtrl/GetSysStrategieMotDePasseHistoById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetSysStrategieMotDePasseHistoById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetSysStrategieMotDePasseHistosByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetSysStrategieMotDePasseHistosByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveSysStrategieMotDePasseHistos', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveSysStrategieMotDePasseHistos', 'POST', param, response);
         });

         // TableauRepartitionCharge
         router.get('/api/ReGeneratedCtrl/GetTableauRepartitionChargeById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetTableauRepartitionChargeById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetTableauRepartitionChargesByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetTableauRepartitionChargesByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveTableauRepartitionCharges', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveTableauRepartitionCharges', 'POST', param, response);
         });

         // Terme
         router.get('/api/ReGeneratedCtrl/GetTermeById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetTermeById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetTermesByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetTermesByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveTermes', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveTermes', 'POST', param, response);
         });

         // TermeTraite
         router.get('/api/ReGeneratedCtrl/GetTermeTraiteById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetTermeTraiteById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetTermeTraitesByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetTermeTraitesByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveTermeTraites', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveTermeTraites', 'POST', param, response);
         });

         // Trace
         router.get('/api/ReGeneratedCtrl/GetTraceById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetTraceById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetTracesByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetTracesByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveTraces', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveTraces', 'POST', param, response);
         });

         // Traite
         router.get('/api/ReGeneratedCtrl/GetTraiteById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetTraiteById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetTraitesByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetTraitesByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveTraites', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveTraites', 'POST', param, response);
         });

         // TypeEchange
         router.get('/api/ReGeneratedCtrl/GetTypeEchangeById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetTypeEchangeById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetTypeEchangesByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetTypeEchangesByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveTypeEchanges', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveTypeEchanges', 'POST', param, response);
         });

         // TypePersonne
         router.get('/api/ReGeneratedCtrl/GetTypePersonneById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetTypePersonneById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetTypePersonnesByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetTypePersonnesByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveTypePersonnes', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveTypePersonnes', 'POST', param, response);
         });

         // TypeProcessu
         router.get('/api/ReGeneratedCtrl/GetTypeProcessuById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetTypeProcessuById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetTypeProcessusByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetTypeProcessusByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveTypeProcessus', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveTypeProcessus', 'POST', param, response);
         });

         // Utilisateur
         router.get('/api/ReGeneratedCtrl/GetUtilisateurById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetUtilisateurById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetUtilisateursByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetUtilisateursByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveUtilisateurs', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveUtilisateurs', 'POST', param, response);
         });

         // UtilisateurFonctionnalite
         router.get('/api/ReGeneratedCtrl/GetUtilisateurFonctionnaliteById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetUtilisateurFonctionnaliteById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetUtilisateurFonctionnalitesByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetUtilisateurFonctionnalitesByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveUtilisateurFonctionnalites', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveUtilisateurFonctionnalites', 'POST', param, response);
         });

         // UtilisateurFonctionnaliteHisto
         router.get('/api/ReGeneratedCtrl/GetUtilisateurFonctionnaliteHistoById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetUtilisateurFonctionnaliteHistoById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetUtilisateurFonctionnaliteHistosByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetUtilisateurFonctionnaliteHistosByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveUtilisateurFonctionnaliteHistos', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveUtilisateurFonctionnaliteHistos', 'POST', param, response);
         });

         // UtilisateurFonctionnalitePrive
         router.get('/api/ReGeneratedCtrl/GetUtilisateurFonctionnalitePriveById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetUtilisateurFonctionnalitePriveById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetUtilisateurFonctionnalitePrivesByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetUtilisateurFonctionnalitePrivesByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveUtilisateurFonctionnalitePrives', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveUtilisateurFonctionnalitePrives', 'POST', param, response);
         });

         // UtilisateurFonctionnalitePriveHisto
         router.get('/api/ReGeneratedCtrl/GetUtilisateurFonctionnalitePriveHistoById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetUtilisateurFonctionnalitePriveHistoById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetUtilisateurFonctionnalitePriveHistosByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetUtilisateurFonctionnalitePriveHistosByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveUtilisateurFonctionnalitePriveHistos', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveUtilisateurFonctionnalitePriveHistos', 'POST', param, response);
         });

         // UtilisateurHisto
         router.get('/api/ReGeneratedCtrl/GetUtilisateurHistoById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetUtilisateurHistoById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetUtilisateurHistosByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetUtilisateurHistosByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveUtilisateurHistos', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveUtilisateurHistos', 'POST', param, response);
         });

         // UtilisateurProfil
         router.get('/api/ReGeneratedCtrl/GetUtilisateurProfilById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetUtilisateurProfilById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetUtilisateurProfilsByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetUtilisateurProfilsByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveUtilisateurProfils', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveUtilisateurProfils', 'POST', param, response);
         });

         // UtilisateurProfilHisto
         router.get('/api/ReGeneratedCtrl/GetUtilisateurProfilHistoById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetUtilisateurProfilHistoById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetUtilisateurProfilHistosByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetUtilisateurProfilHistosByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveUtilisateurProfilHistos', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveUtilisateurProfilHistos', 'POST', param, response);
         });

         // UtilisateurRestriction
         router.get('/api/ReGeneratedCtrl/GetUtilisateurRestrictionById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetUtilisateurRestrictionById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetUtilisateurRestrictionsByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetUtilisateurRestrictionsByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveUtilisateurRestrictions', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveUtilisateurRestrictions', 'POST', param, response);
         });

         // UtilisateurRestrictionHisto
         router.get('/api/ReGeneratedCtrl/GetUtilisateurRestrictionHistoById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetUtilisateurRestrictionHistoById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetUtilisateurRestrictionHistosByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetUtilisateurRestrictionHistosByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveUtilisateurRestrictionHistos', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveUtilisateurRestrictionHistos', 'POST', param, response);
         });

         // UtilisateurSession
         router.get('/api/ReGeneratedCtrl/GetUtilisateurSessionById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetUtilisateurSessionById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetUtilisateurSessionsByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetUtilisateurSessionsByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveUtilisateurSessions', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveUtilisateurSessions', 'POST', param, response);
         });

         // ViewSysStrategieMotDePasse
         router.post('/api/ReGeneratedCtrl/GetViewSysStrategieMotDePassesByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetViewSysStrategieMotDePassesByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveViewSysStrategieMotDePasses', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveViewSysStrategieMotDePasses', 'POST', param, response);
         });

         // ViewUtilisateur
         router.post('/api/ReGeneratedCtrl/GetViewUtilisateursByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetViewUtilisateursByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveViewUtilisateurs', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveViewUtilisateurs', 'POST', param, response);
         });

         // ViewUtilisateurProfil
         router.post('/api/ReGeneratedCtrl/GetViewUtilisateurProfilsByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetViewUtilisateurProfilsByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveViewUtilisateurProfils', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveViewUtilisateurProfils', 'POST', param, response);
         });

         // ViewUtilisateurRestriction
         router.post('/api/ReGeneratedCtrl/GetViewUtilisateurRestrictionsByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetViewUtilisateurRestrictionsByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveViewUtilisateurRestrictions', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveViewUtilisateurRestrictions', 'POST', param, response);
         });

         // Structure
         router.get('/api/ReGeneratedCtrl/GetStructureById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetStructureById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetStructuresByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetStructuresByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveStructures', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveStructures', 'POST', param, response);
         });

         // Civilite
         router.get('/api/ReGeneratedCtrl/GetCiviliteById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetCiviliteById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetCivilitesByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetCivilitesByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveCivilites', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveCivilites', 'POST', param, response);
         });

         // TypePieceIdentite
         router.get('/api/ReGeneratedCtrl/GetTypePieceIdentiteById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetTypePieceIdentiteById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetTypePieceIdentitesByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetTypePieceIdentitesByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveTypePieceIdentites', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveTypePieceIdentites', 'POST', param, response);
         });

         // CategorieProcessu
         router.get('/api/ReGeneratedCtrl/GetCategorieProcessuById', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetCategorieProcessuById', 'GET', param, response);
         });
         router.post('/api/ReGeneratedCtrl/GetCategorieProcessusByCriteria', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/GetCategorieProcessusByCriteria', 'POST', param, response);
         });
         router.post('/api/ReGeneratedCtrl/SaveCategorieProcessus', function(param, response) {
      		  utility.performRequest(utility.host, utility.port.re,'/api/ReGeneratedCtrl/SaveCategorieProcessus', 'POST', param, response);
         });

*/
