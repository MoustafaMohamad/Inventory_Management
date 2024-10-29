using Inventory_Management.Common.Helpers;
using Inventory_Management.Common;
using Inventory_Management.Entities;
using MediatR;

namespace Inventory_Management.Features.Otps.Commands
{
    public record StoreOtpCommand(string Email, string Otp) : IRequest<ResultDto<bool>>;

    public class StoreOtpCommandHandler : BaseRequestHandler<OtpVerification, StoreOtpCommand, ResultDto<bool>>
    {
        public StoreOtpCommandHandler(RequestParameters<OtpVerification> requestParameters) : base(requestParameters)
        {
        }
        public async override Task<ResultDto<bool>> Handle(StoreOtpCommand request, CancellationToken cancellationToken)
        {
            var otpVerification = new OtpVerification
            {
                Email = request.Email,
                Otp = request.Otp,
                ExpiryTime = DateTime.UtcNow.AddMinutes(15)
            };
            await _repository.AddAsync(otpVerification);
            return ResultDto<bool>.Sucess(true);
        }
    }

}
