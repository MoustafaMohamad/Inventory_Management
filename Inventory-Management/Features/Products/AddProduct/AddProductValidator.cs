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
            RuleFor(p => p.Name).NotNull().Length(3, 15);
            RuleFor(p => p.Price).NotNull().PrecisionScale(4, 2,true);
            RuleFor(p => p.Quantity).NotNull().GreaterThan(0).GreaterThan(p => p.Threshold);
            RuleFor(p => p.Unit).NotNull();
            RuleFor(p => p.ExpiryDate).NotNull();
            RuleFor(p => p.Threshold).NotNull().GreaterThan(0);
        }
    }
  
}
