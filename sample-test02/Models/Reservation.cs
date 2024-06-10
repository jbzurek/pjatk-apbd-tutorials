namespace sample_test02.Models;

public class Reservation
{
    public int Id { get; set; }
    public int IdClient { get; set; }
    public Client Client { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public List<Boat> Boats { get; set; }
    public bool Fulfilled { get; set; }
    public bool IsAccepted { get; set; }
    public bool IsRejected { get; set; }
    public string CancelReason { get; set; }
}