using AnimalExceptions;
using AnimalModels.Data;
using AnimalServices.Intefaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using RequestDTOs;

namespace AnimalServices.Implementations;

public class AnimalService : IAnimalService
{
    private readonly AppDbContext context;
    public AnimalService(AppDbContext context)
        => this.context = context;

    public async Task Add(AnimalDTO animalDTO)
    {
        if (animalDTO.Weight <= 0 || animalDTO.Age <= 0)
            throw new AnimalInvalidData("Invalid Animal Input Data");

        var animal = AnimalMapper.MapToEntity(animalDTO);

        await context.Animals.AddAsync(animal);
        await context.SaveChangesAsync();
    }

    public async Task<AnimalDTO> Get(int id)
    {
        var animal = await context.Animals
            .SingleOrDefaultAsync(item => item.Id == id);

        if (animal is null)
            throw new AnimalNotFoundException();

        return AnimalMapper.MapToDTO(animal);
    }

    public async Task<IEnumerable<AnimalDTO>> GetAll()
    {
        var animals = await context.Animals
            .AsNoTracking()
            .ToListAsync();

        return AnimalMapper.MapToDTOs(animals);
    }

    public async Task Update(int id, AnimalDTO animalDTO)
    {
        if (animalDTO is null)
            throw new AnimalInvalidData("AnimalDTO is null");

        if (animalDTO.Weight <= 0 || animalDTO.Age <= 0)
            throw new AnimalInvalidData("Invalid Animal Input Data");

        if (animalDTO.RowVersion.Length == 0)
            throw new AnimalInvalidData("RowVersion is required");

        var animal = await context.Animals
            .SingleOrDefaultAsync(item => item.Id == id);

        if (animal is null)
            throw new AnimalNotFoundException();

        context.Entry(animal)
           .Property(item => item.RowVersion)
           .OriginalValue = animalDTO.RowVersion;

        animal.Name = animalDTO.Name;
        animal.Age = animalDTO.Age;
        animal.Weight = animalDTO.Weight;
        animal.Color = animalDTO.Color;
        animal.Gender = animalDTO.Gender;

        try
        {
            await context.SaveChangesAsync();
        }
        catch(DbUpdateConcurrencyException)
        {
            throw new AnimalConcurrencyException();
        }
    }

    public async Task Remove(int id)
    {
        var animal = await context.Animals
            .SingleOrDefaultAsync(item => item.Id == id);

        if (animal is null)
            throw new AnimalNotFoundException();

        context.Animals.Remove(animal);

        await context.SaveChangesAsync();
    }

    public async Task Close(int id)
    {
        var animal = await context.Animals
            .SingleOrDefaultAsync(item => item.Id == id);

        if (animal is null)
            throw new AnimalNotFoundException();

        animal.ClosedDate = DateTime.UtcNow;

        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<AnimalDTO>> GetAnimalByPageAndCount(int page, int count)
    {
        if (page < 1 || count < 1)
            return [];

        var animals = await context.Animals
            .AsNoTracking()
            .Skip((page - 1) * 10)
            .Take(count)
            .ToListAsync();

        return AnimalMapper.MapToDTOs(animals);
    }

    public async Task<IEnumerable<AnimalDTO>> SearchBy(string word)
    {
        if (string.IsNullOrEmpty(word))
            return [];

        var animals = await context.Animals
            .AsNoTracking()
            .Where(animal =>
                animal.Name != null &&
                animal.Name.Contains(word))
            .ToListAsync();

        return AnimalMapper.MapToDTOs(animals);
    }
}