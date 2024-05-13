using Microsoft.AspNetCore.Mvc;
using sampletest01.Models;
using sampletest01.Models.DTOs;
using sampletest01.Services;

namespace sampletest01.Controllers;

[ApiController]
[Route("api/prescriptions")]
public class PrescriptionsController : ControllerBase
{
    private readonly IPrescriptionsService _prescriptionsService;

    public PrescriptionsController(IPrescriptionsService prescriptionsService)
    {
        _prescriptionsService = prescriptionsService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Prescription>>> GetPrescriptionsAsync(string? doctorLastName)
    {
        var prescriptions = await _prescriptionsService.GetPrescriptionsAsync(doctorLastName);
        return Ok(prescriptions);
    }

    [HttpPost]
    public async Task<ActionResult<Prescription>> CreatePrescriptionAsync(CreatePrescriptionDto createPrescriptionDto)
    {
        if (createPrescriptionDto.DueDate <= createPrescriptionDto.Date)
        {
            return BadRequest("DueDate must be later than Date!");
        }

        var prescription = await _prescriptionsService.CreatePrescriptionAsync(createPrescriptionDto);
        return StatusCode(StatusCodes.Status201Created, prescription);
    }
}