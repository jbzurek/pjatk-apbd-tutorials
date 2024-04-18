namespace tutorial06.DTOs;

public class AnimalDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; }
    public string Category { get; set; } = null!;
    public string Area { get; set; } = null!;
}