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
        if (!(file.Length > 0))
            return new("File is empty");
        
        if (!new FileHelper().ValidateFile(file))
            return new("only csv and xlsx files are accepted.");

        var result = new FileHelper().WriteFileAsync(file);
        var filename = DateTime.Now.ToString("yyyy'-'MM'-'dd'-'hh'-'mm'-'ss");

        var fileEntity = new Task.Entities.File(filename + result.Item1, result.Item2);

        try
        {
            var createdFile = await _unitOfWork.Files.AddAsync(fileEntity);
            var fileFormat = new FileHelper().GetFileXElement(result.Item2);

            fileEntity.Information = fileFormat.ToString();
            fileEntity.Filename = filename + ".xml";

            return new(true) { Data = ToModel(createdFile) };
        }
        catch (Exception e)
        {
            _logger.LogError($"Error occured at {nameof(FileService)}", e);
            throw new("Couldn't create File. Contact support.", e);
        }
    }
}