using AnimalModels.Entitites;
using RequestDTOs;

namespace AnimalServices;

public static class AnimalMapper
{
    public static AnimalDTO MapToDTO(Animal animal)
        => new AnimalDTO
        {
            Name = animal.Name,
            Age = animal.Age,
            Weight = animal.Weight,
            Color = animal.Color,
            Gender = animal.Gender
        };
    public static IEnumerable<AnimalDTO> MapToDTOs(IEnumerable<Animal> animals)
    {
        if (animals.Count() == 0)
            return [];

        List<AnimalDTO> animalDTOs = new();
        foreach (var animal in animals)
        {
            var animalDTO = MapToDTO(animal);
            animalDTOs.Add(animalDTO);
        }

        return animalDTOs;
    }
    public static Animal MapToEntity(AnimalDTO animal)
        => new Animal
        {
            Name = animal.Name,
            Age = animal.Age,
            Weight = animal.Weight,
            Color = animal.Color,
            Gender = animal.Gender,
            CreatedAt = DateTime.UtcNow
        };
    public static IEnumerable<Animal> MapToEntities(IEnumerable<AnimalDTO> animalDTOs)
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
