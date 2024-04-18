namespace tutorial06.Models;

public class Animal
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; }
    public string Category { get; set; } = null!;
    public string Area { get; set; } = null!;
}