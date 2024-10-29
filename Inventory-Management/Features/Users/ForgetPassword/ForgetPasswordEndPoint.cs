
using Common.Helpers;
using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers.ResultViewModel;
using Inventory_Management.Features.Users.ForgetPassword.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management.Features.Users.ForgetPassword
{
    [ApiController]
    [Route("[controller]")]
    public class ForgetPasswordEndPoint :ControllerBase
    {
        private readonly IMediator _mediator;
        public ForgetPasswordEndPoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPasswordAsync(ForgetPasswordEndPointRequest request)
        {
            var result =await  _mediator.Send(request.MapOne<ForgetPasswordCommand>());
            if (!result.IsSuccess)
            {
                throw new BusinessException(result.ErrorCode,result.Message);
            }
            return Ok(ResultViewModel.Sucess(result.Data));
        }
    }
}
