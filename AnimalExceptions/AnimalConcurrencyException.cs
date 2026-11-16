namespace AnimalExceptions;

public class AnimalConcurrencyException : AnimalException
{
    public AnimalConcurrencyException()
        : base("This animal has been updated. Please refresh and try again.", 409)
    { }
}