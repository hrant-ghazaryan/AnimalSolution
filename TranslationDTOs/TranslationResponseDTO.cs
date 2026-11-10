
namespace TranslationDTOs;

public class TranslationResponseDTO
{
    public string OriginalWord { get; set; } = null!;
    public List<TranslationDTO> Translations { get; set; } = new();
}
