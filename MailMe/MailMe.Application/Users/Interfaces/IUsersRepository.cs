using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MailMe.Application.Users.Entity;

namespace MailMe.Application.Users.Interfaces
{
    public interface IUserRepository
    {
        Task<User> AddAsync(AddUser user, CancellationToken cancellationToken = default);
        Task RemoveAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<User>> GetBySubscriptionIdAsync(int subscriptionId,
            CancellationToken cancellationToken = default);
        Task BindUserWithSubscription(int userId, int[] subscriptionIds, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(int userId, CancellationToken cancellationToken);
        Task UpdateAsync(User user, CancellationToken cancellationToken);
    }
}