using UI.Menus.Abstractions;

internal static class ConsoleApp
{
    private static Menu? _menu;
    private static Menu? _previousMenu;


    internal static async Task RunAsync(Menu menu)
    {
        _menu = menu;

        while (true)
        {
            var length = await _menu.ShowMenu();
            var input = GetInput(length);
            if (input == 0)
            {
                return;
            }
            await _menu.Execute(input);
        }
    }

    public static void Goto(Menu menu)
    {
        _previousMenu = _menu;
        _menu = menu;
    }

    public static void Back()
    {
        if (_previousMenu is not null)
        {
            (_previousMenu, _menu) = (_menu, _previousMenu);
        }
    }

    public static int GetInput(int length)
    {
        string? input;
        int result;

        do
        {
            Console.WriteLine("SELECT: ");
            input = Console.ReadLine();
        } while (input is null || !int.TryParse(input, out result) || result < 0 || result > length);

        return result;
    }

    public static string GetStringInput()
    {
        string? input;

        do
        {
            input = Console.ReadLine();
        } while (string.IsNullOrEmpty(input));

        return input;
    }

    internal static int GetInput(IEnumerable<int> availableValues)
    {
        string? input;
        int result;

        do
        {
            Console.WriteLine("SELECT: ");
            input = Console.ReadLine();
        } while (input is null || !int.TryParse(input, out result) || !availableValues.Contains(result));

        return result;
    }
}