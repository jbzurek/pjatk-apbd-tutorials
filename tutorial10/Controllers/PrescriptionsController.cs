using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tutorial10.Models;
using tutorial10.Models.DTOs;

namespace tutorial10.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrescriptionsController : ControllerBase
{
    private readonly Context _context;

    public PrescriptionsController(Context context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> AddPrescription(AddPrescriptionDto dto)
    {
        if (dto.Medicaments.Count > 10)
        {
            return BadRequest("A prescription can include a maximum of 10 medications.");
        }

        var patient = await _context.Patients
            .FirstOrDefaultAsync(p => p.FirstName == dto.Patient.FirstName && p.LastName == dto.Patient.LastName);
        
        if (patient == null)
        {
            patient = new Patient
            {
                FirstName = dto.Patient.FirstName,
                LastName = dto.Patient.LastName,
                BirthDate = dto.Patient.BirthDate
            };
            _context.Patients.Add(patient);
        }

        var doctor = await _context.Doctors.FindAsync(dto.IdDoctor);
        if (doctor == null)
        {
            return BadRequest("Doctor not found.");
        }

        var prescription = new Prescription
        {
            IdDoctor = doctor,
            Date = dto.Date,
            DueDate = dto.DueDate
        };


        foreach (var med in dto.Medicaments)
        {
            var medicament = await _context.Medicaments.FindAsync(med.IdMedicament);
            if (medicament == null)
            {
                return BadRequest("Medicament not found.");
            }

            prescription.PrescriptionMedicaments.Add(new PrescriptionMedicament
            {
                IdMedicament = med.IdMedicament,
                Dose = med.Dose,
                Details = med.Details
            });
        }

        _context.Prescriptions.Add(prescription);
        await _context.SaveChangesAsync();

        return Ok(prescription);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatientDetails(int id)
    {
        var patient = await _context.Patients
            .Include(p => p.Prescriptions)
            .ThenInclude(p => p.IdDoctor)
            .Include(p => p.Prescriptions)
            .ThenInclude(p => p.PrescriptionMedicaments)
            .ThenInclude(pm => pm.IdMedicamentNav)
            .FirstOrDefaultAsync(p => p.IdPatient == id);

        if (patient == null)
        {
            return NotFound("Patient not found.");
        }

        var result = new
        {
            patient.IdPatient,
            patient.FirstName,
            patient.LastName,
            patient.BirthDate,
            Prescriptions = patient.Prescriptions
                .OrderBy(p => p.DueDate)
                .Select(p => new
                {
                    p.IdPrescription,
                    p.Date,
                    p.DueDate,
                    Doctor = new { p.IdDoctor, p.IdDoctor.FirstName, p.IdDoctor.LastName },
                    Medicaments = p.PrescriptionMedicaments.Select(pm => new
                    {
                        pm.IdMedicament,
                        pm.IdMedicamentNav.Name,
                        pm.Dose,
                        pm.Details
                    })
                })
        };

        return Ok(result);
    }
}