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
        
        switch (orderBy.ToLower())
        {
            case "name":
                command.CommandText = "SELECT IdAnimal, Name, Description, Category, Area FROM Animal ORDER BY Name";
                break;
            case "description":
                command.CommandText = "SELECT IdAnimal, Name, Description, Category, Area FROM Animal ORDER BY Description";
                break;
            case "category":
                command.CommandText = "SELECT IdAnimal, Name, Description, Category, Area FROM Animal ORDER BY Category";
                break;
            case "area":
                command.CommandText = "SELECT IdAnimal, Name, Description, Category, Area FROM Animal ORDER BY Area";
                break;
            default:
                return new List<Animal>();
        }

        var dataReader = command.ExecuteReader();
        var animals = new List<Animal>();

        while (dataReader.Read())
        {
            var animal = new Animal()
            {
                IdAnimal = (int)dataReader["IdAnimal"],
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
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        using var command = new SqlCommand();
        command.Connection = connection;
        command.CommandText =
            "SELECT IdAnimal, Name, Description, Category, Area FROM Animal WHERE IdAnimal = @IdAnimal";
        command.Parameters.AddWithValue("@IdAnimal", id);

        var dataReader = command.ExecuteReader();
        if (!dataReader.Read())
        {
            return null;
        }

        var animal = new Animal()
        {
            IdAnimal = (int)dataReader["IdAnimal"],
            Name = dataReader["Name"].ToString(),
            Description = dataReader["Description"].ToString(),
            Category = dataReader["Category"].ToString(),
            Area = dataReader["Area"].ToString()
        };
        return animal;
    }

    public int CreateAnimal(Animal animal)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        using var command = new SqlCommand();
        command.Connection = connection;
        command.CommandText =
            "INSERT INTO Animal(Name, Description, Category, Area) VALUES (@Name, @Description, @Category, @Area)";
        command.Parameters.AddWithValue("@Name", animal.Name);
        command.Parameters.AddWithValue("@Description", animal.Description);
        command.Parameters.AddWithValue("@Category", animal.Category);
        command.Parameters.AddWithValue("@Area", animal.Area);

        var affectedCount = command.ExecuteNonQuery();
        return affectedCount;
    }

    public int DeleteAnimal(int id)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        using var command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "DELETE FROM Animal WHERE IdAnimal = @IdAnimal";
        command.Parameters.AddWithValue("@IdAnimal", id);

        var affectedCount = command.ExecuteNonQuery();
        return affectedCount;
    }

    public int UpdateAnimal(Animal animal)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        using var command = new SqlCommand();
        command.Connection = connection;
        command.CommandText =
            "UPDATE Animal SET Name = @Name, Description = @Description, Category = @Category, Area = @Area WHERE IdAnimal = @IdAnimal";
        command.Parameters.AddWithValue("@IdAnimal", animal.IdAnimal);
        command.Parameters.AddWithValue("@Name", animal.Name);
        command.Parameters.AddWithValue("@Description", animal.Description);
        command.Parameters.AddWithValue("@Category", animal.Category);
        command.Parameters.AddWithValue("@Area", animal.Area);

        var affectedCount = command.ExecuteNonQuery();
        return affectedCount;
    }
}