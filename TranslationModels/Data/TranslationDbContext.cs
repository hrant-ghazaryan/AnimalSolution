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
            entity.Property(t => t.EnumType)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(t => t.EnumValue)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(t => t.LanguageCode)
                .HasMaxLength(10)
                .IsRequired();

            entity.Property(t => t.TranslatedText)
                .HasMaxLength(1000)
                .IsRequired();

            entity.HasIndex(t => new
            {
                t.EnumType,
                t.EnumValue,
                t.LanguageCode
            }).IsUnique();
        });
    }
}
