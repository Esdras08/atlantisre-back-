  /*

app.factory("reGeneratedCtrlService", ["$resource", "urlSw", function ($resource, urlSw) {
        var url = urlSw.getUrl(urlSw.UrlReBase, ":service", ":method");

        return $resource(url, {}, {

         // Affaire
         GetAffaireById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetAffaireById", id:"@id"} },
         GetAffairesByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetAffairesByCriteria" } },
         SaveAffaires: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveAffaires" } },            

         // Assure
         GetAssureById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetAssureById", id:"@id"} },
         GetAssuresByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetAssuresByCriteria" } },
         SaveAssures: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveAssures" } },            

         // Branche
         GetBrancheById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetBrancheById", id:"@id"} },
         GetBranchesByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetBranchesByCriteria" } },
         SaveBranches: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveBranches" } },            

         // DeclarationSinistre
         GetDeclarationSinistreById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetDeclarationSinistreById", id:"@id"} },
         GetDeclarationSinistresByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetDeclarationSinistresByCriteria" } },
         SaveDeclarationSinistres: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveDeclarationSinistres" } },            

         // DemandePlacementFacultative
         GetDemandePlacementFacultativeById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetDemandePlacementFacultativeById", id:"@id"} },
         GetDemandePlacementFacultativesByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetDemandePlacementFacultativesByCriteria" } },
         SaveDemandePlacementFacultatives: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveDemandePlacementFacultatives" } },            

         // Devise
         GetDeviseById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetDeviseById", id:"@id"} },
         GetDevisesByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetDevisesByCriteria" } },
         SaveDevises: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveDevises" } },            

         // DocumentEchange
         GetDocumentEchangeById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetDocumentEchangeById", id:"@id"} },
         GetDocumentEchangesByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetDocumentEchangesByCriteria" } },
         SaveDocumentEchanges: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveDocumentEchanges" } },            

         // DomaineActivite
         GetDomaineActiviteById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetDomaineActiviteById", id:"@id"} },
         GetDomaineActivitesByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetDomaineActivitesByCriteria" } },
         SaveDomaineActivites: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveDomaineActivites" } },            

         // Echange
         GetEchangeById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetEchangeById", id:"@id"} },
         GetEchangesByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetEchangesByCriteria" } },
         SaveEchanges: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveEchanges" } },            

         // EtapeProcessu
         GetEtapeProcessuById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetEtapeProcessuById", id:"@id"} },
         GetEtapeProcessusByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetEtapeProcessusByCriteria" } },
         SaveEtapeProcessus: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveEtapeProcessus" } },            

         // Filiale
         GetFilialeById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetFilialeById", id:"@id"} },
         GetFilialesByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetFilialesByCriteria" } },
         SaveFiliales: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveFiliales" } },            

         // Fonctionnalite
         GetFonctionnaliteById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetFonctionnaliteById", id:"@id"} },
         GetFonctionnalitesByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetFonctionnalitesByCriteria" } },
         SaveFonctionnalites: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveFonctionnalites" } },            

         // FormeJuridique
         GetFormeJuridiqueById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetFormeJuridiqueById", id:"@id"} },
         GetFormeJuridiquesByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetFormeJuridiquesByCriteria" } },
         SaveFormeJuridiques: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveFormeJuridiques" } },            

         // HistoriqueLigneSchemasPlacement
         GetHistoriqueLigneSchemasPlacementById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetHistoriqueLigneSchemasPlacementById", id:"@id"} },
         GetHistoriqueLigneSchemasPlacementsByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetHistoriqueLigneSchemasPlacementsByCriteria" } },
         SaveHistoriqueLigneSchemasPlacements: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveHistoriqueLigneSchemasPlacements" } },            

         // HistoriqueLigneTableauRepartitionCharge
         GetHistoriqueLigneTableauRepartitionChargeById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetHistoriqueLigneTableauRepartitionChargeById", id:"@id"} },
         GetHistoriqueLigneTableauRepartitionChargesByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetHistoriqueLigneTableauRepartitionChargesByCriteria" } },
         SaveHistoriqueLigneTableauRepartitionCharges: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveHistoriqueLigneTableauRepartitionCharges" } },            

         // InterlocuteurFiliale
         GetInterlocuteurFilialeById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetInterlocuteurFilialeById", id:"@id"} },
         GetInterlocuteurFilialesByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetInterlocuteurFilialesByCriteria" } },
         SaveInterlocuteurFiliales: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveInterlocuteurFiliales" } },            

         // InterlocuteurReassureur
         GetInterlocuteurReassureurById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetInterlocuteurReassureurById", id:"@id"} },
         GetInterlocuteurReassureursByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetInterlocuteurReassureursByCriteria" } },
         SaveInterlocuteurReassureurs: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveInterlocuteurReassureurs" } },            

         // LigneRepartitionCharge
         GetLigneRepartitionChargeById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetLigneRepartitionChargeById", id:"@id"} },
         GetLigneRepartitionChargesByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetLigneRepartitionChargesByCriteria" } },
         SaveLigneRepartitionCharges: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveLigneRepartitionCharges" } },            

         // LigneSchemasPlacement
         GetLigneSchemasPlacementById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetLigneSchemasPlacementById", id:"@id"} },
         GetLigneSchemasPlacementsByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetLigneSchemasPlacementsByCriteria" } },
         SaveLigneSchemasPlacements: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveLigneSchemasPlacements" } },            

         // MailParametre
         GetMailParametreById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetMailParametreById", id:"@id"} },
         GetMailParametresByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetMailParametresByCriteria" } },
         SaveMailParametres: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveMailParametres" } },            

         // MouvementTresorerie
         GetMouvementTresorerieById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetMouvementTresorerieById", id:"@id"} },
         GetMouvementTresoreriesByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetMouvementTresoreriesByCriteria" } },
         SaveMouvementTresoreries: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveMouvementTresoreries" } },            

         // Pay
         GetPayById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetPayById", id:"@id"} },
         GetPaysByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetPaysByCriteria" } },
         SavePays: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SavePays" } },            

         // Personne
         GetPersonneById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetPersonneById", id:"@id"} },
         GetPersonnesByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetPersonnesByCriteria" } },
         SavePersonnes: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SavePersonnes" } },            

         // Processu
         GetProcessuById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetProcessuById", id:"@id"} },
         GetProcessusByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetProcessusByCriteria" } },
         SaveProcessus: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveProcessus" } },            

         // Profession
         GetProfessionById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetProfessionById", id:"@id"} },
         GetProfessionsByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetProfessionsByCriteria" } },
         SaveProfessions: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveProfessions" } },            

         // Profil
         GetProfilById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetProfilById", id:"@id"} },
         GetProfilsByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetProfilsByCriteria" } },
         SaveProfils: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveProfils" } },            

         // ProfilFonctionnalite
         GetProfilFonctionnaliteById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetProfilFonctionnaliteById", id:"@id"} },
         GetProfilFonctionnalitesByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetProfilFonctionnalitesByCriteria" } },
         SaveProfilFonctionnalites: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveProfilFonctionnalites" } },            

         // ProfilFonctionnaliteHisto
         GetProfilFonctionnaliteHistoById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetProfilFonctionnaliteHistoById", id:"@id"} },
         GetProfilFonctionnaliteHistosByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetProfilFonctionnaliteHistosByCriteria" } },
         SaveProfilFonctionnaliteHistos: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveProfilFonctionnaliteHistos" } },            

         // Reassureur
         GetReassureurById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetReassureurById", id:"@id"} },
         GetReassureursByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetReassureursByCriteria" } },
         SaveReassureurs: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveReassureurs" } },            

         // SchemasPlacement
         GetSchemasPlacementById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetSchemasPlacementById", id:"@id"} },
         GetSchemasPlacementsByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetSchemasPlacementsByCriteria" } },
         SaveSchemasPlacements: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveSchemasPlacements" } },            

         // SecteurActivite
         GetSecteurActiviteById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetSecteurActiviteById", id:"@id"} },
         GetSecteurActivitesByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetSecteurActivitesByCriteria" } },
         SaveSecteurActivites: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveSecteurActivites" } },            

         // SituationMatrimoniale
         GetSituationMatrimonialeById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetSituationMatrimonialeById", id:"@id"} },
         GetSituationMatrimonialesByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetSituationMatrimonialesByCriteria" } },
         SaveSituationMatrimoniales: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveSituationMatrimoniales" } },            

         // StatutAffaire
         GetStatutAffaireById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetStatutAffaireById", id:"@id"} },
         GetStatutAffairesByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetStatutAffairesByCriteria" } },
         SaveStatutAffaires: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveStatutAffaires" } },            

         // StatutMouvement
         GetStatutMouvementById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetStatutMouvementById", id:"@id"} },
         GetStatutMouvementsByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetStatutMouvementsByCriteria" } },
         SaveStatutMouvements: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveStatutMouvements" } },            

         // SuiviProcessu
         GetSuiviProcessuById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetSuiviProcessuById", id:"@id"} },
         GetSuiviProcessusByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetSuiviProcessusByCriteria" } },
         SaveSuiviProcessus: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveSuiviProcessus" } },            

         // SysComplexiteMotDePasse
         GetSysComplexiteMotDePasseById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetSysComplexiteMotDePasseById", id:"@id"} },
         GetSysComplexiteMotDePassesByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetSysComplexiteMotDePassesByCriteria" } },
         SaveSysComplexiteMotDePasses: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveSysComplexiteMotDePasses" } },            

         // SysLog
         GetSysLogById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetSysLogById", id:"@id"} },
         GetSysLogsByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetSysLogsByCriteria" } },
         SaveSysLogs: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveSysLogs" } },            

         // SysMailBox
         GetSysMailBoxById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetSysMailBoxById", id:"@id"} },
         GetSysMailBoxsByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetSysMailBoxsByCriteria" } },
         SaveSysMailBoxs: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveSysMailBoxs" } },            

         // SysNotification
         GetSysNotificationById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetSysNotificationById", id:"@id"} },
         GetSysNotificationsByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetSysNotificationsByCriteria" } },
         SaveSysNotifications: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveSysNotifications" } },            

         // SysObjet
         GetSysObjetById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetSysObjetById", id:"@id"} },
         GetSysObjetsByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetSysObjetsByCriteria" } },
         SaveSysObjets: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveSysObjets" } },            

         // SysStrategieMotDePasse
         GetSysStrategieMotDePasseById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetSysStrategieMotDePasseById", id:"@id"} },
         GetSysStrategieMotDePassesByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetSysStrategieMotDePassesByCriteria" } },
         SaveSysStrategieMotDePasses: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveSysStrategieMotDePasses" } },            

         // SysStrategieMotDePasseHisto
         GetSysStrategieMotDePasseHistoById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetSysStrategieMotDePasseHistoById", id:"@id"} },
         GetSysStrategieMotDePasseHistosByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetSysStrategieMotDePasseHistosByCriteria" } },
         SaveSysStrategieMotDePasseHistos: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveSysStrategieMotDePasseHistos" } },            

         // TableauRepartitionCharge
         GetTableauRepartitionChargeById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetTableauRepartitionChargeById", id:"@id"} },
         GetTableauRepartitionChargesByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetTableauRepartitionChargesByCriteria" } },
         SaveTableauRepartitionCharges: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveTableauRepartitionCharges" } },            

         // Terme
         GetTermeById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetTermeById", id:"@id"} },
         GetTermesByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetTermesByCriteria" } },
         SaveTermes: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveTermes" } },            

         // TermeTraite
         GetTermeTraiteById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetTermeTraiteById", id:"@id"} },
         GetTermeTraitesByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetTermeTraitesByCriteria" } },
         SaveTermeTraites: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveTermeTraites" } },            

         // Trace
         GetTraceById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetTraceById", id:"@id"} },
         GetTracesByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetTracesByCriteria" } },
         SaveTraces: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveTraces" } },            

         // Traite
         GetTraiteById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetTraiteById", id:"@id"} },
         GetTraitesByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetTraitesByCriteria" } },
         SaveTraites: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveTraites" } },            

         // TypeEchange
         GetTypeEchangeById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetTypeEchangeById", id:"@id"} },
         GetTypeEchangesByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetTypeEchangesByCriteria" } },
         SaveTypeEchanges: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveTypeEchanges" } },            

         // TypePersonne
         GetTypePersonneById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetTypePersonneById", id:"@id"} },
         GetTypePersonnesByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetTypePersonnesByCriteria" } },
         SaveTypePersonnes: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveTypePersonnes" } },            

         // TypeProcessu
         GetTypeProcessuById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetTypeProcessuById", id:"@id"} },
         GetTypeProcessusByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetTypeProcessusByCriteria" } },
         SaveTypeProcessus: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveTypeProcessus" } },            

         // Utilisateur
         GetUtilisateurById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetUtilisateurById", id:"@id"} },
         GetUtilisateursByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetUtilisateursByCriteria" } },
         SaveUtilisateurs: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveUtilisateurs" } },            

         // UtilisateurFonctionnalite
         GetUtilisateurFonctionnaliteById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetUtilisateurFonctionnaliteById", id:"@id"} },
         GetUtilisateurFonctionnalitesByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetUtilisateurFonctionnalitesByCriteria" } },
         SaveUtilisateurFonctionnalites: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveUtilisateurFonctionnalites" } },            

         // UtilisateurFonctionnaliteHisto
         GetUtilisateurFonctionnaliteHistoById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetUtilisateurFonctionnaliteHistoById", id:"@id"} },
         GetUtilisateurFonctionnaliteHistosByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetUtilisateurFonctionnaliteHistosByCriteria" } },
         SaveUtilisateurFonctionnaliteHistos: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveUtilisateurFonctionnaliteHistos" } },            

         // UtilisateurFonctionnalitePrive
         GetUtilisateurFonctionnalitePriveById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetUtilisateurFonctionnalitePriveById", id:"@id"} },
         GetUtilisateurFonctionnalitePrivesByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetUtilisateurFonctionnalitePrivesByCriteria" } },
         SaveUtilisateurFonctionnalitePrives: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveUtilisateurFonctionnalitePrives" } },            

         // UtilisateurFonctionnalitePriveHisto
         GetUtilisateurFonctionnalitePriveHistoById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetUtilisateurFonctionnalitePriveHistoById", id:"@id"} },
         GetUtilisateurFonctionnalitePriveHistosByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetUtilisateurFonctionnalitePriveHistosByCriteria" } },
         SaveUtilisateurFonctionnalitePriveHistos: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveUtilisateurFonctionnalitePriveHistos" } },            

         // UtilisateurHisto
         GetUtilisateurHistoById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetUtilisateurHistoById", id:"@id"} },
         GetUtilisateurHistosByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetUtilisateurHistosByCriteria" } },
         SaveUtilisateurHistos: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveUtilisateurHistos" } },            

         // UtilisateurProfil
         GetUtilisateurProfilById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetUtilisateurProfilById", id:"@id"} },
         GetUtilisateurProfilsByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetUtilisateurProfilsByCriteria" } },
         SaveUtilisateurProfils: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveUtilisateurProfils" } },            

         // UtilisateurProfilHisto
         GetUtilisateurProfilHistoById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetUtilisateurProfilHistoById", id:"@id"} },
         GetUtilisateurProfilHistosByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetUtilisateurProfilHistosByCriteria" } },
         SaveUtilisateurProfilHistos: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveUtilisateurProfilHistos" } },            

         // UtilisateurRestriction
         GetUtilisateurRestrictionById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetUtilisateurRestrictionById", id:"@id"} },
         GetUtilisateurRestrictionsByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetUtilisateurRestrictionsByCriteria" } },
         SaveUtilisateurRestrictions: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveUtilisateurRestrictions" } },            

         // UtilisateurRestrictionHisto
         GetUtilisateurRestrictionHistoById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetUtilisateurRestrictionHistoById", id:"@id"} },
         GetUtilisateurRestrictionHistosByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetUtilisateurRestrictionHistosByCriteria" } },
         SaveUtilisateurRestrictionHistos: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveUtilisateurRestrictionHistos" } },            

         // UtilisateurSession
         GetUtilisateurSessionById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetUtilisateurSessionById", id:"@id"} },
         GetUtilisateurSessionsByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetUtilisateurSessionsByCriteria" } },
         SaveUtilisateurSessions: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveUtilisateurSessions" } },            

         // ViewSysStrategieMotDePasse
         GetViewSysStrategieMotDePassesByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetViewSysStrategieMotDePassesByCriteria" } },
         SaveViewSysStrategieMotDePasses: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveViewSysStrategieMotDePasses" } },            

         // ViewUtilisateur
         GetViewUtilisateursByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetViewUtilisateursByCriteria" } },
         SaveViewUtilisateurs: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveViewUtilisateurs" } },            

         // ViewUtilisateurProfil
         GetViewUtilisateurProfilsByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetViewUtilisateurProfilsByCriteria" } },
         SaveViewUtilisateurProfils: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveViewUtilisateurProfils" } },            

         // ViewUtilisateurRestriction
         GetViewUtilisateurRestrictionsByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetViewUtilisateurRestrictionsByCriteria" } },
         SaveViewUtilisateurRestrictions: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveViewUtilisateurRestrictions" } },            

         // Structure
         GetStructureById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetStructureById", id:"@id"} },
         GetStructuresByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetStructuresByCriteria" } },
         SaveStructures: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveStructures" } },            

         // Civilite
         GetCiviliteById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetCiviliteById", id:"@id"} },
         GetCivilitesByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetCivilitesByCriteria" } },
         SaveCivilites: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveCivilites" } },            

         // TypePieceIdentite
         GetTypePieceIdentiteById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetTypePieceIdentiteById", id:"@id"} },
         GetTypePieceIdentitesByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetTypePieceIdentitesByCriteria" } },
         SaveTypePieceIdentites: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveTypePieceIdentites" } },            

         // CategorieProcessu
         GetCategorieProcessuById: { method: "GET", params: { service: "ReGeneratedCtrl", method: "GetCategorieProcessuById", id:"@id"} },
         GetCategorieProcessusByCriteria: { method: "POST", params: { service: "ReGeneratedCtrl", method: "GetCategorieProcessusByCriteria" } },
         SaveCategorieProcessus: { method: "POST", params: { service: "ReGeneratedCtrl", method: "SaveCategorieProcessus" } },            
    });
}]);

*/
