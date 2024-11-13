using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.DocumentTemplate.Response;

namespace POS.Application.UseCases.DocumentTemplate.Queries.GetAllQuery;

public class GetAllDocumentTemplateQuery : BaseFilters, IRequest<BaseResponse<IEnumerable<DocumentTemplateResponseDto>>>
{
}