using AutoMapper;
using POS.Application.Commons.Select.Response;
using POS.Application.Dtos.Status.Response;
using POS.Application.UseCases.Status.Commands.CreateCommand;
using POS.Application.UseCases.Status.Commands.UpdateCommand;
using POS.Domain.Entities;
using POS.Utilities.Static;

namespace POS.Application.Mappings;

public class StatusMapping : Profile
{
    public StatusMapping()
    {
        CreateMap<Status, StatusResponseDto>()
            .ForMember(x => x.StatusId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.StateStatus, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Activo) ? "ACTIVO" : "INACTIVO"))
            .ReverseMap();

        CreateMap<Status, SelectResponse>()
            .ForMember(x => x.Id, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.Description, x => x.MapFrom(y => y.Name))
            .ReverseMap();

        CreateMap<Status, StatusByIdResponseDto>()
            .ForMember(x => x.StatusId, x => x.MapFrom(y => y.Id))
            .ReverseMap();

        CreateMap<CreateStatusCommand, Status>();

        CreateMap<UpdateStatusCommand, Status>();
    }
}