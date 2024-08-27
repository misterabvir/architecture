namespace RemoteCleaner.Client.Components.Pages
{
    public partial class Home
    {
        public record Room(int RoomId, string Name, int X, int Y, DateTime CleanedAt);
        public record Station(int StationId, string SerialNumber, string Name, int X, int Y);
        public record Log(string Message, DateTime Time);
    }
}