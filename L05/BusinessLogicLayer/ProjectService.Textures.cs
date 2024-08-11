using BusinessLogicLayer.Abstractions;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer;

internal partial class ProjectService : IProjectService
{
     public async Task<Project> AddTexture(int projectId, int modelId, string color, string pattern)
    {
        var project = await context.Projects.FirstAsync(p => p.Id == projectId);
        var model = project.Models.First(m => m.Id == modelId);

        var texture = new Texture() { ModelId = modelId, Color = color, Pattern = pattern };    
        model.Textures.Add(texture);    
        await context.SaveChangesAsync();
        context.ChangeTracker.Clear();
        return project;
    }

    public async Task<Project> UpdateTextureColor(int projectId, int modelId, int textureId, string color)
    {
        var project = await context.Projects.FirstAsync(p => p.Id == projectId);
        var model = project.Models.First(m => m.Id == modelId);

        var texture = model.Textures.First(m => m.Id == textureId);
        texture.Color = color;
        await context.SaveChangesAsync();
        context.ChangeTracker.Clear();
        return project;
    }

    public async Task<Project> UpdateTexturePattern(int projectId, int modelId, int textureId, string pattern)
    {
        var project = await context.Projects.FirstAsync(p => p.Id == projectId);
        var model = project.Models.First(m => m.Id == modelId);

        var texture = model.Textures.First(m => m.Id == textureId);
        texture.Pattern = pattern;
        await context.SaveChangesAsync();
        context.ChangeTracker.Clear();
        return project;
    }

    public async Task<Project> RemoveTexture(int projectId, int modelId, int textureId)
    {
        var project = await context.Projects.FirstAsync(p => p.Id == projectId);
        var model = project.Models.First(m => m.Id == modelId);
        var texture = model.Textures.First(m => m.Id == textureId);
        model.Textures.Remove(texture);
        await context.SaveChangesAsync();
        context.ChangeTracker.Clear();
        return project;
    }
}
