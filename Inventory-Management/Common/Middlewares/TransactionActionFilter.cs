using Inventory_Management.Data;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Inventory_Management.Common.Middlewares
{
    public class TransactionActionFilter : IAsyncActionFilter
    {
        private readonly Context _context;

        public TransactionActionFilter(Context context)
        {
            _context = context;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string method = context.HttpContext.Request.Method.ToUpper();


            if (method == "POST" || method == "PUT" || method == "DELETE")
            {
                var transaction = await _context.Database.BeginTransactionAsync();

                try
                {
                    var resultContext = await next();
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();


                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }

            }
            else
            {
                await next();
            }
        }
    }
}
