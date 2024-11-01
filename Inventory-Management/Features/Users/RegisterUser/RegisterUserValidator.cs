using FluentValidation;
using Inventory_Management.Common.Exceptions;

namespace Inventory_Management.Features.Users.RegisterUser
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserEndPointRequest>
    {
        public RegisterUserValidator()
        {
            RuleFor(user => user.UserName)
               .NotEmpty().WithErrorCode(ErrorCode.ThisFieldIsRequierd.ToString()).WithMessage("User name Is Requierd")
               .MinimumLength(5)
               .MaximumLength(15);

            RuleFor(user => user.Email)
              .NotEmpty().WithErrorCode(ErrorCode.ThisFieldIsRequierd.ToString()).WithMessage("Email is required.")
              .EmailAddress().WithErrorCode(ErrorCode.EmailIsNotValid.ToString()).WithMessage("Invalid Email Address");

            RuleFor(user => user.Password).NotEmpty().WithErrorCode(ErrorCode.ThisFieldIsRequierd.ToString()).WithMessage("password is required.")
                     .Matches("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,}").WithErrorCode(ErrorCode.PasswordIsNotValid.ToString()).WithMessage("Invalid Password ");

            RuleFor(user => user.ConfirmPassword).NotEmpty().WithErrorCode(ErrorCode.ThisFieldIsRequierd.ToString()).WithMessage("Confirm password is required.")
                .Equal(user=>user.Password).WithErrorCode(ErrorCode.PasswordsDontMatch.ToString()).WithMessage("Password don't match.")
                   .Matches("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,}").WithErrorCode(ErrorCode.PasswordIsNotValid.ToString()).WithMessage("Invalid Password ");



        }
    }
}
