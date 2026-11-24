using AnimalDTOs;
using AnimalExceptions;
using AnimalModels.Data;
using AnimalModels.Entitites;
using AnimalServices.Implementations;
using AnimalServices.Intefaces;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;


namespace AnimalAPI.IntegrationTests;

public class AnimalServiceTests : SqliteTestBase
{

    [Fact]
    public async Task AnimalCreate_ValidDto_NotThrow()
    {
        var animalCreateDto = AnimalFactory.CreateDTO();

        await using (var context = CreateContext())
        {
            var sut = new AnimalService(context);
            await sut.Add(animalCreateDto);
        }

        await using var savedContext = CreateContext();
        var saved = await savedContext.Animals.SingleAsync();

        Assert.True(saved.Id > 0);
        Assert.Equal(animalCreateDto.Name, saved.Name);
        Assert.Equal(animalCreateDto.Age, saved.Age);
        Assert.Equal(animalCreateDto.Weight, saved.Weight);
        Assert.Equal(animalCreateDto.Color, saved.Color);
        Assert.Equal(animalCreateDto.Gender, saved.Gender);
        Assert.Equal(animalCreateDto.OwnedBy, saved.OwnedBy);
    }

    [Fact]
    public async Task AnimalCreate_InValidDto_Throws()
    {
        var animalCreateDto = AnimalFactory.CreateDTO();
        animalCreateDto.Age = -1;

        await using (var context = CreateContext())
        {
            var sut = new AnimalService(context);
            await Assert.ThrowsAsync<AnimalInvalidData>(() => sut.Add(animalCreateDto));
        }

        await using var savedContext = CreateContext();
        Assert.Equal(0, savedContext.Animals.Count());
    }

    [Fact]
    public async Task AnimalUpdate_ConcurrencyCheck_Throws()
    {
        var seeded = await SeedAsync(AnimalFactory.CreateEntity(name: "Rex"));
        var id = seeded[0].Id;
        var rowVersionSeenByBoth = seeded[0].RowVersion;

        await using (var user1 = CreateContext())
        {
            var sut = new AnimalService(user1);

            var updateDto1 = AnimalFactory.CreateDTO(name: "Grey");
            updateDto1.RowVersion = rowVersionSeenByBoth;

            await sut.Update(id, updateDto1);
        }

        await using (var user2 = CreateContext())
        {
            var sut = new AnimalService(user2);

            var updateDto2 = AnimalFactory.CreateDTO(name: "Mia");
            updateDto2.RowVersion = rowVersionSeenByBoth;

            await Assert.ThrowsAsync<AnimalConcurrencyException>(() => sut.Update(id, updateDto2));
        }

        await using var assertContext = CreateContext();
        var finalAnimal = await assertContext.Animals.SingleAsync(a => a.Id == id);
        Assert.Equal("Grey", finalAnimal.Name);
    }
}