using Microsoft.EntityFrameworkCore;

namespace Task.Data;

public class AppDbContext : DbContext
{
    public  DbSet<Task.Entities.File>? Files { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}