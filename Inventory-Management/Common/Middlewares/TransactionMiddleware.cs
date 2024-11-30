using Inventory_Management.Common.Helpers;
using Inventory_Management.Data;
using Microsoft.AspNetCore.Http;
using System.Net.Http;

namespace Inventory_Management.Common.Middlewares
{
    public class TransactionMiddleware
    {
       private RequestDelegate _next;
        private Context _context;
        public TransactionMiddleware(RequestDelegate next ,Context context) { 
        
            _next = next;
            _context = context;
        }

        public async Task InvokeAsync(HttpContext context ) { 
            
            string method= context.Request.Method.ToUpper();

            if (method=="POST" || method == "PUT" || method == "DELETE" ) {
                var transaction = _context.Database.BeginTransaction();
                try
                {
                    await _next(context);
                    transaction.Commit();
                    await _context.SaveChangesAsync();


                }
                catch (Exception ex)
                {

                    transaction.Rollback();
                    throw;
                }
            }

            else
            {

                await _next(context);
            }
        }



    }
}
