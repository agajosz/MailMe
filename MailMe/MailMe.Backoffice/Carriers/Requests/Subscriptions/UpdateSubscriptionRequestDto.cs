namespace MailMe.Backend.Carriers.Requests.Subscriptions
{
    public class UpdateSubscriptionRequestDto
    {
        public int Id { get; set; }
        public string NewsletterType { get; set; }
        public string NewsletterPeriod { get; set; }
        public string Season { get; set; }
        public int LeagueId { get; set; }
    }
}