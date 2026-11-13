namespace AnimalExceptions;

public class UserExistsException : AuthenticationException
{
    public UserExistsException() 
        : base("User with this email or username already exists.", 400)
    { }
}
