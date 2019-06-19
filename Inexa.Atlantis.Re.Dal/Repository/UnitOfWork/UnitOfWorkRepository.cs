using System;
using System.Data.Entity;
using System.Linq;
using Inexa.Atlantis.Re.Commons.Infras.Domains;
using Inexa.Atlantis.Re.Dal.Repository;

namespace Inexa.Atlantis.Re.Dal.Repository.UnitOfWork
{
    public class UnitOfWorkRepository<T> : IUnitOfWorkRepository<T> where T : EntityBase
    {
        public DbSet<T> GetObjectSet()
        {
            return DataContextFactory.GetDataContext().Set<T>();
        }

        //private Dictionary<EntityBase, IUnitOfWorkRepository<T>> addedEntities;

        //public UnitOfWorkRepository()
        //{
        //    addedEntities = new Dictionary<EntityBase, IUnitOfWorkRepository<T>>();
        //}

        #region IUnitOfWorkRepository Membres

        public EntityBase PersistCreationOf(EntityBase entity, DbContext cnx = null)
        {
            if (cnx == null)
            {
                cnx = DataContextFactory.GetDataContext();
            }
            return cnx.Set<T>().Add((T)entity);
        }

        public EntityBase PersistUpdateOf(EntityBase entity, Func<T, bool> predicate, DbContext cnx = null)
        {
            if (cnx == null)
            {
                cnx = DataContextFactory.GetDataContext();
            }


            if (predicate == null)
            {
                // on ne veut pas tracker l'objet dans le contexte pour voir s'il est déjà attaché
                // risque de génération d'exception si l'objet est déjà attaché.

                cnx.Set<T>().Attach((T) entity);
                cnx.Entry((T)entity).State = EntityState.Modified;

                //this.GetObjectSet().Attach((T)entity);
                //DataContextFactory.GetDataContext().Entry((T)entity).State = EntityState.Modified;
            }
            else
            {
                var entry = cnx.Entry((T)entity);

                if (entry.State == EntityState.Detached)
                {
                    var set = cnx.Set<T>();
                    var attachedEntity = set.Local.SingleOrDefault(predicate);
                    if (attachedEntity != null)
                    {
                        var attachedEntry = cnx.Entry((T)attachedEntity);
                        attachedEntry.CurrentValues.SetValues(entity);
                    }
                    else
                    {
                        set.Attach((T)entity);
                        entry.State = EntityState.Modified;
                    }
                }
            }

            return entity;
        }

        public bool PersistDeletionOf(EntityBase entity, DbContext cnx = null)
        {
            //this.PersistDeletedItem((T)entity);
            return this.GetObjectSet().Remove((T)entity) == null;
        }

        #endregion
    }
}
