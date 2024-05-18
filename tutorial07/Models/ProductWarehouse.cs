using System.ComponentModel.DataAnnotations;

namespace tutorial07.Models;

public class ProductWarehouse
{
    [Required]
    public int IdProduct { get; }
    
    [Required]
    public int IdWarehouse { get; }
    
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Amount should be greater than 0!")]
    public int Amount { get; }
    
    [Required]
    public DateTime CreatedAt { get; }
}