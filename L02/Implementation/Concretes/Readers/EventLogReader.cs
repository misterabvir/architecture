using Implementation.Abstractions;
using Implementation.Models;
using System.Diagnostics;
namespace Implementation.Concretes.Readers;

public class EventLogReader: BaseLogReader<string>
{
    private const int Limit = 5;

    protected override LogEntry Parse(string instance) => new(instance);

    protected override IEnumerable<string> ReadLines(int position)
    {
        EventLog[] eventLogs = EventLog.GetEventLogs(Source);
        return eventLogs
            .SelectMany(log => log.Entries.Cast<EventLogEntry>()
            .Select(entry => entry.Message.Split(Environment.NewLine)
            .First()))
            .Skip(position)
            .Take(Limit); // added for more readable output
    }
}
