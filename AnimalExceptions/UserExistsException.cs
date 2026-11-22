namespace AnimalExceptions;

public class UserExistsException : AppException
{
    public UserExistsException() 
        : base("User with this email or username already exists.", 400)
    { }
}