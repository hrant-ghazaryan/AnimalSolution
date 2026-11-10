using TranslationDTOs;

namespace AnimalApi;

public interface ITranslationApiClient
{
    Task<TranslationResponseDTO?> GetTranslationsAsync(
           string word,
           CancellationToken cancellationToken = default);
}
