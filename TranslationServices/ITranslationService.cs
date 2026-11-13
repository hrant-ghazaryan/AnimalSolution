using TranslationDTOs;

namespace TranslationServices;

public interface ITranslationService
{
    Task<TranslationGroupResponseDTO?> GetEnumTranslationsAsync(string enumType, string languageCode);

    Task AddTranslationAsync(TranslationCreateDTO translationCreateDTO);

    Task DeleteTranslationAsync(int id);
}
