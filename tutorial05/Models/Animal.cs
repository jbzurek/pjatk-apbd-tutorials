namespace tutorial05.Models;

public class Animal
{
    public required int Id { get; init; }
    public string Name { get; set; }
    public string Category { get; set; }
    public double Weight { get; set; }
    public string Color { get; set; }
}