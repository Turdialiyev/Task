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

    [HttpGet]
    public async Task<IActionResult> GetFileAsync()
    {
        try
        {
            // if (filename == string.Empty)
            //     return BadRequest(new { ErrorMessage = "filename is wrong." });
            
            var result = await _fileService.GetFileByNameAsync("import 2022-09-13-09-52-38.csv");
            if (!result.IsSuccess)
                return NotFound(new { ErrorMessage = result.ErrorMessage });

            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { ErrorMessage = e.Message });
        }
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Task.Dtos.File))]
    public async Task<IActionResult> PostFile(IFormFile file)
    {
        try
        {

            var createFileResult = await _fileService.CreateFileAsync(file);

            if (!createFileResult.IsSuccess)
                return BadRequest(new { ErrorMessage = createFileResult.ErrorMessage });

            return CreatedAtAction(nameof(File), new { Id = createFileResult?.Data?.Id }, ToDto(createFileResult?.Data!));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { ErrorMessage = e.Message });
        }
    }

    private Task.Dtos.File ToDto(Task.Models.File model)
    => new()
    {
        Id = model.Id,
        Filename = model.Filename,
    };
}