using Inventory_Management.Entities;
using Inventory_Management.Features.Products.AddProduct.Commands;
using Inventory_Management.Features.Products.AddProduct;
using AutoMapper;
using Inventory_Management.Features.Products.GetProductDetails.Dtos;
using Inventory_Management.Features.Products.GetProductDetails;
using Inventory_Management.Features.Products.GetAllProducts;
using Inventory_Management.Features.Products.UpdateProduct;
using Inventory_Management.Features.Products.UpdateProduct.Commands;
using Inventory_Management.Features.Reports.LowStockReport.Dtos;
using Inventory_Management.Features.Reports.LowStockReport;

namespace Inventory_Management.Common.Profiles
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<AddProductEndPointRequest, AddProductCommand>();
            CreateMap<AddProductCommand, Product>();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<ProductDto, GetProductByIdEndPointResponse>()
                .ForMember(dst => dst.Category,
                opt => opt.MapFrom(src => src.Category.ToString()))
                .ForMember(dst => dst.Available,
                opt => opt.MapFrom(src => src.Available.ToString()));
            CreateMap<ProductDto, GetAllProductsEndPointResponse>().ForMember(dst => dst.Category,
                opt => opt.MapFrom(src => src.Category.ToString()))
                .ForMember(dst => dst.Available,
                opt => opt.MapFrom(src => src.Available.ToString()));
            CreateMap<UpdateProductEndPointRequest, UpdateProductCommand>();
            CreateMap<UpdateProductCommand, Product>();
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, ProductReportDto>();
            CreateMap<ProductReportDto, LowStockReportEndPointResponse>();
        }
}
}
