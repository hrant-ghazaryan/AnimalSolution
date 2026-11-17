namespace RequestDTOs;

public class AnimalListResponseDTO
{
    public int Count { get; set; }

    public IEnumerable<AnimalResponseDTO> Animals { get; set; } = [];
}
