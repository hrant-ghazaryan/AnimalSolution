namespace AnimalExceptions;

public class AnimalInvalidData : AppException
{
    public AnimalInvalidData(string message) : base(message, 400)
    { }
}
