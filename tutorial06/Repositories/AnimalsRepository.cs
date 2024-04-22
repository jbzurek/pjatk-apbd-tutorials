using System.Data.SqlClient;
using tutorial06.Models;

namespace tutorial06.Repositories;

public class AnimalsRepository : IAnimalsRepository
{
    private IConfiguration _configuration;

    public AnimalsRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public IEnumerable<Animal> GetAnimals(string orderBy)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        using var command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "SELECT IdAnimal, Name, Description, Category, Area FROM Animal ORDER BY @Name, @Description, @Category, @Area";
        command.Parameters.AddWithValue("@Name", orderBy);
        command.Parameters.AddWithValue("@Description", orderBy);
        command.Parameters.AddWithValue("@Category", orderBy);
        command.Parameters.AddWithValue("@Area", orderBy);

        var dataReader = command.ExecuteReader();
        var animals = new List<Animal>();

        while (dataReader.Read())
        {
            var animal = new Animal()
            {
                IdAnimal = (int) dataReader["IdAnimal"],
                Name = dataReader["Name"].ToString(),
                Description = dataReader["Description"].ToString(),
                Category = dataReader["Category"].ToString(),
                Area = dataReader["Area"].ToString()
            };
            animals.Add(animal);
        }

        return animals;
    }

    public Animal GetAnimalById(int id)
    {
        throw new NotImplementedException();
    }

    public int CreateAnimal(Animal animal)
    {
        throw new NotImplementedException();
    }

    public int DeleteAnimal(int id)
    {
        throw new NotImplementedException();
    }

    public int UpdateAnimal(Animal animal)
    {
        throw new NotImplementedException();
    }
}