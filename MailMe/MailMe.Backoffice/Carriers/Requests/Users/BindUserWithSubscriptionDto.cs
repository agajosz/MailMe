namespace MailMe.Backend.Carriers.Requests.Users
{
    public class BindUserWithSubscriptionDto
    {
        public int UserId { get; set; }
        public int[] SubscriptionIds { get; set; }
    }
}