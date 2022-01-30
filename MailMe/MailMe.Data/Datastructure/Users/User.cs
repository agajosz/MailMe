using System.Collections.Generic;
using MailMe.Data.Datastructure.Subscriptions;
using MailMe.Data.Repositories.Subscriptions;

namespace MailMe.Data.Datastructure.Users
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        private readonly List<SubscriptionUser> _subscriptions;
        public IReadOnlyCollection<SubscriptionUser> Subscriptions => _subscriptions;

        public User()
        {
            _subscriptions = new List<SubscriptionUser>();
        }
    }
}
