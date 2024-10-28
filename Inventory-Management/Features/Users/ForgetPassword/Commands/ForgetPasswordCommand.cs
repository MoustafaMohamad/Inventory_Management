using Common.Helpers;
using Inventory_Management.Common;
using Inventory_Management.Common.Helpers;
using Inventory_Management.Entities;
using Inventory_Management.Features.Common.Users.Queries;
using MediatR;

namespace Inventory_Management.Features.Users.ForgetPassword.Commands
{
    public record ForgetPasswordCommand(string Email) : IRequest<ResultDto<bool> >;
    public class ForgetPasswordCommandhandler : BaseRequestHandler<User, ForgetPasswordCommand, ResultDto<bool>>
    {
        
        public ForgetPasswordCommandhandler(RequestParameters<User> requestParameters) : base(requestParameters)
        {
           
        }
        public async override Task<ResultDto<bool>> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            var userResult = await _mediator.Send(new GetUserByEmailQuery(request.Email));
            if (!userResult.IsSuccess)
            {
                return ResultDto<bool>.Faliure(userResult.ErrorCode, userResult.Message);
            }
            var user = userResult.Data.MapOne<User>();     
            var otpCode = OTPGenerator.GenerateOTP();
            user.OtpCode = otpCode;
            user.OtpExpiry = DateTime.UtcNow.AddMinutes(15);
           await  _repository.Update(user);
           await  _repository.SaveChanges();
            ////send email with otp
            //EmailHelper.SendEmail(user.Email, "OTP", otpCode);
            return ResultDto<bool>.Sucess(true, "OTP Had been sent to your Email");
        }
    }
}
