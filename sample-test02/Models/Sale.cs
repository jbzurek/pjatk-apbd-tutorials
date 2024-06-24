namespace sample_test02.Models;

public class Sale
{
    public int IdSale { get; set; }

    public int IdClient { get; set; }

    public int IdSubscription { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Client IdClientNavigation { get; set; } = null!;

    public virtual Subscription IdSubscriptionNavigation { get; set; } = null!;
}