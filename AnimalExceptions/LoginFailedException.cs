namespace AnimalExceptions;

public class LoginFailedException : AppException
{
    public LoginFailedException()
        : base("Invalid email or password.", 401)
    { }
}
