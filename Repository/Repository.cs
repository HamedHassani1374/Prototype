using LinqKit.Core;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class Repository<T> : IRepository<T> where T : class , IBaseEntity
    {
        private readonly ModelContext context;
        internal DbSet<T> dbSet;
        public Repository(ModelContext Context)
        {
            this.context = Context;
            this.dbSet = context.Set<T>();
        }

        public void Create(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public async void CreateAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public void Delete(long id)
        {
            var currentEntity = context.Set<T>().FirstOrDefault(e => e.Id == id);
            if (currentEntity != null)
            {
                context.Set<T>().Remove(currentEntity);
            }
        }
        public async Task<bool> Existance(Expression<Func<T, bool>> filter)
        {
            return await context.Set<T>().AnyAsync(filter);
        }
        public void Edit(T entity)
        {
            var editedEntity = context.Set<T>().FirstOrDefault(e => e.Id == entity.Id);
            editedEntity = entity;
        }

        public async void EditAsync(T entity)
        {
            var editedEntity = await context.Set<T>().FirstOrDefaultAsync(e => e.Id == entity.Id);
            editedEntity = entity;
        }
        public IEnumerable<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = context.Set<T>();
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<T, object>(navigationProperty);
            return dbQuery;
        }
        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = context.Set<T>();
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<T, object>(navigationProperty);
            return dbQuery;
        }
        public IEnumerable<T> Filter(params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = context.Set<T>();
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include(navigationProperty);
            return dbQuery;
        }

        public IEnumerable<T> Filter(Func<T, bool> predicate, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = context.Set<T>();
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include(navigationProperty);
            return dbQuery.Where(predicate);
        }


        public IEnumerable<T> FilterByDynamicEXP(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = context.Set<T>();
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include(navigationProperty);

            return dbQuery.AsExpandable().Where(predicate);
        }
        public async Task<IEnumerable<T>> FilterByDynamicEXPAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = context.Set<T>();
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include(navigationProperty);

            return dbQuery.AsExpandable().Where(predicate);
        }

        public T GetById(long id)
        {
            return context.Set<T>().FirstOrDefault(e => e.Id == id);
        }

        public T GetSingle(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = context.Set<T>();
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include(navigationProperty);
            return dbQuery.FirstOrDefault(where);
        }
        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = context.Set<T>();
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include(navigationProperty);
            return await dbQuery.FirstOrDefaultAsync(where);
        }
        public IEnumerable<T> FilterByQuery(string query)
        {
            var dbQuery = context.Set<T>().FromSqlRaw(query);
            return dbQuery;
        }

        public IEnumerable<T> FindAll()
        {
            return dbSet;
        }

    }
}
