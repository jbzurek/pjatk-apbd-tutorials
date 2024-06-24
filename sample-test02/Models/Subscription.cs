namespace sample_test02.Models;

public class Subscription
{
    public int IdSubscription { get; set; }

    public string Name { get; set; } = null!;

    public int RenewalPeriod { get; set; }

    public DateTime EndTime { get; set; }

    public decimal Price { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}