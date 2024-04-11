using Microsoft.AspNetCore.Mvc;
using tutorial05.DataStores;
using tutorial05.DTOs;
using tutorial05.Models;

namespace tutorial05.Controllers;

// Routing matches a request URI to an action on a controller
// Once we send a HTTP request the MVC framework tries to take an URI
// and map it to a controller - preferred way is endpoint routing
[Route("api/animals")]
[ApiController]
public class AnimalsController : ControllerBase
{
    // Gets list of animals
    [HttpGet]
    public ActionResult<IEnumerable<Animal>> GetAnimals()
    {
        return Ok(AnimalsDataStore.Current.Animals);
    }

    // Gets specific animal
    [HttpGet("{id}")]
    public ActionResult<Animal> GetAnimal(int id)
    {
        var animalToReturn = AnimalsDataStore.Current.Animals.FirstOrDefault(x => x.Id == id);
        if (animalToReturn == null)
        {
            return NotFound();
        }

        return Ok(animalToReturn);
    }

    // Creates new animal
    [HttpPost]
    public ActionResult<Animal> PostAnimal(AnimalPostDto animalPostDto)
    {
        var animal = new Animal()
        {
            Id = AnimalsDataStore.Current.Animals.Count + 1,
            Name = animalPostDto.Name,
            Category = animalPostDto.Category,
            Weight = animalPostDto.Weight,
            Color = animalPostDto.Color
        };
        AnimalsDataStore.Current.Animals.Add(animal);
        return Created();
    }

    // Updates animal data
    [HttpPut("{id}")]
    public ActionResult<Animal> PutAnimal(AnimalPutDto animalPutDto, int id)
    {
        var animalToPut = AnimalsDataStore.Current.Animals.FirstOrDefault(x => x.Id == id);
        if (animalToPut == null)
        {
            return NotFound();
        }

        animalToPut.Name = animalPutDto.Name;
        animalToPut.Weight = animalPutDto.Weight;
        animalToPut.Color = animalPutDto.Color;

        return Ok(animalToPut);
    }

    // Deletes animal
    [HttpDelete("{id}")]
    public ActionResult<Animal> DeleteAnimal(int id)
    {
        var animalToDelete = AnimalsDataStore.Current.Animals.FirstOrDefault(x => x.Id == id);
        if (animalToDelete == null)
        {
            return NotFound();
        }

        AnimalsDataStore.Current.Animals.Remove(animalToDelete);
        return Ok(animalToDelete);
    }
}