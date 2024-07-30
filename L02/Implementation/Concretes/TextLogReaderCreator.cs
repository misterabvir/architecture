using Implementation.Abstractions;
using Implementation.Concretes.Readers;
using Implementation.Models;

namespace Implementation.Concretes;

public class TextLogReaderCreator : BaseLogReaderCreator<string>
{
    protected override BaseLogReader<string> CreateInstance(LogReaderType type)
    {
        return type switch
        {
            LogReaderType.File => new FileLogReader(),
            LogReaderType.SimpleText => new SimpleTextLogReader(),
            LogReaderType.Event => new EventLogReader(),    
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, "Invalid log reader type")
        };
    }
}