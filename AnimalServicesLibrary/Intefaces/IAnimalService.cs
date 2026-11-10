using RequestDTOs;

namespace AnimalServicesLibrary.Intefaces;

public interface IAnimalService
{
    public void Add(AnimalDTO animal);
    public void Remove(int id);
    public void Update(int id, AnimalDTO animal);
    public Task<AnimalDTO> Get(int id);
    public IEnumerable<AnimalDTO> GetAll();
}