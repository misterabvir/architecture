namespace RemoteCleaner.Server.Domain;

public class Room : ILocation
{
    public int RoomId { get; set; }
    public string Name { get; set; } = null!;
    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; set; }
    public int Length { get; set; }
    public DateTime CleanedAt { get; set; } = DateTime.MinValue;
}
