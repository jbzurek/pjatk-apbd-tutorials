using sampletest01.Models;

namespace sampletest01.Repositories;

public interface IPrescriptionsRepository
{
    Task<IEnumerable<Prescription>> GetPrescriptionsAsync(string doctorLastName);
}