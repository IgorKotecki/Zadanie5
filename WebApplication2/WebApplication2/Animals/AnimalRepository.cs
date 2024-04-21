using System.Data.SqlClient;

namespace WebApplication2.Animals;

public interface IAnimalRepository
{
    public IEnumerable<Animal> GetAllAnimals(string orderBy);
    public bool CreateAnimal(string name, string description, string category, string area);
    public Animal GetOneAnimal(int id);
    public int UpdateAnimal(Animal animal);
    public int DeleteAnimal(int id);
}

public class AnimalRepository : IAnimalRepository
{
    private readonly IConfiguration _configuration;

    public AnimalRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Animal GetOneAnimal(int id)
    {
        SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        using var command = new SqlCommand($"SELECT IdAnimal, Name, Description, Category, Area FROM Animal WHERE IdAnimal=@id", connection);
        
        command.Parameters.AddWithValue("@id", id);
        
        using var reader = command.ExecuteReader();
        
        if (!reader.Read()) return null;
        
        var animal = new Animal() 
        { ID = (int)reader["IdAnimal"], Name = (string)reader["Name"], Description = (string)reader["Description"], 
            Category = (string)reader["Category"], Area = (string)reader["Area"]!
        };
        
        connection.Close();
        return animal;
    }

    public IEnumerable<Animal> GetAllAnimals(string orderBy)
    {
        SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        var safeOrderBy = new string[] {"Name","Description","Category","Area"}.Contains(orderBy) ? orderBy : "Name";
        using var command = new SqlCommand($"SELECT Name, Description, Category, Area FROM Animal ORDER BY {safeOrderBy}", connection);
        using var reader = command.ExecuteReader();

        var animals = new List<Animal>();
        while (reader.Read())
        {
            var animal = new Animal()
            {
                ID = (int)reader["ID"], Name = (string)reader["Name"], Description = (string)reader["Description"],
                Category = (string)reader["Category"], Area = (string)reader["Area"]!
            };
            animals.Add(animal);
        }
        connection.Close();
        return animals;
    }

    public bool CreateAnimal(string name,string description,string category,string area)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        using var command = new SqlCommand($"INSERT INTO Animal (Name,Description,Category,Area) Values (@name, @description, @category, @area)",connection);
        
        command.Parameters.AddWithValue("@name", name);
        command.Parameters.AddWithValue("@description", description);
        command.Parameters.AddWithValue("@category", category);
        command.Parameters.AddWithValue("@area", area);
        
        var affectedRows = command.ExecuteNonQuery();
        
        connection.Close();
        return affectedRows == 1;
    }

    public int DeleteAnimal(int id)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        using var command = new SqlCommand("DELETE FROM Animal WHERE IdAnimal=@Id",connection);
        command.Parameters.AddWithValue("@Id", id);

        var affected = command.ExecuteNonQuery();
        return affected;
    }

    public int UpdateAnimal(Animal animal)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        using var command = new SqlCommand("UPDATE Animal SET Name = @name, Description = @description, Category = @category, Area = @area WHERE IdAnimal = @Id", connection);
        command.Parameters.AddWithValue("@Id", animal.ID);
        command.Parameters.AddWithValue("@name", animal.Name);
        command.Parameters.AddWithValue("@description", animal.Description);
        command.Parameters.AddWithValue("@category", animal.Category);
        command.Parameters.AddWithValue("@area", animal.Area);
        
        var affectedCount = command.ExecuteNonQuery();
        return affectedCount;
    }
}