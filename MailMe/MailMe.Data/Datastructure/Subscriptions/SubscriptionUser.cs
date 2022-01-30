using MailMe.Data.Datastructure.Users;

namespace MailMe.Data.Datastructure.Subscriptions
{
    public class SubscriptionUser
    {
        public Subscription Subscription { get; set; }
        public int SubscriptionId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}