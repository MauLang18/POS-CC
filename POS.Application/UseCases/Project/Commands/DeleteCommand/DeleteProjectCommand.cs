using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.Project.Commands.DeleteCommand;

public class DeleteProjectCommand : IRequest<BaseResponse<bool>>
{
    public int ProjectId { get; set; }
}