using FluentValidation;
using Inventory_Management.Common.Exceptions;

namespace Inventory_Management.Features.Products.AddProduct
{

    public class AddProductValidator : AbstractValidator<AddProductEndPointRequest>
    {
        public AddProductValidator()
        {
            RuleFor(p => p.Name)
                .NotNull().WithErrorCode(ErrorCode.ThisFieldIsRequierd.ToString()).WithMessage("This Field Is Requierd")
                .Length(3, 20).WithErrorCode(ErrorCode.ProductNameLength.ToString()).WithMessage("The Name should be between 3 & 15 char");

            RuleFor(p => p.Price)
                .NotNull().WithErrorCode(ErrorCode.ThisFieldIsRequierd.ToString()).WithMessage("This Field Is Requierd")
                .GreaterThan(0).WithErrorCode(ErrorCode.GreaterThan0.ToString()).WithMessage("Price should be more than 0");
              

            RuleFor(p => p.Quantity)
                .NotNull().WithErrorCode(ErrorCode.ThisFieldIsRequierd.ToString()).WithMessage("This Field Is Requierd")
                .GreaterThan(0).WithErrorCode(ErrorCode.GreaterThan0.ToString()).WithMessage("Quantity should be more than 0")
                .GreaterThan(p => p.Threshold).WithErrorCode(ErrorCode.GreaterThanThreshold.ToString()).WithMessage("Quantity should be more than Threshold");

            RuleFor(p => p.Unit)
                .NotNull().WithErrorCode(ErrorCode.ThisFieldIsRequierd.ToString()).WithMessage("This Field Is Requierd");

            RuleFor(p => p.ExpiryDate)
                .NotNull().WithErrorCode(ErrorCode.ThisFieldIsRequierd.ToString()).WithMessage("This Field Is Requierd");

            RuleFor(p => p.Threshold)
                .NotNull().WithErrorCode(ErrorCode.ThisFieldIsRequierd.ToString()).WithMessage("This Field Is Requierd")
                .GreaterThan(0).WithErrorCode(ErrorCode.GreaterThan0.ToString()).WithMessage("Threshold should be more than 0");
        }
    }
  
}
