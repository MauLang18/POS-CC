using AutoMapper;
using POS.Application.Commons.Select.Response;
using POS.Application.Dtos.Unit.Response;
using POS.Application.UseCases.Unit.Commands.CreateCommand;
using POS.Application.UseCases.Unit.Commands.UpdateCommand;
using POS.Domain.Entities;
using POS.Utilities.Static;

namespace POS.Application.Mappings;

public class UnitMapping : Profile
{
    public UnitMapping()
    {
        CreateMap<Unit, UnitResponseDto>()
            .ForMember(x => x.UnitId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.StateUnit, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Activo) ? "ACTIVO" : "INACTIVO"))
            .ReverseMap();

        CreateMap<Unit, SelectResponse>()
            .ForMember(x => x.Id, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.Description, x => x.MapFrom(y => y.Name))
            .ReverseMap();

        CreateMap<Unit, UnitByIdResponseDto>()
            .ForMember(x => x.UnitId, x => x.MapFrom(y => y.Id))
            .ReverseMap();

        CreateMap<CreateUnitCommand, Unit>();

        CreateMap<UpdateUnitCommand, Unit>();
    }
}