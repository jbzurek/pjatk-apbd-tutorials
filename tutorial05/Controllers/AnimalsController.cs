using Microsoft.AspNetCore.Mvc;

namespace tutorial05.Controllers;

// TODO: AnimalCreationDTO, AnimalUpdateDTO, AppointmentCreationDTO, AppointmentController

// Routing matches a request URI to an action on a controller
// Once we send a HTTP request the MVC framework tries to take an URI
// and map it to a controller - preferred way is endpoint routing
[Route("api/animals")]
[ApiController]
public class AnimalsController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Animal>> GetAnimals()
    {
        return Ok(AnimalsDataStore.Current.Animals);
    }

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
}