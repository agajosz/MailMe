using System.Threading;
using System.Threading.Tasks;
using MailMe.Application.Subscriptions.Entity;

namespace MailMe.Application.Subscriptions.Interfaces
{
    public interface ISubscriptionsBusiness
    {
        Task<Subscription> AddAsync(Subscription subscription, CancellationToken cancellationToken = default);
        Task RemoveAsync(int subscriptionId, CancellationToken cancellationToken = default);

        Task UpdateAsync(Subscription subscription,
            CancellationToken cancellationToken = default);
        Task<Subscription> GetSubscriptionByDetailsAsync(SubscriptionDetails details,
            CancellationToken cancellationToken = default);
        Task<Subscription> GetSubscriptionByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}