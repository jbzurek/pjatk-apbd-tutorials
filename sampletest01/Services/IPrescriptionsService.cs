using sampletest01.Models;
using sampletest01.Models.DTOs;

namespace sampletest01.Services;

public interface IPrescriptionsService
{
    Task<IEnumerable<Prescription>> GetPrescriptionsAsync(string doctorLastName);
    Task<Prescription> CreatePrescriptionAsync(CreatePrescriptionDto createPrescriptionDto);
}