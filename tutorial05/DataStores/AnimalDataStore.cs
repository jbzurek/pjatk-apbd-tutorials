using tutorial05.Models;

namespace tutorial05.DataStores;

public class AnimalsDataStore
{
    public List<Animal> Animals { get; set; }

    public static AnimalsDataStore Current { get; } = new AnimalsDataStore();

    public AnimalsDataStore()
    {
        Animals = new List<Animal>()
        {
            new Animal
            {
                Id = 1,
                Name = "Fifek",
                Category = "Dog",
                Color = "White",
                Weight = 12,
            },
            new Animal
            {
                Id = 2,
                Name = "Miauczek",
                Category = "Cat",
                Color = "Orange",
                Weight = 7,
            },
            new Animal
            {
                Id = 3,
                Name = "Owca",
                Category = "Sheep",
                Color = "Black",
                Weight = 9,
            },
        };
    }
}