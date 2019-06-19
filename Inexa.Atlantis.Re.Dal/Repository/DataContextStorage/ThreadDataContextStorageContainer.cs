using System.Collections;
using System.Threading;

namespace Inexa.Atlantis.Re.Dal.Repository.DataContextStorage
{
    public class ThreadDataContextStorageContainer : IDataContextStorageContainer
    {
        private static readonly Hashtable ApplicationContext = new Hashtable();

        public AtlantisReDbContext GetDataContext()
        {
            AtlantisReDbContext applicationContext = null;

            if (ApplicationContext.Contains(GetThreadName()))
                applicationContext = (AtlantisReDbContext)ApplicationContext[GetThreadName()];

            return applicationContext;
        }

        public void Store(AtlantisReDbContext applicationContext)
        {
            if (ApplicationContext.Contains(GetThreadName()))
                ApplicationContext[GetThreadName()] = applicationContext;
            else
                ApplicationContext.Add(GetThreadName(), applicationContext);
        }

        private static string GetThreadName()
        {
            return Thread.CurrentThread.Name ?? string.Empty;
        }
    }
}
