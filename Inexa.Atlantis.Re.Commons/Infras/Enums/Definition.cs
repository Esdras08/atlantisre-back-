namespace Inexa.Atlantis.Re.Commons.Infras.Enums
{
    public class GlobalConstantes
    {
        public const byte MatriculeAssureLength = 11;

        public const string MsgErreurAuthentification = "Login/Mot de passe incorrect";
        public const string MsgErreurProfil =
            "Cet utilisateur n'a aucun profil valide. Veuillez contacter un administrateur pour plus d'information";
        public const string MsgErreurActif =
            "Votre compte utilisateur est désactivé. Veuillez contacter un administrateur pour plus d'information";

        public const string CustomException = @"INEXA-CUSTOM-EXCEPTION";
        public const string CustomRegleGestionException = @"INEXA-CUSTOM-REGLE-GESTION-EXCEPTION";
    }

    public enum Statut
    {
        Existe = 0,
        Supprime = 1
    }

    public class StatutConnexionEnum
    {
        public const string Fail = "Echec";
        public const string Succes = "Succes";
    }

    public class IdComplexiteMdp
    {
        public const short MdpLibre = 1;
        public const short MdpAlphaNum = 2;
        public const short MdpAlphaNumSpe = 3;
    }

    public enum StatutsFamilleEnum
    {
        IdAdherent = 1,
        IdConjoint = 2,
        IdEnfant = 3
    }

    public class StatutsPecEnum
    {
        public const string CREEE = "Créée";
        public const string TRAITEE = "Traitée";
        public const string VALIDEE = "Validée";
        public const string REJETEE = "Rejetée";
    }

    public class Processus
    {
        public const int CreerAdherent = 3;
        public const int CreerConjoint = 4;
        public const int CreerEnfant = 5;
        public const int LeverSuspensionAdherent = 1005;
        public const int LeverSuspensionConjoint = 1006;
        public const int LeverSuspensionEnfant = 1007;
        public const int CreerUnePecAuto = 1008;
        public const int CreerPrestation = 1009;
        public const int FairePrescription = 2008;
        public const int FaireOrientation = 2009;
    }

    public class PrestataireActionErreur
    {
        public const string CreerPec = "Ce prestataire ne peut pas creer une prise en charge";
        public const string CreerAffection = "Ce prestataire ne peut creer une affection";
        public const string RealiserPrestation = "Ce prestataire ne peut pas realiser une prestation";
        public const string FaireOrientation = "Ce prestataire ne peut pas faire une orientation";
        public const string ExecuterOrientation = "Ce prestataire ne peut pas executer une orientation";
        public const string FairePrescription = "Ce prestataire ne peut pas faire une prescription";
        public const string DispenserMedicament = "Ce prestataire ne peut pas dispenser un medicament";
    }

    public class RoleVariableEnvEnum
    {
        public const string VariableAControler = "Variable à contrôler";
        public const string VariableDeReference = "Variable de référence";
    }

    public class StatutsDetPecEnum
    {
        public const string CREEE = "Créée";
        public const string TRAITEE = "Traitée";
        public const string VALIDEE = "Validée";
        public const string REJETEE = "Exclue";
    }

    public class StatutsAccordPrealableEnum
    {
        public const string MISEATTENTE = "Mise en attente";
        public const string VALIDEE = "Validée";
        public const string REJETEE = "Rejetée";
    }

    public class StatutsFactureEnum
    {
        public const string CREEE = "Créée";
        public const string TRAITEE = "Traitée";
        public const string VALIDEE = "Validée";
        public const string REJETEE = "Rejetée";
        public const string REGLEE = "Réglée";
    }

    public class StatutsAssureEnum
    {
        public const string CREEE = "Créé";
        public const string EMIS = "Emis";
        public const string VALIDEE = "Validé";
        public const string SUSPENDU = "Suspendu";
        public const string RADIE = "Radié";
        public const string ARCHIVEE = "Archivé";
    }

    public class StatutsDemandeCarteEnum
    {
        public const string EMIS = "Emise";
        public const string VALIDEE = "Validée";
        public const string IMPRIMER = "Imprimée";
    }

    public static class GlobalEnum
    {
        public const string CustomException = @"INEXA-CUSTOM-EXCEPTION";
        public const string CustomRegleGestionException = @"INEXA-CUSTOM-REGLE-GESTION-EXCEPTION";
    }

    public class MySortOrder
    {
        public const string Ascending = "Ascending";
        public const string Descending = "Descending";
    }

    public class EntiteNameCodification
    {
        public const string Adherent = "Sa-Adherent";
        public const string AyantDroit = "Sa-Ayant droit";
        public const string Gestionnaire = "Sa-Gestionnaire";
        public const string Sam = "Sa-Sam";
        public const string Police = "Sa-Police";
        public const string Contrat = "Sa-Contrat";
        public const string Standing = "Sa-Standing";
        public const string Prestataire = "Sa-Prestataire";
        public const string Medecin = "Sa-Medecin";
        public const string ReseauxSoins = "Sa-Reseaux soins";
        public const string PriseEnCharge = "Sa-PEC";
        public const string Decompte = "Sa-Decompte";
        public const string Facture = "Sa-Facture";
    }

    public class OperatorEnum
    {
        public const string EQUAL = "EQUAL";
        public const string NOTEQUAL = "NOT EQUAL";
        public const string BETWEEN = "BETWEEN";
        public const string STARSTWITH = "STARTS WITH";
        public const string ENDSWITH = "ENDS WITH";
        public const string CONTAINS = "CONTAINS";
        public const string LESS = "LESS";
        public const string LESSOREQUAL = "LESS OR EQUAL";
        public const string MORE = "MORE";
        public const string MOREOREQUAL = "MORE OR EQUAL";
    }


    public class Sens
    {
        public const string DEBIT = "D";
        public const string CREDIT = "C";
    }

    public class EtatJournee
    {
        public const string Ouvert = "Ouvert";
        public const string Cloture = "Cloture";
    }

    public class ModeAmortissement
    {
        public const string INFINE = "IFN";
        public const string CONSTANTSURTITRE = "CTI";
        public const string CONSTANTSURCAPITAL = "CCA";
        public const string ANNUITESCONSTANTES = "ACS";
    }

    public class Periodicite
    {
        public const string MENSUELLE = "MEN";
        public const string TRIMESTRIELLE = "TRI";
        public const string SEMESTRIELLE = "SEM";
        public const string ANNUELLE = "ANN";
    }

    public class TypeReference
    {
        public const string TYPEDETITRE = "05";
        public const string TYPEDORDRE = "06";
        public const string NATUREDORDRE = "07";
        public const string TRANSFERT = "09";
        public const string MODEREGLEMENT = "13";
        public const string TYPEDETRANSACTION = "08";
        public const string ETATCOMPTE = "20";
        public const string DIVIDENDE = "21";
        public const string TYPEDEREMBOURSEMENT = "12";
    }

    public class CodeTypeMouvement
    {
        public const string AnnulationMouvementCredit = "ANC";
        public const string AnnulationMouvementDebit = "AND";
        public const string RetraitDeLiquidite = "RTL";
        public const string ApportDeLiquidite = "APL";
        public const string TransfertTitre = "TRT";
        public const string MouvementSystemeCredit = "SYC";
        public const string MouvementSystemeDebit = "SYD";
        public const string PaiementDividende = "PDV";
        public const string PaiementInterets = "PAI_INTERETS";
        public const string RemboursementCapital = "REMB_CAPITAL";
    }

    public class TypeTitre
    {
        public const string ACTIONS = "ACT";
        public const string BONS = "BON";
        public const string OBLIGATIONS = "OBL";
        public const string OPCVM = "OPC";
    }

    public class CodeTypeTitre
    {
        public const string ACTIONS = "ACT";
        public const string BONS = "BON";
        public const string OBLIGATIONS = "OBL";
        public const string OPCVM = "OPC";
    }

    public class TypeDordre
    {
        public const string AuMarche = "MAR";
        public const string CoursLimite = "CRL";
        public const string ToutOuRien = "TOR";
    }

    public class SensOrdre
    {
        public const string ACHAT = "ACH";
        public const string VENTE = "VET";
        public const string SOUSCRIPTION = "SOUSC";
    }
    public class EtatMouvement
    {
        public const string Comptabilise = "Comptabilisé";
        public const string Annule = "Annulé";
        public const string AucunEtat = "En Attente";
        public const string Valider = "Validé";
    }

    public class CodeTypeTransfert
    {
        public const string TransfertInterne = "TRI";
        public const string TransfertSortant = "TRES";
        public const string TransfertEntrant = "TREE";
    }

    public class EtatTransfert
    {
        public const string EnAttente = "En Attente";
        public const string Valide = "Validé";
        public const string Rejete = "Rejeté";
        public const string Annule = "Annulé";
    }

    public class ButtonsOrdreNames
    {
        public const string Ajourner = "Ajourner";
        public const string Annuler = "Annuler";
        public const string Executer = "Executer";
        public const string Forcer = "Forcer";
        public const string Nouveau = "Nouveau";
        public const string Rejeter = "Rejeter";
        public const string Valider = "Valider";
    }

    public static class Origine
    {
        public const string Ordre = "ORDRE";
        public const string Transaction = "TRANSACTION";
        public const string Transfert = "TRANSFERT";
        public const string Ost = "OST";
        public const string Fractionnement = "FRACTIONNEMENT";
        public const string Dividende = "DIVIDENDE";
        public const string Remboursement = "REMBOURSEMENTECHEANCE";
        public const string RemboursementBon = "REMBOURSEMENTBON";
        public const string Attribution = "ATTRIBUTION";
        public const string Mouvement = "MOUVEMENT";
    }


    public class TypeTransaction
    {
        public const string SystemeDebit = "SYS_DEBIT";
        public const string SystemeCredit = "SYS_CREDIT";
        public const string Debit = "DEBIT";
        public const string Credit = "CREDIT";
    }


    public class ModeReglement
    {
        public const string Espece = "Espèce";
        public const string Cheque = "Chèque";
        public const string Prelevement = "Prélèvement";
        public const string Virement = "Virement";

        public const string CodeEspece = "ESP";
    }


    public class EtatOrdre
    {
        public const string Nouveau = "Nouveau";
        public const string EnAttente = "En attente";
        public const string EnCours = "En cours";
        public const string PrisEnCompte = "Pris en compte";
        public const string Annule = "Annulé";
        public const string Cloture = "Cloturé";
        public const string Valide = "Validé";
        public const string Rejete = "Rejeté";
        public const string Ajourne = "Ajourné";
    }

    public class EtatCompte
    {
        public const string EN_ATTENTE = "00";
        public const string OPERATIONNEL = "01";
        public const string AJOURNE = "02";
        public const string REJETE = "03";
        public const string ENCOURS = "04";
        public const string SUSPENDU = "05";
        public const string CLOTURE = "06";
    }

    public class TempsHistorique
    {
        public const string AVANT = "AV";
        public const string APRES = "AP";
    }


    public class EtatOst
    {
        public const string Nouveau = "Nouveau";
        public const string EnAttente = "En Attente";
        public const string Valide = "Validé";
        public const string Rejete = "Rejeté";
        public const string Effectue = "Effectué";
        public const string Annule = "Annulé";

        public const string Annonce = "Annoncé";
        public const string Paye = "Payé";
    }

    public class TypeDividendes
    {
        public const string DividendeNouveau = "DIVIDENTE_NV";
        public const string DividendeAnnonce = "DIVIDENTE_AN";
        public const string DividendeValider = "DIVIDENTE_VAL";
    }

    public class TypeRemboursement
    {
        public const string CapitalUniquement = "PAIE_CAPITAL";
        public const string CapitalEtInterets = "CAPITAL_INTERET";
        public const string InteretsUniquement = "PAIE_INTERET";
    }

    public class CodeTypeTiers
    {
        public const string Mandataire = "Mandataire";
        public const string Beneficiaire = "Bénéficiaire";
        public const string Usufruitier = "Usufruitier";
    }

    public class EtatValidationEtude
    {
        public const string Rejete = "Rejeté";
        public const string Ajourne = "Ajourné";
        public const string Accepte = "Accepté";
        public const string EnAttente = "En Attente";
    }

    public class CodeTypeCircuit
    {
        public const string OuvertureCompte = "6";
    }

    public class CodeTypeCompte
    {
        public const string Individuel = "Individuel";
        public const string Indivision = "Indivision";
        public const string Joint = "Joint";
        public const string MineursMajeurs = "MinMaj";
        public const string NuePropUsuf = "Usuf";
        public const string Conservation = "ConservExec";
    }

    public class CodeTypeActionFacturation
    {
        public const string EnConsultation = "CONSULTATION";
        public const string EnFacturation = "FACTURATION";
    }

    public class EtatFacturation
    {
        public const string NonFacture = "Non facturé";
        public const string Facture = "Facturé";
    }

    public class TypeFacturation
    {
        public const string DroitsGarde = "DROIT_DE_GARDE";
        public const string GestionSousMandat = "FRAIS_GESTION_SOUS_MANDAT";
        public const string ComDepositaire = "COM_DEPOSITAIRE";
        public const string ComRegulateur = "COM_REGULATEUR";
    }

    public  class GenerationCode
    {
        public const string Compte = "CPT";
        public const string Ordre = "ORD";
        public const string Transaction = "TRA";
        public const string Mouvement = "MVT";
        public const string Transfert = "TSF";
        public const string RemboursementEcheance = "REC";
        public const string RemboursementBon = "REB";
    }

    public class CheckTransaction
    {
        public const string OUI = "OUI";
        public const string NON = "NON";
    }

    public class EtatMail
    {
        public const string Echec = "Echec";
        public const string EnCours = "En Cours";
    }

    public static class CodeOperation
    {
        public const string Save = "SAVE";
        public const string Ajouter = "AJOUTER";
        public const string Modifier = "MODIFIER";
        public const string Supprimer = "SUPPRIMER";
        public const string Annuler = "ANNULER";
        public const string Valider = "VALIDER";
        public const string Report = "REPORT";
        public const string Actualiser = "ACTUALISER";
        public const string Forcer = "FORCER";
        public const string AValider = "A_VALIDER";
    }

    public static class CodeFormulaire
    {
        public const string MouvementEspeces = "MOUVEMENT_ESPECES";
        public const string SaisieOrdre = "SAISIE_ORDRE";
        public const string ExecutionOrdre = "EXECUTION_ORDRE";
    }

    public static class CodeTypeOperation
    {
        public const string RetraitDeLiquidite = "RTL";
        public const string ApportDeLiquidite = "APL";
        public const string AchatTitre = "ACH";
        public const string VenteTitre = "VET";
        public const string ValidationMouvement = "VME";
        public const string AnnulationMouvement = "ANM";
        public const string TransfertTitre = "TRT";
        public const string MouvementSystemeCredit = "SYC";
        public const string MouvementSystemeDebit = "SYD";
        public const string PaiementDividende = "PDV";
        public const string PaiementInterets = "PIN";
        public const string RemboursementEcheance = "REC";
    }

    public static class Operation
    {
        public const string Save = "SAVE";
        public const string Valider = "VALIDER";
        public const string Actualiser = "ACTUALISER";
        public const string Annuler = "ANNULER";
        public const string Activer = "ACTIVER";
        public const string Lever = "LEVER";
    }
}
