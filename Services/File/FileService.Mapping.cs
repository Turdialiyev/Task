namespace Task.Services;

public partial class FileService
{
    private static Task.Models.File ToModel(Task.Entities.File entity)
     => new()
     {
         Id = entity.Id,
         Filename = entity.Filename,
         Information = entity.Information,
     };
}