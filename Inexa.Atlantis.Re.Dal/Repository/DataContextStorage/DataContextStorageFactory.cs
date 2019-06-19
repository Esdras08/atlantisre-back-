using System.Web;

namespace Inexa.Atlantis.Re.Dal.Repository.DataContextStorage
{
    public class DataContextStorageFactory
    {
        public static IDataContextStorageContainer DataContectStorageContainer;

        public static IDataContextStorageContainer CreateStorageContainer()
        {
            if (DataContectStorageContainer == null)
            {
                if (HttpContext.Current == null)                                    
                    DataContectStorageContainer = new ThreadDataContextStorageContainer();
                else
                    DataContectStorageContainer = new HttpDataContextStorageContainer();
            }

            return DataContectStorageContainer;
        }
    }
}
