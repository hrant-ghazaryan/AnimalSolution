namespace TranslationDTOs;

public class TranslationCreateDTO
{
    public string? OriginalWord { get; set; } = null!;
    public string? TranslatedWord { get; set; } = null!;
    public string? SourceLanguageCode { get; set; } = null!;
    public string? TargetLanguageCode { get; set; } = null!;
}