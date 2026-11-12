namespace AnimalExceptions;

public class AnimalLockedException : AnimalException
{
    public AnimalLockedException()
        : base("This animal is currently being updated by another user.", 409)
    { }
}
