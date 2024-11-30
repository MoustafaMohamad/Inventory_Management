using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers.ResultViewModel;
using Inventory_Management.Common.Helpers;
using Inventory_Management.Features.Products.GetAllProducts.Queries;
using Inventory_Management.Features.Products.GetAllProducts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Inventory_Management.Features.Categories.GetAllCategories.Queries;
using Common.Helpers;
using Microsoft.AspNetCore.SignalR;
using Azure.Core;
using Inventory_Management.Entities;

namespace Inventory_Management.Features.Categories.GetAllCategories
{
    [ApiController]
    [Route("api/categories")]
    public class GetAllCategoriesEndPoint:ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHubContext<SingleRNotificationHub> _hubContext;
        public GetAllCategoriesEndPoint(IMediator mediator, IHubContext<SingleRNotificationHub> hubContext)
        {
            _mediator = mediator;
            _hubContext = hubContext;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            var result = await _mediator.Send(new GetAllCategoriesQuery());

            if (!result.IsSuccess)
            {
                throw new BusinessException(result.ErrorCode, result.Message);
            }
            var mappedCategories = result.Data.AsQueryable().Map<GetAllCategoriesEndPointResponse>().ToList();
            await _hubContext.Clients.All.SendAsync("LowStockMessage", 10, "Product one", 15);

            return Ok(ResultViewModel.Sucess(mappedCategories));
        }

    }
}
