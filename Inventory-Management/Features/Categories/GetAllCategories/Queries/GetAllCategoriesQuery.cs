
using Common.Helpers;
using Inventory_Management.Common;
using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers;
using Inventory_Management.Entities;
using Inventory_Management.Features.Categories.GetAllCategories.Dtos;
using MediatR;

namespace Inventory_Management.Features.Categories.GetAllCategories.Queries
{
    public record GetAllCategoriesQuery() :IRequest<ResultDto<IEnumerable<CategoryDto>>>;
    public class GetAllCategoriesQueryHandler : BaseRequestHandler<Category, GetAllCategoriesQuery, ResultDto<IEnumerable<CategoryDto>>>
    {
        public GetAllCategoriesQueryHandler(RequestParameters<Category> requestParameters, ICloudinaryService cloudinaryService) : base(requestParameters)
        {
            
        }
        public async override Task<ResultDto<IEnumerable<CategoryDto>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categoriesQuery =  await _repository.GetAll();
            if (!categoriesQuery.Any()) 
            {
                return ResultDto<IEnumerable<CategoryDto>>.Faliure(ErrorCode.NoCategoriesFound, "No Categories Found");
            }
            var categories = categoriesQuery.Map<CategoryDto>().ToList();
            return ResultDto<IEnumerable<CategoryDto>>.Sucess(categories);
        }
    }
}
