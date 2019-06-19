using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using Inexa.Atlantis.Re.Dal.Repository.UnitOfWork;
using Inexa.Atlantis.Re.Commons.Infras.Domains;
using Inexa.Atlantis.Re.Commons.Infras.Enums;
using Inexa.Atlantis.Re.Commons.Infras.Helpers;


namespace Inexa.Atlantis.Re.Dal.Repository
{
    //public class Repository<T> : IRepository<T>, IUnitOfWorkRepository where T : EntityBase
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        #region Contructors & Proprety

        private readonly IUnitOfWork _uow;

        private static DbSet<T> GetObjectSet()
        {
            return DataContextFactory.GetDataContext().Set<T>();
        }

        public Repository(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Repository(bool withTransaction = false) : this(new EfUnitOfWork())
        {

        }

        #endregion

        #region Methods CUD

        public T Add(T entity)
        {
            if (_uow != null)
            {
                var cnx = new AtlantisReDbContext();

                var response = (T)_uow.RegisterNew(entity, new UnitOfWorkRepository<T>(), cnx);
                _uow.Commit(cnx);
                return response;
            }
            return null;
        }

        public bool Remove(T entity)
        {
            var response = false;
            if (_uow != null)
            {
                _uow.RegisterRemoved(entity, new UnitOfWorkRepository<T>());
                _uow.Commit();
                response = true;
            }

            return response;
        }

        public T Update(T entity, Func<T, bool> predicate = null)
        {
            if (_uow != null)
            {
                var cnx = new AtlantisReDbContext();

                var response = (T)_uow.RegisterUpdate(entity, new UnitOfWorkRepository<T>(), predicate, cnx);
                _uow.Commit(cnx);

                return response;
            }
            return null;
        }

        #endregion

        #region Methods Read

        public T this[object key]
        {
            get
            {
                return GetById(key);
            }
            set
            {
                if (GetById(key) == null)
                {
                    Add(value);
                }
                else
                {
                    _uow.RegisterUpdate(value, new UnitOfWorkRepository<T>(), null);
                }
            }
        }

        public T GetById(object id)
        {
            return GetObjectSet().Find(id);
        }

        public T GetById(int id)
        {
            return GetObjectSet().Find(id);
        }

        public T GetById(Guid id)
        {
            return GetObjectSet().Find(id);
        }

        public T GetOne(Expression<Func<T, bool>> where)
        {
            return GetObjectSet().Where(where).FirstOrDefault<T>();
        }

        public IEnumerable<T> GetMany(Expression<Func<T, bool>> criteria)
        {
            return GetObjectSet().Where(criteria).ToList();
        }

        public IEnumerable<T> GetMany(Expression<Func<T, bool>> criteria, int index, int count)
        {
            return GetObjectSet().Where(criteria).OrderBy(e => true).Skip(index).Take(count).ToList<T>();
        }

        public IEnumerable<T> GetMany(Expression<Func<T, bool>> criteria, int index, int count, out int totalRecord)
        {
            totalRecord = GetObjectSet().Count(criteria);
            return GetObjectSet().Where(criteria).OrderBy(e => true).Skip(index).Take(count).ToList<T>();
        }

        public IEnumerable<T> GetMany(Expression<Func<T, bool>> criteria, int index, int count, out int totalRecord, Expression<Func<T, object>> orderBy, string sortOrder = MySortOrder.Ascending)
        {
            totalRecord = GetObjectSet().Count(criteria);
            return GetObjectSet()
                .Where(criteria)
                .OrderByMember(orderBy,
                    sortOrder.Equals(MySortOrder.Ascending) ? SortOrder.Ascending : SortOrder.Descending)
                .Skip(index)
                .Take(count)
                .ToList<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return GetObjectSet().ToList();
        }

        public IEnumerable<T> GetAll(int index, int count, out int totalRecord)
        {
            totalRecord = GetObjectSet().Count();
            return GetObjectSet().OrderBy(e => true).Skip(index).Take(count).ToList<T>();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, object>> orderBy, string sortOrder = MySortOrder.Ascending)
        {
            return GetObjectSet()
                .OrderByMember(orderBy,
                    sortOrder.Equals(MySortOrder.Ascending) ? SortOrder.Ascending : SortOrder.Descending)
                .ToList<T>();
        }

        public IEnumerable<T> GetAll(int index, int count, out int totalRecord, Expression<Func<T, object>> orderBy,
            string sortOrder = MySortOrder.Ascending)
        {
            totalRecord = GetObjectSet().Count();
            return GetObjectSet()
                .OrderByMember(orderBy,
                    sortOrder.Equals(MySortOrder.Ascending) ? SortOrder.Ascending : SortOrder.Descending)
                .Skip(index)
                .Take(count)
                .ToList<T>();
        }

        public virtual decimal GetSum(Expression<Func<T, bool>> where, Expression<Func<T, decimal?>> sumExp)
        {
            var sum = GetObjectSet().Where(@where).Sum(sumExp);
            if (sum != null)
                return (decimal)sum;
            return 0;
        }

        #endregion

    }
}
