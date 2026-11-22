namespace AnimalExceptions;

public class AnimalNotFoundException : AppException
{
    public AnimalNotFoundException() : base("Animal not found", 404)
    { }
}
