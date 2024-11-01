using Common.Helpers;
using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers.ResultViewModel;
using Inventory_Management.Features.Users.ChangePassword.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management.Features.Users.ChangePassword
{
    [ApiController]
    [Route("api/users/changepassword")]
    public class ChangePasswordEndPoint : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ChangePasswordValidator _validator;
        public ChangePasswordEndPoint(IMediator mediator, ChangePasswordValidator validator)
        {
            _mediator = mediator;
            _validator = validator;
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordEndPointRequest request)
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
            var result = await _mediator.Send(request.MapOne<ChangePasswordCommand>());
            if (!result.IsSuccess)
            {
                throw new BusinessException(result.ErrorCode, result.Message);
            }
            return Ok(ResultViewModel.Sucess(result.Data));
        }
    }
}
