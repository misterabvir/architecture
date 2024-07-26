namespace Implementation.ModelElements;

public class Scene
{
    private static int _idCounter = 0;

    public int SceneId { get; private set; } = ++_idCounter;

    private List<Flash> _flashes = [];
    private List<PolygonalModel> _models = [];
    private List<Camera> _cameras = [];

    public IReadOnlyList<Flash> Flashes => _flashes.AsReadOnly();
    public IReadOnlyList<PolygonalModel> Models => _models.AsReadOnly();
    public IReadOnlyList<Camera> Cameras => _cameras.AsReadOnly();

    public Scene(PolygonalModel model, Camera camera)
    {
        _models.Add(model);
        _cameras.Add(camera);
    }

    public void AddModel(PolygonalModel model) => _models.Add(model);
    public bool RemoveModel(PolygonalModel model) => _models.Remove(model);

    public void AddCamera(Camera camera) => _cameras.Add(camera);
    public bool RemoveCamera(Camera camera) => _cameras.Remove(camera);

    public void AddFlash(Flash flash) => _flashes.Add(flash);
    public bool RemoveFlash(Flash flash) => _flashes.Remove(flash);
}