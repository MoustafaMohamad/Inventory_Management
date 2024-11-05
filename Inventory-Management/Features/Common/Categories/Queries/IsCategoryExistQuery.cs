using Inventory_Management.Common;
using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers;
using Inventory_Management.Entities;
using MediatR;

namespace Inventory_Management.Features.Common.Categories.Queries
{
    public record IsCategoryExistQuery(int ID):IRequest<ResultDto<bool>>;
    public class IsCategoryExistQueryHandler :BaseRequestHandler<Category, IsCategoryExistQuery, ResultDto<bool>>
    {
        public IsCategoryExistQueryHandler(RequestParameters<Category> requestParameters) : base(requestParameters)
        {
            
        }

        public async override Task<ResultDto<bool>> Handle(IsCategoryExistQuery request, CancellationToken cancellationToken)
        {
            var categoryResult = await _repository.FirstAsync(c => c.ID == request.ID);
            if (categoryResult == null)
            {
                return ResultDto<bool>.Faliure(ErrorCode.CategoryIsNotFound, "Category Is Not Found");
            }
            return ResultDto<bool>.Sucess(true);
        }
    }
}
