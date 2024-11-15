using AutoMapper;
using POS.Application.Dtos.Sale.Response;
using POS.Application.UseCases.Sale.Commands.CreateCommand;
using POS.Domain.Entities;

namespace POS.Application.Mappings;

public class SaleMapping : Profile
{
    public SaleMapping()
    {
        CreateMap<Sale, SaleResponseDto>()
            .ForMember(x => x.SaleId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.Customer, x => x.MapFrom(y => y.Customer.Name))
            .ForMember(x => x.Status, x => x.MapFrom(y => y.Status.Name))
            .ReverseMap();

        CreateMap<Sale, SaleByIdResponseDto>()
            .ForMember(x => x.SaleId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.CustomerName, x => x.MapFrom(y => y.Customer.Name))
            .ForMember(x => x.CustomerAddress, x => x.MapFrom(y => y.Customer.Address))
            .ForMember(x => x.CustomerPhone, x => x.MapFrom(y => y.Customer.Phone))
            .ForMember(x => x.PaymentTerms, x => x.MapFrom(y => y.Customer.CreditType.Name))
            .ForMember(x => x.AuditCreateDate, x => x.MapFrom(y => y.AuditCreateDate.ToString("yyyy-MM-dd"))) // Solo fecha
            .ReverseMap();

        CreateMap<SaleDetail, SaleDetailsByIdResponseDto>()
            .ForMember(x => x.ProductServiceId, x => x.MapFrom(y => y.ProductServiceId))
            .ForMember(x => x.Code, x => x.MapFrom(y => y.ProductService.Code))
            .ForMember(x => x.Name, x => x.MapFrom(y => y.ProductService.Name))
            .ForMember(x => x.Image, x => x.MapFrom(y => y.ProductService.Image))
            .ReverseMap();

        CreateMap<CreateSaleCommand, Sale>();

        CreateMap<CreateSaleDetailCommand, SaleDetail>();
    }
}