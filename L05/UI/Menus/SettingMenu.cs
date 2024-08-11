using BusinessLogicLayer.Abstractions;
using DataAccessLayer.Entities;
using UI.Extensions;
using UI.Menus.Abstractions;

namespace UI.Menus;

internal class SettingMenu : Menu
{
    private Project? _project;
    private readonly IProjectService _projectService;

    public SettingMenu(IProjectService projectService)
    {
        _projectService = projectService;
    }

    internal void SetProject(Project project)
    {
        _project = project;
        _commands.Clear();
        _commands.AddRange([
                new Command("1. Show all", ShowHandler),
                new Command("2. Create", CreateHandler),
                new Command("3. ChangeValue", ChangeValueHandler),
                new Command("4. Remove", RemoveHandler),
                new Command("5. Back", BackHandler),           
            ]);
    }

    private async Task RemoveHandler()
    {
        Console.WriteLine("Type index: ");
        var input = ConsoleApp.GetInput(_project!.Settings.Select(s => s.Id));
        _project = await _projectService.RemoveSettings(_project.Id, input);
    }

    private async Task ChangeValueHandler()
    {
        Console.WriteLine("Type index: ");
        var input = ConsoleApp.GetInput(_project!.Settings.Select(s => s.Id));
        Console.WriteLine("Type new value: ");
        var value = ConsoleApp.GetStringInput();
        _project = await _projectService.UpdateSettings(_project.Id, input, value);
    }

    private async Task CreateHandler()
    {
        Console.WriteLine("Type new parameter: ");
        var parameter = ConsoleApp.GetStringInput();
        Console.WriteLine("Type new value: ");
        var value = ConsoleApp.GetStringInput();
        _project = await _projectService.CreateSettings(_project!.Id, parameter, value);
    }

    private async Task ShowHandler()
    {
        await Task.CompletedTask;
        _project!.Settings.Print();
    }

    private async Task BackHandler()
    {
        await Task.CompletedTask;
        ConsoleApp.Back();
    }
}
