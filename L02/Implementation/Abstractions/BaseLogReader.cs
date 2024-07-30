using Implementation.Models;

namespace Implementation.Abstractions;

public abstract class BaseLogReader<T>
{
    public T Source { get; set; } = default!;


    public IEnumerable<LogEntry> Read(int position)
    {
        var lines = ReadLines(position);
        return lines.Select(Parse);
    }

    protected abstract IEnumerable<string> ReadLines(int position);
    protected abstract LogEntry Parse(string instance);

}
