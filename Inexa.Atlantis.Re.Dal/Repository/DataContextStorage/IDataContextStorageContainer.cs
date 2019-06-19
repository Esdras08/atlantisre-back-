
namespace Inexa.Atlantis.Re.Dal.Repository.DataContextStorage
{
    public interface IDataContextStorageContainer
    {
        AtlantisReDbContext GetDataContext();
        void Store(AtlantisReDbContext applicationContext);
    }
}
