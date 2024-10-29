using FluentEmail.Core;
using Inventory_Management.Common;
using Inventory_Management.CQRS.OTP.Commands;
using Inventory_Management.CQRS.OTP.Queries;
using Inventory_Management.Entities;
using MediatR;
using static System.Net.WebRequestMethods;

namespace Inventory_Management.CQRS.OTP.Orchestore
{
  




    public record Validate_Otp_Orchestrator(string email, string otp) : IRequest<bool>;

    public class Validate_Otp_OrchestratorHandler : BaseRequestHandler<OtpVerification, Validate_Otp_Orchestrator, bool>
    {
        public Validate_Otp_OrchestratorHandler(RequestParameters<OtpVerification> requestParameters) : base(requestParameters)
        {
        }
        public async override Task<bool> Handle(Validate_Otp_Orchestrator request, CancellationToken cancellationToken)
        {
            var otpEntry = await _mediator.Send(new GetOtpQuery( request.email, request.otp));

            if (otpEntry == null || otpEntry.ExpiryTime < DateTime.UtcNow)
            {
                return false;
            }

            await _mediator.Send(new RemoveOtpCommand(otpEntry));

            _repository.SaveChanges();
            return true;
        }

       
    }





}
