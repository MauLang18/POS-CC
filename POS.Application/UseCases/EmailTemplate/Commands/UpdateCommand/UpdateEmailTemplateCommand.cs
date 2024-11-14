using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.EmailTemplate.Commands.UpdateCommand;

public class UpdateEmailTemplateCommand : IRequest<BaseResponse<bool>>
{
    public int EmailTemplateId { get; set; }
    public int TemplateTypeId { get; set; }
    public string Subject { get; set; } = null!;
    public string Body { get; set; } = null!;
    public int State { get; set; }
}