using AnimalModels.Entitites;

namespace AnimalDTOs;

public class AnimalDTO
{
    public string? Name { get; set; }
    public int Age { get; set; }
    public double Weight { get; set; }
    public string? OwnedBy { get; set; }

    public Color Color { get; set; }
    public AnimalGender Gender { get; set; }

    public byte[] RowVersion { get; set; } = [];
}
