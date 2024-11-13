using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.DocumentTemplate.Response;

namespace POS.Application.UseCases.DocumentTemplate.Queries.GetByIdQuery;

public class GetDocumentTemplateByIdQuery : IRequest<BaseResponse<DocumentTemplateByIdResponseDto>>
{
    public int DocumentTemplateId { get; set; }
}