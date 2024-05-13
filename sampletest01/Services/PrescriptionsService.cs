using sampletest01.Models;
using sampletest01.Models.DTOs;
using sampletest01.Repositories;

namespace sampletest01.Services;

public class PrescriptionsService : IPrescriptionsService
{
    private readonly IPrescriptionsRepository _prescriptionsRepository;

    public PrescriptionsService(IPrescriptionsRepository prescriptionsRepository)
    {
        _prescriptionsRepository = prescriptionsRepository;
    }

    public Task<IEnumerable<Prescription>> GetPrescriptionsAsync(string doctorLastName)
    {
        return _prescriptionsRepository.GetPrescriptionsAsync(doctorLastName);
    }

    public Task<Prescription> CreatePrescriptionAsync(CreatePrescriptionDto createPrescriptionDto)
    {
        return _prescriptionsRepository.CreatePrescriptionAsync(createPrescriptionDto);
    }
}