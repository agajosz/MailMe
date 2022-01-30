using System.Collections.Generic;
using MailMe.Backend.Carriers.Responses.Subscriptions;

namespace MailMe.Backend.Carriers.Responses.Users
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public ICollection<SubscriptionDto> Subscriptions { get; set; }
    }
}