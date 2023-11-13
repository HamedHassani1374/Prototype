using Prototype.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.Repository
{
    public interface IRepository<T> where T : IBaseEntity
    {
        void Create(T entity);
        void CreateAsync(T entity);
        void Delete(T entity);
        void Delete(long id);
        Task<bool> Existance(Expression<Func<T, bool>> filter);
        void Edit(T entity);
        void EditAsync(T entity);
        IEnumerable<T> GetAll(params Expression<Func<T, object>>[] navigationProperties);
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] navigationProperties);
        T GetById(long id);
        IEnumerable<T> Filter(params Expression<Func<T, object>>[] navigationProperties);
        IEnumerable<T> Filter(Func<T, bool> predicate, params Expression<Func<T, object>>[] navigationProperties);
        IEnumerable<T> FilterByDynamicEXP(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] navigationProperties);
        Task<IEnumerable<T>> FilterByDynamicEXPAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] navigationProperties);
        T GetSingle(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties);
        IEnumerable<T> FilterByQuery(string query);
        IEnumerable<T> FindAll();
    }
}
