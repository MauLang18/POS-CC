using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.DocumentType.Response;

namespace POS.Application.UseCases.DocumentType.Queries.GetAllQuery;

public class GetAllDocumentTypeQuery : BaseFilters, IRequest<BaseResponse<IEnumerable<DocumentTypeResponseDto>>>
{
}