using FluentEmail.Core;
using Inventory_Management.Common;
using Inventory_Management.Entities;
using MediatR;
using static System.Net.WebRequestMethods;

namespace Inventory_Management.CQRS.OTP.Commands
{





    
        public record RemoveOtpCommand( OtpVerification otp) : IRequest<bool>;

        public class RemoveOtpCommandHandler : BaseRequestHandler<OtpVerification, RemoveOtpCommand, bool>
        {
        public RemoveOtpCommandHandler(RequestParameters<OtpVerification> requestParameters) : base(requestParameters)
        {
        }

        public override Task<bool> Handle(RemoveOtpCommand request, CancellationToken cancellationToken)
        {

            _repository.Delete(request.otp);

            //_repository.SaveChanges();
            return Task.FromResult(true);
        }
    }
    
}
