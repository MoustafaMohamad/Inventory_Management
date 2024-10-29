using Inventory_Management.Common.Helpers;
using Inventory_Management.Common;
using Inventory_Management.Entities;
using Inventory_Management.Features.Common.Users.Queries;
using MediatR;


namespace Inventory_Management.Features.Users.ResetPassword.Commands
{
    public record ResetPasswordCommand(string Email,string Otp,string oldPassword,string newPassword,string ConfirmPassword) : IRequest<ResultDto>;
    public class ResetPasswordCommandhandler : BaseRequestHandler<User, ResetPasswordCommand, ResultDto>
    {

        public ResetPasswordCommandhandler(RequestParameters<User> requestParameters) : base(requestParameters)
        {

        }
        public async override Task<ResultDto> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var userResult = await _mediator.Send(new GetUserByEmailQuery(request.Email));
            if (!userResult.IsSuccess)
            {
                return ResultDto.Faliure(userResult.ErrorCode, userResult.Message);
            }
            var user = userResult.Data;
            var oldPasswordCheckResult = BCrypt.Net.BCrypt.Verify(request.oldPassword, user.password);
           
            var otpCode = OTPGenerator.GenerateOTP();
            user.OtpCode = otpCode;
            user.OtpExpiry = DateTime.UtcNow.AddMinutes(15);
            _repository.Update(user);
            _repository.SaveChanges();
            ////send email with otp
            //EmailHelper.SendEmail(user.Email, "OTP", otpCode);
            return ResultDto.Sucess(true, "OTP Had been sent to your Email");
        }
    }
}
