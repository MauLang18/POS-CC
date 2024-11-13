using AutoMapper;
using POS.Application.Commons.Select.Response;
using POS.Application.Dtos.DocumentType.Response;
using POS.Application.UseCases.DocumentType.Commands.CreateCommand;
using POS.Application.UseCases.DocumentType.Commands.UpdateCommand;
using POS.Domain.Entities;
using POS.Utilities.Static;

namespace POS.Application.Mappings;

public class DocumentTypeMapping : Profile
{
    public DocumentTypeMapping()
    {
        CreateMap<DocumentType, DocumentTypeResponseDto>()
            .ForMember(x => x.DocumentTypeId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.StateDocumentType, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Activo) ? "ACTIVO" : "INACTIVO"))
            .ReverseMap();

        CreateMap<DocumentType, SelectResponse>()
            .ForMember(x => x.Id, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.Description, x => x.MapFrom(y => y.Name))
            .ReverseMap();

        CreateMap<DocumentType, DocumentTypeByIdResponseDto>()
            .ForMember(x => x.DocumentTypeId, x => x.MapFrom(y => y.Id))
            .ReverseMap();

        CreateMap<CreateDocumentTypeCommand, DocumentType>();

        CreateMap<UpdateDocumentTypeCommand, DocumentType>();
    }
}