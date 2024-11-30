
using Common.Helpers;
using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers.ResultViewModel;
using Inventory_Management.Features.Users.ForgetPassword.Orchestrators;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management.Features.Users.ForgetPassword
{
    [ApiController]
    [Route("api/users/forgetpassword")]
    public class ForgetPasswordEndPoint :ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ForgetPasswordValidator _validator;
        public ForgetPasswordEndPoint(IMediator mediator, ForgetPasswordValidator validator)
        {
            _mediator = mediator;
            _validator = validator;
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPasswordAsync([FromBody] ForgetPasswordEndPointRequest request)
        {
            #region Validation
            var validationResults = _validator.Validate(request);

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
            var result =await  _mediator.Send(request.MapOne<ForgetPasswordOrchestrator>());
            if (!result.IsSuccess)
            {
                throw new BusinessException(result.ErrorCode,result.Message);
            }
            return Ok(ResultViewModel.Sucess(result.Data));
        }
    }
}
