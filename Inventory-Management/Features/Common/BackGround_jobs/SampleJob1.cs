using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers.ResultViewModel;
using Inventory_Management.Features.Products.GetAllProducts.Queries;
using Inventory_Management.Features.Products.GetAllProducts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Inventory_Management.Entities;
using Common.Helpers;
using Inventory_Management.CQRS.Email.Command;
using Inventory_Management.Features.Products.GetProductDetails.Dtos;

namespace Inventory_Management.Features.Common.BackGround_jobs
{
    public class SampleJob1
    {




        private readonly IMediator _mediator;
        public SampleJob1(IMediator mediator)
        {
            _mediator = mediator;
        }


        public async Task ExecuteJob()
        {
            var result = await _mediator.Send(new GetAllProductsQuery());
            //IEnumerable<ProductDto>  result=[];

            if (!result.IsSuccess)
            {
                throw new BusinessException(result.ErrorCode, result.Message);
            }

            var mappedProducts = result.Data.AsQueryable().ToList();

            List<Product> products = new List<Product>();
            string message = null;
            foreach (var product in mappedProducts)
            {
                if (product.Quantity <= product.Threshold)
                {
                    products.Add(product.MapOne<Product>());
                    message += $"Product={product.Name}     quantity={product.Quantity}   threshold={product.Threshold}  \n";
                }
            }

            
            await _mediator.Send(new SendEmailCommand("mohamed.salah1999147@gmail.com", "Products low quantity", message));

            //var mappedProducts = result.Data.AsQueryable().Map<GetAllProductsEndPointResponse>().ToList();
            //return Ok(ResultViewModel.Sucess(mappedProducts, result.Message));
        }

    }
}
