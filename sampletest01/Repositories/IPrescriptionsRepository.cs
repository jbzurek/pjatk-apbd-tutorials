using sampletest01.Models;
using sampletest01.Models.DTOs;

namespace sampletest01.Repositories;

public interface IPrescriptionsRepository
{
    Task<IEnumerable<Prescription>> GetPrescriptionsAsync(string doctorLastName);
    Task<Prescription> CreatePrescriptionAsync(CreatePrescriptionDto createPrescriptionDto);
}