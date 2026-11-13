namespace TranslationModels.Entitites;

public class Translation
{
    public int Id { get; set; }

    public string EnumType { get; set; } = null!;
    public string EnumValue { get; set; } = null!;
    public string LanguageCode { get; set; } = null!;
    public string TranslatedText { get; set; } = null!;
}