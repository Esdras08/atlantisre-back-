using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Inexa.Atlantis.Re.Dal.SpProvider
{
    public class DbManager
    {
        //private static DbManager _instance;

        //public static DbManager Instance
        //{
        //    get { return _instance ?? (_instance = new DbManager()); }
        //}

        readonly string ConnectionStrings = ConfigurationManager.ConnectionStrings["BD_ATLANTIS_RE"].ConnectionString;

        public IDbConnection DbConnection;
        public IDbTransaction DbTransaction;

        public DbManager()
        {
            DbConnection = new SqlConnection(ConnectionStrings);
        }


        public void OpenConnection()
        {
            try
            {
                if (DbConnection.State == ConnectionState.Closed)
                {
                    //DbConnection.ConnectionString = ConnectionStrings;
                    DbConnection.Open();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CloseConnection()
        {
            try
            {
                if (DbConnection != null)
                {
                    if (DbConnection.State == ConnectionState.Open)
                    {
                        DbConnection.Close();
                        DbConnection.Dispose();
                    }
                    DbConnection = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
