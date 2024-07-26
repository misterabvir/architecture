using Implementation.InMemoryModel.Abstractions;
using Implementation.ModelElements;
using Implementation.ModelElements.Primitives;

namespace Implementation.InMemoryModel;

public class ModelStore : IModelChanger
{
    private readonly List<PolygonalModel> _models = [];
    private readonly List<Scene> _scenes = [];
    private readonly List<Flash> _flashes = [];
    private readonly List<Camera> _cameras = [];
    private readonly List<IModelChangedObserver> _observers = [];

    public IReadOnlyList<PolygonalModel> Models => _models;
    public IReadOnlyList<Scene> Scenes => _scenes;
    public IReadOnlyList<Flash> Flashes => _flashes;
    public IReadOnlyList<Camera> Cameras => _cameras;

    public ModelStore(PolygonalModel model, Scene scene, Flash flash, Camera camera)
    {
        _models.Add(model);
        _scenes.Add(scene);
        _flashes.Add(flash);
        _cameras.Add(camera);
    }

    public void AddModel(PolygonalModel model)
    {
        _models.Add(model);
        NotifyChange(this);
    }

    public void AddScene(Scene scene)
    {
        _scenes.Add(scene);
        NotifyChange(this);
    }

    public void AddFlash(Flash flash)
    {
        _flashes.Add(flash);
        NotifyChange(this);
    }

    public void AddCamera(Camera camera)
    {
        _cameras.Add(camera);
        NotifyChange(this);
    }

    public void RemoveModel(PolygonalModel model)
    {
        _models.Remove(model);
        NotifyChange(this);
    }

    public void RemoveScene(Scene scene)
    {
        _scenes.Remove(scene);
        NotifyChange(this);
    }

    public void RemoveFlash(Flash flash)
    {
        _flashes.Remove(flash);
        NotifyChange(this);
    }

    public void RemoveCamera(Camera camera)
    {
        _cameras.Remove(camera);
        NotifyChange(this);
    }

    public void RotateCamera(Camera camera, Angle3D direction)
    {
        if (_cameras.Contains(camera))
        {
            camera.Rotate(direction);
            NotifyChange(this);
        }
    }

    public void MoveCamera(Camera camera, Point3D direction)
    {
        if (_cameras.Contains(camera))
        {
            camera.Move(direction);
            NotifyChange(this);
        }
    }

    public void RotateFlash(Flash flash, Angle3D direction)
    {
        if (_flashes.Contains(flash))
        {
            flash.Rotate(direction);
            NotifyChange(this);
        }
    }

    public void MoveFlash(Flash flash, Point3D direction)
    {
        if (_flashes.Contains(flash))
        {
            flash.Move(direction);
            NotifyChange(this);
        }
    }

    public void ChangeFlashColor(Flash flash, Color color)
    {
        if (_flashes.Contains(flash))
        {
            flash.ChangeColor(color);
            NotifyChange(this);
        }
    }

    public void ChangeFlashPower(Flash flash, double power)
    {
        if (_flashes.Contains(flash))
        {
            flash.ChangePower(power);
            NotifyChange(this);
        }
    }

    public void RegisterObserver(IModelChangedObserver observer) => _observers.Add(observer);
    public void RemoveObserver(IModelChangedObserver observer) => _observers.Remove(observer);

    public void NotifyChange(IModelChanger sender)
    {
        foreach (var observer in _observers)
        {
            observer.ApplyUpdateModel(sender);
        }
    }
}