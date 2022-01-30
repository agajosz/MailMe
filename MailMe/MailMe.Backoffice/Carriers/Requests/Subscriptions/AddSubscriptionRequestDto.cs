namespace MailMe.Backend.Carriers.Requests.Subscriptions
{
    public class AddSubscriptionRequestDto
    {
        public string NewsletterType { get; set; }
        public string NewsletterPeriod { get; set; }
        public string Season { get; set; }
        public int LeagueId { get; set; }
    }
}