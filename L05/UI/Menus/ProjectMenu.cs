using DataAccessLayer.Entities;
using UI.Menus.Abstractions;

namespace UI.Menus;

internal class ProjectMenu : Menu
{
    private readonly SettingMenu _settingMenu;
    private readonly ModelMenu _modelMenu;
    private Project? _project;
    public ProjectMenu(SettingMenu settingMenu, ModelMenu modelMenu)
    {
        _settingMenu = settingMenu;
        _modelMenu = modelMenu;
    }

    internal void SetProject(Project project)
    {
        _project = project;
        _commands.Clear();
        _commands.Add(new Command("1. Settings", SettingsHandler));
        _commands.Add(new Command("2. Models", ModelsHandler));
    }

    private async Task ModelsHandler()
    {
        await Task.CompletedTask;
        _modelMenu.SetProject(_project!);
        ConsoleApp.Goto(_modelMenu);
    }

    private async Task SettingsHandler()
    {
        await Task.CompletedTask;
        _settingMenu.SetProject(_project!);
        ConsoleApp.Goto(_settingMenu);
    }
}
