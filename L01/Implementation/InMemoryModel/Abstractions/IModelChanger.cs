namespace Implementation.InMemoryModel.Abstractions;

public interface IModelChanger
{
    void NotifyChange(IModelChanger sender);
}