using Task.Models;

namespace Task.Services;

public interface IFileService 
{
    // ValueTask<Result<FileStream>> GetAllFileAsync(string filename); 
    ValueTask<Result<FileStream>> GetFileByNameAsync(string fileName);
    ValueTask<Result<Task.Models.File>> CreateFileAsync(IFormFile file);
}