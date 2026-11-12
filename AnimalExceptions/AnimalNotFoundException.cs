namespace AnimalExceptions;

public class AnimalNotFoundException : AnimalException
{
    public AnimalNotFoundException() : base("Animal not found", 404)
    { }
}
