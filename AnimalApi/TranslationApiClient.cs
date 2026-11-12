using TranslationDTOs;

namespace AnimalApi;

public class TranslationApiClient : ITranslationApiClient
{
    private readonly HttpClient httpClient;

    public TranslationApiClient(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<IDictionary<string, string>> GetEnumTranslationsAsync<T>(string languageCode)
        where T : Enum
    {
        var resourceType = typeof(T).Name;

        var requestUrl =
            $"api/Translations/resources/{Uri.EscapeDataString(resourceType)}" +
            $"?languageCode={Uri.EscapeDataString(languageCode)}";

        var result = await httpClient.GetFromJsonAsync<TranslationGroupResponseDTO>(
            requestUrl);

        return result?.Values ?? new Dictionary<string, string>();
    }
}