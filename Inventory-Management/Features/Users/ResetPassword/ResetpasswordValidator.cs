using FluentValidation;
using Inventory_Management.Common.Exceptions;
using Inventory_Management.Features.Users.ChangePassword;

namespace Inventory_Management.Features.Users.ResetPassword
{
    
    public class ResetpasswordValidator : AbstractValidator<ResetPasswordEndPointRequest>
    {
        public ResetpasswordValidator()
        {

            RuleFor(user => user.Email)
              .NotEmpty().WithErrorCode(ErrorCode.ThisFieldIsRequierd.ToString()).WithMessage("Email is required.")
              .EmailAddress().WithErrorCode(ErrorCode.EmailIsNotValid.ToString()).WithMessage("Invalid Email Address");

            RuleFor(user => user.NewPassword)
                .NotEmpty().WithErrorCode(ErrorCode.ThisFieldIsRequierd.ToString()).WithMessage("password is required.")
                     .Matches("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,}").WithErrorCode(ErrorCode.PasswordIsNotValid.ToString()).WithMessage("Invalid Password ");

            RuleFor(user => user.ConfirmPassword)
                .NotEmpty().WithErrorCode(ErrorCode.ThisFieldIsRequierd.ToString()).WithMessage("Confirm password is required.")
                .Equal(user => user.NewPassword).WithErrorCode(ErrorCode.PasswordsDontMatch.ToString()).WithMessage("Password don't match.")
                   .Matches("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,}").WithErrorCode(ErrorCode.PasswordIsNotValid.ToString()).WithMessage("Invalid Password ");



        }
    }
}
