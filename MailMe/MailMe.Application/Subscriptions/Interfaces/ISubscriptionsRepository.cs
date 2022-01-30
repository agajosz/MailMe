using System.Threading;
using System.Threading.Tasks;
using MailMe.Application.Subscriptions.Entity;

namespace MailMe.Application.Subscriptions.Interfaces
{
    public interface ISubscriptionsRepository
    {
        public Task<Subscription> AddAsync(Subscription subscription, CancellationToken cancellationToken = default);
        public Task RemoveAsync(int subscriptionId, CancellationToken cancellationToken = default);
        public Task UpdateAsync(Subscription subscription, CancellationToken cancellationToken = default);
        public Task<bool> ExistsByIdAsync(int subscriptionId, CancellationToken cancellationToken = default);
        Task<Subscription> GetSubscriptionByDetailsAsync(SubscriptionDetails details,
            CancellationToken cancellationToken = default);
        public Task<Subscription> GetByIdAsync(int subscriptionId, CancellationToken cancellationToken = default);
    }
}