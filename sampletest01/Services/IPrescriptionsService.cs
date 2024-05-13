using sampletest01.Models;

namespace sampletest01.Services;

public interface IPrescriptionsService
{
    Task<IEnumerable<Prescription>> GetPrescriptionsAsync(string doctorLastName);
}