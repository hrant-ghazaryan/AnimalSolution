using AnimalDTOs;
using TranslationDTOs;

namespace TranslationServices;

public interface ITranslationService
{
    Task<TranslationGroupResponseDTO?> GetEnumTranslationsAsync(string enumType, string languageCode);

    public Task<List<AnimalResponseDTO>> TranslateAnimalsListDtos(List<AnimalResponseDTO> animals, string languageCode);

    Task AddTranslationAsync(TranslationCreateDTO translationCreateDTO);

    Task DeleteTranslationAsync(int id);
}
