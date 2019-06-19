  /*
         mainSelf.Affaire = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdAffaire = obj.IdAffaire;
            self.NumeroOrdre = obj.NumeroOrdre;
            self.NumeroPolice = obj.NumeroPolice;
            self.CapitauxAssure = obj.CapitauxAssure;
            self.Activite = obj.Activite;
            self.IdStatutAffaire = obj.IdStatutAffaire;
            self.IdBranche = obj.IdBranche;
            self.IdFiliale = obj.IdFiliale;
            self.IdAssure = obj.IdAssure;
            self.IsDeleted = obj.IsDeleted;
            self.DateCreation = obj.DateCreation;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
            self.CreatedBy = obj.CreatedBy;
            self.ModifiedBy = obj.ModifiedBy;
        };

         mainSelf.Assure = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdAssure = obj.IdAssure;
            self.NumeroAssure = obj.NumeroAssure;
            self.IdFiliale = obj.IdFiliale;
            self.IdPersonne = obj.IdPersonne;
            self.IsDeleted = obj.IsDeleted;
            self.DateCreation = obj.DateCreation;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
            self.CreatedBy = obj.CreatedBy;
            self.ModifiedBy = obj.ModifiedBy;
        };

         mainSelf.Branche = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdBranche = obj.IdBranche;
            self.Libelle = obj.Libelle;
            self.Description = obj.Description;
            self.Code = obj.Code;
            self.IsDeleted = obj.IsDeleted;
            self.DateCreation = obj.DateCreation;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
            self.CreatedBy = obj.CreatedBy;
            self.ModifiedBy = obj.ModifiedBy;
        };

         mainSelf.DeclarationSinistre = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdDeclarationSinistre = obj.IdDeclarationSinistre;
            self.DateSurvenance = obj.DateSurvenance;
            self.DateDeclaration = obj.DateDeclaration;
            self.NatureSinistre = obj.NatureSinistre;
            self.Evaluation = obj.Evaluation;
            self.HonoraireExperts = obj.HonoraireExperts;
            self.MontantPaye = obj.MontantPaye;
            self.MontantRestant = obj.MontantRestant;
            self.IdProcessus = obj.IdProcessus;
            self.IsDeleted = obj.IsDeleted;
            self.DateCreation = obj.DateCreation;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
            self.CreatedBy = obj.CreatedBy;
            self.ModifiedBy = obj.ModifiedBy;
        };

         mainSelf.DemandePlacementFacultative = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdDemandePlacementFacultative = obj.IdDemandePlacementFacultative;
            self.DateConsultation = obj.DateConsultation;
            self.GarantieCedee = obj.GarantieCedee;
            self.Prime = obj.Prime;
            self.DateEffet = obj.DateEffet;
            self.DateEcheance = obj.DateEcheance;
            self.EnCours = obj.EnCours;
            self.SMP = obj.SMP;
            self.PartFiliale = obj.PartFiliale;
            self.VersementAuTraite = obj.VersementAuTraite;
            self.IdProcessus = obj.IdProcessus;
            self.IdPays = obj.IdPays;
            self.IsDeleted = obj.IsDeleted;
            self.DateCreation = obj.DateCreation;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
            self.CreatedBy = obj.CreatedBy;
            self.ModifiedBy = obj.ModifiedBy;
        };

         mainSelf.Devise = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdDevise = obj.IdDevise;
            self.CodeDevise = obj.CodeDevise;
            self.SymboleDevise = obj.SymboleDevise;
            self.LibelleDevise = obj.LibelleDevise;
            self.DateCreation = obj.DateCreation;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
            self.ModifiedBy = obj.ModifiedBy;
            self.IsDeleted = obj.IsDeleted;
            self.CreatedBy = obj.CreatedBy;
        };

         mainSelf.DocumentEchange = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdEchange = obj.IdEchange;
            self.IdDocument = obj.IdDocument;
            self.IsDeleted = obj.IsDeleted;
            self.CreatedBy = obj.CreatedBy;
            self.DateCreation = obj.DateCreation;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
            self.ModifiedBy = obj.ModifiedBy;
        };

         mainSelf.DomaineActivite = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdDomaineActivite = obj.IdDomaineActivite;
            self.Libelle = obj.Libelle;
            self.DateCreation = obj.DateCreation;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
            self.ModifiedBy = obj.ModifiedBy;
            self.IsDeleted = obj.IsDeleted;
            self.Description = obj.Description;
        };

         mainSelf.Echange = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdEchange = obj.IdEchange;
            self.DateEchange = obj.DateEchange;
            self.IdEmetteur = obj.IdEmetteur;
            self.IdDestinataire = obj.IdDestinataire;
            self.NatureEmetteur = obj.NatureEmetteur;
            self.NatureDestinataire = obj.NatureDestinataire;
            self.IdProcessus = obj.IdProcessus;
            self.IdTypeEchange = obj.IdTypeEchange;
            self.IsDeleted = obj.IsDeleted;
            self.CreatedBy = obj.CreatedBy;
            self.DateCreation = obj.DateCreation;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
            self.ModifiedBy = obj.ModifiedBy;
        };

         mainSelf.EtapeProcessu = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdEtapeProcessus = obj.IdEtapeProcessus;
            self.Libelle = obj.Libelle;
            self.Description = obj.Description;
            self.Position = obj.Position;
            self.IsDeleted = obj.IsDeleted;
            self.CreatedBy = obj.CreatedBy;
            self.DateCreation = obj.DateCreation;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
            self.ModifiedBy = obj.ModifiedBy;
        };

         mainSelf.Filiale = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdFiliale = obj.IdFiliale;
            self.NomFilliale = obj.NomFilliale;
            self.AdresseFiliale = obj.AdresseFiliale;
            self.CodeFiliale = obj.CodeFiliale;
            self.IdPays = obj.IdPays;
            self.IsDeleted = obj.IsDeleted;
            self.DateCreation = obj.DateCreation;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
            self.CreatedBy = obj.CreatedBy;
            self.ModifiedBy = obj.ModifiedBy;
        };

         mainSelf.Fonctionnalite = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdFonctionnalite = obj.IdFonctionnalite;
            self.CodeParent = obj.CodeParent;
            self.Code = obj.Code;
            self.Libelle = obj.Libelle;
            self.Description = obj.Description;
            self.IdModule = obj.IdModule;
            self.CodeSousModule = obj.CodeSousModule;
            self.OrdreSousModule = obj.OrdreSousModule;
            self.Ordre = obj.Ordre;
            self.Activer = obj.Activer;
            self.Police = obj.Police;
            self.IsDeleted = obj.IsDeleted;
            self.CreatedBy = obj.CreatedBy;
            self.DateCreation = obj.DateCreation;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
        };

         mainSelf.FormeJuridique = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdFormeJuridique = obj.IdFormeJuridique;
            self.Libelle = obj.Libelle;
            self.DateCreation = obj.DateCreation;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
            self.ModifiedBy = obj.ModifiedBy;
            self.IsDeleted = obj.IsDeleted;
            self.CreatedBy = obj.CreatedBy;
        };

         mainSelf.HistoriqueLigneSchemasPlacement = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdHistoriqueLigneSchemaPlacement = obj.IdHistoriqueLigneSchemaPlacement;
            self.IdLigneSchemaPlacement = obj.IdLigneSchemaPlacement;
            self.Proposition = obj.Proposition;
            self.Pourcentage = obj.Pourcentage;
            self.CapitauxAssures = obj.CapitauxAssures;
            self.TauxCommission = obj.TauxCommission;
            self.Commission = obj.Commission;
            self.PrimeNette = obj.PrimeNette;
            self.IdReassureur = obj.IdReassureur;
            self.IsDeleted = obj.IsDeleted;
            self.DateCreation = obj.DateCreation;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
            self.CreatedBy = obj.CreatedBy;
            self.ModifiedBy = obj.ModifiedBy;
        };

         mainSelf.HistoriqueLigneTableauRepartitionCharge = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdHistoriqueLigneTableauRepartitionCharge = obj.IdHistoriqueLigneTableauRepartitionCharge;
            self.IdTableauRepartition = obj.IdTableauRepartition;
            self.IsDeleted = obj.IsDeleted;
            self.CreatedBy = obj.CreatedBy;
            self.ModifiedBy = obj.ModifiedBy;
            self.DateCreation = obj.DateCreation;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
            self.Proposition = obj.Proposition;
            self.IdReassureur = obj.IdReassureur;
        };

         mainSelf.InterlocuteurFiliale = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdInterlocuteurFiliale = obj.IdInterlocuteurFiliale;
            self.Poste = obj.Poste;
            self.IdFiliale = obj.IdFiliale;
            self.IdPersonne = obj.IdPersonne;
            self.IsDeleted = obj.IsDeleted;
            self.CreatedBy = obj.CreatedBy;
            self.DateCreation = obj.DateCreation;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
            self.ModifiedBy = obj.ModifiedBy;
        };

         mainSelf.InterlocuteurReassureur = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdInterlocuteurReassureur = obj.IdInterlocuteurReassureur;
            self.Poste = obj.Poste;
            self.IdReassureur = obj.IdReassureur;
            self.IdPersonne = obj.IdPersonne;
            self.IsDeleted = obj.IsDeleted;
            self.CreatedBy = obj.CreatedBy;
            self.DateCreation = obj.DateCreation;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
            self.ModifiedBy = obj.ModifiedBy;
        };

         mainSelf.LigneRepartitionCharge = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdLigneRepartitionCharge = obj.IdLigneRepartitionCharge;
            self.IdTableauRepartitionCharge = obj.IdTableauRepartitionCharge;
            self.IsDeleted = obj.IsDeleted;
            self.CreatedBy = obj.CreatedBy;
            self.DateCreation = obj.DateCreation;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
            self.ModifiedBy = obj.ModifiedBy;
            self.IdReassureur = obj.IdReassureur;
        };

         mainSelf.LigneSchemasPlacement = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdLigneSchemaPlacement = obj.IdLigneSchemaPlacement;
            self.Proposition = obj.Proposition;
            self.Pourcentage = obj.Pourcentage;
            self.CapitauxAssures = obj.CapitauxAssures;
            self.TauxCommission = obj.TauxCommission;
            self.Commission = obj.Commission;
            self.PrimeNette = obj.PrimeNette;
            self.IdReassureur = obj.IdReassureur;
            self.IsDeleted = obj.IsDeleted;
            self.CreatedBy = obj.CreatedBy;
            self.DateCreation = obj.DateCreation;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
            self.ModifiedBy = obj.ModifiedBy;
            self.IdSchemasPlacement = obj.IdSchemasPlacement;
        };

         mainSelf.MailParametre = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdMailParametre = obj.IdMailParametre;
            self.Code = obj.Code;
            self.Description = obj.Description;
            self.Valeur = obj.Valeur;
            self.Ordre = obj.Ordre;
            self.DateCreation = obj.DateCreation;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
        };

         mainSelf.MouvementTresorerie = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdMouvement = obj.IdMouvement;
            self.IdStatutMouvement = obj.IdStatutMouvement;
            self.Entrant = obj.Entrant;
            self.Libelle = obj.Libelle;
            self.Code = obj.Code;
            self.IdProcessus = obj.IdProcessus;
            self.IsDeleted = obj.IsDeleted;
            self.DateCreation = obj.DateCreation;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
            self.CreatedBy = obj.CreatedBy;
            self.ModifiedBy = obj.ModifiedBy;
        };

         mainSelf.Pay = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdPays = obj.IdPays;
            self.Libelle = obj.Libelle;
            self.LibelleNationnalite = obj.LibelleNationnalite;
            self.DateCreation = obj.DateCreation;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
            self.ModifiedBy = obj.ModifiedBy;
            self.IsDeleted = obj.IsDeleted;
            self.CreatedBy = obj.CreatedBy;
        };

         mainSelf.Personne = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdPersonne = obj.IdPersonne;
            self.TypePersonne = obj.TypePersonne;
            self.IdCivilite = obj.IdCivilite;
            self.Nom = obj.Nom;
            self.Prenom = obj.Prenom;
            self.DateNaissance = obj.DateNaissance;
            self.LieuNaissance = obj.LieuNaissance;
            self.IdTypePieceIdentite = obj.IdTypePieceIdentite;
            self.NumeroPieceIdentite = obj.NumeroPieceIdentite;
            self.IdPays = obj.IdPays;
            self.DatePieceIdentite = obj.DatePieceIdentite;
            self.DateValiditePieceIdentite = obj.DateValiditePieceIdentite;
            self.IdFormeJuridique = obj.IdFormeJuridique;
            self.Actif = obj.Actif;
            self.IsDeleted = obj.IsDeleted;
            self.DateCreation = obj.DateCreation;
            self.DateMaj = obj.DateMaj;
            self.CreatedBy = obj.CreatedBy;
            self.DataKey = obj.DataKey;
            self.ModifiedBy = obj.ModifiedBy;
        };

         mainSelf.Processu = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdProcessus = obj.IdProcessus;
            self.IdAffaire = obj.IdAffaire;
            self.IdTypeProcessus = obj.IdTypeProcessus;
            self.Categorie = obj.Categorie;
            self.IsDeleted = obj.IsDeleted;
            self.CreatedBy = obj.CreatedBy;
            self.DateCreation = obj.DateCreation;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
            self.ModifiedBy = obj.ModifiedBy;
        };

         mainSelf.Profession = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdProfession = obj.IdProfession;
            self.Libelle = obj.Libelle;
            self.IsDeleted = obj.IsDeleted;
            self.DateCreation = obj.DateCreation;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
            self.ModifiedBy = obj.ModifiedBy;
            self.CreatedBy = obj.CreatedBy;
            self.Description = obj.Description;
        };

         mainSelf.Profil = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdProfil = obj.IdProfil;
            self.CodeProfil = obj.CodeProfil;
            self.Intitule = obj.Intitule;
            self.Description = obj.Description;
            self.IsDeleted = obj.IsDeleted;
            self.CreatedBy = obj.CreatedBy;
            self.ModifiedBy = obj.ModifiedBy;
            self.DateCreation = obj.DateCreation;
            self.DateMaj = obj.DateMaj;
            self.DataKey = obj.DataKey;
        };

         mainSelf.ProfilFonctionnalite = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdProfilFonctionnalite = obj.IdProfilFonctionnalite;
            self.IdProfil = obj.IdProfil;
            self.CodeFonctionnalite = obj.CodeFonctionnalite;
            self.Autorise = obj.Autorise;
            self.DateCreation = obj.DateCreation;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
        };

         mainSelf.ProfilFonctionnaliteHisto = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdProfilFonctionnalite = obj.IdProfilFonctionnalite;
            self.IdProfil = obj.IdProfil;
            self.CodeFonctionnalite = obj.CodeFonctionnalite;
            self.Autorise = obj.Autorise;
            self.DateCreation = obj.DateCreation;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
        };

         mainSelf.Reassureur = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdReassureur = obj.IdReassureur;
            self.CodeReassureur = obj.CodeReassureur;
            self.NomReassureur = obj.NomReassureur;
            self.IdPays = obj.IdPays;
            self.IsDeleted = obj.IsDeleted;
            self.DateCreation = obj.DateCreation;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
            self.CreatedBy = obj.CreatedBy;
            self.ModifiedBy = obj.ModifiedBy;
        };

         mainSelf.SchemasPlacement = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdSchemaPlacement = obj.IdSchemaPlacement;
            self.Proposition = obj.Proposition;
            self.NumeroFiche = obj.NumeroFiche;
            self.IdProcessus = obj.IdProcessus;
            self.IsDeleted = obj.IsDeleted;
            self.DateCreation = obj.DateCreation;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
            self.CreatedBy = obj.CreatedBy;
            self.ModifiedBy = obj.ModifiedBy;
        };

         mainSelf.SecteurActivite = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdDomaineActivite = obj.IdDomaineActivite;
            self.Libelle = obj.Libelle;
            self.DateCreation = obj.DateCreation;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
            self.ModifiedBy = obj.ModifiedBy;
            self.IsDeleted = obj.IsDeleted;
            self.CreatedBy = obj.CreatedBy;
        };

         mainSelf.SituationMatrimoniale = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdSituationMatrimoniale = obj.IdSituationMatrimoniale;
            self.Libelle = obj.Libelle;
            self.ModifiedBy = obj.ModifiedBy;
            self.DateCreation = obj.DateCreation;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
            self.IsDeleted = obj.IsDeleted;
            self.CreatedBy = obj.CreatedBy;
        };

         mainSelf.StatutAffaire = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdStatutAffaire = obj.IdStatutAffaire;
            self.Description = obj.Description;
            self.Libelle = obj.Libelle;
            self.IsDeleted = obj.IsDeleted;
            self.DateCreation = obj.DateCreation;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
            self.CreatedBy = obj.CreatedBy;
            self.ModifiedBy = obj.ModifiedBy;
        };

         mainSelf.StatutMouvement = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdStatutMouvement = obj.IdStatutMouvement;
            self.Libelle = obj.Libelle;
            self.IsDeleted = obj.IsDeleted;
            self.CreatedBy = obj.CreatedBy;
            self.DateCreation = obj.DateCreation;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
            self.ModifiedBy = obj.ModifiedBy;
        };

         mainSelf.SuiviProcessu = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdTypeEchange = obj.IdTypeEchange;
            self.IdEtapeProcessus = obj.IdEtapeProcessus;
            self.IsDeleted = obj.IsDeleted;
            self.CreatedBy = obj.CreatedBy;
            self.DateCreation = obj.DateCreation;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
            self.ModifiedBy = obj.ModifiedBy;
        };

         mainSelf.SysComplexiteMotDePasse = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdSysComplexiteMotDePasse = obj.IdSysComplexiteMotDePasse;
            self.Code = obj.Code;
            self.Libelle = obj.Libelle;
            self.DateCreation = obj.DateCreation;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
        };

         mainSelf.SysLog = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdSysLog = obj.IdSysLog;
            self.Entite = obj.Entite;
            self.IdEntite = obj.IdEntite;
            self.AppMessage = obj.AppMessage;
            self.SysMessage = obj.SysMessage;
            self.CreatedBy = obj.CreatedBy;
            self.DateCreation = obj.DateCreation;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
        };

         mainSelf.SysMailBox = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdSysMailBox = obj.IdSysMailBox;
            self.Entite = obj.Entite;
            self.IdEntite = obj.IdEntite;
            self.IdMailitem = obj.IdMailitem;
            self.Statut = obj.Statut;
            self.DateCreation = obj.DateCreation;
            self.MailMessage = obj.MailMessage;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
        };

         mainSelf.SysNotification = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdNotification = obj.IdNotification;
            self.Titre = obj.Titre;
            self.Message = obj.Message;
            self.Url = obj.Url;
            self.IdUtilisateur = obj.IdUtilisateur;
            self.DateCreation = obj.DateCreation;
            self.IsClicked = obj.IsClicked;
            self.IsDeleted = obj.IsDeleted;
            self.CreatedBy = obj.CreatedBy;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
        };

         mainSelf.SysObjet = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdSysObjet = obj.IdSysObjet;
            self.TypeObjet = obj.TypeObjet;
            self.Code = obj.Code;
            self.Libelle = obj.Libelle;
            self.Description = obj.Description;
            self.DateCreation = obj.DateCreation;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
        };

         mainSelf.SysStrategieMotDePasse = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdSysStrategieMotDePasse = obj.IdSysStrategieMotDePasse;
            self.Libelle = obj.Libelle;
            self.LongeurMinimum = obj.LongeurMinimum;
            self.DureeDeVie = obj.DureeDeVie;
            self.NombreTentativeAvantVerrouillage = obj.NombreTentativeAvantVerrouillage;
            self.IdSysComplexiteMotDePasse = obj.IdSysComplexiteMotDePasse;
            self.UtiliserAncienMotDePasse = obj.UtiliserAncienMotDePasse;
            self.ChangerMotDePasseApresAttribution = obj.ChangerMotDePasseApresAttribution;
            self.Actif = obj.Actif;
            self.IsDeleted = obj.IsDeleted;
            self.DateCreation = obj.DateCreation;
            self.DateMaj = obj.DateMaj;
            self.CreatedBy = obj.CreatedBy;
            self.ModifiedBy = obj.ModifiedBy;
            self.DataKey = obj.DataKey;
        };

         mainSelf.SysStrategieMotDePasseHisto = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdSysStrategieMotDePasseHisto = obj.IdSysStrategieMotDePasseHisto;
            self.IdSysStrategieMotDePasse = obj.IdSysStrategieMotDePasse;
            self.Libelle = obj.Libelle;
            self.LongeurMinimum = obj.LongeurMinimum;
            self.DureeDeVie = obj.DureeDeVie;
            self.NombreTentativeAvantVerrouillage = obj.NombreTentativeAvantVerrouillage;
            self.IdSysComplexiteMotDePasse = obj.IdSysComplexiteMotDePasse;
            self.UtiliserAncienMotDePasse = obj.UtiliserAncienMotDePasse;
            self.ChangerMotDePasseApresAttribution = obj.ChangerMotDePasseApresAttribution;
            self.Actif = obj.Actif;
            self.IsDeleted = obj.IsDeleted;
            self.DateCreation = obj.DateCreation;
            self.DateMaj = obj.DateMaj;
            self.CreatedBy = obj.CreatedBy;
            self.ModifiedBy = obj.ModifiedBy;
            self.Temps = obj.Temps;
            self.DataKey = obj.DataKey;
        };

         mainSelf.TableauRepartitionCharge = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdTableauRepartitionCharge = obj.IdTableauRepartitionCharge;
            self.NumeroFiche = obj.NumeroFiche;
            self.Proposition = obj.Proposition;
            self.IdProcessus = obj.IdProcessus;
            self.IsDeleted = obj.IsDeleted;
            self.DateCreation = obj.DateCreation;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
            self.CreatedBy = obj.CreatedBy;
            self.ModifiedBy = obj.ModifiedBy;
        };

         mainSelf.Terme = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdTerme = obj.IdTerme;
            self.LimiteSouscription = obj.LimiteSouscription;
            self.Exclusion = obj.Exclusion;
            self.Taux = obj.Taux;
            self.IsDeleted = obj.IsDeleted;
            self.DateCreation = obj.DateCreation;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
            self.CreatedBy = obj.CreatedBy;
            self.ModifiedBy = obj.ModifiedBy;
        };

         mainSelf.TermeTraite = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdTermeTraite = obj.IdTermeTraite;
            self.IdTraite = obj.IdTraite;
            self.IdTerme = obj.IdTerme;
            self.IsDeleted = obj.IsDeleted;
            self.DateCreation = obj.DateCreation;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
            self.CreatedBy = obj.CreatedBy;
            self.ModifiedBy = obj.ModifiedBy;
        };

         mainSelf.Trace = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdTrace = obj.IdTrace;
            self.IdStructure = obj.IdStructure;
            self.IdUtilisateur = obj.IdUtilisateur;
            self.DateOperation = obj.DateOperation;
            self.HeureOperation = obj.HeureOperation;
            self.DateSysteme = obj.DateSysteme;
            self.HeureSysteme = obj.HeureSysteme;
            self.IdTypeOperation = obj.IdTypeOperation;
            self.CodeOperation = obj.CodeOperation;
            self.Origine = obj.Origine;
            self.IdOrigine = obj.IdOrigine;
            self.IdFormulaire = obj.IdFormulaire;
            self.CodeFormulaire = obj.CodeFormulaire;
            self.LibelleOperation = obj.LibelleOperation;
            self.DateCreation = obj.DateCreation;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
        };

         mainSelf.Traite = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdTraite = obj.IdTraite;
            self.Libelle = obj.Libelle;
            self.Description = obj.Description;
            self.NatureActivite = obj.NatureActivite;
            self.IdBranche = obj.IdBranche;
            self.IdReassureur = obj.IdReassureur;
            self.IdStructure = obj.IdStructure;
            self.IsDeleted = obj.IsDeleted;
            self.DateCreation = obj.DateCreation;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
            self.CreatedBy = obj.CreatedBy;
            self.ModifiedBy = obj.ModifiedBy;
        };

         mainSelf.TypeEchange = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdTypeEchange = obj.IdTypeEchange;
            self.Libelle = obj.Libelle;
            self.Description = obj.Description;
            self.IsDeleted = obj.IsDeleted;
            self.CreatedBy = obj.CreatedBy;
            self.DateCreation = obj.DateCreation;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
            self.ModifiedBy = obj.ModifiedBy;
        };

         mainSelf.TypePersonne = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdTypePersonne = obj.IdTypePersonne;
            self.LibelleTypePersonne = obj.LibelleTypePersonne;
            self.IsDeleted = obj.IsDeleted;
            self.DateCreation = obj.DateCreation;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
            self.CreatedBy = obj.CreatedBy;
            self.ModifiedBy = obj.ModifiedBy;
        };

         mainSelf.TypeProcessu = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdTypeProcessus = obj.IdTypeProcessus;
            self.Libelle = obj.Libelle;
            self.IsDeleted = obj.IsDeleted;
            self.CreatedBy = obj.CreatedBy;
            self.DateCreation = obj.DateCreation;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
            self.ModifiedBy = obj.ModifiedBy;
        };

         mainSelf.Utilisateur = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdUtilisateur = obj.IdUtilisateur;
            self.Login = obj.Login;
            self.Password = obj.Password;
            self.Nom = obj.Nom;
            self.Prenom = obj.Prenom;
            self.Email = obj.Email;
            self.Telephone = obj.Telephone;
            self.Adresse = obj.Adresse;
            self.Photo = obj.Photo;
            self.VoirCompteSensible = obj.VoirCompteSensible;
            self.ChangeLogin = obj.ChangeLogin;
            self.ChangePassword = obj.ChangePassword;
            self.TimeSession = obj.TimeSession;
            self.Actif = obj.Actif;
            self.IsConnected = obj.IsConnected;
            self.ComptePermanent = obj.ComptePermanent;
            self.DateDebutValidite = obj.DateDebutValidite;
            self.DateFinValidite = obj.DateFinValidite;
            self.RecevoirMail = obj.RecevoirMail;
            self.RecevoirMailSysteme = obj.RecevoirMailSysteme;
            self.IdProfil = obj.IdProfil;
            self.CanApplySingleSession = obj.CanApplySingleSession;
            self.IsOldUser = obj.IsOldUser;
            self.IsWindowAccount = obj.IsWindowAccount;
            self.IsDeleted = obj.IsDeleted;
            self.DateCreation = obj.DateCreation;
            self.DateMaj = obj.DateMaj;
            self.CreatedBy = obj.CreatedBy;
            self.ModifiedBy = obj.ModifiedBy;
            self.IdStructure = obj.IdStructure;
            self.TentativeConnexion = obj.TentativeConnexion;
            self.DateDebutValiditePassword = obj.DateDebutValiditePassword;
            self.DateFinValiditePassword = obj.DateFinValiditePassword;
            self.UserSageCode = obj.UserSageCode;
            self.DataKey = obj.DataKey;
        };

         mainSelf.UtilisateurFonctionnalite = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdUtilisateurFonctionnalite = obj.IdUtilisateurFonctionnalite;
            self.IdUtilisateur = obj.IdUtilisateur;
            self.CodeFonctionnalite = obj.CodeFonctionnalite;
            self.Autorise = obj.Autorise;
            self.DateCreation = obj.DateCreation;
            self.CreatedBy = obj.CreatedBy;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
        };

         mainSelf.UtilisateurFonctionnaliteHisto = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdUtilisateurFonctionnalite = obj.IdUtilisateurFonctionnalite;
            self.IdUtilisateur = obj.IdUtilisateur;
            self.CodeFonctionnalite = obj.CodeFonctionnalite;
            self.Autorise = obj.Autorise;
            self.DateCreation = obj.DateCreation;
            self.CreatedBy = obj.CreatedBy;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
        };

         mainSelf.UtilisateurFonctionnalitePrive = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdUtilisateurFonctionnalitePrive = obj.IdUtilisateurFonctionnalitePrive;
            self.IdUtilisateur = obj.IdUtilisateur;
            self.CodeFonctionnalite = obj.CodeFonctionnalite;
            self.Autorise = obj.Autorise;
            self.DateCreation = obj.DateCreation;
            self.CreatedBy = obj.CreatedBy;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
        };

         mainSelf.UtilisateurFonctionnalitePriveHisto = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdUtilisateurFonctionnalitePrive = obj.IdUtilisateurFonctionnalitePrive;
            self.IdUtilisateur = obj.IdUtilisateur;
            self.CodeFonctionnalite = obj.CodeFonctionnalite;
            self.Autorise = obj.Autorise;
            self.DateCreation = obj.DateCreation;
            self.CreatedBy = obj.CreatedBy;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
        };

         mainSelf.UtilisateurHisto = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdUtilisateurHisto = obj.IdUtilisateurHisto;
            self.IdUtilisateur = obj.IdUtilisateur;
            self.Login = obj.Login;
            self.Password = obj.Password;
            self.Nom = obj.Nom;
            self.Prenom = obj.Prenom;
            self.Email = obj.Email;
            self.Telephone = obj.Telephone;
            self.Adresse = obj.Adresse;
            self.Photo = obj.Photo;
            self.VoirCompteSensible = obj.VoirCompteSensible;
            self.ChangeLogin = obj.ChangeLogin;
            self.ChangePassword = obj.ChangePassword;
            self.TimeSession = obj.TimeSession;
            self.Actif = obj.Actif;
            self.IsConnected = obj.IsConnected;
            self.ComptePermanent = obj.ComptePermanent;
            self.DateDebutValidite = obj.DateDebutValidite;
            self.DateFinValidite = obj.DateFinValidite;
            self.RecevoirMail = obj.RecevoirMail;
            self.RecevoirMailSysteme = obj.RecevoirMailSysteme;
            self.IdProfil = obj.IdProfil;
            self.CanApplySingleSession = obj.CanApplySingleSession;
            self.IsOldUser = obj.IsOldUser;
            self.IsWindowAccount = obj.IsWindowAccount;
            self.IsDeleted = obj.IsDeleted;
            self.DateCreation = obj.DateCreation;
            self.DateMaj = obj.DateMaj;
            self.CreatedBy = obj.CreatedBy;
            self.ModifiedBy = obj.ModifiedBy;
            self.IdStructure = obj.IdStructure;
            self.TentativeConnexion = obj.TentativeConnexion;
            self.DateDebutValiditePassword = obj.DateDebutValiditePassword;
            self.DateFinValiditePassword = obj.DateFinValiditePassword;
            self.UserSageCode = obj.UserSageCode;
            self.DataKey = obj.DataKey;
        };

         mainSelf.UtilisateurProfil = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdUtilisateurProfil = obj.IdUtilisateurProfil;
            self.IdUtilisateur = obj.IdUtilisateur;
            self.IdProfil = obj.IdProfil;
            self.DateValiditeDebut = obj.DateValiditeDebut;
            self.DateValiditeFin = obj.DateValiditeFin;
            self.DateFinIndeterminer = obj.DateFinIndeterminer;
            self.Autorise = obj.Autorise;
            self.IsDeleted = obj.IsDeleted;
            self.CreatedBy = obj.CreatedBy;
            self.ModifiedBy = obj.ModifiedBy;
            self.DateCreation = obj.DateCreation;
            self.DateMaj = obj.DateMaj;
            self.DataKey = obj.DataKey;
        };

         mainSelf.UtilisateurProfilHisto = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdUtilisateurProfil = obj.IdUtilisateurProfil;
            self.IdUtilisateur = obj.IdUtilisateur;
            self.IdProfil = obj.IdProfil;
            self.DateValiditeDebut = obj.DateValiditeDebut;
            self.DateValiditeFin = obj.DateValiditeFin;
            self.DateFinIndeterminer = obj.DateFinIndeterminer;
            self.Autorise = obj.Autorise;
            self.IsDeleted = obj.IsDeleted;
            self.CreatedBy = obj.CreatedBy;
            self.ModifiedBy = obj.ModifiedBy;
            self.DateCreation = obj.DateCreation;
            self.DateMaj = obj.DateMaj;
            self.DataKey = obj.DataKey;
        };

         mainSelf.UtilisateurRestriction = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdUtilisateurRestriction = obj.IdUtilisateurRestriction;
            self.IdUtilisateur = obj.IdUtilisateur;
            self.IdSysObjet = obj.IdSysObjet;
            self.DateCreation = obj.DateCreation;
            self.CreatedBy = obj.CreatedBy;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
        };

         mainSelf.UtilisateurRestrictionHisto = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdUtilisateurRestrictionHisto = obj.IdUtilisateurRestrictionHisto;
            self.IdUtilisateurRestriction = obj.IdUtilisateurRestriction;
            self.IdUtilisateur = obj.IdUtilisateur;
            self.IdSysObjet = obj.IdSysObjet;
            self.DateCreation = obj.DateCreation;
            self.CreatedBy = obj.CreatedBy;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
        };

         mainSelf.UtilisateurSession = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdUtilisateurSession = obj.IdUtilisateurSession;
            self.IdUtilisateur = obj.IdUtilisateur;
            self.TimeSession = obj.TimeSession;
            self.IsConnected = obj.IsConnected;
            self.DateLastConnection = obj.DateLastConnection;
            self.DateLastActivity = obj.DateLastActivity;
            self.DateCreation = obj.DateCreation;
            self.DateMaj = obj.DateMaj;
            self.IsDeleted = obj.IsDeleted;
            self.DataKey = obj.DataKey;
        };

         mainSelf.ViewSysStrategieMotDePasse = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdSysStrategieMotDePasse = obj.IdSysStrategieMotDePasse;
            self.Libelle = obj.Libelle;
            self.LongeurMinimum = obj.LongeurMinimum;
            self.DureeDeVie = obj.DureeDeVie;
            self.NombreTentativeAvantVerrouillage = obj.NombreTentativeAvantVerrouillage;
            self.IdSysComplexiteMotDePasse = obj.IdSysComplexiteMotDePasse;
            self.LibelleComplexite = obj.LibelleComplexite;
            self.UtiliserAncienMotDePasse = obj.UtiliserAncienMotDePasse;
            self.ChangerMotDePasseApresAttribution = obj.ChangerMotDePasseApresAttribution;
            self.Actif = obj.Actif;
            self.IsDeleted = obj.IsDeleted;
            self.DateCreation = obj.DateCreation;
            self.DateMaj = obj.DateMaj;
            self.CreatedBy = obj.CreatedBy;
            self.ModifiedBy = obj.ModifiedBy;
            self.Code = obj.Code;
        };

         mainSelf.ViewUtilisateur = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdUtilisateur = obj.IdUtilisateur;
            self.Login = obj.Login;
            self.Password = obj.Password;
            self.TimeSession = obj.TimeSession;
            self.Actif = obj.Actif;
            self.IsWindowAccount = obj.IsWindowAccount;
            self.CanApplySingleSession = obj.CanApplySingleSession;
            self.IsDeleted = obj.IsDeleted;
            self.DateMaj = obj.DateMaj;
            self.DateCreation = obj.DateCreation;
            self.CreatedBy = obj.CreatedBy;
            self.ModifiedBy = obj.ModifiedBy;
            self.Adresse = obj.Adresse;
            self.Email = obj.Email;
            self.Telephone = obj.Telephone;
            self.NomUtilisateur = obj.NomUtilisateur;
            self.Photo = obj.Photo;
            self.VoirCompteSensible = obj.VoirCompteSensible;
            self.IsOldUser = obj.IsOldUser;
            self.Nom = obj.Nom;
            self.Prenom = obj.Prenom;
            self.IsConnected = obj.IsConnected;
            self.ComptePermanent = obj.ComptePermanent;
            self.DateDebutValidite = obj.DateDebutValidite;
            self.DateFinValidite = obj.DateFinValidite;
            self.ChangeLogin = obj.ChangeLogin;
            self.ChangePassword = obj.ChangePassword;
            self.IdProfil = obj.IdProfil;
            self.IdStructure = obj.IdStructure;
            self.RecevoirMail = obj.RecevoirMail;
            self.RecevoirMailSysteme = obj.RecevoirMailSysteme;
            self.TentativeConnexion = obj.TentativeConnexion;
            self.DateDebutValiditePassword = obj.DateDebutValiditePassword;
            self.DateFinValiditePassword = obj.DateFinValiditePassword;
            self.UserSageCode = obj.UserSageCode;
        };

         mainSelf.ViewUtilisateurProfil = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdUtilisateurProfil = obj.IdUtilisateurProfil;
            self.IdUtilisateur = obj.IdUtilisateur;
            self.NomUtilisateur = obj.NomUtilisateur;
            self.IdProfil = obj.IdProfil;
            self.Intitule = obj.Intitule;
            self.DateValiditeDebut = obj.DateValiditeDebut;
            self.DateValiditeFin = obj.DateValiditeFin;
            self.DateFinIndeterminer = obj.DateFinIndeterminer;
            self.Autorise = obj.Autorise;
            self.IsDeleted = obj.IsDeleted;
            self.CreatedBy = obj.CreatedBy;
            self.ModifiedBy = obj.ModifiedBy;
            self.DateCreation = obj.DateCreation;
            self.DateMaj = obj.DateMaj;
        };

         mainSelf.ViewUtilisateurRestriction = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdUtilisateurRestriction = obj.IdUtilisateurRestriction;
            self.IdUtilisateur = obj.IdUtilisateur;
            self.Login = obj.Login;
            self.Nom = obj.Nom;
            self.Prenom = obj.Prenom;
            self.IdSysObjet = obj.IdSysObjet;
            self.TypeObjet = obj.TypeObjet;
            self.Code = obj.Code;
            self.Libelle = obj.Libelle;
            self.Description = obj.Description;
            self.Autorise = obj.Autorise;
            self.DateCreation = obj.DateCreation;
            self.CreatedBy = obj.CreatedBy;
        };

         mainSelf.Structure = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdStructure = obj.IdStructure;
            self.RaisonSocialeStructure = obj.RaisonSocialeStructure;
            self.DateCreation1 = obj.DateCreation1;
            self.DecretCreation = obj.DecretCreation;
            self.NumeroAgrement = obj.NumeroAgrement;
            self.IdDevise = obj.IdDevise;
            self.CapitalSocial = obj.CapitalSocial;
            self.IdPays = obj.IdPays;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
            self.ModifiedBy = obj.ModifiedBy;
            self.IsDeleted = obj.IsDeleted;
            self.CreatedBy = obj.CreatedBy;
        };

         mainSelf.Civilite = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdCivilite = obj.IdCivilite;
            self.Libelle = obj.Libelle;
            self.IsDeleted = obj.IsDeleted;
            self.CreatedBy = obj.CreatedBy;
            self.ModifiedBy = obj.ModifiedBy;
            self.DateCreation = obj.DateCreation;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
        };

         mainSelf.TypePieceIdentite = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdTypePieceIdentite = obj.IdTypePieceIdentite;
            self.Libelle = obj.Libelle;
            self.IsDeleted = obj.IsDeleted;
            self.CreatedBy = obj.CreatedBy;
            self.ModifiedBy = obj.ModifiedBy;
            self.DateCreation = obj.DateCreation;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
        };

         mainSelf.CategorieProcessu = function(obj) {

            var self = this;            
            
            obj = (commonUtilities.IsUndefinedOrNull(obj) ? new Object() : obj);

            self.IdCategorieProcessus = obj.IdCategorieProcessus;
            self.Libelle = obj.Libelle;
            self.IsDeleted = obj.IsDeleted;
            self.CreatedBy = obj.CreatedBy;
            self.ModifiedBy = obj.ModifiedBy;
            self.DateCreation = obj.DateCreation;
            self.DateMAJ = obj.DateMAJ;
            self.DataKey = obj.DataKey;
        };


*/
