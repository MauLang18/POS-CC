using AutoMapper;
using POS.Application.Commons.Select.Response;
using POS.Application.Dtos.Supplier.Response;
using POS.Application.UseCases.Supplier.Commands.CreateCommand;
using POS.Application.UseCases.Supplier.Commands.UpdateCommand;
using POS.Domain.Entities;
using POS.Utilities.Static;

namespace POS.Application.Mappings;

public class SupplierMapping : Profile
{
    public SupplierMapping()
    {
        CreateMap<Supplier, SupplierResponseDto>()
            .ForMember(x => x.SupplierId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.DocumentType, x => x.MapFrom(y => y.DocumentType.Abbreviation))
            .ForMember(x => x.StateSupplier, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Activo) ? "ACTIVO" : "INACTIVO"))
            .ReverseMap();

        CreateMap<Supplier, SelectResponse>()
            .ForMember(x => x.Id, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.Description, x => x.MapFrom(y => y.Name))
            .ReverseMap();

        CreateMap<Supplier, SupplierByIdResponseDto>()
            .ForMember(x => x.SupplierId, x => x.MapFrom(y => y.Id))
            .ReverseMap();

        CreateMap<CreateSupplierCommand, Supplier>();

        CreateMap<UpdateSupplierCommand, Supplier>();
    }
}