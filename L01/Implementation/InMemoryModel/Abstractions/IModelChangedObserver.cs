namespace Implementation.InMemoryModel.Abstractions;

public interface IModelChangedObserver
{
    void ApplyUpdateModel(IModelChanger modelChanger);
}
