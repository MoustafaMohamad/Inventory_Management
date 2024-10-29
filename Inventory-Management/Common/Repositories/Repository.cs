using Inventory_Management.Data;
using Inventory_Management.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Inventory_Management.Common.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        private readonly Context _context;

        public Repository(Context context)
        {
            _context = context;
        }
        
        public async Task AddAsync(T entity)
        {
           await _context.Set<T>().AddAsync(entity);
           
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task< T> First(Expression<Func<T, bool>> predicate)
        {
            return  await _context.Set<T>().FirstOrDefaultAsync(predicate);    
            
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<T>> GetAll()
        {
            return _context.Set<T>().Where(e => !e.IsDeleted);
        }

        public T GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task SaveChanges()
        {
            _context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
             _context.Update(entity);
        }

    }
}
