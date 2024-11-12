using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.Category.Commands.CreateCommand;

public class CreateCategoryCommand : IRequest<BaseResponse<bool>>
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int State { get; set; }
}