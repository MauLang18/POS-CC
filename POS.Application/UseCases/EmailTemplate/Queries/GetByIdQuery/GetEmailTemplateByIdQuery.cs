using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.EmailTemplate.Response;

namespace POS.Application.UseCases.EmailTemplate.Queries.GetByIdQuery;

public class GetEmailTemplateByIdQuery : IRequest<BaseResponse<EmailTemplateByIdResponseDto>>
{
    public int EmailTemplateId { get; set; }
}