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

            CreateMap<ProductDto, Product>();
            CreateMap<ProductDto, GetProductByIdEndPointResponse>();
                //.ForMember(dst => dst.Available,
                //opt => opt.MapFrom(src => src.Available.ToString()));
            CreateMap<ProductDto, GetAllProductsEndPointResponse>();
               
            CreateMap<UpdateProductEndPointRequest, UpdateProductCommand>();
            CreateMap<UpdateProductCommand, Product>();
            CreateMap<Product, ProductDto>().ForMember(dst => dst.CategoryName,
                opt => opt.MapFrom(src => src.Category.Name)) .ForMember(dst => dst.Available,
                opt => opt.MapFrom(src => src.Available.ToString()));
            CreateMap<ProductDto, ProductReportDto>();
            CreateMap<ProductReportDto, LowStockReportEndPointResponse>();
        }
}
}
