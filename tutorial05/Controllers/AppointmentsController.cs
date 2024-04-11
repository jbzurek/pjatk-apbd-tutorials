using Microsoft.AspNetCore.Mvc;
using tutorial05.DataStores;
using tutorial05.DTOs;
using tutorial05.Models;

namespace tutorial05.Controllers;

[Route("api/appointments")]
[ApiController]
public class AppointmentsController : ControllerBase
{
    // Gets animal appointments
    [HttpGet("{id}")]
    public ActionResult<IEnumerable<Appointment>> GetAnimalAppointments(int id)
    {
        var animal = AnimalsDataStore.Current.Animals.FirstOrDefault(x => x.Id == id);
        if (animal == null)
        {
            return NotFound();
        }

        var appointments = AppointmentDataStore.Current.Appointments
            .Where(x => x.AnimalId == id)
            .ToList();

        return Ok(appointments);
    }

    // Creates new appointment
    [HttpPost("{id}")]
    public ActionResult<Appointment> PostAppointment(AppointmentPostDto appointmentPostDto, int id)
    {
        var animal = AnimalsDataStore.Current.Animals.Any(x => x.Id == id);
        if (!animal)
        {
            return NotFound();
        }

        var appointment = new Appointment()
        {
            DateOfAppointment = appointmentPostDto.DateOfAppointment,
            AnimalId = id,
            Description = appointmentPostDto.Description,
            Price = appointmentPostDto.Price
        };

        AppointmentDataStore.Current.Appointments.Add(appointment);
        return Created();
    }
}