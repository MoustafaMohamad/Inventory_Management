using AutoMapper;
using Inventory_Management.Entities;
using Inventory_Management.Features.Categories.GetAllCategories;
using Inventory_Management.Features.Categories.GetAllCategories.Dtos;

namespace Inventory_Management.Common.Profiles
{
    public class CategoryProfile:Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, GetAllCategoriesEndPointResponse>();
        }
    }
}
