using tutorial06.Models;
using tutorial06.Repositories;

namespace tutorial06.Services;

public class AnimalsService : IAnimalsService
{
    private readonly IAnimalsRepository _animalsRepository;

    public AnimalsService(IAnimalsRepository animalsRepository)
    {
        _animalsRepository = animalsRepository;
    }

    public IEnumerable<Animal> GetAnimals(string orderBy)
    {
        return _animalsRepository.GetAnimals(orderBy);
    }

    public Animal GetAnimalById(int id)
    {
        return _animalsRepository.GetAnimalById(id);
    }

    public int CreateAnimal(Animal animal)
    {
        return _animalsRepository.CreateAnimal(animal);
    }

    public int DeleteAnimal(int id)
    {
        return _animalsRepository.DeleteAnimal(id);
    }

    public int UpdateAnimal(Animal animal)
    {
        return _animalsRepository.UpdateAnimal(animal);
    }
}