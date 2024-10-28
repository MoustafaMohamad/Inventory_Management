using Inventory_Management.Data;
using Inventory_Management.Entities;
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

      
    }
}
