using BusinessLogicLayer.Abstractions;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer;

internal partial class ProjectService : IProjectService
{  
    public async Task<Project> CreateModel(int projectId, string name)
    {
        var project = await context.Projects.FirstAsync(p => p.Id == projectId);
        var model = new Model() { ProjectId = projectId, Name = name };
        project.Models.Add(model);
        await context.SaveChangesAsync();
        context.ChangeTracker.Clear();
        return project;
    }

    public async Task<Project> UpdateModel(int projectId, int modelId, string name)
    {
        var project = await context.Projects.FirstAsync(p => p.Id == projectId);
        var model = project.Models.First(m => m.Id == modelId);
        model.Name = name;
        await context.SaveChangesAsync();
        context.ChangeTracker.Clear();
        return project;
    }

    public async Task<Project> RemoveModel(int projectId, int modelId)
    {
        var project = await context.Projects.FirstAsync(p => p.Id == projectId);
        var model = project.Models.First(m => m.Id == modelId);
        project.Models.Remove(model);
        await context.SaveChangesAsync();
        return project;
    }
}
