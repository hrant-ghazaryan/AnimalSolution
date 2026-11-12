namespace AnimalExceptions;

public class AnimalInvalidData : AnimalException
{
    public AnimalInvalidData(string message) : base(message, 400)
    { }
}
