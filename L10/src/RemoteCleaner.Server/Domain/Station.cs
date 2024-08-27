namespace RemoteCleaner.Server.Domain;

public class Station : ILocation
{
    public int StationId { get; set; }
    public string SerialNumber { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int X { get; set; }
    public int Y { get; set; }
    public List<Log> Logs { get; set; } = [];
}
