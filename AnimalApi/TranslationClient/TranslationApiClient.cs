using AnimalDTOs;
using TranslationDTOs;

namespace AnimalApi.TranslationClient;

public class TranslationApiClient : ITranslationApiClient
{
    private readonly HttpClient httpClient;
    public TranslationApiClient(HttpClient httpClient)
       => this.httpClient = httpClient;

    public async Task<IDictionary<string, string>> GetEnumTranslationsAsync<T>(string languageCode)
        where T : Enum
    {
        var enumType = typeof(T).Name;

        var requestUrl =
            $"api/Translations/resources/{Uri.EscapeDataString(enumType)}" +
            $"?languageCode={Uri.EscapeDataString(languageCode)}";

        var result = await httpClient.
            GetFromJsonAsync<TranslationGroupResponseDTO>(requestUrl);

        return result?.TranslatedWords ?? [];
    }

    public async Task<List<AnimalResponseDTO>> TranslateAnimalsListDtos(List<AnimalResponseDTO> animals, string languageCode)
    {
        var requestUrl = $"api/Translations/translateanimals?languageCode={Uri.EscapeDataString(languageCode)}";

        var response = await httpClient.PostAsJsonAsync(requestUrl, animals);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Translation API error {(int)response.StatusCode}: {error}");
        }

        return await response.Content.ReadFromJsonAsync<List<AnimalResponseDTO>>() ?? [];
    }
}