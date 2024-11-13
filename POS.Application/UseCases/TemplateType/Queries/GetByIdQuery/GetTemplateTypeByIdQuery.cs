using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.TemplateType.Response;

namespace POS.Application.UseCases.TemplateType.Queries.GetByIdQuery;

public class GetTemplateTypeByIdQuery : IRequest<BaseResponse<TemplateTypeByIdResponseDto>>
{
    public int TemplateTypeId { get; set; }
}