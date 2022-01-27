using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Entities.Abstract;

namespace Data.Abstract
{
    /// <summary>
    /// Interface for repository pattern
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class, IEntity
    {
        List<T> GetList(Expression<Func<T, bool>> filter = null, params Expression<Func<T, Object>>[] includes);
        T Get(Expression<Func<T, bool>> filter, params Expression<Func<T, Object>>[] includes);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        void Delete(T entity);
    }
}
