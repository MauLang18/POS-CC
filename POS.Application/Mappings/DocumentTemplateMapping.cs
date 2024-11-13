using AutoMapper;
using POS.Application.Commons.Select.Response;
using POS.Application.Dtos.DocumentTemplate.Response;
using POS.Application.UseCases.DocumentTemplate.Commands.CreateCommand;
using POS.Application.UseCases.DocumentTemplate.Commands.UpdateCommand;
using POS.Domain.Entities;
using POS.Utilities.Static;

namespace POS.Application.Mappings;

public class DocumentTemplateMapping : Profile
{
    public DocumentTemplateMapping()
    {
        CreateMap<DocumentTemplate, DocumentTemplateResponseDto>()
            .ForMember(x => x.DocumentTemplateId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.TemplateType, x => x.MapFrom(y => y.TemplateType.Name))
            .ForMember(x => x.StateDocumentTemplate, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Activo) ? "ACTIVO" : "INACTIVO"))
            .ReverseMap();

        CreateMap<DocumentTemplate, SelectResponse>()
            .ForMember(x => x.Id, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.Description, x => x.MapFrom(y => y.Name))
            .ReverseMap();

        CreateMap<DocumentTemplate, DocumentTemplateByIdResponseDto>()
            .ForMember(x => x.DocumentTemplateId, x => x.MapFrom(y => y.Id))
            .ReverseMap();

        CreateMap<CreateDocumentTemplateCommand, DocumentTemplate>();

        CreateMap<UpdateDocumentTemplateCommand, DocumentTemplate>();
    }
}