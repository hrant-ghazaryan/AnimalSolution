using Microsoft.EntityFrameworkCore;
using TranslationDTOs;
using TranslationModels.Data;
using TranslationModels.Entitites;

namespace TranslationServices;

public class TranslationService : ITranslationService
{
    private readonly TranslationDbContext context;

    public TranslationService(TranslationDbContext context)
        => this.context = context;

    public async Task<TranslationGroupResponseDTO?> GetResourceTranslationsAsync(
        string resourceType,
        string languageCode,
        CancellationToken cancellationToken = default)
    {
        var normalizedResourceType = resourceType.Trim();
        var normalizedLanguageCode = languageCode.Trim().ToLowerInvariant();

        var values = await context.Translations
            .AsNoTracking()
            .Where(translation =>
                translation.ResourceType == normalizedResourceType &&
                translation.LanguageCode == normalizedLanguageCode)
            .ToDictionaryAsync(
                translation => translation.ResourceCode,
                translation => translation.Value,
                cancellationToken);

        return new TranslationGroupResponseDTO
        {
            ResourceType = normalizedResourceType,
            LanguageCode = normalizedLanguageCode,
            Values = values
        };
    }

    public async Task AddTranslationAsync(
        TranslationCreateDTO dto,
        CancellationToken cancellationToken = default)
    {
        var resourceType = dto.ResourceType.Trim();
        var resourceCode = dto.ResourceCode.Trim();
        var languageCode = dto.LanguageCode.Trim().ToLowerInvariant();
        var value = dto.Value.Trim();

        var alreadyExists = await context.Translations.AnyAsync(
            translation =>
                translation.ResourceType == resourceType &&
                translation.ResourceCode == resourceCode &&
                translation.LanguageCode == languageCode,
            cancellationToken);

        if (alreadyExists)
            return;

        var translation = new Translation
        {
            ResourceType = resourceType,
            ResourceCode = resourceCode,
            LanguageCode = languageCode,
            Value = value
        };

        await context.Translations.AddAsync(translation, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteTranslationAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var translation = await context.Translations
            .SingleOrDefaultAsync(item => item.Id == id, cancellationToken);

        if (translation is null)
            return;

        context.Translations.Remove(translation);
        await context.SaveChangesAsync(cancellationToken);
    }
}