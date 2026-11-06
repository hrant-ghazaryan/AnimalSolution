namespace AnimalServicesLibrary.Intefaces;

interface IAnimalService<T1,T2>
{
    public void Add(T1 animal);
    public void Remove(int id);
    public void Update(int id, T1 animal);
    public T2 Get(int id);
    public IEnumerable<T2> GetAll();
}