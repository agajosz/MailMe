using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MailMe.Application.Users.Entity;
using MailMe.Application.Users.Interfaces;
using MailMe.Data.Datastructure.Subscriptions;
using Microsoft.EntityFrameworkCore;

namespace MailMe.Data.Repositories.Users
{
    public class UsersRepository : IUserRepository
    {
        private readonly MailMeDbContext _dbContext;
        private readonly IMapper _mapper;

        public UsersRepository(MailMeDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<User> AddAsync(AddUser user, CancellationToken cancellationToken = default)
        {
            var newUser = _mapper.Map<Datastructure.Users.User>(user);
            await _dbContext.Users.AddAsync(newUser, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<User>(newUser);
        }

        public async Task RemoveAsync(int id, CancellationToken cancellationToken = default)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            await BindUserWithSubscription(id, Array.Empty<int>(), cancellationToken);
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default) => 
            await _dbContext.Users
                .Include(x => x.Subscriptions)
                .ThenInclude(x => x.Subscription)
                .ProjectTo<User>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

        public async Task<IEnumerable<User>> GetBySubscriptionIdAsync(int subscriptionId,
            CancellationToken cancellationToken = default)
        {
            var allUsersWithSubscriptions = _dbContext.Users
                .Include(x => x.Subscriptions)
                .ThenInclude(x => x.Subscription);
             var usersWithGivenSubscription = allUsersWithSubscriptions
                 .Where(x => x.Subscriptions.Any(s => s.SubscriptionId == subscriptionId));
             
             return await usersWithGivenSubscription
                 .ProjectTo<User>(_mapper.ConfigurationProvider)
                 .ToListAsync(cancellationToken);
        }

        public async Task BindUserWithSubscription(int userId, int[] subscriptionIds, CancellationToken cancellationToken = default)
        {
            var subscriptionsForGivenUser = _dbContext.SubscriptionsUsers
                .Where(x => x.UserId == userId);
            var subscriptionsToBeAdded = subscriptionIds
                .Except(subscriptionsForGivenUser
                .Select(x => x.SubscriptionId))
                .ToList();
            
            if (subscriptionsToBeAdded.Any())
            {
                await AddSubscriptionToUser(userId, subscriptionsToBeAdded, cancellationToken);
            }

            var subscriptionsToBeDeleted = subscriptionsForGivenUser
                .Where(x => !subscriptionIds.Contains(x.SubscriptionId))
                .ToList();
            
            if (subscriptionsToBeDeleted.Any())
            {
                 RemoveSubscriptionsFromUser(subscriptionsToBeDeleted);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private void RemoveSubscriptionsFromUser(IEnumerable<SubscriptionUser> subscriptionsToBeDeleted)
        {
             _dbContext.SubscriptionsUsers.RemoveRange(subscriptionsToBeDeleted);
        }

        private async Task AddSubscriptionToUser(int userId, List<int> subscriptionsToBeAdded, CancellationToken cancellationToken = default)
        {
            var userSubscriptions = subscriptionsToBeAdded.Select(subscriptionId => new SubscriptionUser
            {
                UserId = userId,
                SubscriptionId = subscriptionId
            }).ToList();

            await _dbContext.SubscriptionsUsers.AddRangeAsync(userSubscriptions, cancellationToken);
        }

        public async Task<bool> ExistsAsync(int userId, CancellationToken cancellationToken)
        {
            return await _dbContext.Users.AnyAsync(x => x.Id == userId, cancellationToken);
        }

        public async Task UpdateAsync(User user, CancellationToken cancellationToken)
        {
            var userToUpdate = _mapper.Map<Datastructure.Users.User>(user);
            _dbContext.Users.Update(userToUpdate);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
