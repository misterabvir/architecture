using Implementation.Concretes;
using Implementation.Extensions;
using Implementation.Models;

string simpleText = """
    Lorem ipsum dolor sit amet, consectetur adipiscing elit. 
    Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. 
    Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. 
    Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. 
    Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.
    """;
string filePath = Path.Combine(Environment.CurrentDirectory, "Program.cs");
string machineName = Environment.MachineName;


var creator = new TextLogReaderCreator();

var reader = creator.Create(LogReaderType.SimpleText, simpleText);
reader.Read(2).Log();

reader = creator.Create(LogReaderType.File, filePath);
reader.Read(23).Log();

reader = creator.Create(LogReaderType.Event, machineName);
reader.Read(0).Log();

