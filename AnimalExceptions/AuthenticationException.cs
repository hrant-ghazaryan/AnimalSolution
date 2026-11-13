namespace AnimalExceptions;

public class AuthenticationException : AnimalException
{
    public AuthenticationException(string message, int statusCode)
        : base(message, statusCode)
    { }
}
