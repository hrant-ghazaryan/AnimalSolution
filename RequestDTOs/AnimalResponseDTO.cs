namespace RequestDTOs;

public class AnimalResponseDTO
{
    public string? Name { get; set; }

    public int Age { get; set; }

    public double Weight { get; set; }

    public string Color { get; set; } = null!;

    public string Gender { get; set; } = null!;
}