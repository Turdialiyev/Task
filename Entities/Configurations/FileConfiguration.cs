using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Task.Entities.Configurations;

public class FileConfiguration<T> : IEntityTypeConfiguration<T> where T : Task.Entities.File
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.Property(t => t.Id)
            .ValueGeneratedOnAdd();
        builder.Property(t => t.Filename).IsRequired();
        builder.Property(t => t.Information).IsRequired();
    }
}