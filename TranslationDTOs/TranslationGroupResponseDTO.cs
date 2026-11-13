namespace TranslationDTOs;

public class TranslationGroupResponseDTO
{
    public string EnumType { get; set; } = null!;
    public string LanguageCode { get; set; } = null!;

    public Dictionary<string, string> TranslatedWords { get; set; } = new();
}
