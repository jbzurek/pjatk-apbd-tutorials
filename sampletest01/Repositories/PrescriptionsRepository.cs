using System.Data.SqlClient;
using sampletest01.Models;

namespace sampletest01.Repositories;

public class PrescriptionsRepository : IPrescriptionsRepository
{
    private readonly IConfiguration _configuration;

    public PrescriptionsRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<IEnumerable<Prescription>> GetPrescriptionsAsync(string? doctorLastName = null)
    {
        await using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        await connection.OpenAsync();

        await using var command = new SqlCommand();
        command.Connection = connection;

        var prescriptions = new List<Prescription>();

        if (doctorLastName != null)
        {
            command.CommandText = "SELECT p.Date, p.DueDate, p.IdPatient, p.IdDoctor " +
                                  "FROM Prescription p INNER JOIN Doctor d ON p.IdDoctor = d.IdDoctor " +
                                  "WHERE d.LastName = @doctorLastName " +
                                  "ORDER BY p.Date DESC; ";
            command.Parameters.AddWithValue("@doctorLastName", doctorLastName);

            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var prescription = new Prescription()
                {
                    Date = DateTime.Parse(reader["Date"].ToString()),
                    DueDate = DateTime.Parse(reader["DueDate"].ToString()),
                    IdPatient = (int)reader["IdPatient"],
                    IdDoctor = (int)reader["IdDoctor"]
                };

                prescriptions.Add(prescription);
            }

            await reader.CloseAsync();
            command.Parameters.Clear();
        }
        else
        {
            command.CommandText = "SELECT Date, DueDate, IdPatient, IdDoctor " +
                                  "FROM Prescription " +
                                  "ORDER BY Prescription.Date DESC; ";
            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var prescription = new Prescription()
                {
                    Date = DateTime.Parse(reader["Date"].ToString()),
                    DueDate = DateTime.Parse(reader["DueDate"].ToString()),
                    IdPatient = (int)reader["IdPatient"],
                    IdDoctor = (int)reader["IdDoctor"]
                };

                prescriptions.Add(prescription);
            }

            await reader.CloseAsync();
        }

        await connection.CloseAsync();
        return prescriptions;
    }
}