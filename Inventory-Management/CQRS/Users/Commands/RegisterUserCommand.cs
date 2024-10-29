using Inventory_Management.Common;
using Inventory_Management.Entities;
using MediatR;

namespace Inventory_Management.CQRS.Users.Commands
{
    



    public record RegisterUserCommand(User dto) : IRequest<bool>;

    public class RegisterUserCommandHandler : BaseRequestHandler<User, RegisterUserCommand, bool>
    {
        public RegisterUserCommandHandler(RequestParameters<User> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {

            var user = new User
            {
                Email = request. dto.Email,
                Password = request.dto.Email,
                IsActive = false
            };

           await _repository.AddAsync(user);

            //_repository.SaveChanges();

            return true;
        }
    }
}
