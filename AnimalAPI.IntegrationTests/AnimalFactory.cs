using AnimalDTOs;
using AnimalModels.Entitites;

namespace AnimalAPI.IntegrationTests;

public static class AnimalFactory
{
    public static AnimalCreateDTO CreateDTO(string name = "Rex", int age = 2, double weight = 25,
        Color color = Color.White, AnimalGender gender = AnimalGender.Male, string ownedBy = "Jack")
        => new AnimalCreateDTO
        {
            Name = name,
            OwnedBy = ownedBy,
            Age = age,
            Weight = weight,
            Color = color,
            Gender = gender
        };

    public static Animal CreateEntity(string name = "Rex", int age = 2, double weight = 25,
        Color color = Color.White, AnimalGender gender = AnimalGender.Male, string ownedBy = "Jack")
        => new() 
        {
            Name = name,
            OwnedBy = ownedBy,
            Age = age,
            Weight = weight,
            Color = color,
            Gender = gender
        };
}
