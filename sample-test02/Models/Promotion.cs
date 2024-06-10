namespace sample_test02.Models;

public class Promotion
{
    public int Id { get; set; }
    public string Code { get; set; }
    public decimal Discount { get; set; }
    public ICollection<Client> Clients { get; set; }
}