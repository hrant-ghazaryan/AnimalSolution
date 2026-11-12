using RequestDTOs;

namespace AnimalServices.Intefaces;

public interface IAnimalService
{
    Task Add(AnimalDTO animal);

    Task<AnimalDTO> Get(int id);

    Task<IEnumerable<AnimalDTO>> GetAll();

    Task Update(int id, AnimalDTO animal);

    Task Remove(int id);

    Task Close(int id);

    Task<IEnumerable<AnimalDTO>> GetAnimalByPageAndCount(int page, int count);

    Task<IEnumerable<AnimalDTO>> SearchBy(string word);
}