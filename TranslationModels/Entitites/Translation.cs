namespace TranslationModels.Entitites;

public class Translation
{
    public int Id { get; set; }

    public string ResourceType { get; set; } = null!;
    public string ResourceCode { get; set; } = null!;
    public string LanguageCode { get; set; } = null!;
    public string Value { get; set; } = null!;
}