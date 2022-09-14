namespace Task.Repositories;

public interface IUnitOfWork : IDisposable
{
    IFileRepository Files { get; }
    int Save();
}