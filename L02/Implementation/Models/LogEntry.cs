namespace Implementation.Models;

public class LogEntry(string message)
{
    public string Message => message;
    public override string ToString() => Message;
}