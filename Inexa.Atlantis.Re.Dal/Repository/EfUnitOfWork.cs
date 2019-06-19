using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Text;
using Inexa.Atlantis.Re.Commons.Infras.Domains;
using Inexa.Atlantis.Re.Dal.Repository.UnitOfWork;
using Inexa.Atlantis.Re.Dal.Repository;

namespace Inexa.Atlantis.Re.Dal.Repository
{
    public class EfUnitOfWork : IUnitOfWork
    {
        public void Commit(DbContext cnx = null)
        {
            try
            {
                if (cnx == null)
                {
                    cnx = DataContextFactory.GetDataContext();
                }
                cnx.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                var innerEx = e.InnerException;

                while (innerEx.InnerException != null)
                    innerEx = innerEx.InnerException;

                throw new Exception(innerEx.Message);
            }
            catch (DbEntityValidationException e)
            {
                var sb = new StringBuilder();

                foreach (var entry in e.EntityValidationErrors)
                {
                    foreach (var error in entry.ValidationErrors)
                    {
                        sb.AppendLine(string.Format("{0}-{1}-{2}",
                            entry.Entry.Entity,
                            error.PropertyName,
                            error.ErrorMessage
                            )
                        );
                    }
                }
                throw new Exception(sb.ToString());
            }
        }

        public EntityBase RegisterUpdate<T>(EntityBase entity, IUnitOfWorkRepository<T> unitofWorkRepository, Func<T, bool> predicate, DbContext cnx = null)
        {
            return unitofWorkRepository.PersistUpdateOf(entity, predicate, cnx);
        }

        public EntityBase RegisterNew(EntityBase entity, IUnitOfWorkRepository<EntityBase> unitofWorkRepository, DbContext cnx = null)
        {
            return unitofWorkRepository.PersistCreationOf(entity, cnx);
        }

        public void RegisterRemoved(EntityBase entity, IUnitOfWorkRepository<EntityBase> unitofWorkRepository, DbContext cnx = null)
        {
            unitofWorkRepository.PersistDeletionOf(entity);
        }
    }

}
