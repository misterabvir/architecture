using BusinessLogicLayer.Abstractions;
using DataAccessLayer.Entities;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Xml.Linq;
using UI.Extensions;
using UI.Menus.Abstractions;

namespace UI.Menus;

internal class ModelMenu : Menu
{
    private Project? _project;
    private readonly IProjectService _projectService;

    public ModelMenu(IProjectService projectService)
    {
        _projectService = projectService;
    }

    internal void SetProject(Project project)
    {
        _project = project;
        _commands.Clear();
        _commands.AddRange([
            new Command("1. Show all", ShowHandler),
            new Command("2. Create Model", CreateModelHandler),
            new Command("3. Change Name Model", ChangeNameModelHandler),
            new Command("4. Remove Model", RemoveModelHandler),
            new Command("5. Add Texture to Model", AddTextureToModelHandler),
            new Command("6. Change Color Texture in Model", ChangeColorTextureToModelHandler),
            new Command("7. Change Pattern Texture in Model", ChangePatternTextureToModelHandler),
            new Command("8. Remove Texture from Model", RemoveTextureFromModelHandler),
            ]);
    }

    private async Task RemoveTextureFromModelHandler()
    {
        Console.Write("Type model id: ");
        int modelId = ConsoleApp.GetInput(_project!.Models.Select(s => s.Id));
        Console.Write("Type texture id: ");
        int textureId = ConsoleApp.GetInput(_project!.Models.First(t => t.Id == modelId).Textures.Select(t => t.Id));
        _project = await _projectService.RemoveTexture(_project.Id, modelId, textureId);
    }

    private async Task ChangePatternTextureToModelHandler()
    {
        Console.Write("Type model id: ");
        int modelId = ConsoleApp.GetInput(_project!.Models.Select(s => s.Id));
        Console.Write("Type texture id: ");
        int textureId = ConsoleApp.GetInput(_project!.Models.First(t => t.Id == modelId).Textures.Select(t => t.Id));
        Console.Write("Type texture pattern: ");
        string pattern = ConsoleApp.GetStringInput();
        _project = await _projectService.UpdateTexturePattern(_project.Id, modelId, textureId, pattern);
    }

    private async Task ChangeColorTextureToModelHandler()
    {
        Console.Write("Type model id: ");
        int modelId = ConsoleApp.GetInput(_project!.Models.Select(s => s.Id));
        Console.Write("Type texture id: ");
        int textureId = ConsoleApp.GetInput(_project!.Models.First(t=>t.Id == modelId).Textures.Select(t=>t.Id));
        Console.Write("Type texture color: ");
        string color = ConsoleApp.GetStringInput();
        _project = await _projectService.UpdateTextureColor(_project.Id, modelId, textureId, color);
    }

    private async Task AddTextureToModelHandler()
    {
        Console.Write("Type model id: ");
        int modelId = ConsoleApp.GetInput(_project!.Models.Select(s => s.Id));
        Console.Write("Type texture color: ");
        string color = ConsoleApp.GetStringInput();
        Console.Write("Type texture pattern: ");
        string pattern = ConsoleApp.GetStringInput();
        _project = await _projectService.AddTexture(_project.Id, modelId, color, pattern);
    }

    private async Task RemoveModelHandler()
    {
        Console.Write("Type model id: ");
        int modelId = ConsoleApp.GetInput(_project!.Models.Select(s => s.Id));
        _project = await _projectService.RemoveModel(_project!.Id, modelId);
    }

    private async Task ChangeNameModelHandler()
    {
        Console.Write("Type model id: ");
        int modelId = ConsoleApp.GetInput(_project!.Models.Select(s => s.Id));
       
        Console.Write("Type Name: ");
        var name = ConsoleApp.GetStringInput();
        _project = await _projectService.UpdateModel(_project!.Id, modelId, name);
    }

    private async Task CreateModelHandler()
    {
        Console.Write("Type Name: ");
        var name = ConsoleApp.GetStringInput();
        _project = await _projectService.CreateModel(_project!.Id, name);
    }

    private async Task ShowHandler()
    {
        await Task.CompletedTask;
        _project!.Models.Print();
    }
}