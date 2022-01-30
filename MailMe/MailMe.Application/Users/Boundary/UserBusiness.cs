using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MailMe.Application.Subscriptions.Interfaces;
using MailMe.Application.Users.Entity;
using MailMe.Application.Users.Interfaces;

namespace MailMe.Application.Users.Boundary
{
    public class UserBusiness : IUsersBusiness
    {
        private readonly IUserRepository _userRepository;
        private readonly ISubscriptionsRepository _subscriptionsRepository;
        public UserBusiness(IUserRepository userRepository, ISubscriptionsRepository subscriptionsRepository)
        {
            _userRepository = userRepository;
            _subscriptionsRepository = subscriptionsRepository;
        }

        public async Task<User> AddAsync(AddUser user, CancellationToken cancellationToken = default)
        {
            return await _userRepository.AddAsync(user, cancellationToken);
        }

        public async Task RemoveAsync(int id, CancellationToken cancellationToken = default)
        {
            await _userRepository.RemoveAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _userRepository.GetAllAsync(cancellationToken);
        }

        public async Task<IEnumerable<User>> GetBySubscriptionIdAsync(int subscriptionId,
            CancellationToken cancellationToken = default)
        {
            return await _userRepository.GetBySubscriptionIdAsync(subscriptionId, cancellationToken);
        }

        public async Task BindUserWithSubscription(int userId, int[] subscriptionIds, CancellationToken cancellationToken = default)
        {
            await ValidateUserExistsAsync(userId, cancellationToken);
            await ValidateSubscriptionsExistAsync(subscriptionIds, cancellationToken);
            
            await _userRepository.BindUserWithSubscription(userId, subscriptionIds, cancellationToken);
        }

        public async Task UpdateUser(User user, CancellationToken cancellationToken = default)
        {
            await _userRepository.UpdateAsync(user, cancellationToken);
        }

        private async Task ValidateSubscriptionsExistAsync(int[] subscriptionIds, CancellationToken cancellationToken)
        {
            foreach (var subscriptionId in subscriptionIds)
            {
                var subscriptionsExist = await _subscriptionsRepository.ExistsByIdAsync(subscriptionId, cancellationToken);
                if (!subscriptionsExist)
                {
                    throw new ArgumentException($"Subscription with id: {subscriptionId} not found!");
                }
            }
        }

        private async Task ValidateUserExistsAsync(int userId, CancellationToken cancellationToken = default)
        {
            var userExists = await _userRepository.ExistsAsync(userId, cancellationToken);
            if (!userExists)
            {
                throw new ArgumentException($"User with id: {userId} not found!");
            }
        }
    }
}