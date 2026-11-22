using AnimalDTOs;

namespace AnimalApi.TranslationClient;

public interface ITranslationApiClient
{
    Task<IDictionary<string, string>> GetEnumTranslationsAsync<T>(string languageCode) 
        where T : Enum;

    public Task<List<AnimalResponseDTO>> TranslateAnimalsListDtos(List<AnimalResponseDTO> animals, string languageCode);
}