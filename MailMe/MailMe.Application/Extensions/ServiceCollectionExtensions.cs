using MailMe.Application.Fixtures.Boundary;
using MailMe.Application.Fixtures.Interfaces;
using MailMe.Application.Subscriptions.Boundary;
using MailMe.Application.Subscriptions.Interfaces;
using MailMe.Application.Users.Boundary;
using MailMe.Application.Users.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MailMe.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomain(this IServiceCollection services) =>
             services
             .AddScoped<IUsersBusiness, UserBusiness>()
             .AddScoped<ISubscriptionsBusiness, SubscriptionsBusiness>()
             .AddScoped<IFixturesBusiness, FixturesBusiness>();
            
    }
}
