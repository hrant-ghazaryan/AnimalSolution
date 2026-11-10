namespace AnimalExceptions;

public class AnimalWeightException : AnimalException
{
    public AnimalWeightException()
        : base("Animal Weight can`t be < 0", 400)
    { }
}