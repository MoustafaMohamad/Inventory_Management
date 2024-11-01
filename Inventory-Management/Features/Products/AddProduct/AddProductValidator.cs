using FluentValidation;
using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers;
using Inventory_Management.Entities;
using Inventory_Management.Features.Products.AddProduct.Commands;
using Inventory_Management.Features.Users.RegisterUser.Commands;
using System;

namespace Inventory_Management.Features.Products.AddProduct
{
  
    public class AddProductValidator : AbstractValidator<AddProductEndPointRequest>
    {
        public AddProductValidator()
        {
            RuleFor(p => p.Name).NotNull().WithErrorCode(ErrorCode.ThisFieldIsRequierd.ToString()).WithMessage("This Field Is Requierd")
                .Length(3, 15);

            RuleFor(p => p.Price).NotNull().WithErrorCode(ErrorCode.ThisFieldIsRequierd.ToString()).WithMessage("This Field Is Requierd")
                .GreaterThan(0).WithErrorCode(ErrorCode.GreaterThan0.ToString()).WithMessage("Price should be more than 0")
                .PrecisionScale(4, 2,true);

            RuleFor(p => p.Quantity).NotNull().WithErrorCode(ErrorCode.ThisFieldIsRequierd.ToString()).WithMessage("This Field Is Requierd")
                .GreaterThan(0).WithErrorCode(ErrorCode.GreaterThan0.ToString()).WithMessage("Quantity should be more than 0")
                .GreaterThan(p => p.Threshold).WithErrorCode(ErrorCode.GreaterThanThreshold.ToString()).WithMessage("Quantity should be more than Threshold");

            RuleFor(p => p.Unit).NotNull().WithErrorCode(ErrorCode.ThisFieldIsRequierd.ToString()).WithMessage("This Field Is Requierd");

            RuleFor(p => p.ExpiryDate).NotNull().WithErrorCode(ErrorCode.ThisFieldIsRequierd.ToString()).WithMessage("This Field Is Requierd");

            RuleFor(p => p.Threshold).NotNull().WithErrorCode(ErrorCode.ThisFieldIsRequierd.ToString()).WithMessage("This Field Is Requierd")
                .GreaterThan(0).WithErrorCode(ErrorCode.GreaterThan0.ToString()).WithMessage("Threshold should be more than 0");
        }
    }
  
}
