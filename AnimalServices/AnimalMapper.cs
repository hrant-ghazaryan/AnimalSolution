using AnimalModels.Entitites;
using AnimalDTOs;
using AnimalExceptions;

namespace AnimalServices;

public static class AnimalMapper
{
    public static AnimalResponseDTO MapToDTO(Animal animal)
        => new AnimalResponseDTO
        {
            Name = animal.Name,
            Age = animal.Age,
            Weight = animal.Weight,
            Color = animal.Color.ToString(),
            Gender = animal.Gender.ToString(),
            OwnedBy = animal.OwnedBy
        };
    public static IEnumerable<AnimalResponseDTO> MapToDTOs(IEnumerable<Animal> animals)
    {
        if (animals.Count() == 0)
            return [];

        List<AnimalResponseDTO> animalDTOs = new();
        foreach (var animal in animals)
        {
            var animalDTO = MapToDTO(animal);
            animalDTOs.Add(animalDTO);
        }

        return animalDTOs;
    }
    public static Animal MapToEntity(AnimalResponseDTO animal)
    {
        var animalColor = Enum.TryParse(animal.Color, out Color color);
        var animalGender = Enum.TryParse(animal.Gender, out AnimalGender gender);

        if (!animalColor || !animalGender)
            throw new AnimalInvalidData("Invalid Color or Gender value");

        return new Animal
        {
            Name = animal.Name,
            Age = animal.Age,
            Weight = animal.Weight,
            Color = color,
            Gender = gender,
            OwnedBy = animal.OwnedBy,
            CreatedAt = DateTime.UtcNow
        };
    }
    public static IEnumerable<Animal> MapToEntities(IEnumerable<AnimalResponseDTO> animalDTOs)
    {
        if (animalDTOs.Count() == 0)
            return [];

        List<Animal> animals = new();
        foreach (var animalDTO in animalDTOs)
        {
            var animal = MapToEntity(animalDTO);
            animals.Add(animal);
        }

        return animals;
    }
}
