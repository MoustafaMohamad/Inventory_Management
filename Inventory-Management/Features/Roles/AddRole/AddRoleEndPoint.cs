using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management.Features.Roles.AddRole
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AddRoleEndPoint : ControllerBase 
    {
        private readonly IMediator _mediator;

        public AddRoleEndPoint(IMediator mediator )
        {
            _mediator = mediator;
        }


       

    }
}
