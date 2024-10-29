using Inventory_Management.Entities;
using System.Linq.Expressions;

namespace Inventory_Management.Common.Repositories
{
    public interface IRepository<T> where T : BaseModel 
    {
        Task AddAsync(T entity); 
        Task Update(T entity);
        void Delete(T entity);
        void Delete(int id);
        T GetById(int id);
        Task<T> First(Expression<Func<T, bool>> predicate);
        Task<IQueryable<T>> GetAll();
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        bool Any(Expression<Func<T, bool>> predicate);
        Task SaveChanges();
    }
}
