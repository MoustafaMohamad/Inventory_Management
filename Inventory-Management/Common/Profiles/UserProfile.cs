using AutoMapper;
using Inventory_Management.Entities;
using Inventory_Management.Features.Common.Users.Dto;


namespace Inventory_Management.Common.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            //CreateMap<ForgetPasswordEndPointRequest, ForgetPasswordCommand>();
            CreateMap<UserDto,User>().ReverseMap();
            //CreateMap<ResetPasswordEndPointRequest, ResetPasswordCommand>();
        }
    }
}
