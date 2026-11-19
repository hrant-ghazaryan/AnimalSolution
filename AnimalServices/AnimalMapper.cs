using AnimalModels.Entitites;
using AnimalDTOs;

namespace AnimalServices;

public static class AnimalMapper
{
    public static AnimalCreateDTO MapToDTO(Animal animal)
        => new AnimalCreateDTO
        {
            Name = animal.Name,
            Age = animal.Age,
            Weight = animal.Weight,
            OwnedBy = animal.OwnedBy,
            Color = animal.Color,
            Gender = animal.Gender,
            RowVersion = animal.RowVersion
        };
    public static IEnumerable<AnimalCreateDTO> MapToDTOs(IEnumerable<Animal> animals)
    {
        if (animals.Count() == 0)
            return [];

        List<AnimalCreateDTO> animalDTOs = new();
        foreach (var animal in animals)
        {
            var animalDTO = MapToDTO(animal);
            animalDTOs.Add(animalDTO);
        }

        return animalDTOs;
    }
    public static Animal MapToEntity(AnimalCreateDTO animal)
        => new Animal
        {
            Name = animal.Name,
            Age = animal.Age,
            Weight = animal.Weight,
            OwnedBy = animal.OwnedBy,
            Color = animal.Color,
            Gender = animal.Gender,
            CreatedAt = DateTime.UtcNow
        };
    public static IEnumerable<Animal> MapToEntities(IEnumerable<AnimalCreateDTO> animalDTOs)
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
