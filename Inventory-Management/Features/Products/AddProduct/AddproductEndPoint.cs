using Common.Helpers;
using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers;
using Inventory_Management.Common.Helpers.ResultViewModel;
using Inventory_Management.Features.Products.AddProduct.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management.Features.Products.AddProduct
{
    [ApiController]
    [Route("api/Products/add-product")]
    public class AddproductEndPoint:ControllerBase
    {
        private readonly IMediator _mediator;

        private readonly AddProductValidator _productValidator;
        public AddproductEndPoint(IMediator mediator, AddProductValidator productValidator)
        {
             _mediator =mediator;
           _productValidator = productValidator;
        }
        [HttpPost]
       // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddProductAsync([FromForm] AddProductEndPointRequest request)
        {
            #region Validation
            var validationResults = _productValidator.Validate(request);

            if (!validationResults.IsValid)
            {

                foreach (var error in validationResults.Errors)
                {
                    Console.WriteLine(error.ErrorCode);
                    ErrorCode errorCode = (ErrorCode)Enum.Parse(typeof(ErrorCode), error.ErrorCode);
                    throw new BusinessException(errorCode, error.ErrorMessage);
                }
            }
            #endregion 
            var result = await _mediator.Send(request.MapOne<AddProductCommand>());
            if (!result.IsSuccess)
            {
                throw new BusinessException(result.ErrorCode,result.Message);
            }
            return Ok(ResultViewModel.Sucess(result.Data, result.Message));  
        }
    }
}
