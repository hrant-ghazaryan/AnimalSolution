using AnimalDTOs;

namespace AnimalServices.Intefaces;

public interface IAnimalService
{
    Task Add(AnimalCreateDTO animal);

    Task<AnimalResponseDTO> Get(int id);

    Task<IEnumerable<AnimalResponseDTO>> GetAll();

    Task Update(int id, AnimalCreateDTO animal);

    Task Remove(int id);

    Task Close(int id);

    Task<IEnumerable<AnimalResponseDTO>> GetAnimalByPageAndCount(int page, int count);

    Task<IEnumerable<AnimalResponseDTO>> SearchBy(string word);
}