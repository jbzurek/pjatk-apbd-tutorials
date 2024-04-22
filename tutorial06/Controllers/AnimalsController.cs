using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using tutorial06.DTOs;

namespace tutorial06.Controllers;

[ApiController]
[Route("api/animals")]
public class AnimalsController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<AnimalDto>> GetAnimals()
    {
        using SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=APBD;Integrated Security=True");
        using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "SELECT * FROM Animals";

        connection.Open();

        SqlDataReader reader = command.ExecuteReader();

        List<AnimalDto> animals = new List<AnimalDto>();
        while (reader.Read())
        {
            AnimalDto animal = new AnimalDto();
            animal.Id = (int)reader["id"];
            animal.Name = (string)reader["name"];
            animal.Description = (string)reader["description"];
            animal.Area = (string)reader["area"];
            animal.Category = (string)reader["category"];

            animals.Add(animal);
        }

        return Ok();
    }
}