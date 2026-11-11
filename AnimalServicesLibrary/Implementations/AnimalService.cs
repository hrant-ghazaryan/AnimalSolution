using AnimalExceptions;
using AnimalModels.Data;
using AnimalServices;
using AnimalServicesLibrary.Intefaces;
using Microsoft.EntityFrameworkCore;
using RequestDTOs;

namespace AnimalServicesLibrary.Implementations;

class AnimalService : IAnimalService
{
    private readonly AppDbContext context;
    public AnimalService(AppDbContext Context)
        => context = Context;

    public async void Add(AnimalDTO animalDTO)
    {
        if (animalDTO.Weight < 0)
            throw new AnimalWeightException();

        var animal = AnimalMapper.MapToEntity(animalDTO);
        context.Animals.Add(animal);
        await context.SaveChangesAsync();
    }

    public async void Close(int id)
    {
        var animal = await context.Animals.SingleOrDefaultAsync(a => a.Id == id);
        if (animal is null)
            throw new ArgumentNullException();

        animal.ClosedDate = DateTime.UtcNow;
        await context.SaveChangesAsync();
    }

    public async Task<AnimalDTO> Get(int id)
    {
        var animal = await context.Animals.FirstOrDefaultAsync(x => x.Id == id);
        if (animal is null)
            throw new ArgumentNullException();

        var animalDTO = AnimalMapper.MapToDTO(animal);
        return animalDTO;
    }

    public IEnumerable<AnimalDTO> GetAll()
        => AnimalMapper.MapToDTOs(context.Animals.AsNoTracking());

    public IEnumerable<AnimalDTO> GetAnimalByPageAndCount(int page, int count)
    {
        var animals = context.Animals.
            Skip(page * 10).
            Take(count);

        if (animals.Count() is 0)
            return [];

        return AnimalMapper.MapToDTOs(animals);
    }

    public async void Remove(int id)
    {
        var animal = await context.Animals.FirstOrDefaultAsync(a => a.Id == id);
        if (animal is null)
            return;

        context.Animals.Remove(animal);
        await context.SaveChangesAsync();
    }

    public IEnumerable<AnimalDTO> SearchBy(string word)
    {
        if (string.IsNullOrEmpty(word))
            return [];
        
        var animals = context.Animals.
            Where(a => a.Name != null && a.Name.Contains(word));

        return AnimalMapper.MapToDTOs(animals);
    }

    public async void Update(int id, AnimalDTO animal)
    {
        var oldAnimal = await context.Animals.FirstOrDefaultAsync(a => a.Id == id);
        if (oldAnimal is null)
            throw new ArgumentNullException();

        oldAnimal.Name = animal.Name;
        oldAnimal.Age = animal.Age;
        oldAnimal.Weight = animal.Weight;
        oldAnimal.Color = animal.Color;
        oldAnimal.Gender = animal.Gender;

        await context.SaveChangesAsync();
    }
}
