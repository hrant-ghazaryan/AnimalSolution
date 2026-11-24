using AnimalDTOs;
using AnimalExceptions;
using AnimalModels.Entitites;
using AnimalServices.Implementations;

namespace AniamlAPI.UnitTests;

public class AnimalValidatorTests
{
    private static AnimalCreateDTO CreateValidDTO()
        => new AnimalCreateDTO
    {
        Name = "Rex",
        Age = 2,
        Weight = 25,
        Color = Color.Red,
        Gender = AnimalGender.Male,
        OwnedBy = "Levon"
    };

    [Fact]
    public void ValidateAnimal_ValidAnimal_DoesNotThrow()
    {
        var validDto = CreateValidDTO();

        var exception = Record.Exception(() => AnimalService.ValidateAnimal(validDto));

        Assert.Null(exception);
    }

    [Fact]
    public void ValidateAnimal_NullDTO_Throws()
    {
        var exception = Assert.Throws<ArgumentNullException>(() => AnimalService.ValidateAnimal(null));

        Assert.Equal("animalDTO", exception.ParamName);
    }

    [Fact]
    public void ValidateAnimal_EmptyName_Throws()
    {
        var animalDto = CreateValidDTO();
        animalDto.Name = null;

        Assert.Throws<ArgumentNullException>(() => AnimalService.ValidateAnimal(animalDto));
    }

    [Fact]
    public void ValidateAnimal_NegativAge_Throws()
    {
        var animalDto = CreateValidDTO();
        animalDto.Age = -1;

        Assert.Throws<AnimalInvalidData>(() => AnimalService.ValidateAnimal(animalDto));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void ValidateAnimal_InvalidWeight_Throws(int weight)
    {
        var animalDto = CreateValidDTO();
        animalDto.Weight = weight;

        Assert.Throws<AnimalInvalidData>(() => AnimalService.ValidateAnimal(animalDto));
    }

    [Fact]
    public void ValidateAnimal_EmptyNameAndInvalidAge_NameIsCheckedFirst()
    {
        var animalDto = CreateValidDTO();
        animalDto.Name = " ";
        animalDto.Age = -1;

        Assert.Throws<ArgumentException>(() => AnimalService.ValidateAnimal(animalDto));
    }

    [Theory]
    [InlineData(1, 0.01)]     
    [InlineData(1, 1)]
    [InlineData(100, 5000)]   
    public void ValidateAnimal_BoundaryValidValues_DoesNotThrow(int age, double weight)
    {
        var dto = CreateValidDTO();
        dto.Age = age;
        dto.Weight = weight;

        Assert.Null(Record.Exception(() => AnimalService.ValidateAnimal(dto)));
    }
}
