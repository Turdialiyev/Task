namespace Task.Entities;

public class File
{
    public ulong Id { get; set; }
    public string? Filename { get; set; }
    public string? Information { get; set; }

    [Obsolete("This constructor only use ")]
    public File() { }
    public File(string filename, string information)
    {
        Filename = filename;
        Information = information;
    }
}