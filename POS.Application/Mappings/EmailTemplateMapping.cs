using AutoMapper;
using POS.Application.Commons.Select.Response;
using POS.Application.Dtos.EmailTemplate.Response;
using POS.Application.UseCases.EmailTemplate.Commands.CreateCommand;
using POS.Application.UseCases.EmailTemplate.Commands.UpdateCommand;
using POS.Domain.Entities;
using POS.Utilities.Static;

namespace POS.Application.Mappings;

public class EmailTemplateMapping : Profile
{
    public EmailTemplateMapping()
    {
        CreateMap<EmailTemplate, EmailTemplateResponseDto>()
            .ForMember(x => x.EmailTemplateId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.TemplateType, x => x.MapFrom(y => y.TemplateType.Name))
            .ForMember(x => x.StateEmailTemplate, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Activo) ? "ACTIVO" : "INACTIVO"))
            .ReverseMap();

        CreateMap<EmailTemplate, SelectResponse>()
            .ForMember(x => x.Id, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.Description, x => x.MapFrom(y => y.Subject))
            .ReverseMap();

        CreateMap<EmailTemplate, EmailTemplateByIdResponseDto>()
            .ForMember(x => x.EmailTemplateId, x => x.MapFrom(y => y.Id))
            .ReverseMap();

        CreateMap<CreateEmailTemplateCommand, EmailTemplate>();

        CreateMap<UpdateEmailTemplateCommand, EmailTemplate>();
    }
}