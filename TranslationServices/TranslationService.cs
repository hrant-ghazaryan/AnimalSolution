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

    public async Task<TranslationGroupResponseDTO?> GetEnumTranslationsAsync(
        string enumType, string languageCode)
    {
        var normalizedEnumType = enumType.Trim();
        var normalizedLanguageCode = languageCode.Trim().ToLowerInvariant();

        var translatedWards = await context.Translations
            .AsNoTracking()
            .Where(translation =>
                translation.EnumType == normalizedEnumType &&
                translation.LanguageCode == normalizedLanguageCode)
            .ToDictionaryAsync(
                translation => translation.EnumValue,
                translation => translation.TranslatedText);

        return new TranslationGroupResponseDTO
        {
            EnumType = normalizedEnumType,
            LanguageCode = normalizedLanguageCode,
            TranslatedWords = translatedWards
        };
    }

    public async Task AddTranslationAsync(
        TranslationCreateDTO dto)
    {
        var enumType = dto.EnumType.Trim();
        var enumValue = dto.EnumValue.Trim();
        var languageCode = dto.LanguageCode.Trim().ToLowerInvariant();
        var translatedText = dto.TranslatedText.Trim();

        var alreadyExists = await context.Translations.AnyAsync(
            translation =>
                translation.EnumType == enumType &&
                translation.EnumValue == enumValue &&
                translation.LanguageCode == languageCode);

        if (alreadyExists)
            return;

        var translation = new Translation
        {
            EnumType = enumType,
            EnumValue = enumValue,
            LanguageCode = languageCode,
            TranslatedText = translatedText
        };

        await context.Translations.AddAsync(translation);
        await context.SaveChangesAsync();
    }

    public async Task DeleteTranslationAsync(int id)
    {
        var translation = await context.Translations
            .SingleOrDefaultAsync(t => t.Id == id);

        if (translation is null)
            return;

        context.Translations.Remove(translation);
        await context.SaveChangesAsync();
    }
}