namespace Inventory_Management.Common.Exceptions
{
    public enum ErrorCode
    {


        None = 200,                      // OK
        UnKnown = 500,                   // Internal Server Error
        BadRequest = 400,                   // Bad Request
        Unauthorized = 401,                 // Unauthorized
        NotFound = 404,                     // Not Found
        Conflict = 409,                     // Conflict
        InternalServerError = 500,          // Internal Server Error
                                            // USER
        EmailIsNotFound = 404,           // Not Found
        WrongPassword = 401,             // Unauthorized
        PasswordsDontMatch = 400,        // Bad Request
        EmailAlreadyExist = 409,         // Conflict
        UserNameAlreadyExist = 409,      // Conflict
        WrongPasswordOrEmail = 401,      // Unauthorized
        UnableToGenerateToken = 500,     // Internal Server Error
        RoleNotFound = 404,              // Not Found
        OtpIsNotValid = 400,             // Bad Request
        OtpIsNotFound = 404,
        UnableTogenerateToken = 500,     // Internal Server Error

    }
}
