using DataAccessLayer.Entities;

namespace BusinessLogicLayer.Abstractions;

public interface IProjectService
{
    Task<IEnumerable<Project>> GetProjectsAsync();
    Task<Project?> GetProjectById(int projectId);
    Task<Project> CreateProject(string name);
    Task<Project> RemoveSettings(int projectId, int settingId);
    Task<Project> UpdateSettings(int projectId, int settingId, string value);
    Task<Project> CreateSettings(int projectId, string parameter, string value);
    Task<Project> CreateModel(int projectId, string name);
    Task<Project> UpdateModel(int id, int modelId, string name);
    Task<Project> RemoveModel(int id, int modelId);
    Task<Project> AddTexture(int id, int modelId, string color, string pattern);
    Task<Project> UpdateTextureColor(int id, int modelId, int textureId, string color);
    Task<Project> UpdateTexturePattern(int id, int modelId, int textureId, string pattern);
    Task<Project> RemoveTexture(int id, int modelId, int textureId);
}

