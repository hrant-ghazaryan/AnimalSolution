namespace TranslationDTOs;

public class TranslationGroupResponseDTO
{
    public string ResourceType { get; set; } = null!;
    public string LanguageCode { get; set; } = null!;

    public Dictionary<string, string> Values { get; set; } = new();
}
