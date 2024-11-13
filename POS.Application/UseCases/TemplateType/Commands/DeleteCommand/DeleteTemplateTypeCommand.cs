using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.TemplateType.Commands.DeleteCommand;

public class DeleteTemplateTypeCommand : IRequest<BaseResponse<bool>>
{
    public int TemplateTypeId { get; set; }
}