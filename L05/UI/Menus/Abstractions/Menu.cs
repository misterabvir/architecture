namespace UI.Menus.Abstractions;

internal abstract class Menu
{
    protected List<Command> _commands = [];
    
    public async Task Execute(int input)
    {
        await _commands[input - 1].Handler();
    }

    public async Task<int> ShowMenu()
    {
        await Task.CompletedTask;
        foreach (var command in _commands)
        {
            Console.WriteLine(command.MenuName);
        }
        Console.WriteLine("0. Exit");
        return _commands.Count;
    }

}