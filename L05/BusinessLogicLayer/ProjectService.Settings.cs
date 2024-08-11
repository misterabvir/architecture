using BusinessLogicLayer.Abstractions;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer;

internal partial class ProjectService : IProjectService
{
    public async Task<Project> CreateSettings(int projectId, string parameter, string value)
    {
        var project = await context.Projects.FirstAsync(p => p.Id == projectId);
        var setting = new Setting()
        {
            ProjectId = projectId,
            Parameter = parameter,
            Value = value
        };
        project.Settings.Add(setting);
        await context.SaveChangesAsync();
        context.ChangeTracker.Clear();

        return project;
    }

    public async Task<Project> RemoveSettings(int projectId, int settingId)
    {
        var project = await context.Projects.FirstAsync(p => p.Id == projectId);
        var setting = project.Settings.First(s => s.Id == settingId);
        project.Settings.Remove(setting);
        await context.SaveChangesAsync();
        context.ChangeTracker.Clear();
        return project;
    }

    public async Task<Project> UpdateSettings(int projectId, int settingId, string value)
    {
        var project = await context.Projects.FirstAsync(p => p.Id == projectId);
        var setting = project.Settings.First(s => s.Id == settingId);
        setting.Value = value;
        await context.SaveChangesAsync();
        context.ChangeTracker.Clear();
        return project;
    }
}
