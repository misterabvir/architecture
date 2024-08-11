using DataAccessLayer.Entities;

namespace UI.Extensions;

internal static class ProjectExtensions
{
    public static void Print(this IEnumerable<Setting> settings)
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine($"Settings: ");
        foreach (var setting in settings)
        {
            Console.WriteLine($"\tID# {setting.Id}. {setting.Parameter} : {setting.Value}");
        }
        Console.ResetColor();
    }

    public static void Print(this IEnumerable<Model> models)
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine($"Models: ");
        foreach (var model in models)
        {
            Console.WriteLine($"\tModel: ID#{model.Id}, NAME: {model.Name}");
            foreach (var texture in model.Textures)
            {
                Console.WriteLine($"\t\tID#{texture.Id}. COLOR: {texture.Color} PATTERN: {texture.Pattern}");
            }
        }
        Console.ResetColor();
    }
}