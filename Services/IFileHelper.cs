namespace Task.Services;

public interface IFileHelper
{
    bool ValidateFile(IFormFile file);
    Tuple<string, string> WriteFileAsync(IFormFile file);
    ValueTask<FileStream?> GetFileByNameAsync(string filename);
}