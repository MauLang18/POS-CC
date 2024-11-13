using AutoMapper;
using POS.Application.Commons.Select.Response;
using POS.Application.Dtos.ProductService.Response;
using POS.Application.UseCases.ProductService.Commands.CreateCommand;
using POS.Application.UseCases.ProductService.Commands.UpdateCommand;
using POS.Domain.Entities;
using POS.Utilities.Static;

namespace POS.Application.Mappings;

public class ProductServiceMapping : Profile
{
    public ProductServiceMapping()
    {
        CreateMap<ProductService, ProductServiceResponseDto>()
            .ForMember(x => x.ProductServiceId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.Category, x => x.MapFrom(y => y.Category.Name))
            .ForMember(x => x.Unit, x => x.MapFrom(y => y.Unit.Abbreviation))
            .ForMember(x => x.Service, x => x.MapFrom(y => y.IsService.Equals((int)ServiceType.Producto) ? "PRODUCTO" : "SERVICIO"))
            .ForMember(x => x.StateProductService, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Activo) ? "ACTIVO" : "INACTIVO"))
            .ReverseMap();

        CreateMap<ProductService, SelectResponse>()
            .ForMember(x => x.Id, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.Description, x => x.MapFrom(y => y.Name))
            .ReverseMap();

        CreateMap<ProductService, ProductServiceByIdResponseDto>()
            .ForMember(x => x.ProductServiceId, x => x.MapFrom(y => y.Id))
            .ReverseMap();

        CreateMap<CreateProductServiceCommand, ProductService>();

        CreateMap<UpdateProductServiceCommand, ProductService>();
    }
}