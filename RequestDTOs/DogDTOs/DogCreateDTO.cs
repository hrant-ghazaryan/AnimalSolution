using AnimalModels.Entitites.Abstractions;
using AnimalModels.Entitites.AnimalEntities;
using Color = AnimalModels.Entitites.Abstractions.Color;

namespace RequestDTOs.DogDTOs;

public class DogCreateDTO
{
    public string? Name { get; set; }
    public double Weight { get; set; }

    public List<Color> Color { get; set; } = null!;
    public AnimalGender Gender { get; set; }
    public ActivityPattern ActivityPattern { get; set; }

    public bool IsTrained { get; set; }
    public DogBreed Breed { get; init; }
    public string? MicrochipNumber { get; set; }
}
