namespace AnimalApp;

class InvalidAnimalWeightException : ApplicationException
{
    public InvalidAnimalWeightException(string message) : base(message) { }
}
