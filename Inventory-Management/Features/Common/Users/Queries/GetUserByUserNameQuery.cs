using Common.Helpers;
using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers;
using Inventory_Management.Common;
using Inventory_Management.Entities;
using Inventory_Management.Features.Common.Users.Dto;
using MediatR;

namespace Inventory_Management.Features.Common.Users.Queries
{
    public record GetUserByUserNameQuery(string UserName) : IRequest<ResultDto<UserDto>>;

    public class GetUserByUserNameQueryHandler : BaseRequestHandler<User, GetUserByUserNameQuery, ResultDto<UserDto>>
    {
        public GetUserByUserNameQueryHandler(RequestParameters<User> requestParameters) : base(requestParameters)
        {

        }


        public override async Task<ResultDto<UserDto>> Handle(GetUserByUserNameQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.FirstOrDefaultAsync(u => u.UserName == request.UserName);
            if (user is null)
            {
                return ResultDto<UserDto>.Faliure(ErrorCode.NotFound, "User Name is not Found");
            }

            return ResultDto<UserDto>.Sucess(user.MapOne<UserDto>());
        }
    }
}
