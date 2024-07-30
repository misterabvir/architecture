using Implementation.Abstractions;
using Implementation.Models;

namespace Implementation.Concretes.Readers;

public class FileLogReader : BaseLogReader<string>
{
    protected override LogEntry Parse(string instance) => new(instance);

    protected override IEnumerable<string> ReadLines(int position)
    {
        if (string.IsNullOrEmpty(Source))
        {
            return [];
        }
        return File.ReadAllLines(Source).Skip(position);
    }
}
