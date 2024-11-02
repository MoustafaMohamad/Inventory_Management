using Common.Helpers;
using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers;
using Inventory_Management.Common;
using Inventory_Management.Entities;
using Inventory_Management.Features.Common.Users.Dto;
using MediatR;

namespace Inventory_Management.Features.Common.Users.Queries
{
    public record GetUserByUserNameQuery(string UserName) : IRequest<ResultDto<GetUserDTO>>;

    public class GetUserByUserNameQueryHandler : BaseRequestHandler<User, GetUserByUserNameQuery, ResultDto<GetUserDTO>>
    {
        public GetUserByUserNameQueryHandler(RequestParameters<User> requestParameters) : base(requestParameters)
        {

        }


        public override async Task<ResultDto<GetUserDTO>> Handle(GetUserByUserNameQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.FirstOrDefaultAsync(u => u.UserName == request.UserName);
            if (user is null)
            {
                return ResultDto<GetUserDTO>.Faliure(ErrorCode.NotFound, "User Name is not Found");
            }

            return ResultDto<GetUserDTO>.Sucess(user.MapOne<GetUserDTO>());
        }
    }
}
