//using Inventory_Management.Common;
//using Inventory_Management.CQRS.Email.Command;
//using Inventory_Management.CQRS.OTP.Commands;
//using Inventory_Management.CQRS.OTP.Queries;
//using Inventory_Management.CQRS.Users.Commands;
//using Inventory_Management.Entities;
//using MediatR;
//using Microsoft.AspNetCore.Identity;

//namespace Inventory_Management.CQRS.Users.Orchestrator
//{
    




//    public record ConfirmUserOrchestrator(string email, string password) : IRequest<bool>;

//    public class ConfirmUserOrchestratorHandler : BaseRequestHandler<OtpVerification, ConfirmUserOrchestrator, bool>
//    {
//        public ConfirmUserOrchestratorHandler(RequestParameters<OtpVerification> requestParameters) : base(requestParameters)
//        {
//        }

//        public async override Task<bool> Handle(ConfirmUserOrchestrator request, CancellationToken cancellationToken)
//        {
//            var user = await _mediator.Send(new EmailExistsQuery(request.email));

//            if (user == null)
//            {
//                return false;
//            }
//            var check_password=user.Password.Equals(request.password);
//            if (check_password==false)
//            {
//                return false;
//            }
//            user.IsActive = true;

//            _repository.SaveChanges();
//            return true;
//        }


//    }

//}
