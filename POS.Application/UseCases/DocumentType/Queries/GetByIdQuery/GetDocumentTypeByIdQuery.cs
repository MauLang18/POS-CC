using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.DocumentType.Response;

namespace POS.Application.UseCases.DocumentType.Queries.GetByIdQuery;

public class GetDocumentTypeByIdQuery : IRequest<BaseResponse<DocumentTypeByIdResponseDto>>
{
    public int DocumentTypeId { get; set; }
}