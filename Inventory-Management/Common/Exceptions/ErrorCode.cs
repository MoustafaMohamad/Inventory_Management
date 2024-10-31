namespace Inventory_Management.Common.Exceptions
{
    public enum ErrorCode
    {
        None,
        UnKnown,
        ThisFieldIsRequierd,
        // User 100-199
        EmailIsNotFound,
        WrongPassword,
        PasswordsDontMatch,
        EmailAlreadyExist,
        UserNameAlreadyExist,
        WrongPasswordOrEmail,
        UnableTogenerateToken,
        RoleNotFound,
        OtpIsNotValid,
        OtpIsNotFound,
        //Product 200-299
        InvalidProductID,
        NoProductsFound,
        GreaterThan0,
        GreaterThanThreshold,
        InvalidExpirtDate,
    }
}
