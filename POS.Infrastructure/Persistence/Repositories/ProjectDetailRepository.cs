using Microsoft.EntityFrameworkCore;
using POS.Application.Interfaces.Persistence;
using POS.Domain.Entities;
using POS.Infrastructure.Persistence.Contexts;

public class ProjectDetailRepository : IProjectDetailRepository
{
    private readonly ApplicationDbContext _context;

    public ProjectDetailRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // Método para obtener los detalles del proyecto por su ID
    public async Task<IEnumerable<ProjectDetail>> GetProjectDetailByProjectId(int id)
    {
        return await _context.ProjectDetails
            .AsNoTracking()
            .Where(pd => pd.ProjectId == id)
            .Include(pd => pd.Status)
            .ToListAsync();
    }

    public async Task UpdateProjectDetails(int projectId, List<ProjectDetail> newDetails)
    {
        var existingDetails = await _context.ProjectDetails
            .Where(pd => pd.ProjectId == projectId)
            .ToListAsync();

        var deletedDetails = existingDetails
            .Where(ed => !newDetails.Any(nd => nd.Id == ed.Id))
            .ToList();

        if (deletedDetails.Any())
        {
            _context.ProjectDetails.RemoveRange(deletedDetails);
        }

        await _context.SaveChangesAsync();
    }
}
