namespace AnimalModels.Entitites.Abstractions.Interfaces;

interface IWalkable
{
    public int LegCount { get; set; }
    void Walk();
}