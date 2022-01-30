using System.Collections.Generic;
using System.Linq;
using MailMe.Data.Datastructure.Users;

namespace MailMe.Data.Datastructure.Subscriptions
{
    public class Subscription
    {
        public int Id { get; set; }
        public string NewsletterType { get; set; }
        public string NewsletterPeriod { get; set; }
        public string Season { get; set; }
        public int LeagueId { get; set; }
        private readonly List<SubscriptionUser> _users;
        public IReadOnlyCollection<SubscriptionUser> Users => _users;

        public Subscription()
        {
            _users = new List<SubscriptionUser>();
        }

        public void AddUser(User user)
        {
            if (_users.Any(x => x.User == user))
            {
                return;
            }

            var subscriptionUser = new SubscriptionUser
            {
                Subscription = this,
                User = user
            };
            
            _users.Add(subscriptionUser);
        }

        public void RemoveUser(User user)
        {
            var subscriptionUser = _users.FirstOrDefault(x => x.UserId == user.Id);
            if (subscriptionUser is null)
            {
                return;
            }

            _users.Remove(subscriptionUser);
        }
    }
}