using TranslationDTOs;

namespace TranslationServices;

public interface ITranslationService
{
    Task<TranslationResponseDTO?> GetTranslationsAsync(string word);

    Task AddTranslationAsync(TranslationCreateDTO translationCreateDTO);

    Task DeleteTranslationAsync(int id);
}
