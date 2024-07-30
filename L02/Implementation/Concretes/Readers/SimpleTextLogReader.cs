using Implementation.Abstractions;
using Implementation.Models;
namespace Implementation.Concretes.Readers;

public class SimpleTextLogReader : BaseLogReader<string>
{
    protected override LogEntry Parse(string instance) => new(instance);

    protected override IEnumerable<string> ReadLines(int position)
    {
        if (string.IsNullOrEmpty(Source))
        {
            return [];
        }
        var strings = Source.Split(Environment.NewLine);
        return strings.Skip(position);
    }
}
