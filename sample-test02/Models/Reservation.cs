namespace sample_test02.Models;

public class Reservation
{
    public int Id { get; set; }
    public int IdClient { get; set; }
    public Client Client { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public int IdBoatStandard { get; set; }
    public int NumOfBoats { get; set; }
    public bool Fulfilled { get; set; }
    public string CancelReason { get; set; }
    public decimal Price { get; set; }
}