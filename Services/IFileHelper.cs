namespace Task.Services;

public interface IFileHelper
{
    bool ValidateFile(IFormFile file);
   ValueTask<string> WriteFileAsync(IFormFile file);
    ValueTask<FileStream?> GetFileByNameAsync(string filename);
}