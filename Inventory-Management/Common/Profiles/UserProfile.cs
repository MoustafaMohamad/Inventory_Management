using AutoMapper;
using Inventory_Management.Features.Users.ForgetPassword;
using Inventory_Management.Features.Users.ForgetPassword.Commands;

namespace Inventory_Management.Common.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<ForgetPasswordEndPointRequest, ForgetPasswordCommand>();
        }
    }
}
