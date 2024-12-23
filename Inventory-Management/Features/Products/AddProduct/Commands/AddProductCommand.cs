﻿using Common.Helpers;
using Inventory_Management.Common;
using Inventory_Management.Common.Enums;
using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers;
using Inventory_Management.Entities;
using Inventory_Management.Features.Common.Categories.Queries;
using MediatR;

namespace Inventory_Management.Features.Products.AddProduct.Commands
{
    public record AddProductCommand(string Name, int CategoryID,
        int Quantity, decimal Price, int Threshold,
        DateTime ExpiryDate, string Unit,
        IFormFile Image
        ) : IRequest<ResultDto<int>>;

    public class AddproductCommandHandler : BaseRequestHandler<Product, AddProductCommand, ResultDto<int>> 
    {
        private readonly ICloudinaryService _cloudinaryService;
        public AddproductCommandHandler(RequestParameters<Product> requestParameters, ICloudinaryService cloudinaryService) : base(requestParameters)
        {
            _cloudinaryService = cloudinaryService;
        }
        public async override Task <ResultDto<int>> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var categoryExistResult = await _mediator.Send(new IsCategoryExistQuery(request.CategoryID));
            if (!categoryExistResult.IsSuccess) 
            {
                return ResultDto<int>.Faliure(categoryExistResult.ErrorCode,categoryExistResult.Message);
            }
            if (request.ExpiryDate <= DateTime.UtcNow)
            {
                return ResultDto<int>.Faliure(ErrorCode.InvalidExpirtDate, "Invalid Expiry Date");
            }
             
            var imageUploadResult =await _cloudinaryService.UploadImageAsync(request.Image);
            var imageUrl = imageUploadResult.Url.ToString();
            var product =  request.MapOne<Product>();
            product.Name = request.Name;
            product.CategoryID = request.CategoryID;
            product.Quantity = request.Quantity;
            product.Price = request.Price;
            product.Threshold = request.Threshold;
            product.ExpiryDate = request.ExpiryDate;
            product.Unit = request.Unit;
            product.ImageUrl = imageUrl;
            product.Available = productAvailability.InStock;
            product.CreatedAt = DateTime.UtcNow;

            var addedProduct = await _repository.AddAsync(product);
            await _repository.SaveChanges();
            return ResultDto<int>.Sucess(addedProduct.ID);
        }
    }

}
