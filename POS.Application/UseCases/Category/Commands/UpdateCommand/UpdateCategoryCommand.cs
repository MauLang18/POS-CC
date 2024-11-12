using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.Category.Commands.UpdateCommand;

public class UpdateCategoryCommand : IRequest<BaseResponse<bool>>
{
    public int CategoryId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int State { get; set; }
}