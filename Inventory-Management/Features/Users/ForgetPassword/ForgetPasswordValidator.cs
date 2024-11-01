using FluentValidation;
using Inventory_Management.Common.Exceptions;

namespace Inventory_Management.Features.Users.ForgetPassword
{

    public class ForgetPasswordValidator : AbstractValidator<ForgetPasswordEndPointRequest>
    {
        public ForgetPasswordValidator()
        {

            RuleFor(user => user.Email)
              .NotEmpty().WithErrorCode(ErrorCode.ThisFieldIsRequierd.ToString()).WithMessage("Email is required.")
              .EmailAddress().WithErrorCode(ErrorCode.EmailIsNotValid.ToString()).WithMessage("Invalid Email Address");

        }
    }
}
