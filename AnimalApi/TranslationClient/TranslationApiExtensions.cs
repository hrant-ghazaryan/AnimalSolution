namespace AnimalApi.TranslationClient;

public static class TranslationApiExtensions
{
    public static IServiceCollection AddTranslationApi(
        this IServiceCollection services, IConfiguration configuration)
    {
        var translationApiBaseUrl = configuration["TranslationApi:BaseUrl"]
            ?? throw new InvalidOperationException("TranslationApi BaseUrl is not configured.");

        services.AddHttpClient<ITranslationApiClient, TranslationApiClient>(httpClient =>
        {
            httpClient.BaseAddress = new Uri(translationApiBaseUrl);
        });

        return services;
    }
}
