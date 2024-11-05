using FluentValidation;
using Inventory_Management.Common.Exceptions;
using Inventory_Management.Features.Users.RegisterUser;

namespace Inventory_Management.Features.Users.ChangePassword
{
    
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordEndPointRequest>
    {
        public ChangePasswordValidator()
        {

            RuleFor(user => user.Email)
              .NotEmpty().WithErrorCode(ErrorCode.ThisFieldIsRequierd.ToString()).WithMessage("Email is required.")
              .EmailAddress().WithErrorCode(ErrorCode.EmailIsNotValid.ToString()).WithMessage("Invalid Email Address");

            RuleFor(user => user.newPassword)
                .NotEmpty().WithErrorCode(ErrorCode.ThisFieldIsRequierd.ToString()).WithMessage("new password is required.")
                .Matches("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,}").WithErrorCode(ErrorCode.PasswordIsNotValid.ToString()).WithMessage("Invalid Password ");

            RuleFor(user => user.ConfirmPassword)
                .NotEmpty().WithErrorCode(ErrorCode.ThisFieldIsRequierd.ToString()).WithMessage("Confirm new password is required.")
                .Equal(user => user.newPassword).WithErrorCode(ErrorCode.PasswordsDontMatch.ToString()).WithMessage("Password don't match.")
                .Matches("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,}").WithErrorCode(ErrorCode.PasswordIsNotValid.ToString()).WithMessage("Invalid Password ");

            RuleFor(user => user.oldPassword)
                .NotEmpty().WithErrorCode(ErrorCode.ThisFieldIsRequierd.ToString()).WithMessage("old password is required.")
                .Matches("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,}").WithErrorCode(ErrorCode.PasswordIsNotValid.ToString()).WithMessage("Invalid Password ");


        }
    }
}
