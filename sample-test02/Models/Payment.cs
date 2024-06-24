namespace sample_test02.Models;

public class Payment
{
    public int IdPayment { get; set; }

    public DateTime Date { get; set; }

    public int IdClient { get; set; }

    public int IdSubscription { get; set; }

    public decimal Value { get; set; }

    public virtual Client IdClientNavigation { get; set; } = null!;

    public virtual Subscription IdSubscriptionNavigation { get; set; } = null!;
}