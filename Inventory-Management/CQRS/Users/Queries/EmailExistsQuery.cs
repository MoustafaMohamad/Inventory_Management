using Inventory_Management.Common;
using Inventory_Management.CQRS.OTP.Commands;
using Inventory_Management.Entities;
using MediatR;

namespace Inventory_Management.CQRS.Email.Command
{


    public record EmailExistsQuery(string Email) : IRequest<User>;

    public class EmailExistsQueryHandler : BaseRequestHandler<User, EmailExistsQuery, User>
    {

        public EmailExistsQueryHandler(RequestParameters<User> requestParameters) : base(requestParameters)
        {
        }

        public override Task<User> Handle(EmailExistsQuery request, CancellationToken cancellationToken)
        {
            Task<User> check = _repository.First(o => o.Email == request.Email);
            if (check != null)
            {
                return check;
            }
            return Task.FromResult<User>(null);

        }
    }
}
