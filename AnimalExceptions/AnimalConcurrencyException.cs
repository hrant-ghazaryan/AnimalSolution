namespace AnimalExceptions;

public class AnimalConcurrencyException : AppException
{
    public AnimalConcurrencyException()
        : base("This animal has been updated. Please refresh and try again.", 409)
    { }
}