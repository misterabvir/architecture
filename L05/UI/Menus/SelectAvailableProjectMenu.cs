using BusinessLogicLayer.Abstractions;
using DataAccessLayer.Entities;
using UI.Menus.Abstractions;

namespace UI.Menus;

internal class SelectAvailableProjectMenu : Menu
{
    private readonly IProjectService _service;
    private readonly ProjectMenu _projectMenu;
    private List<Project> _projects = [];

    public SelectAvailableProjectMenu(IProjectService service, ProjectMenu projectMenu)
    {
        _service = service;
        _projectMenu = projectMenu;
    }

    public void SetAvailableProjects(IEnumerable<Project> projects)
    {
        _projects = projects.ToList();
        _commands.Clear();
        _commands.AddRange(_projects.Select((project, index) => new Command($"{index + 1}. {project.Name}", () => OpenHandler(index))));
        _commands.Add(new Command($"{_commands.Count + 1}. Back", BackHandler));
    }

    private async Task OpenHandler(int index)
    {
        await Task.CompletedTask;
        _projectMenu.SetProject(_projects[index]);
        ConsoleApp.Goto(_projectMenu);
    }

    private async Task BackHandler()
    {
        await Task.CompletedTask;
        ConsoleApp.Back();
    }
}
