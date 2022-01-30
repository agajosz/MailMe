using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MailMe.Application.Subscriptions.Entity;
using MailMe.Application.Subscriptions.Interfaces;

namespace MailMe.Application.Subscriptions.Boundary
{
    public class SubscriptionsBusiness : ISubscriptionsBusiness
    {
        private readonly ISubscriptionsRepository _subscriptionsRepository;

        public SubscriptionsBusiness(ISubscriptionsRepository subscriptionsRepository)
        {
            _subscriptionsRepository = subscriptionsRepository;
        }
        
        public async Task<Subscription> AddAsync(Subscription subscription, 
            CancellationToken cancellationToken = default)
        {
            return await _subscriptionsRepository.AddAsync(subscription, cancellationToken);
        }

        public async Task RemoveAsync(int subscriptionId, CancellationToken cancellationToken = default)
        {
            await _subscriptionsRepository.RemoveAsync(subscriptionId, cancellationToken);
        }

        public async Task UpdateAsync(Subscription subscription, CancellationToken cancellationToken = default)
        {
            await _subscriptionsRepository.UpdateAsync(subscription, cancellationToken);
        }

        public async Task<Subscription> GetSubscriptionByDetailsAsync(SubscriptionDetails details, CancellationToken cancellationToken = default)
        {
            return await _subscriptionsRepository.GetSubscriptionByDetailsAsync(details, cancellationToken);
        }

        public async Task<Subscription> GetSubscriptionByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _subscriptionsRepository.GetByIdAsync(id, cancellationToken);
        }
    }
}