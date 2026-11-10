namespace TranslationModels.Entitites;

public class Translation
{
    public int Id { get; set; }

    public int? SourceLanguageId { get; set; }
    public Language? SourceLanguage { get; set; }

    public int? TargetLanguageId { get; set; }
    public Language? TargetLanguage { get; set; }

    public string Word { get; set; } = null!;
    public string TranslatedWord { get; set; } = null!;
}
