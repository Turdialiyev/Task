using Task.Data;

namespace Task.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public IFileRepository Files { get; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Files = new FileRepository(context);
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

    public int Save()
        => _context.SaveChanges();
}