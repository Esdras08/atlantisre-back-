using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Inexa.Atlantis.Re.Commons.Infras.Domains;
using Inexa.Atlantis.Re.Commons.Infras.Enums;

namespace Inexa.Atlantis.Re.Dal.Repository
{
    public interface IRepository<T> where T : EntityBase 
    {
        T Add(T entity);
        T Update(T entity, Func<T, bool> predicate = null);
        bool Remove(T entity);

        T this[object key] { get; set; }
        T GetById(object id);
        T GetById(int id);
        T GetById(Guid id);
        T GetOne(Expression<Func<T, bool>> where);

        IEnumerable<T> GetMany(Expression<Func<T, bool>> criteria);
        IEnumerable<T> GetMany(Expression<Func<T, bool>> criteria, int index, int count);
        IEnumerable<T> GetMany(Expression<Func<T, bool>> criteria, int index, int count, out int totalRecord);
        IEnumerable<T> GetMany(Expression<Func<T, bool>> criteria, int index, int count, out int totalRecord,
            Expression<Func<T, object>> orderBy, string sortOrder = MySortOrder.Ascending);

        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(int index, int count, out int totalRecord);
        IEnumerable<T> GetAll(Expression<Func<T, object>> orderBy, string sortOrder = MySortOrder.Ascending);
        IEnumerable<T> GetAll(int index, int count, out int totalRecord ,Expression<Func<T, object>> orderBy,
            string sortOrder = MySortOrder.Ascending);

        decimal GetSum(Expression<Func<T, bool>> where, Expression<Func<T, decimal?>> sumExp);
    }
}
