namespace RequestDTOs;

public class AnimalResponseDTO
{
    public string? Name { get; set; }

    public int Age { get; set; }

    public double Weight { get; set; }

    public TranslatedValueDTO Color { get; set; } = null!;

    public TranslatedValueDTO Gender { get; set; } = null!;
}