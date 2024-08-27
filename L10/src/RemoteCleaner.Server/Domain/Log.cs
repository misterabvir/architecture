namespace RemoteCleaner.Server.Domain;

public class Log
{
    public int LogId { get; set; }
    public int StationId { get; set; }
    public string Message { get; set; } = string.Empty;
    public DateTime Time { get; set; } = DateTime.UtcNow;
}
