using Inventory_Management.Entities;
using System.Linq.Expressions;

namespace Inventory_Management.Common.Repositories
{
    public interface IRepository<T> where T : BaseModel 
    {
        Task<T> AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(int id);

        T GetByID(int id);
        Task<IQueryable<TResult>> GetAllWithProjectTo<TResult>(Expression<Func<T, bool>> predicate);
        Task<TResult> GetByIDWithProjection<TResult>(Expression<Func<T, bool>> predicate);
        Task<T> FirstAsync(Expression<Func<T, bool>> predicate);
        Task<IQueryable<T>> GetAll();
       Task< IQueryable<T>> Get(Expression<Func<T, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task SaveChanges();
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        void UpdateIncluded(T entity, params string[] updatedProperties);
        T UpdatewithReturn(T entity);
        Task SaveChangesAsync(); 
    }
}
