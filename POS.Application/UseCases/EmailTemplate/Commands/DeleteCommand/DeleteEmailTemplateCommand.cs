using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.EmailTemplate.Commands.DeleteCommand;

public class DeleteEmailTemplateCommand : IRequest<BaseResponse<bool>>
{
    public int EmailTemplateId { get; set; }
}