using Implementation.Models;

namespace Implementation.Extensions;

public static class LogEntryExtensions
{
    public static void Log(this IEnumerable<LogEntry> logEntries)
    {
        foreach (var logEntry in logEntries)
        {
            Console.WriteLine(logEntry);
        }
    }
}