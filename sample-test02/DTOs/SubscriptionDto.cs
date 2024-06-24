namespace sample_test02.DTOs;

public class SubscriptionDto
{
    public int IdSubscription { get; set; }
    public string Name { get; set; }
    public int RenewalPeriod { get; set; }
    public decimal TotalPaidAmount { get; set; }
}