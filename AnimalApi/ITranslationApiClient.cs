namespace AnimalApi;

public interface ITranslationApiClient
{
    Task<IDictionary<string, string>> GetEnumTranslationsAsync<T>(string languageCode) 
        where T : Enum;
}