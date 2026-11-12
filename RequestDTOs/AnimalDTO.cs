using AnimalModels.Entitites;

namespace RequestDTOs;

public class AnimalDTO
{
    public string? Name { get; set; }
    public int Age { get; set; }
    public double Weight { get; set; }

    public Color Color { get; set; }
    public AnimalGender Gender { get; set; }
}