using Microsoft.EntityFrameworkCore;
using POS.Application.Interfaces.Persistence;
using POS.Domain.Entities;
using POS.Infrastructure.Persistence.Contexts;

namespace POS.Infrastructure.Persistence.Repositories;

public class ProjectDetailRepository : IProjectDetailRepository
{
    private readonly ApplicationDbContext _context;

    public ProjectDetailRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProjectDetail>> GetProjectDetailByProjectId(int id)
    {
        return await _context.ProjectDetails
            .AsNoTracking()
            .Where(pd => pd.ProjectId == id)
            .Include(pd => pd.Status)
            .ToListAsync();
    }
}