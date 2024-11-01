using Common.Helpers;
using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers;
using Inventory_Management.Common;
using Inventory_Management.Entities;
using Inventory_Management.Features.Common.Users.Dto;
using MediatR;

namespace Inventory_Management.Features.Common.Users.Queries
{
    public record IsUserExistsByUserName(string UserName) : IRequest<bool>;

    public class IsUserExistsByUserNameQueryHandler : BaseRequestHandler<User, IsUserExistsByUserName, bool>
    {
        public IsUserExistsByUserNameQueryHandler(RequestParameters<User> requestParameters) : base(requestParameters)
        {

        }


        public override async Task<bool> Handle(IsUserExistsByUserName request, CancellationToken cancellationToken)
        {
            var user = await _repository.FirstOrDefaultAsync(u => u.UserName == request.UserName);
            if (user is null)
            {
                return false; 
            }

            return true; 
        }
    }
}
