using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers.ResultViewModel;

namespace Inventory_Management.Common.Middlewares
{
    public class GlobalErrorHandlerMiddleware
    {
        RequestDelegate _next;

        public GlobalErrorHandlerMiddleware(RequestDelegate next )
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context) {

            try
            {
                await _next(context);
            }
            catch (Exception ex )
            {
                string message = "Error Occured";
                ErrorCode errorCode = ErrorCode.UnKnown;
                if (ex is BusinessException businessException)
                {
                    message = businessException.Message;
                    errorCode = businessException.ErrorCode;
                }

                var result = ResultViewModel.Faliure(errorCode, message);

                await context.Response.WriteAsJsonAsync(result);
            }
        }
    }
}
