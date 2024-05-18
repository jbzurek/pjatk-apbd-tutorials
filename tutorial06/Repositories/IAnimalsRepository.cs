using tutorial06.Models;

namespace tutorial06.Repositories;

public interface IAnimalsRepository
{
    IEnumerable<Animal> GetAnimals(string orderBy);
    Animal GetAnimalById(int id);
    public int CreateAnimal(Animal animal);
    public int DeleteAnimal(int id);
    public int UpdateAnimal(Animal animal);
}