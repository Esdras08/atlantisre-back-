using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Inexa.Atlantis.Re.Commons.Infras.Domains;
using Inexa.Atlantis.Re.Commons.Dtos;

namespace Inexa.Atlantis.Re.Dal.SpProvider
{
    public class AdminProvider
    {
        private static AdminProvider _instance;

        public static AdminProvider Instance
        {
            get { return _instance ?? (_instance = new AdminProvider()); }
        }

        private AdminProvider()
        {
        }

        public BusinessResponse<UtilisateurDto> UtilisateurSave(UtilisateurDto oObjectDto)
        {
            DbManager DbManager = new DbManager();
            var response = new BusinessResponse<UtilisateurDto>();
            try
            {
                var parameter = new DynamicParameters();
                if (oObjectDto.IdUtilisateur == 0)
                {
                    oObjectDto.IsDeleted = false;
                    //oObjectDto.IsWindowAccount = false;
                   // oObjectDto.IsConnected = "Non";
                }

                parameter.Add("@IdUtilisateur", oObjectDto.IdUtilisateur, DbType.Int64, ParameterDirection.InputOutput);
                parameter.Add("@Login", oObjectDto.Login);
                parameter.Add("@Password", oObjectDto.Password);
                parameter.Add("@Nom", oObjectDto.Nom);
                parameter.Add("@Prenom", oObjectDto.Prenom);
                parameter.Add("@Email", oObjectDto.Email);
               // parameter.Add("@Telephone", oObjectDto.Telephone);
                parameter.Add("@Adresse", oObjectDto.Adresse);
               // parameter.Add("@Photo", oObjectDto.Photo);
                //parameter.Add("@VoirCompteSensible", oObjectDto.VoirCompteSensible);
                //parameter.Add("@ChangeLogin", oObjectDto.ChangeLogin);
               // parameter.Add("@ChangePassword", oObjectDto.ChangePassword);  //oObjectDto.CanResetPassword);
               // parameter.Add("@TimeSession", oObjectDto.TimeSession);
               // parameter.Add("@Actif", oObjectDto.Actif);
              //  parameter.Add("@IsConnected", oObjectDto.IsConnected);
              //  parameter.Add("@ComptePermanent", oObjectDto.ComptePermanent);
               // parameter.Add("@DateDebutValidite", oObjectDto.DateDebutValidite);
              //  parameter.Add("@DateFinValidite", oObjectDto.DateFinValidite);
              //  parameter.Add("@CanApplySingleSession", oObjectDto.CanApplySingleSession);
                parameter.Add("@IdProfil", oObjectDto.IdProfil);
              //  parameter.Add("@IsOldUser", oObjectDto.IsOldUser);
               // parameter.Add("@IsWindowAccount", oObjectDto.IsWindowAccount);
              //  parameter.Add("@RecevoirMail", oObjectDto.RecevoirMail);
               // parameter.Add("@RecevoirMailSysteme", oObjectDto.RecevoirMailSysteme);
                parameter.Add("@IsDeleted", oObjectDto.IsDeleted);
                parameter.Add("@CreatedBy", oObjectDto.CreatedBy);

                DbManager.OpenConnection();
                DbManager.DbTransaction = DbManager.DbConnection.BeginTransaction();
                DbManager.DbConnection.Execute("sp_UtilisateurSave", parameter, commandType: CommandType.StoredProcedure, transaction: DbManager.DbTransaction);

                oObjectDto.IdUtilisateur = parameter.Get<long>("@IdUtilisateur");

                if (oObjectDto.Profils == null)
                {
                    oObjectDto.Profils = new List<UtilisateurProfilDto>();
                    if (oObjectDto.IdProfil != null)
                        oObjectDto.Profils.Add(new UtilisateurProfilDto() {IdProfil = (int)oObjectDto.IdProfil ,IdUtilisateur = (long)(oObjectDto.IdUtilisateur) });
                }
                UtilisateurProfilSave(oObjectDto, DbManager);

                if (oObjectDto.Fonctionnalites != null)
                    UtilisateurFonctionnaliteSave(oObjectDto, DbManager);

                if (oObjectDto.Restrictions != null)
                    UtilisateurRestrictionSave(oObjectDto, DbManager);

                DbManager.DbTransaction.Commit();

                response.Items.Add(oObjectDto);

                return response;
            }
            catch (SqlException ex)
            {
                DbManager.DbTransaction.Rollback();
                throw new Exception(ex.Message);
            }
            finally
            {
                DbManager.CloseConnection();
            }
        }

        public BusinessResponse<UtilisateurDto> UtilisateurChangePass(UtilisateurDto oObjectDto)
        {
            DbManager DbManager = new DbManager();
            var response = new BusinessResponse<UtilisateurDto>();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("@IdUtilisateur", oObjectDto.IdUtilisateur, DbType.Int64, ParameterDirection.InputOutput);
                parameter.Add("@Login", oObjectDto.Login);
                parameter.Add("@Password ", oObjectDto.Password);
               // parameter.Add("@IsOldUser", oObjectDto.IsOldUser);
               // parameter.Add("@ChangeLogin", oObjectDto.ChangeLogin);
                parameter.Add("@CreatedBy", oObjectDto.CreatedBy);

                DbManager.OpenConnection();
                DbManager.DbTransaction = DbManager.DbConnection.BeginTransaction();
                DbManager.DbConnection.Execute("sp_UtilisateurChangePass", parameter, commandType: CommandType.StoredProcedure, transaction: DbManager.DbTransaction);

                oObjectDto.IdUtilisateur = parameter.Get<long>("@IdUtilisateur");
                DbManager.DbTransaction.Commit();

                response.Items.Add(oObjectDto);

                return response;
            }
            catch (SqlException ex)
            {
                DbManager.DbTransaction.Rollback();
                throw new Exception(ex.Message);
            }
            finally
            {
                DbManager.CloseConnection();
            }
        }

        public void UtilisateurProfilSave(UtilisateurDto oUtilisateurDto, DbManager DbManager)
        {
            var parameterList = new List<DynamicParameters>();
            try
            {
                foreach (var oObjectDto in oUtilisateurDto.Profils)
                {
                    if (oObjectDto.Autorise == true)
                    {
                        var parameter = new DynamicParameters();
                        parameter.Add("@IdUtilisateurProfil", 0);
                        parameter.Add("@IdUtilisateur", oUtilisateurDto.IdUtilisateur);
                        parameter.Add("@IdProfil", oObjectDto.IdProfil);
                        parameter.Add("@DateValiditeDebut", oObjectDto.DateValiditeDebut);
                        parameter.Add("@DateValiditeFin", oObjectDto.DateValiditeFin);
                        parameter.Add("@DateFinIndeterminer ", oObjectDto.DateFinIndeterminer);
                        parameter.Add("@Autorise", oObjectDto.Autorise);
                        parameter.Add("@IsDeleted", oUtilisateurDto.IsDeleted);
                        parameter.Add("@CreatedBy", oUtilisateurDto.CreatedBy);
                        parameterList.Add(parameter);
                    }
                }

                if(parameterList.Count>0)
                    DbManager.DbConnection.Execute("sp_UtilisateurProfilSave", parameterList, commandType: CommandType.StoredProcedure, transaction: DbManager.DbTransaction);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UtilisateurFonctionnaliteSave(UtilisateurDto oUtilisateurDto, DbManager DbManager)
        {
            var parameterList = new List<DynamicParameters>();
            try
            {
                foreach (var oObjectDto in oUtilisateurDto.Fonctionnalites)
                {
                    var parameter = new DynamicParameters();
                    parameter.Add("@IdUtilisateurFonctionnalite", 0);
                    parameter.Add("@IdUtilisateur", oUtilisateurDto.IdUtilisateur);
                    parameter.Add("@CodeFonctionnalite ", oObjectDto.CodeFonctionnalite);
                    parameter.Add("@Autorise", oObjectDto.Autorise);
                    parameter.Add("@CreatedBy", oUtilisateurDto.CreatedBy);
                    parameterList.Add(parameter);
                }
                DbManager.DbConnection.Execute("sp_UtilisateurFonctionnaliteSave", parameterList, commandType: CommandType.StoredProcedure, transaction: DbManager.DbTransaction);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {

            }
        }

        public BusinessResponse<ViewUtilisateurDto> UtilisateurGet(UtilisateurDto oObjectDto)
        {
            DbManager DbManager = new DbManager();
            var response = new BusinessResponse<ViewUtilisateurDto>();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("@Login", oObjectDto.Login);
                parameter.Add("@Password ", oObjectDto.Password);
                parameter.Add("@IdUtilisateur", oObjectDto.IdUtilisateur);
                parameter.Add("@CreatedBy", oObjectDto.CreatedBy);
                parameter.Add("@Offset", oObjectDto.Offset);
                parameter.Add("@Row", oObjectDto.Rows);

                DbManager.OpenConnection();
                var oObjectList = DbManager.DbConnection.Query<ViewUtilisateurDto>("sp_UtilisateurGet", parameter, commandType: CommandType.StoredProcedure).AsList();

                //var CurrentSeance = JourneeGet("LAST", oObjectDto.CreatedBy, DbManager);

                if (oObjectList.Count > 0)
                {
                    foreach (var oUtilisateur in oObjectList)
                    {
                        UtilisateurDto oUser = new UtilisateurDto
                        {
                            IdUtilisateur = oUtilisateur.IdUtilisateur,
                            CreatedBy = oUtilisateur.CreatedBy
                        };
                        var oProfils = UtilisateurProfilGet(oUser, DbManager);
                        var oFonctionnalites = UtilisateurFonctionnaliteGet(oUser, DbManager);
                        var oRestrictions = UtilisateurRestrictionGet(oUser, DbManager).Where(e=> e.Autorise == "Non").AsList();

                        oUtilisateur.Profils = oProfils;
                        oUtilisateur.Fonctionnalites = oFonctionnalites;
                        oUtilisateur.Restrictions = oRestrictions;
                        //oUtilisateur.CurrentSeance = CurrentSeance;

                        if ((bool)oUtilisateur.IsOldUser)
                            oObjectDto.PasswordMustBeChanged = oObjectDto.MustChangePassword = true;
                    }
                }

                response.Items = oObjectList;

                if (oObjectList.Count > 0)
                {
                    response.Count = oObjectList[0].RowsCount;
                    response.IndexDebut = oObjectDto.Offset;
                    response.IndexFin = response.IndexDebut + oObjectList.Count - 1;
                }

                return response;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                DbManager.CloseConnection();
            }
        }

        public BusinessResponse<ViewUtilisateurDto> UtilisateurGetAll(ViewUtilisateurDto oObjectDto)
        {
            DbManager DbManager = new DbManager();
            var response = new BusinessResponse<ViewUtilisateurDto>();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("@IdUtilisateur", oObjectDto.IdUtilisateur);
                parameter.Add("@Login", oObjectDto.Login);
                parameter.Add("@Password ", oObjectDto.Password);
                parameter.Add("@Nom", oObjectDto.Nom);
                parameter.Add("@Prenom", oObjectDto.Prenom);
                parameter.Add("@NomUtilisateur ", oObjectDto.NomUtilisateur);
                parameter.Add("@Email", oObjectDto.Email);
                parameter.Add("@Telephone", oObjectDto.Telephone);
                parameter.Add("@Adresse", oObjectDto.Adresse);
                parameter.Add("@ComptePermanent", oObjectDto.ComptePermanent);
                parameter.Add("@IsConnected ", oObjectDto.IsConnected);
                parameter.Add("@Actif", oObjectDto.Actif);
                parameter.Add("@VoirCompteSensible", oObjectDto.VoirCompteSensible);                
                //parameter.Add("@DateDebutValidite", oObjectDto.DateDebutValidite);
                //parameter.Add("@DateFinValidite", oObjectDto.DateFinValidite);                
                parameter.Add("@CreatedBy ", oObjectDto.CreatedBy);
                parameter.Add("@Offset", oObjectDto.Offset);
                parameter.Add("@Row", oObjectDto.Rows);

                DbManager.OpenConnection();
                var oObjectList = DbManager.DbConnection.Query<ViewUtilisateurDto>("sp_UtilisateurGetAll", parameter, commandType: CommandType.StoredProcedure).AsList();

                //var CurrentSeance = JourneeGet("LAST", oObjectDto.CreatedBy, DbManager);

                if (oObjectList.Count > 0)
                {
                    foreach (var oUtilisateur in oObjectList)
                    {
                        UtilisateurDto oUser = new UtilisateurDto
                        {
                            IdUtilisateur = oUtilisateur.IdUtilisateur,
                            CreatedBy = oUtilisateur.CreatedBy
                        };
                        var oProfils = UtilisateurProfilGet(oUser, DbManager);
                        var oFonctionnalites = UtilisateurFonctionnaliteGet(oUser, DbManager);
                        var oRestrictions = UtilisateurRestrictionGet(oUser, DbManager).Where(e => e.Autorise == "Non").AsList(); ;

                        oUtilisateur.Profils = oProfils;
                        oUtilisateur.Fonctionnalites = oFonctionnalites;
                        oUtilisateur.Restrictions = oRestrictions;
                        //oUtilisateur.CurrentSeance = CurrentSeance;

                        if ((bool)oUtilisateur.IsOldUser)
                            oObjectDto.PasswordMustBeChanged = oObjectDto.MustChangePassword = true;
                    }
                }

                response.Items = oObjectList;

                if (oObjectList.Count > 0)
                {
                    response.Count = oObjectList[0].RowsCount;
                    response.IndexDebut = oObjectDto.Offset;
                    response.IndexFin = response.IndexDebut + oObjectList.Count - 1;
                }

                return response;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                DbManager.CloseConnection();
            }
        }
        public List<UtilisateurProfilDto> UtilisateurProfilGet(UtilisateurDto oObjectDto, DbManager DbManager)
        {
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("@IdUtilisateur", oObjectDto.IdUtilisateur);

                var oObjectList = DbManager.DbConnection.Query<UtilisateurProfilDto>("sp_UtilisateurProfilGet", parameter, commandType: CommandType.StoredProcedure).AsList();

                return oObjectList;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
            }
        }

        public BusinessResponse<UtilisateurProfilDto> UtilisateurProfilGet(UtilisateurDto oObjectDto)
        {
            DbManager DbManager = new DbManager();
            var response = new BusinessResponse<UtilisateurProfilDto>();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("@IdUtilisateur", oObjectDto.IdUtilisateur);

                DbManager.OpenConnection();
                var oObjectList = DbManager.DbConnection.Query<UtilisateurProfilDto>("sp_UtilisateurProfilGet", parameter, commandType: CommandType.StoredProcedure).AsList();
                response.Items = oObjectList;

                return response;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                DbManager.CloseConnection();
            }
        }

        public BusinessResponse<ViewUtilisateurProfilDto> UtilisateurProfilGet(long IdUtilisateur)
        {
            DbManager DbManager = new DbManager();
            var response = new BusinessResponse<ViewUtilisateurProfilDto>();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("@IdUtilisateur", IdUtilisateur);

                DbManager.OpenConnection();
                var oObjectList = DbManager.DbConnection.Query<ViewUtilisateurProfilDto>("sp_UtilisateurProfilGetById", parameter, commandType: CommandType.StoredProcedure).AsList();
                response.Items = oObjectList;

                return response;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                DbManager.CloseConnection();
            }
        }
        public BusinessResponse<ViewUtilisateurProfilDto> UtilisateurProfilGet(ViewUtilisateurProfilDto oObjectDto)
        {
            DbManager DbManager = new DbManager();
            var response = new BusinessResponse<ViewUtilisateurProfilDto>();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("@IdUtilisateur", oObjectDto.IdUtilisateur);

                DbManager.OpenConnection();
                var oObjectList = DbManager.DbConnection.Query<ViewUtilisateurProfilDto>("sp_UtilisateurProfilGet", parameter, commandType: CommandType.StoredProcedure).AsList();
                response.Items = oObjectList;

                if (oObjectList.Count > 0)
                {
                    response.Count = oObjectList[0].RowsCount;
                    response.IndexDebut = oObjectDto.Offset;
                    response.IndexFin = response.IndexDebut + oObjectList.Count - 1;
                }

                return response;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                DbManager.CloseConnection();
            }
        }

        public List<UtilisateurFonctionnaliteDto> UtilisateurFonctionnaliteGet(UtilisateurDto oObjectDto, DbManager DbManager)
        {
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("@IdUtilisateur", oObjectDto.IdUtilisateur);
                parameter.Add("@CreatedBy", oObjectDto.CreatedBy);

                var oObjectList = DbManager.DbConnection.Query<UtilisateurFonctionnaliteDto>("sp_UtilisateurFonctionnaliteGet", parameter, commandType: CommandType.StoredProcedure).AsList();

                return oObjectList;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
            }
        }

        public BusinessResponse<UtilisateurFonctionnaliteDto> UtilisateurFonctionnaliteGet(UtilisateurDto oObjectDto)
        {
            DbManager DbManager = new DbManager();
            var response = new BusinessResponse<UtilisateurFonctionnaliteDto>();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("@IdUtilisateur", oObjectDto.IdUtilisateur);
                parameter.Add("@CreatedBy", oObjectDto.CreatedBy);

                DbManager.OpenConnection();
                var oObjectList = DbManager.DbConnection.Query<UtilisateurFonctionnaliteDto>("sp_UtilisateurFonctionnaliteGet", parameter, commandType: CommandType.StoredProcedure).AsList();
                response.Items = oObjectList;

                return response;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                DbManager.CloseConnection();
            }
        }

        public List<ViewUtilisateurRestrictionDto> UtilisateurRestrictionGet(UtilisateurDto oObjectDto, DbManager DbManager)
        {
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("@IdUtilisateur", oObjectDto.IdUtilisateur);
                parameter.Add("@CreatedBy", oObjectDto.CreatedBy);

                var oObjectList = DbManager.DbConnection.Query<ViewUtilisateurRestrictionDto>("sp_UtilisateurRestrictionGet", parameter, commandType: CommandType.StoredProcedure).AsList();

                return oObjectList;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
            }
        }

        public BusinessResponse<ViewUtilisateurRestrictionDto> UtilisateurRestrictionGet(ViewUtilisateurRestrictionDto oObjectDto)
        {
            DbManager DbManager = new DbManager();
            var response = new BusinessResponse<ViewUtilisateurRestrictionDto>();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("@IdUtilisateur", oObjectDto.IdUtilisateur);
                parameter.Add("@CreatedBy", oObjectDto.CreatedBy);

                DbManager.OpenConnection();
                var oObjectList = DbManager.DbConnection.Query<ViewUtilisateurRestrictionDto>("sp_UtilisateurRestrictionGet", parameter, commandType: CommandType.StoredProcedure).AsList();
                response.Items = oObjectList;

                return response;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                DbManager.CloseConnection();
            }
        }

        public void UtilisateurRestrictionSave(UtilisateurDto oUtilisateurDto, DbManager DbManager)
        {
            var parameterList = new List<DynamicParameters>();
            var parameter = new DynamicParameters();
            try
            {
                foreach (var oDto in oUtilisateurDto.Restrictions)
                {
                    if (oDto.Autorise == "Non")
                    {
                        parameter = new DynamicParameters();
                        parameter.Add("@IdUtilisateurRestriction", 0);
                        parameter.Add("@IdUtilisateur", oUtilisateurDto.IdUtilisateur);
                        parameter.Add("@IdSysObjet ", oDto.IdSysObjet);
                        parameter.Add("@CreatedBy", oUtilisateurDto.CreatedBy);
                        parameterList.Add(parameter);
                    }
                }
                if (parameterList.Count > 0)
                {
                    //DbManager.DbConnection.Execute("INSERT INTO UtilisateurRestrictionHisto SELECT * FROM UtilisateurRestriction WHERE IdUtilisateur=@IdUtilisateur", new { IdUtilisateur = oUtilisateurDto.IdUtilisateur }, commandType: CommandType.Text, transaction: DbManager.DbTransaction);
                    //DbManager.DbConnection.Execute("DELETE FROM UtilisateurRestriction WHERE IdUtilisateur=@IdUtilisateur", new { IdUtilisateur = oUtilisateurDto.IdUtilisateur }, commandType: CommandType.Text, transaction: DbManager.DbTransaction);
                    parameter = new DynamicParameters();
                    parameter.Add("@IdUtilisateur", oUtilisateurDto.IdUtilisateur);
                    parameter.Add("@CreatedBy", oUtilisateurDto.CreatedBy);
                    DbManager.DbConnection.Execute("sp_UtilisateurRestrictionHistoSave", parameter, commandType: CommandType.StoredProcedure, transaction: DbManager.DbTransaction);
                    DbManager.DbConnection.Execute("sp_UtilisateurRestrictionSave", parameterList, commandType: CommandType.StoredProcedure, transaction: DbManager.DbTransaction);
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {

            }
        }

        public void UtilisateurFonctionnaliteUpdate(ProfilDto oProfilDto, DbManager DbManager)
        {
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("@IdProfil", oProfilDto.IdProfil);
                parameter.Add("@CreatedBy", oProfilDto.CreatedBy);
                DbManager.DbConnection.Execute("sp_UtilisateurFonctionnaliteUpdate", parameter, commandType: CommandType.StoredProcedure, transaction: DbManager.DbTransaction);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {

            }
        }

        public BusinessResponse<ProfilDto> ProfilSave(ProfilDto oObjectDto)
        {
            DbManager DbManager = new DbManager();
            var response = new BusinessResponse<ProfilDto>();
            try
            {
                var parameter = new DynamicParameters();
                if (oObjectDto.IdProfil == 0)
                {
                    oObjectDto.IsDeleted = false;
                }
                parameter.Add("@IdProfil", oObjectDto.IdProfil, DbType.Int32, ParameterDirection.InputOutput);
                //parameter.Add("@Intitule", oObjectDto.Intitule);
              //  parameter.Add("@Description ", oObjectDto.Description);
                parameter.Add("@IsDeleted", oObjectDto.IsDeleted);
                parameter.Add("@CreatedBy", oObjectDto.CreatedBy);

                DbManager.OpenConnection();
                DbManager.DbTransaction = DbManager.DbConnection.BeginTransaction();
                DbManager.DbConnection.Execute("sp_ProfilSave", parameter, commandType: CommandType.StoredProcedure, transaction: DbManager.DbTransaction);

                oObjectDto.IdProfil = parameter.Get<int>("@IdProfil");

                if (oObjectDto.Fonctionnalites != null)
                {
                    ProfilFonctionnaliteSave(oObjectDto, DbManager);
                    UtilisateurFonctionnaliteUpdate(oObjectDto, DbManager);
                }

                DbManager.DbTransaction.Commit();

                response.Items.Add(oObjectDto);

                return response;
            }
            catch (SqlException ex)
            {
                DbManager.DbTransaction.Rollback();
                throw new Exception(ex.Message);
            }
            finally
            {
                DbManager.CloseConnection();
            }
        }

        public void ProfilFonctionnaliteSave(ProfilDto oProfilDto, DbManager DbManager)
        {
            var parameterList = new List<DynamicParameters>();
            try
            {
                foreach (var oObjectDto in oProfilDto.Fonctionnalites)
                {
                    if (oObjectDto.Autorise)
                    {
                        var parameter = new DynamicParameters();
                        parameter.Add("@IdProfilFonctionnalite", 0);
                        parameter.Add("@IdProfil", oProfilDto.IdProfil);
                        parameter.Add("@CodeFonctionnalite ", oObjectDto.Code);
                        parameter.Add("@Autorise", oObjectDto.Autorise);
                       // parameter.Add("@CreatedBy", oObjectDto.CreatedBy);
                        parameterList.Add(parameter);
                    }
                }
                if (parameterList.Count > 0)
                {
                    DbManager.DbConnection.Execute("DELETE FROM ProfilFonctionnalite WHERE IdProfil=@IdProfil", new { IdProfil = oProfilDto.IdProfil }, commandType: CommandType.Text, transaction: DbManager.DbTransaction);
                    DbManager.DbConnection.Execute("sp_ProfilFonctionnaliteSave", parameterList, commandType: CommandType.StoredProcedure, transaction: DbManager.DbTransaction);
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {

            }
        }

        public void ProfilFonctionnaliteSave(List<ProfilFonctionnaliteDto> oObjectDtoList, long CreatedBy)
        {
            DbManager DbManager = new DbManager();
            var parameterList = new List<DynamicParameters>();
            try
            {
                var profilFonctionnaliteDto = oObjectDtoList.FirstOrDefault();
                if (profilFonctionnaliteDto != null)
                {
                    var idProfil = profilFonctionnaliteDto.IdProfil;
                    foreach (var oObjectDto in oObjectDtoList)
                    {
                        if (oObjectDto.Autorise)
                        {
                            var parameter = new DynamicParameters();
                            parameter.Add("@IdProfilFonctionnalite", 0);
                            parameter.Add("@IdProfil", oObjectDto.IdProfil);
                            parameter.Add("@CodeFonctionnalite", oObjectDto.Code);
                            parameter.Add("@Autorise", oObjectDto.Autorise);
                            parameter.Add("@CreatedBy", CreatedBy);
                            parameterList.Add(parameter);
                        }
                    }
                    if (parameterList.Count > 0)
                    {
                        DbManager.OpenConnection();
                        DbManager.DbTransaction = DbManager.DbConnection.BeginTransaction();
                        DbManager.DbConnection.Execute("DELETE FROM ProfilFonctionnalite WHERE IdProfil=@IdProfil", new { IdProfil = idProfil }, commandType: CommandType.Text, transaction: DbManager.DbTransaction);
                        DbManager.DbConnection.Execute("sp_ProfilFonctionnaliteSave", parameterList, commandType: CommandType.StoredProcedure, transaction: DbManager.DbTransaction);
                        DbManager.DbTransaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                DbManager.DbTransaction.Rollback();
                throw new Exception(ex.Message);
            }
            finally
            {
                DbManager.CloseConnection();
            }
        }

        public BusinessResponse<ProfilDto> ProfilGet(ProfilDto oObjectDto)
        {
            DbManager DbManager = new DbManager();
            var response = new BusinessResponse<ProfilDto>();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("@IdProfil", oObjectDto.IdProfil);
               // parameter.Add("@CodeProfil", oObjectDto.CodeProfil);
               // parameter.Add("@Intitule", oObjectDto.Intitule);
               // parameter.Add("@Description", oObjectDto.Description);
                parameter.Add("@CreatedBy", oObjectDto.CreatedBy);
                parameter.Add("@Offset", oObjectDto.Offset);
                parameter.Add("@Row", oObjectDto.Rows);

                DbManager.OpenConnection();
                var oObjectList = DbManager.DbConnection.Query<ProfilDto>("sp_ProfilGet", parameter, commandType: CommandType.StoredProcedure).AsList();

                //if (oObjectList.Count > 0)
                //{
                //    foreach (var oProfil in oObjectList)
                //    {
                //        var oFonctionnalites = ProfilFonctionnaliteGet(oProfil, DbManager);
                //        oProfil.Fonctionnalites = oFonctionnalites;
                //    }
                //}

                response.Items = oObjectList;

                if (oObjectList.Count > 0)
                {
                    response.Count = oObjectList[0].RowsCount;
                    response.IndexDebut = oObjectDto.Offset;
                    response.IndexFin = response.IndexDebut + oObjectList.Count - 1;
                }

                return response;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                DbManager.CloseConnection();
            }
        }
        
        public List<FonctionnaliteDto> ProfilFonctionnaliteGet(ProfilDto oObjectDto, DbManager DbManager)
        {
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("@IdProfil", oObjectDto.IdProfil);
                parameter.Add("@CreatedBy", oObjectDto.CreatedBy);

                var oObjectList = DbManager.DbConnection.Query<FonctionnaliteDto>("sp_ProfilFonctionnaliteGet", parameter, commandType: CommandType.StoredProcedure).AsList();

                return oObjectList;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public BusinessResponse<FonctionnaliteDto> ProfilFonctionnaliteGet(ProfilDto oObjectDto)
        {
            DbManager DbManager = new DbManager();
            var response = new BusinessResponse<FonctionnaliteDto>();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("@IdProfil", oObjectDto.IdProfil);
                parameter.Add("@CreatedBy", oObjectDto.CreatedBy);
                //parameter.Add("@Offset", oObjectDto.Offset);
                //parameter.Add("@Row", oObjectDto.Rows);

                DbManager.OpenConnection();
                var oObjectList = DbManager.DbConnection.Query<FonctionnaliteDto>("sp_ProfilFonctionnaliteGet", parameter, commandType: CommandType.StoredProcedure).AsList();
                response.Items = oObjectList;

                //if (oObjectList.Count > 0)
                //{
                //    response.Count = oObjectList[0].RowsCount;
                //    response.IndexDebut = oObjectDto.Offset;
                //    response.IndexFin = response.IndexDebut + oObjectList.Count - 1;
                //}

                return response;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                DbManager.CloseConnection();
            }
        }
       
        public BusinessResponse<FonctionnaliteDto> FonctionnaliteGet(FonctionnaliteDto oObjectDto)
        {
            DbManager DbManager = new DbManager();
            var response = new BusinessResponse<FonctionnaliteDto>();
            try
            {
                var parameter = new DynamicParameters();
               // parameter.Add("@CreatedBy", oObjectDto.CreatedBy);

                DbManager.OpenConnection();
                var oObjectList = DbManager.DbConnection.Query<FonctionnaliteDto>("sp_FonctionnaliteGet", parameter, commandType: CommandType.StoredProcedure).AsList();
                response.Items = oObjectList;

                return response;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                DbManager.CloseConnection();
            }
        }

        public BusinessResponse<UtilisateurSessionDto> UtilisateurSessionSave(UtilisateurSessionDto oObjectDto)
        {
            DbManager DbManager = new DbManager();
            var response = new BusinessResponse<UtilisateurSessionDto>();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("@IdUtilisateurSession", oObjectDto.IdUtilisateurSession);
                parameter.Add("@IdUtilisateur ", oObjectDto.IdUtilisateur);
                parameter.Add("@TimeSession", oObjectDto.TimeSession);
                parameter.Add("@IsConnected", oObjectDto.IsConnected);

                DbManager.OpenConnection();
                DbManager.DbTransaction = DbManager.DbConnection.BeginTransaction();
                DbManager.DbConnection.Execute("sp_UtilisateurSessionSave", parameter, commandType: CommandType.StoredProcedure, transaction: DbManager.DbTransaction);
                DbManager.DbTransaction.Commit();

                response.Items.Add(oObjectDto);

                return response;
            }
            catch (SqlException ex)
            {
                DbManager.DbTransaction.Rollback();
                throw new Exception(ex.Message);
            }
            finally
            {
                DbManager.CloseConnection();
            }
        }

        public void TraceSave(TraceDto oObjectDto)
        {
            DbManager DbManager = new DbManager();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("@IdUtilisateur ", oObjectDto.IdUtilisateur);
                parameter.Add("@DateOperation", oObjectDto.DateOperation);
                parameter.Add("@CodeOperation", oObjectDto.CodeOperation);
               // parameter.Add("@CodeFormulaire", oObjectDto.CodeFormulaire);
               // parameter.Add("@LibelleOperation", oObjectDto.LibelleOperation);

                DbManager.OpenConnection();
                DbManager.DbTransaction = DbManager.DbConnection.BeginTransaction();
                DbManager.DbConnection.Execute("sp_TraceSave", parameter, commandType: CommandType.StoredProcedure, transaction: DbManager.DbTransaction);
                DbManager.DbTransaction.Commit();
            }
            catch (SqlException ex)
            {
                DbManager.DbTransaction.Rollback();
                throw new Exception(ex.Message);
            }
            finally
            {
                DbManager.CloseConnection();
            }
        }

        public BusinessResponse<TraceDto> TraceGet(TraceDto Object)
        {
            DbManager DbManager = new DbManager();
            var response = new BusinessResponse<TraceDto>();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("@IdUtilisateur", Object.IdUtilisateur);
               //parameter.Add("@LibelleOperation", Object.LibelleOperation);
                parameter.Add("@DateDebut", (Object.CrDateDebut.ToShortDateString() == "01/01/0001" ? (object)null : Object.CrDateDebut));
                parameter.Add("@DateFin", (Object.CrDateFin.ToShortDateString() == "01/01/0001" ? (object)null : Object.CrDateFin));
                parameter.Add("@Offset", Object.Offset);
                parameter.Add("@Row", Object.Rows);

                DbManager.OpenConnection();
                var oObjectList = DbManager.DbConnection.Query<TraceDto>("sp_TraceGet", parameter, commandType: CommandType.StoredProcedure).AsList();
                response.Items = oObjectList;

                if (oObjectList.Count > 0)
                {
                    response.Count = oObjectList[0].RowsCount;
                    response.IndexDebut = Object.Offset;
                    response.IndexFin = response.IndexDebut + oObjectList.Count - 1;
                }

                return response;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                DbManager.CloseConnection();
            }
        }

        public BusinessResponse<ViewUtilisateurDto> UtilisateurGetByLoginOrEmail(string login, string email)
        {
            string sqlQuery = "";
            DbManager DbManager = new DbManager();
            var response = new BusinessResponse<ViewUtilisateurDto>();
            try
            {
                DbManager.OpenConnection();
                List<ViewUtilisateurDto> oObjectList = new List<ViewUtilisateurDto>();
                if (login != "" && email != "")
                {
                    sqlQuery = "select * from ViewUtilisateur where Login = @Login or Email = @Email";
                    oObjectList = DbManager.DbConnection.Query<ViewUtilisateurDto>(sqlQuery, new { Login = login, Email = email }).AsList();
                    response.Items = oObjectList;
                }
                else if (login == "")
                {
                    sqlQuery = "select * from ViewUtilisateur where Email = @Email";
                    oObjectList = DbManager.DbConnection.Query<ViewUtilisateurDto>(sqlQuery, new { Email = email }).AsList();
                    response.Items = oObjectList;
                }
                else if (email == "")
                {
                    sqlQuery = "select * from ViewUtilisateur where Login = @Login";
                    oObjectList = DbManager.DbConnection.Query<ViewUtilisateurDto>(sqlQuery, new { Login = login }).AsList();                    
                }

               // var CurrentSeance = JourneeGet("LAST", 1, DbManager);

                if (oObjectList.Count > 0)
                {
                    foreach (var oUtilisateur in oObjectList)
                    {
                        UtilisateurDto oUser = new UtilisateurDto
                        {
                            IdUtilisateur = oUtilisateur.IdUtilisateur,
                            CreatedBy = oUtilisateur.CreatedBy
                        };

                        var oProfils = UtilisateurProfilGet(oUser, DbManager);
                        var oFonctionnalites = UtilisateurFonctionnaliteGet(oUser, DbManager);
                        var oRestrictions = UtilisateurRestrictionGet(oUser, DbManager).Where(e => e.Autorise == "Non").AsList();

                        oUtilisateur.Profils = oProfils;
                        oUtilisateur.Fonctionnalites = oFonctionnalites;
                        oUtilisateur.Restrictions = oRestrictions;
                        //oUtilisateur.CurrentSeance = CurrentSeance;
                    }
                }
                response.Items = oObjectList;

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                DbManager.CloseConnection();
            }
        }
        public BusinessResponse<ViewUtilisateurDto> UtilisateurGetByLoginOrEmail(UtilisateurDto oObject)
        {
            DbManager DbManager = new DbManager();
            var response = new BusinessResponse<ViewUtilisateurDto>();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("@IdUtilisateur", oObject.IdUtilisateur);
                parameter.Add("@Login", oObject.Login);
                parameter.Add("@Email", oObject.Email);
                parameter.Add("@Password", oObject.Password);

                DbManager.OpenConnection();
                var oObjectList = DbManager.DbConnection.Query<ViewUtilisateurDto>("sp_UtilisateurGetByLogin", parameter, commandType: CommandType.StoredProcedure).AsList();

                if (oObjectList.Count > 0)
                {
                    //var CurrentSeance = JourneeGet("LAST", oObject.CreatedBy, DbManager);
                    foreach (var oUtilisateur in oObjectList)
                    {
                        UtilisateurDto oUser = new UtilisateurDto
                        {
                            IdUtilisateur = oUtilisateur.IdUtilisateur,
                            CreatedBy = oUtilisateur.CreatedBy
                        };

                        var oProfils = UtilisateurProfilGet(oUser, DbManager);
                        var oFonctionnalites = UtilisateurFonctionnaliteGet(oUser, DbManager);
                        var oRestrictions = UtilisateurRestrictionGet(oUser, DbManager).Where(e => e.Autorise == "Non").AsList();

                        oUtilisateur.Profils = oProfils;
                        oUtilisateur.Fonctionnalites = oFonctionnalites;
                        oUtilisateur.Restrictions = oRestrictions;
                        //oUtilisateur.CurrentSeance = CurrentSeance;
                    }
                }

                response.Items = oObjectList;
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                DbManager.CloseConnection();
            }
        }
        public BusinessResponse<ViewUtilisateurDto> UtilisateurGetByLoginOrEmail(ViewUtilisateurDto oObject)
        {
            DbManager DbManager = new DbManager();
            var response = new BusinessResponse<ViewUtilisateurDto>();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("@IdUtilisateur", oObject.IdUtilisateur);
                parameter.Add("@Login", oObject.Login);
                parameter.Add("@Email", oObject.Email);
                parameter.Add("@Password", oObject.Password);

                DbManager.OpenConnection();
                var oObjectList = DbManager.DbConnection.Query<ViewUtilisateurDto>("sp_UtilisateurGetByLogin", parameter, commandType: CommandType.StoredProcedure).AsList();

                if (oObjectList.Count > 0)
                {
                    //var CurrentSeance = JourneeGet("LAST", oObject.CreatedBy, DbManager);
                    foreach (var oUtilisateur in oObjectList)
                    {
                        UtilisateurDto oUser = new UtilisateurDto
                        {
                            IdUtilisateur = oUtilisateur.IdUtilisateur,
                            CreatedBy = oUtilisateur.CreatedBy
                        };

                        var oProfils = UtilisateurProfilGet(oUser, DbManager);
                        var oFonctionnalites = UtilisateurFonctionnaliteGet(oUser, DbManager);
                        var oRestrictions = UtilisateurRestrictionGet(oUser, DbManager).Where(e => e.Autorise == "Non").AsList();

                        oUtilisateur.Profils = oProfils;
                        oUtilisateur.Fonctionnalites = oFonctionnalites;
                        oUtilisateur.Restrictions = oRestrictions;
                        //oUtilisateur.CurrentSeance = CurrentSeance;
                    }
                }

                response.Items = oObjectList;
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                DbManager.CloseConnection();
            }
        }
        public string StrategiePasswordTest(int idStrategie, string password)
        {
            DbManager DbManager = new DbManager();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("@IdStrategie", idStrategie);
                parameter.Add("@Password", password);

                DbManager.OpenConnection();
                var response = DbManager.DbConnection.Query<string>("sp_StrategiePasswordTest", parameter , commandType: CommandType.StoredProcedure).FirstOrDefault();
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                DbManager.CloseConnection();
            }
        }

        public string UtilisateurSessionTest(long idUtilisateur)
        {
            DbManager DbManager = new DbManager();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("@IdUtilisateur", idUtilisateur);

                DbManager.OpenConnection();
                var response = DbManager.DbConnection.Query<string>("sp_UtilisateurSessionTest", parameter, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                DbManager.CloseConnection();
            }
        }

        public void StrategieMotDePasseSave(SysStrategieMotDePasseDto oObjectDto)
        {
            DbManager DbManager = new DbManager();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("@IdSysStrategieMotDePasse ", oObjectDto.IdSysStrategieMotDePasse);
                parameter.Add("@Libelle", oObjectDto.Libelle);
                parameter.Add("@LongeurMinimum", oObjectDto.LongeurMinimum);
                parameter.Add("@DureeDeVie", oObjectDto.DureeDeVie);
                parameter.Add("@NombreTentativeAvantVerrouillage", oObjectDto.NombreTentativeAvantVerrouillage);
                parameter.Add("@IdSysComplexiteMotDePasse ", oObjectDto.IdSysComplexiteMotDePasse);
                parameter.Add("@UtiliserAncienMotDePasse", oObjectDto.UtiliserAncienMotDePasse);
                parameter.Add("@ChangerMotDePasseApresAttribution", oObjectDto.ChangerMotDePasseApresAttribution);
                parameter.Add("@Actif", oObjectDto.Actif);
                parameter.Add("@IsDeleted", oObjectDto.IsDeleted);
                parameter.Add("@CreatedBy", oObjectDto.CreatedBy);

                DbManager.OpenConnection();
                DbManager.DbTransaction = DbManager.DbConnection.BeginTransaction();
                DbManager.DbConnection.Execute("sp_SysStrategieMotDePasseSave", parameter, commandType: CommandType.StoredProcedure, transaction: DbManager.DbTransaction);
                DbManager.DbTransaction.Commit();
            }
            catch (SqlException ex)
            {
                DbManager.DbTransaction.Rollback();
                throw new Exception(ex.Message);
            }
            finally
            {
                DbManager.CloseConnection();
            }
        }

        public void sysConnexion(long IdUtilisateur)
        {
            DbManager DbManager = new DbManager();
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("@IdUtilisateur ", IdUtilisateur);

                DbManager.OpenConnection();
                DbManager.DbTransaction = DbManager.DbConnection.BeginTransaction();
                DbManager.DbConnection.Execute("sp_sysConnexion", parameter, commandType: CommandType.StoredProcedure, transaction: DbManager.DbTransaction);
                DbManager.DbTransaction.Commit();
            }
            catch (SqlException ex)
            {
                DbManager.DbTransaction.Rollback();
                throw new Exception(ex.Message);
            }
            finally
            {
                DbManager.CloseConnection();
            }
        }

        //public BusinessResponse<ViewTraceDto> UtilisateurTraceGet(ViewTraceDto oObjectDto)
        //{
        //    DbManager DbManager = new DbManager();
        //    var response = new BusinessResponse<ViewTraceDto>();
        //    try
        //    {
        //        var parameter = new DynamicParameters();
        //        parameter.Add("@IdUtilisateur", oObjectDto.IdUtilisateur);
        //        parameter.Add("@DateDebut", oObjectDto.CrDateDebut);           
        //        parameter.Add("@DateFin", oObjectDto.CrDateFin);
        //        parameter.Add("@LibelleOperation", oObjectDto.LibelleOperation);
        //        parameter.Add("@Offset", oObjectDto.Offset);
        //        parameter.Add("@Row", oObjectDto.Rows);

        //        DbManager.OpenConnection();
        //        var oObjectList = DbManager.DbConnection.Query<ViewTraceDto>("sp_UtilisateurTraceGet", parameter, commandType: CommandType.StoredProcedure).AsList();
        //        response.Items = oObjectList;

        //        if (oObjectList.Count > 0)
        //        {
        //            response.Count = oObjectList[0].RowsCount;
        //            response.IndexDebut = oObjectDto.Offset;
        //            response.IndexFin = response.IndexDebut + oObjectList.Count - 1;
        //        }
        //        return response;
        //    }
        //    catch (SqlException ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    finally
        //    {
        //        DbManager.CloseConnection();
        //    }
        //}

        //public List<Fournisseur> TestExistenceNom(string id, string nomFournisseur, bool isForInsert)
        //{
        //    try
        //    {
        //        //IEnumerable<Fournisseur> listFournisseur = connection.Query<Fournisseur>("select * from Fournisseur where Libelle = @Libelle", new { Libelle = libelle });                
        //        //var listFournisseur = Global.Instance.SqlConnection.Query<Fournisseur>("FournisseurGet", new { Libelle = libelle }, commandType: CommandType.StoredProcedure).AsList();
        //        var sqlQuery = "";
        //        List<Fournisseur> listFournisseur;
        //        if (isForInsert)
        //        {
        //            sqlQuery = "select * from Fournisseur where Nom = @Nom";
        //            listFournisseur = Global.Instance.SqlConnection.Query<Fournisseur>(sqlQuery, new { Nom = nomFournisseur }).AsList();
        //        }
        //        else
        //        {
        //            sqlQuery = "select * from Fournisseur where IdFournisseur <> @IdFournisseur and Nom = @Nom";
        //            listFournisseur = Global.Instance.SqlConnection.Query<Fournisseur>(sqlQuery, new { IdFournisseur = id, Nom = nomFournisseur }).AsList();
        //        }

        //        return listFournisseur;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}




        /************************Fin Classe****************************/

    }
}