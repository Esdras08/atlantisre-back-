using Inexa.Atlantis.Re.Dal.Repository.DataContextStorage;

namespace Inexa.Atlantis.Re.Dal.Repository
{
    public class DataContextFactory
    {
        public static AtlantisReDbContext GetDataContext(string contextKey = "")
        {
            var dataContextStorageContainer = DataContextStorageFactory.CreateStorageContainer();

            var applicationContext = dataContextStorageContainer.GetDataContext();
            if (applicationContext == null)
            {
                applicationContext = new AtlantisReDbContext();
                //applicationContext.Database.Connection.BeginTransaction();
                //applicationContext.Database.Connection.BeginTransaction().Commit();
                //applicationContext.Database.Connection.BeginTransaction().Rollback();
                dataContextStorageContainer.Store(applicationContext);
            }

            return applicationContext;            
        }  
    }
}
