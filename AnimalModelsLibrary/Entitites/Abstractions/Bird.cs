namespace AnimalModels.Entitites.Abstractions;

class Bird : Animal
{
    public bool HasFeathers { get; } = true;
    public bool HasBeak { get; } = true;
    public bool HasWings { get; } = true;
    public bool IsWarmBlooded { get; } = true;
    public decimal? Wingspan { get; set; }
}
