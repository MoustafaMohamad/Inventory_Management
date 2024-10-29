using FluentEmail.Core;
using Inventory_Management.Common;
using Inventory_Management.Entities;
using MediatR;
using static System.Net.WebRequestMethods;

namespace Inventory_Management.CQRS.OTP.Commands
{





    
        public record StoreOtpCommand(string Email, string Otp, string Password) : IRequest<bool>;

        public class StoreOtpCommandHandler : BaseRequestHandler<OtpVerification, StoreOtpCommand, bool>
        {
        public StoreOtpCommandHandler(RequestParameters<OtpVerification> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<bool> Handle(StoreOtpCommand request, CancellationToken cancellationToken)
        {
            
             var otpVerification = new OtpVerification
             {
                 Email = request.Email,         // Use `Email` instead of `email`
                 PasswordHash = request.Password, // Use `Password` instead of `password`
                 Otp = request.Otp,               // Use `Otp` instead of `otp`
                 ExpiryTime = DateTime.UtcNow.AddMinutes(15) // OTP expires after 15 minutes
             };

           await _repository.AddAsync(otpVerification);

            //_repository.SaveChanges();

            return true;
        }
    }
    
}
