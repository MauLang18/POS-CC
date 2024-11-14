using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.EmailTemplate.Response;

namespace POS.Application.UseCases.EmailTemplate.Queries.GetAllQuery;

public class GetAllEmailTemplateQuery : BaseFilters, IRequest<BaseResponse<IEnumerable<EmailTemplateResponseDto>>>
{
}