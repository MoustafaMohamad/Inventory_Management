using Inventory_Management.Common.Exceptions;

namespace Inventory_Management.Common.Helpers.ResultViewModel
{
    public class ResultViewModel
    {
        public bool IsSuccess { get; set; }
        public dynamic Data { get; set; }
        public string Message { get; set; }

        public ErrorCode ErrorCode { get; set; }


        public static ResultViewModel Sucess(dynamic data, string message = "")
        {
            return new ResultViewModel
            {
                IsSuccess = true,
                Data = data,
                Message = message,
                ErrorCode = ErrorCode.None,
            };
        }

        public static ResultViewModel Faliure(ErrorCode errorCode, string message)
        {
            return new ResultViewModel
            {
                IsSuccess = false,
                Data = default,
                Message = message,
                ErrorCode = errorCode,
            };
        }
    }

    
    
}
