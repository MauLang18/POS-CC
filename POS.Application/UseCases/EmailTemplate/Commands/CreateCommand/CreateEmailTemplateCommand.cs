using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.EmailTemplate.Commands.CreateCommand;

public class CreateEmailTemplateCommand : IRequest<BaseResponse<bool>>
{
    public int TemplateTypeId { get; set; }
    public string Subject { get; set; } = null!;
    public string Body { get; set; } = null!;
    public int State { get; set; }
}