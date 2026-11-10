using TranslationDTOs;

namespace AnimalApi;

public class TranslationApiClient : ITranslationApiClient
{
    private readonly HttpClient httpClient;
    public TranslationApiClient(HttpClient httpClient)
        => this.httpClient = httpClient;
    public async Task<TranslationResponseDTO?> GetTranslationsAsync(
    string word,
    CancellationToken cancellationToken = default)
        => await httpClient.GetFromJsonAsync<TranslationResponseDTO>(
            $"api/Translations/{Uri.EscapeDataString(word)}",
            cancellationToken);
}
