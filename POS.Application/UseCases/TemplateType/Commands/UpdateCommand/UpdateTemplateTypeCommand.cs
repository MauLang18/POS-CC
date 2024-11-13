using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.TemplateType.Commands.UpdateCommand;

public class UpdateTemplateTypeCommand : IRequest<BaseResponse<bool>>
{
    public int TemplateTypeId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int State { get; set; }
}