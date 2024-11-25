using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.Project.Commands.CreateCommand;

public class CreateProjectCommand : IRequest<BaseResponse<bool>>
{
    public string InternalName { get; set; } = null!;
    public string CommercialName { get; set; } = null!;
    public int CustomerId { get; set; }
    public int CategoryId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int StatusId { get; set; }
    public IEnumerable<CreateProjectDetailCommand> ProjectDetails { get; set; } = null!;
}

public class CreateProjectDetailCommand
{
    public string Requirement { get; set; } = null!;
    public int StatusId { get; set; }
}