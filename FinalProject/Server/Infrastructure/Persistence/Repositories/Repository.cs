using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class Repository<T, TContext> : IRepository<T>
        where T : class, BaseEntity
        where TContext : DbContext
    {
        private readonly TContext context;
        public Repository(TContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> Add(T entity)
        {
            context.Set<T>().Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Deletes provided entity
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(T entity)
        {
            var deletedEntity = context.Entry(entity);
            deletedEntity.State = EntityState.Deleted;
            context.SaveChanges();
        }

        /// <summary>
        /// Returns one item using filter
        /// If includes params provided, then it includes them
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public T Get(Expression<Func<T, bool>> filter, params Expression<Func<T, Object>>[] includes)
        {
            IQueryable<T> query = context.Set<T>();
            if (includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return query.SingleOrDefault(filter);
        }

        /// <summary>
        /// Returns list of entities using filter
        /// If includes params provided, then it includes them
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public List<T> GetList(Expression<Func<T, bool>> filter = null, params Expression<Func<T, Object>>[] includes)
        {
            IQueryable<T> query = context.Set<T>();
            if (includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return filter == null
                ? query.ToList()
                : query.Where(filter).ToList();
        }

        /// <summary>
        /// Updates provided entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return entity;
        }
    }
}
