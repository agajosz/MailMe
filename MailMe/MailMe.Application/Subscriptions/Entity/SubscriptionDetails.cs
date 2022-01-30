namespace MailMe.Application.Subscriptions.Entity
{
    public class SubscriptionDetails
    {
        public string NewsletterType { get; set; }
        public string NewsletterPeriod { get; set; }
        public string Season { get; set; }
        public int LeagueId { get; set; }
    }
}