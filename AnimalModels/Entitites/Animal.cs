namespace AnimalModels.Entitites;

public class Animal
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Age { get; set; }
    public double Weight { get; set; }

    public Color Color { get; set; }
    public AnimalGender Gender { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? ClosedDate { get; set; }
}
public enum AnimalGender : byte
{
    Male = 1,
    Female
}
public enum Color : byte
{
    White = 1,
    Black,
    Red,
    Blue,
    Green,
    Brown,
    Pink,
    Orange,
    Grey,
    Yellow
}