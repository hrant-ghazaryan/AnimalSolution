using TranslationDTOs;

namespace TranslationServices;

public interface ITranslationService
{
    Task<TranslationGroupResponseDTO?> GetResourceTranslationsAsync(string resourceType, string languageCode,
        CancellationToken cancellationToken);

    Task AddTranslationAsync(TranslationCreateDTO translationCreateDTO, CancellationToken cancellationToken);

    Task DeleteTranslationAsync(int id, CancellationToken cancellationToken);
}
