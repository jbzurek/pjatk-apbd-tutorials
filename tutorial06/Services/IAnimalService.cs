using tutorial06.Models;

namespace tutorial06.Services;

public interface IAnimalService
{
    IEnumerable<Animal> GetAnimals(string orderBy);
    Animal GetAnimalById(int id);
    public int CreateAnimal(Animal animal);
    public int DeleteAnimal(int id);
    public int UpdateAnimal(Animal animal);
}