using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.TemplateType.Commands.CreateCommand;

public class CreateTemplateTypeCommand : IRequest<BaseResponse<bool>>
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int State { get; set; }
}