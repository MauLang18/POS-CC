using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.Category.Commands.DeleteCommand;

public class DeleteCategoryCommand : IRequest<BaseResponse<bool>>
{
    public int CategoryId { get; set; }
}