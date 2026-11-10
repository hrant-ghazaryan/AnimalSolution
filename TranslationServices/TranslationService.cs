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
    public async Task AddTranslationAsync(TranslationCreateDTO dto)
    {
        if (dto is null || 
            string.IsNullOrEmpty(dto.OriginalWord) ||
            string.IsNullOrEmpty(dto.TranslatedWord) ||
            string.IsNullOrEmpty(dto.SourceLanguageCode) ||
            string.IsNullOrEmpty(dto.TargetLanguageCode))
            throw new InvalidDataException("Invalid Data");

        var sourceLanguage = await context.Languages.
            SingleOrDefaultAsync(l => l.Name == dto.SourceLanguageCode);

        var targetLanguage = await context.Languages.
            SingleOrDefaultAsync(l => l.Name == dto.TargetLanguageCode);

        if (sourceLanguage is null || targetLanguage is null)
            return;

        var alreadyExists = await context.Translations
            .AnyAsync(t =>
                t.Word == dto.OriginalWord &&
                t.TargetLanguageId == targetLanguage.Id);

        if (alreadyExists)
            return;

        var translation = new Translation
        {
            Word = dto.OriginalWord,
            TranslatedWord = dto.TranslatedWord,
            SourceLanguageId = sourceLanguage.Id,
            TargetLanguageId = targetLanguage.Id
        };

        await context.Translations.AddAsync(translation);
        await context.SaveChangesAsync();
    }

    public async Task DeleteTranslationAsync(int id)
    {
        var transaction = await context.Translations.SingleOrDefaultAsync(t => t.Id == id);
        if (transaction is null)
            throw new ArgumentNullException();

        context.Translations.Remove(transaction);
        await context .SaveChangesAsync();
    }

    public async Task<TranslationResponseDTO?> GetTranslationsAsync(string word)
    {
        var translations = await context.Translations
            .AsNoTracking()
            .Where(t => t.Word == word && t.TargetLanguage != null)
            .Select(t => new TranslationDTO
            {
                Word = t.TranslatedWord,
                LanguageCode = t.TargetLanguage!.Code
            })
            .ToListAsync();

        if (translations.Count == 0)
            return null;

        return new TranslationResponseDTO
        {
            OriginalWord = word,
            Translations = translations
        };
    }
}
