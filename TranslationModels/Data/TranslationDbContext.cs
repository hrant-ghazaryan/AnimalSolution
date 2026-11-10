using Microsoft.EntityFrameworkCore;
using TranslationModels.Entitites;

namespace TranslationModels.Data;

public class TranslationDbContext : DbContext
{
    public DbSet<Language> Languages { get; set; }
    public DbSet<Translation> Translations { get; set; }

    public TranslationDbContext(DbContextOptions<TranslationDbContext> options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Translation>()
            .HasOne(t => t.SourceLanguage)
            .WithMany()
            .HasForeignKey(t => t.SourceLanguageId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Translation>()
            .HasOne(t => t.TargetLanguage)
            .WithMany()
            .HasForeignKey(t => t.TargetLanguageId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Translation>()
            .HasIndex(t => new { t.Word, t.TargetLanguageId })
            .IsUnique();
    }
}
