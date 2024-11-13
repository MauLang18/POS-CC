using AutoMapper;
using POS.Application.Commons.Select.Response;
using POS.Application.Dtos.VoucherType.Response;
using POS.Application.UseCases.VoucherType.Commands.CreateCommand;
using POS.Application.UseCases.VoucherType.Commands.UpdateCommand;
using POS.Domain.Entities;
using POS.Utilities.Static;

namespace POS.Application.Mappings;

public class VoucherTypeMapping : Profile
{
    public VoucherTypeMapping()
    {
        CreateMap<VoucherType, VoucherTypeResponseDto>()
            .ForMember(x => x.VoucherTypeId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.StateVoucherType, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Activo) ? "ACTIVO" : "INACTIVO"))
            .ReverseMap();

        CreateMap<VoucherType, SelectResponse>()
            .ForMember(x => x.Id, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.Description, x => x.MapFrom(y => y.Name))
            .ReverseMap();

        CreateMap<VoucherType, VoucherTypeByIdResponseDto>()
            .ForMember(x => x.VoucherTypeId, x => x.MapFrom(y => y.Id))
            .ReverseMap();

        CreateMap<CreateVoucherTypeCommand, VoucherType>();

        CreateMap<UpdateVoucherTypeCommand, VoucherType>();
    }
}