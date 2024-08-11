using BusinessLogicLayer.Abstractions;
using DataAccessLayer.Entities;
using DataAccessLayer.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer;

internal partial class ProjectService(ProjectDbContext context) : IProjectService
{
    public async Task<Project> CreateProject(string name)
    {
        Project project = new() { Name = name };
        await context.Projects.AddAsync(project);
        await context.SaveChangesAsync();
        return project;
    }


    public async Task<Project?> GetProjectById(int projectId)
    {
        return await context.Projects.AsNoTracking().FirstOrDefaultAsync(p => p.Id == projectId);
    }

    public async Task<IEnumerable<Project>> GetProjectsAsync()
    {
        return await context.Projects.AsNoTracking().ToListAsync();
    }
}
