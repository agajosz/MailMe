using System.Collections.Generic;
using MailMe.Application.Subscriptions.Entity;

namespace MailMe.Application.Users.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public ICollection<Subscription> Subscriptions { get; set; }

        public User()
        {
            Subscriptions = new List<Subscription>();
        }
    }
}