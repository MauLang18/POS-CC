using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.TemplateType.Response;

namespace POS.Application.UseCases.TemplateType.Queries.GetAllQuery;

public class GetAllTemplateTypeQuery : BaseFilters, IRequest<BaseResponse<IEnumerable<TemplateTypeResponseDto>>>
{
}