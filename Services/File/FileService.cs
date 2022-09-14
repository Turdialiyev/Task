using Microsoft.EntityFrameworkCore;
using Task.Models;
using Task.Repositories;

namespace Task.Services;
public partial class FileService : IFileService
{
    private readonly ILogger<FileService> _logger;
    private readonly IUnitOfWork _unitOfWork;
    public FileService(ILogger<FileService> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async ValueTask<Result<Task.Models.File>> CreateFileAsync(IFormFile file)
    {
        if (!new FileHelper().ValidateFile(file))
            return new("File is invalid");

        var filename = new FileHelper().WriteFileAsync(file);
        var fileEntity = new Task.Entities.File(filename.Result, "bu yerda fileni ishidagi textlari bo'lishi kerak");

        try
        {
            var createdFile = await _unitOfWork.Files.AddAsync(fileEntity);

            return new(true) { Data = ToModel(createdFile) };
        }
        catch (Exception e)
        {
            _logger.LogError($"Error occured at {nameof(FileService)}", e);
            throw new("Couldn't create File. Contact support.", e);
        }
    }

    public async ValueTask<Result<FileStream>> GetFileByNameAsync(string filename)
    {
        var existingFilename = _unitOfWork.Files.GetAll().FirstOrDefault(f => f.Filename == filename);

        if (existingFilename is null)
            return new("File is not exist");

        try
        {
            var file = await new FileHelper().GetFileByNameAsync(filename);
            return new(true) { Data = file };

        }
        catch (Exception e)
        {
            _logger.LogError($"Error occured at {nameof(FileService)}", e);
            throw new("Couldn't get File. Contact support.", e);
        }
    }
}