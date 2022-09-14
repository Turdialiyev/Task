using Task.Models;

namespace Task.Services;

public interface IFileService 
{
    ValueTask<Result<Task.Models.File>> CreateFileAsync(IFormFile file);
}