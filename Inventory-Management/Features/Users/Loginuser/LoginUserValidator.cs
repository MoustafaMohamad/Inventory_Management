using FluentValidation;
using Inventory_Management.Common.Exceptions;
using Inventory_Management.Features.Users.RegisterUser;

namespace Inventory_Management.Features.Users.Loginuser
{
    public class LoginUserValidator : AbstractValidator<LoginUserEndPointRequest>
    {
        public LoginUserValidator()
        {
            RuleFor(user => user.Email)
            .NotEmpty().WithErrorCode(ErrorCode.ThisFieldIsRequierd.ToString()).WithMessage("Email is required.")
            .EmailAddress().WithErrorCode(ErrorCode.EmailIsNotValid.ToString()).WithMessage("Invalid Email Address");

            RuleFor(user => user.Password)
                .NotEmpty().WithErrorCode(ErrorCode.ThisFieldIsRequierd.ToString()).WithMessage("password is required.")
                .Matches("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,}").WithErrorCode(ErrorCode.PasswordIsNotValid.ToString()).WithMessage("Invalid Password ");
        }
    }
}
