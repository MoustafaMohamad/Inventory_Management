﻿using Common.Helpers;
using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers.ResultViewModel;
using Inventory_Management.Features.Users.ResetPassword.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management.Features.Users.ResetPassword
{
    [ApiController]
    [Route("[controller]")]
    public class ResetPasswordEndPoint : ControllerBase
    {
        private readonly IMediator _mediator;
        public ResetPasswordEndPoint(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IActionResult> ForgetPasswordAsync(ResetPasswordEndPointRequest request)
        {
            var result = await _mediator.Send(request.MapOne<ResetPasswordCommand>());
            if (!result.IsSuccess)
            {
                throw new BusinessException(result.ErrorCode, result.Message);
            }
            return Ok(ResultViewModel.Sucess(result.Data));
        }
    }
}