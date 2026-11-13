namespace TranslationDTOs;

public class TranslationCreateDTO
{
    public string EnumType { get; set; } = null!;
    public string EnumValue { get; set; } = null!;
    public string LanguageCode { get; set; } = null!;
    public string TranslatedText { get; set; } = null!;
}