using AutoMapper;
using Inventory_Management.Entities;
using Inventory_Management.Features.Reports.TransactionHistoryReport;
using Inventory_Management.Features.Reports.TransactionHistoryReport.Dtos;
using Inventory_Management.Features.Reports.TransactionHistoryReport.Queries;

namespace Inventory_Management.Common.Profiles
{
    public class TransactionProfile:Profile
    {
        public TransactionProfile()
        {

            CreateMap<TransactionHistoryReportEndPointRequest, TransactionHistoryReportQuery>();
            CreateMap<InventoryTransaction, TransactionReportDto>()
                .ForMember(dst=>dst.UserName,opt=>opt.MapFrom(src=>src.User.UserName))
                .ForMember(dst => dst.ProductName, opt => opt.MapFrom(src => src.Product.Name)); ;
            CreateMap<TransactionReportDto, TransactionHistoryReportEndPointResponse>();
        }
    }
}
