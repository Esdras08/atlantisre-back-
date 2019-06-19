using System;
using System.Data.Entity;
using Inexa.Atlantis.Re.Commons.Infras.Domains;

namespace Inexa.Atlantis.Re.Dal.Repository.UnitOfWork
{
    public interface IUnitOfWorkRepository<out T>
    {
        EntityBase PersistCreationOf(EntityBase entity, DbContext cnx = null);
        EntityBase PersistUpdateOf(EntityBase entity, Func<T, bool> predicate, DbContext cnx = null);
        bool PersistDeletionOf(EntityBase entity, DbContext cnx = null);
    }
}
