using AutoMapper;
using POS.Application.Dtos.Purchase.Response;
using POS.Application.UseCases.Purchase.Commands.CreateCommand;
using POS.Domain.Entities;

namespace POS.Application.Mappings;

public class PurchaseMapping : Profile
{
    public PurchaseMapping()
    {
        CreateMap<Purchase, PurchaseResponseDto>()
            .ForMember(x => x.PurchaseId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.Supplier, x => x.MapFrom(y => y.Supplier.Name))
            .ReverseMap();

        CreateMap<Purchase, PurchaseByIdResponseDto>()
            .ForMember(x => x.PurchaseId, x => x.MapFrom(y => y.Id))
            .ReverseMap();

        CreateMap<PurchaseDetail, PurchaseDetailsByIdResponseDto>()
            .ForMember(x => x.ProductServiceId, x => x.MapFrom(y => y.ProductServiceId))
            .ForMember(x => x.Code, x => x.MapFrom(y => y.ProductService.Code))
            .ForMember(x => x.Name, x => x.MapFrom(y => y.ProductService.Name))
            .ForMember(x => x.Image, x => x.MapFrom(y => y.ProductService.Image))
            .ReverseMap();

        CreateMap<CreatePurchaseCommand, Purchase>();

        CreateMap<CreatePurchaseDetailCommand, PurchaseDetail>();
    }
}