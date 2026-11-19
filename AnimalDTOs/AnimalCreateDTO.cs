using AnimalModels.Entitites;
using Color = AnimalModels.Entitites.Color;

namespace AnimalDTOs;

public class AnimalCreateDTO
{
    public string? Name { get; set; }
    public int Age { get; set; }
    public double Weight { get; set; }
    public string? OwnedBy { get; set; }

    public Color Color { get; set; }
    public AnimalGender Gender { get; set; }

    public byte[] RowVersion { get; set; } = [];
}