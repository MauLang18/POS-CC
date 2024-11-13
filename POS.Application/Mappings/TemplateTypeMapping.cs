using AutoMapper;
using POS.Application.Commons.Select.Response;
using POS.Application.Dtos.TemplateType.Response;
using POS.Application.UseCases.TemplateType.Commands.CreateCommand;
using POS.Application.UseCases.TemplateType.Commands.UpdateCommand;
using POS.Domain.Entities;
using POS.Utilities.Static;

namespace POS.Application.Mappings;

public class TemplateTypeMapping : Profile
{
    public TemplateTypeMapping()
    {
        CreateMap<TemplateType, TemplateTypeResponseDto>()
            .ForMember(x => x.TemplateTypeId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.StateTemplateType, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Activo) ? "ACTIVO" : "INACTIVO"))
            .ReverseMap();

        CreateMap<TemplateType, SelectResponse>()
            .ForMember(x => x.Id, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.Description, x => x.MapFrom(y => y.Name))
            .ReverseMap();

        CreateMap<TemplateType, TemplateTypeByIdResponseDto>()
            .ForMember(x => x.TemplateTypeId, x => x.MapFrom(y => y.Id))
            .ReverseMap();

        CreateMap<CreateTemplateTypeCommand, TemplateType>();

        CreateMap<UpdateTemplateTypeCommand, TemplateType>();
    }
}