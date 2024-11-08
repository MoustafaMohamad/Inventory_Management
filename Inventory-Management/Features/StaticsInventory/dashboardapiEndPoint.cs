
using Common.Helpers;
using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers.ResultViewModel;
using Inventory_Management.Features.StaticsInventory;
using Inventory_Management.Features.StaticsInventory.Number_Categories.Queries;
using Inventory_Management.Features.StaticsInventory.Number_LowStock.Queries;
using Inventory_Management.Features.StaticsInventory.Number_NoStock.Queries;
using Inventory_Management.Features.StaticsInventory.Top_Selling.Queries;
using Inventory_Management.Features.StaticsInventory.Total_Product.Queries;
using Inventory_Management.Features.Users.ForgetPassword.Orchestrators;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sprache;

namespace Inventory_Management.Features.Users.ForgetPassword
{
    [ApiController]
    [Route("api/users/dashboardapi")]
    public class dashboardapiEndPoint :ControllerBase
    {
        private readonly IMediator _mediator;
        //private readonly ForgetPasswordValidator _validator;
        public dashboardapiEndPoint(IMediator mediator)
        {
            _mediator = mediator;
           // _validator = validator;
        }

        [HttpPost]
        public async Task<IActionResult> GetDasboarddataAsync()
        {
            
            var TopSelling = await _mediator.Send(new TopSellingQuery());
            if (!TopSelling.IsSuccess)
            {
                throw new BusinessException(TopSelling.ErrorCode, TopSelling.Message);
            }
            var Number_NoStock = await _mediator.Send(new Number_NoStockQuery());
            if (Number_NoStock.Data == null)
            {
                Number_NoStock.Data = 0;
            }

            var Number_LowStock = await _mediator.Send(new Number_LowStockQuery());
            if (Number_LowStock.Data == null)
            {
                Number_LowStock.Data = 0;
            }
            var Number_Categories = await _mediator.Send(new Number_CategoriesQuery());
            if (Number_Categories.Data == null)
            {
                Number_Categories.Data = 0;
            }





            var Total_Product = await _mediator.Send(new Total_ProductQuery());
            if (Total_Product.Data == null)
            {
                Total_Product.Data = 0;
            }

            var Total_Revenue = await _mediator.Send(new Total_RevenueQuery());
            if (Total_Revenue.Data==null)
            {
                Total_Revenue.Data = 0;
            }




            Dasboard_dataEndPointRequest result = new Dasboard_dataEndPointRequest() { 
                Number_Categories= Number_Categories.Data,
                num_total_product= Total_Product.Data,
                Number_LowStock= Number_LowStock.Data,
                Number_NoStock= Number_NoStock.Data,
                Top_Selling= TopSelling.Data,
                totalRevenue= Total_Revenue.Data,

            };

            return Ok(ResultViewModel.Sucess(result));
        }
    }
}
