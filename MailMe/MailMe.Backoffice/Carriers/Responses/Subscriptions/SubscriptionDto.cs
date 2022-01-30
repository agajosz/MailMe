namespace MailMe.Backend.Carriers.Responses.Subscriptions
{
    public class SubscriptionDto
    {
        public int Id { get; set; }
        public string NewsletterType { get; set; }
        public string NewsletterPeriod { get; set; }
        public string Season { get; set; }
        public int LeagueId { get; set; }
    }
}