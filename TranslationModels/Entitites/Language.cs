namespace TranslationModels.Entitites;

public class Language
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;

    public IEnumerable<Translation>? Translations = new List<Translation>();
}
