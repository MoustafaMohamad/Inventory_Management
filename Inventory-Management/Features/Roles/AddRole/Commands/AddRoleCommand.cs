using Inventory_Management.Common;
using Inventory_Management.Entities;
using Inventory_Management.Features.Roles.Common.DTO;
using MediatR;

namespace Inventory_Management.Features.Roles.AddRole.Commands
{
    public record AddRoleCommand (string Name):IRequest<bool>;

    public class AddRoleCommandHandeler : BaseRequestHandler<Role, AddRoleCommand, bool>
    {
        public AddRoleCommandHandeler(RequestParameters<Role> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<bool> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(new Role
            {
                Name = request.Name
            });

            return true;    
        }
    }
}
