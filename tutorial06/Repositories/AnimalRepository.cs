using tutorial06.Models;
using tutorial06.Repositories.Interfaces;

namespace tutorial06.Repositories;

public class AnimalRepository : IAnimalRepository
{
    public IEnumerable<Animal> GetAnimals(string orderBy)
    {
        throw new NotImplementedException();
    }

    public Animal GetAnimalById(int id)
    {
        throw new NotImplementedException();
    }

    public int CreateAnimal(Animal animal)
    {
        throw new NotImplementedException();
    }

    public int DeleteAnimal(int id)
    {
        throw new NotImplementedException();
    }

    public int UpdateAnimal(Animal animal)
    {
        throw new NotImplementedException();
    }
}