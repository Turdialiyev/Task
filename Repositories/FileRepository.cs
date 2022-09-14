using Task.Data;

namespace Task.Repositories;

public class FileRepository : GenericRepository<Task.Entities.File>, IFileRepository
{
    public FileRepository(AppDbContext context)
        : base(context) { }
}