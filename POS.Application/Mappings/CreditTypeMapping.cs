using AutoMapper;
using POS.Application.Commons.Select.Response;
using POS.Application.Dtos.CreditType.Response;
using POS.Application.UseCases.CreditType.Commands.CreateCommand;
using POS.Application.UseCases.CreditType.Commands.UpdateCommand;
using POS.Domain.Entities;
using POS.Utilities.Static;

namespace POS.Application.Mappings;

public class CreditTypeMapping : Profile
{
    public CreditTypeMapping()
    {
        CreateMap<CreditType, CreditTypeResponseDto>()
            .ForMember(x => x.CreditTypeId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.StateCreditType, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Activo) ? "ACTIVO" : "INACTIVO"))
            .ReverseMap();

        CreateMap<CreditType, SelectResponse>()
            .ForMember(x => x.Id, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.Description, x => x.MapFrom(y => y.Name))
            .ReverseMap();

        CreateMap<CreditType, CreditTypeByIdResponseDto>()
            .ForMember(x => x.CreditTypeId, x => x.MapFrom(y => y.Id))
            .ReverseMap();

        CreateMap<CreateCreditTypeCommand, CreditType>();

        CreateMap<UpdateCreditTypeCommand, CreditType>();
    }
}