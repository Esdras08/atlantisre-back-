using System;
using System.Data.Entity;
using Inexa.Atlantis.Re.Commons.Infras.Domains;

namespace Inexa.Atlantis.Re.Dal.Repository.UnitOfWork
{
    public interface IUnitOfWork
    {
        EntityBase RegisterUpdate<T>(EntityBase entity,
                             IUnitOfWorkRepository<T> unitofWorkRepository, Func<T, bool> predicate, DbContext cnx = null);
        EntityBase RegisterNew(EntityBase entity,
                         IUnitOfWorkRepository<EntityBase> unitofWorkRepository, DbContext cnx = null);
        void RegisterRemoved(EntityBase entity,
                             IUnitOfWorkRepository<EntityBase> unitofWorkRepository, DbContext cnx = null);
        void Commit(DbContext cnx = null);
    }
}
