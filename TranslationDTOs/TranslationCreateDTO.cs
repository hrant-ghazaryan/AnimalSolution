namespace TranslationDTOs;

public class TranslationCreateDTO
{
    public string ResourceType { get; set; } = null!;
    public string ResourceCode { get; set; } = null!;
    public string LanguageCode { get; set; } = null!;
    public string Value { get; set; } = null!;
}