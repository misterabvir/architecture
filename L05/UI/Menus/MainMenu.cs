using BusinessLogicLayer.Abstractions;
using UI.Menus.Abstractions;
namespace UI.Menus;

internal class MainMenu : Menu
{
    private readonly IProjectService _service;
    private readonly SelectAvailableProjectMenu _selectAvailableProjectMenu;
    private readonly ProjectMenu _projectMenu;

    public MainMenu(IProjectService service, SelectAvailableProjectMenu selectAvailableProjectMenu, ProjectMenu projectMenu)
    {
        _service = service;
        _selectAvailableProjectMenu = selectAvailableProjectMenu;
        _projectMenu = projectMenu;
        _commands.AddRange([
                new Command("1. Open Project", OpenProjectHandler),
                new Command("2. Create Project", CreateProjectHandler),
            ]);
    }



    private async Task OpenProjectHandler()
    {        
        var projects = await _service.GetProjectsAsync();
        _selectAvailableProjectMenu.SetAvailableProjects(projects);
        ConsoleApp.Goto(_selectAvailableProjectMenu);
    }

    private async Task CreateProjectHandler()
    {
        await Task.CompletedTask;
        Console.Write("Type Name: ");
        var name = ConsoleApp.GetStringInput();
        var project = await _service.CreateProject(name);
        _projectMenu.SetProject(project);
        ConsoleApp.Goto(_projectMenu);
        
    }
}
