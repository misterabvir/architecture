using Implementation.Models;

namespace Implementation.Abstractions;

public abstract class BaseLogReaderCreator<T>
{
    public BaseLogReader<T> Create(LogReaderType type, T data)
    {
        var reader = CreateInstance(type);
        reader.Source = data;
        return reader;
    }

    protected abstract BaseLogReader<T> CreateInstance(LogReaderType type);
}