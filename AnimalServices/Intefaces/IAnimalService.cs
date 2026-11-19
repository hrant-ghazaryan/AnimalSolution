using AnimalDTOs;

namespace AnimalServices.Intefaces;

public interface IAnimalService
{
    Task Add(AnimalCreateDTO animal);

    Task<AnimalCreateDTO> Get(int id);

    Task<IEnumerable<AnimalCreateDTO>> GetAll();

    Task Update(int id, AnimalCreateDTO animal);

    Task Remove(int id);

    Task Close(int id);

    Task<IEnumerable<AnimalCreateDTO>> GetAnimalByPageAndCount(int page, int count);

    Task<IEnumerable<AnimalCreateDTO>> SearchBy(string word);
}