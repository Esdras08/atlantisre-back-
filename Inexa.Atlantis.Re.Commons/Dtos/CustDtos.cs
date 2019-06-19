using System;
using System.Collections.Generic;
using System.Linq;

using Inexa.Atlantis.Re.Commons.Infras.Domains;


namespace Inexa.Atlantis.Re.Commons.Dtos
{
    public partial class ProfilDto
    {
        public List<FonctionnaliteDto> Fonctionnalites { get; set; }
    }

    public partial class FonctionnaliteDto
    {
        public bool Autorise { get; set; }
      //  public long CreatedBy { get; set; }
    }

    public partial class ViewUtilisateurDto
    {
        public List<UtilisateurProfilDto> Profils { get; set; }
        public List<UtilisateurFonctionnaliteDto> Fonctionnalites { get; set; }
        // public List<UtilisateurRestrictionDto> Restrictions { get; set; }
        public List<ViewUtilisateurRestrictionDto> Restrictions { get; set; }
        public string LogPassword { get; set; }
        public bool MustChangePassword { get; set; }
        public bool PasswordMustBeChanged { get; set; }

        //public JourneeDto CurrentSeance { get; set; }

        public string ChangerMotDePasse { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public partial class UtilisateurDto
    {
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
        public string LogPassword { get; set; }

        public string NewLogin { get; set; }

        public bool CanResetPassword { get; set; }

        public bool PasswordMustBeChanged { get; set; }
        public bool MustChangePassword { get; set; }
        public List<UtilisateurProfilDto> Profils { get; set; }
        public List<UtilisateurFonctionnaliteDto> Fonctionnalites { get; set; }
        public List<ViewUtilisateurRestrictionDto> Restrictions { get; set; }
        //public JourneeDto CurrentSeance { get; set; }

        public string ChangerMotDePasse { get; set; }
        public string ConfirmPassword { get; set; }

    }

    public partial class CriteresRechercheDto : DtoBase
    {
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public DateTime DateOperation { get; set; }
        public long IdTitre { get; set; }
        public long IdTypeTitre { get; set; }
        public long IdTypeClient { get; set; }
        public string CodeSensOrdre { get; set; }


    }

  
    public partial class ProfilFonctionnaliteDto : DtoBase
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Libelle { get; set; }
        public string CodeParent { get; set; }
        public string CodeModule { get; set; }
        public string Etat { get; set; }
        public long CreatedBy { get; set; }
    }
  

    public partial class MailBoxDto
    {
        public string NumeroCompte { get; set; }
        public string LibelleCompte { get; set; }
    }

    public partial class MailToSendDto
    {
        public List<MailBoxDto> oMailBoxDtoList { get; set; }
    }

    public class FichierImporteDto : DtoBase
    {
        public long IdFichierImporte { get; set; }
        public string Entite { get; set; }
        public long IdEntite { get; set; }
        public string Extension { get; set; }
        public string PieceJointe { get; set; }
        public string NomFichier { get; set; }
        public long CreatedBy { get; set; }
        public DateTime DateCreation { get; set; }
    }


    public partial class ParametreDto
    {
        public bool Coche { get; set; }

    }


}