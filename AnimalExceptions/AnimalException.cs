namespace AnimalExceptions;

public class AnimalException : Exception
{
    public int StatusCode { get; }

    public AnimalException(string message, int statusCode) 
        : base(message)
        => StatusCode = statusCode;
}
