using AnimalModels.Entitites;

namespace RequestDTOs;

public class AnimalDTO
{
    public string? Name { get; set; }
    public int Age { get; set; }
    public double Weight { get; set; }

    public List<Color>? Color { get; set; } = null!;
    public AnimalGender Gender { get; set; }
}
