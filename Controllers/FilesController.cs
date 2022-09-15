using ClosedXML.Excel;
using System.Xml;
using Microsoft.AspNetCore.Mvc;
using Task.Services;

namespace Task.Controllers;

[ApiController]
[Route("api/[controller]")]

public class FilesController : ControllerBase
{
    private readonly IFileService _fileService;
    private readonly ILogger<FilesController> _logger;

    public FilesController(ILogger<FilesController> logger, IFileService fileService)
    {
        _fileService = fileService;
        _logger = logger;
    }
    [HttpPost]
    public async Task<IActionResult> PostFile(IFormFile file)
    {
        var createFileResult = await _fileService.CreateFileAsync(file);

        if (!createFileResult.IsSuccess)
            return BadRequest(new { ErrorMessage = createFileResult.ErrorMessage });

        XmlDocument xdoc = new XmlDocument();
        xdoc.LoadXml(createFileResult.Data!.Information!);
        using MemoryStream xmlStream = new MemoryStream();
        xdoc.Save(xmlStream);
        var arr = xmlStream.ToArray();

        return new FileContentResult(arr, "application/xml")
        {
            FileDownloadName = createFileResult.Data!.Filename
        };
    }
}