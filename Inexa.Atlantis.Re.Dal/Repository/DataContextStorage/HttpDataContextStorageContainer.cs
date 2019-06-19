using System.Web;

namespace Inexa.Atlantis.Re.Dal.Repository.DataContextStorage
{
    public class HttpDataContextStorageContainer : IDataContextStorageContainer
    {
        private const string DataContextKey = "ProjectAsoExtranetDbContext";

        public AtlantisReDbContext GetDataContext()
        {
            AtlantisReDbContext applicationContext = null;
            if (HttpContext.Current.Items.Contains(DataContextKey))
                applicationContext = (AtlantisReDbContext)HttpContext.Current.Items[DataContextKey];

            return applicationContext;
        }

        public void Store(AtlantisReDbContext applicationContext)
        {
            if (HttpContext.Current.Items.Contains(DataContextKey))
                HttpContext.Current.Items[DataContextKey] = applicationContext;
            else
                HttpContext.Current.Items.Add(DataContextKey, applicationContext);
        }

    }
}
