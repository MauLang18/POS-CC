using POS.Domain.Entities;

namespace POS.Application.Interfaces.Persistence;

public interface IProjectDetailRepository
{
    Task<IEnumerable<ProjectDetail>> GetProjectDetailByProjectId(int id);
}