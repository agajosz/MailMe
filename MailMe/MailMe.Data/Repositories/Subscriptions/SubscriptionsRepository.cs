using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MailMe.Application.Subscriptions.Entity;
using MailMe.Application.Subscriptions.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MailMe.Data.Repositories.Subscriptions
{
    public class SubscriptionsRepository : ISubscriptionsRepository
    {
        private readonly IMapper _mapper;
        private readonly MailMeDbContext _dbContext;

        public SubscriptionsRepository(IMapper mapper, MailMeDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<Subscription> AddAsync(Subscription subscription, CancellationToken cancellationToken = default)
        {
            var newSubscription = _mapper.Map<Datastructure.Subscriptions.Subscription>(subscription);
            _dbContext.Subscriptions.Add(newSubscription);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<Subscription>(newSubscription);
        }

        public async Task RemoveAsync(int subscriptionId, CancellationToken cancellationToken = default)
        {
            var subscriptionToRemove = _dbContext.Subscriptions.FirstOrDefault(x => x.Id == subscriptionId);
            if(subscriptionToRemove is not null)
            {
                _dbContext.Subscriptions.Remove(subscriptionToRemove);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Subscription subscription, CancellationToken cancellationToken = default)
        {
            var subscriptionToUpdate = _mapper.Map<Datastructure.Subscriptions.Subscription>(subscription);
            _dbContext.Subscriptions.Update(subscriptionToUpdate);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> ExistsByIdAsync(int subscriptionId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Subscriptions.AnyAsync(x => x.Id == subscriptionId, cancellationToken);
        }

        public async Task<Subscription> GetSubscriptionByDetailsAsync(SubscriptionDetails details, CancellationToken cancellationToken = default)
        {
            var result = await _dbContext.Subscriptions.FirstOrDefaultAsync(x =>
                x.LeagueId == details.LeagueId && x.Season == details.Season &&
                x.NewsletterPeriod == details.NewsletterPeriod
                && x.NewsletterType == details.NewsletterType, cancellationToken);
            
            return _mapper.Map<Subscription>(result);
        }

        public async Task<Subscription> GetByIdAsync(int subscriptionId, CancellationToken cancellationToken = default)
        {
            var result = await _dbContext.Subscriptions
                .FirstOrDefaultAsync(x => x.Id == subscriptionId, cancellationToken);
            
            return _mapper.Map<Subscription>(result);
        }
    }
}