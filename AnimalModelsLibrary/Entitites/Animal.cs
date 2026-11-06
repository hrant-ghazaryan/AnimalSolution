namespace AnimalModelsLibrary.Entitites;

class Animal
{
    public string? Name { get; set; }
    public Color Color { get; set; }
    public double Weight { get; set; }
    public DateTime? CreatedAt { get; set; } = DateTime.Now;
    public DateTime? ClosedAt { get; set; }
}

[Flags]
enum Color
{
    White = 1,
    Black = 2,
    Red = 4,
    Blue = 8,
    Green = 16,
    Brown = 32,
    Pink = 64,
    Orange = 128,
    Grey = 256,
    Yellow = 512
}