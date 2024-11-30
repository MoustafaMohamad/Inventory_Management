using Common.Helpers;
using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers.ResultViewModel;
using Inventory_Management.Features.Users.RegisterUser.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management.Features.Users.RegisterUser
{

    [ApiController]
    [Route("api/users/register")]
    public class RegisterUserEndPoint : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly RegisterUserValidator _validator;
        public RegisterUserEndPoint(IMediator mediator, RegisterUserValidator validator)
        {
            _mediator = mediator;
            _validator = validator;
        }
        [HttpPost]
        public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterUserEndPointRequest request)
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
            var result = await _mediator.Send(request.MapOne<RegisterUserCommand>());
            if (!result.IsSuccess)
            {
                throw new BusinessException(result.ErrorCode, result.Message);
            }
            return Ok(ResultViewModel.Sucess(result.Data, result.Message));
        }
    }
}
