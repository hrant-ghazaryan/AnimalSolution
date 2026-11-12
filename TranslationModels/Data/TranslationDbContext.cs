using Microsoft.EntityFrameworkCore;
using TranslationModels.Entitites;

namespace TranslationModels.Data;

public class TranslationDbContext : DbContext
{
    public DbSet<Translation> Translations { get; set; }

    public TranslationDbContext(DbContextOptions<TranslationDbContext> options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Translation>(entity =>
        {
            entity.Property(t => t.ResourceType)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(t => t.ResourceCode)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(t => t.LanguageCode)
                .HasMaxLength(10)
                .IsRequired();

            entity.Property(t => t.Value)
                .HasMaxLength(1000)
                .IsRequired();

            entity.HasIndex(t => new
            {
                t.ResourceType,
                t.ResourceCode,
                t.LanguageCode
            }).IsUnique();
        });
    }
}
