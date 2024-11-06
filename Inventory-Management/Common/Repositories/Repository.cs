using AutoMapper.QueryableExtensions;
using Common.Helpers;
using Inventory_Management.Data;
using Inventory_Management.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;

        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(e => !e.IsDeleted).AnyAsync(predicate);
        }

        public void Delete(int id)
        {
            var entity = GetByID(id);
            Delete(entity);
        }
        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            Update(entity);
        }

        public async Task<T> FirstAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(e => !e.IsDeleted).FirstOrDefaultAsync(predicate);

        }

        public async Task<IQueryable<T>> Get(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(e => !e.IsDeleted).Where(predicate);
        }

        public async Task<IQueryable<T>> GetAll()
        {
            return _context.Set<T>().Where(e => !e.IsDeleted);
        }
        public async Task<IQueryable<TResult>> GetAllWithProjectTo<TResult>(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(e => !e.IsDeleted).Where(predicate).Map<TResult>();
        }

        public async Task<TResult> GetByIDWithProjection<TResult>(Expression<Func<T, bool>> predicate)
        {
            var entities = await GetAllWithProjectTo<TResult>(predicate);
            return await entities.FirstOrDefaultAsync();
        }


        public T GetByID(int id)
        {
            return _context.Set<T>().AsNoTracking().Where(e => !e.IsDeleted).FirstOrDefault(t => t.ID == id);
        }
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            Console.WriteLine(_context.GetHashCode());


            _context.Update(entity);
        }

        public T UpdatewithReturn(T entity)
        {
            _context.Update(entity);
            return entity;
        }

        public void UpdateIncluded(T entity, params string[] updatedProperties)
        {
            T local = _context.Set<T>().Local.FirstOrDefault(x => x.ID == entity.ID);

            EntityEntry entityEntry;




        }
        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }



        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }


    }
}
