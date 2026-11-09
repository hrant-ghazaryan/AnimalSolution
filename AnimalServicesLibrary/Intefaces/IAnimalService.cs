using AnimalModels.Entitites;

namespace AnimalServicesLibrary.Intefaces;

interface IAnimalService
{
    public void Add(Animal animal);
    public void Remove(int id);
    public void Update(int id, Animal animal);
    public Animal Get(int id);
    public IEnumerable<Animal> GetAll();
}