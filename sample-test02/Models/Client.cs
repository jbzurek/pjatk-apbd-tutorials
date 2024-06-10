namespace sample_test02.Models;

public class Client
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public ICollection<Reservation> Reservations { get; set; }
    public ICollection<Promotion> Promotions { get; set; }
}