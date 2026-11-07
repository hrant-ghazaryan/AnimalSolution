using AnimalModels.Entitites.Abstractions;
using AnimalModels.Entitites.Abstractions.Interfaces;

namespace AnimalModels.Entitites.AnimalEntities;

public class Dog : Animal, IWalkable
{
    public bool IsTrained { get; set; }
    public DogBreed Breed { get; init; }
    public string? MicrochipNumber { get; set; }

    public int LegCount { get; set; } = 4;
    public void Walk()
        => Console.WriteLine("Dog Walking");
}
public enum DogBreed : byte
{
    Labrador = 1,
    GermanShepherd,
    Husky,
    Doberman,
    Rottweiler
}