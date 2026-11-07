namespace AnimalModels.Entitites.Abstractions;

public abstract class Animal
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public double Weight { get; set; }

    public List<Color> Color { get; set; } = null!;
    public AnimalGender Gender { get; set; }
    public ActivityPattern ActivityPattern { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.Now;
    public DateTime? ClosedDate { get; set; }
}
public enum AnimalGender : byte
{
    Male = 1,
    Female
}
public enum ActivityPattern : byte
{
    Diurnal = 1,      
    Nocturnal,    
    Crepuscular   
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