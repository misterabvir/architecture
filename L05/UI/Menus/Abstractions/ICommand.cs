namespace UI.Menus.Abstractions;

internal record Command(string MenuName, Func<Task> Handler);

