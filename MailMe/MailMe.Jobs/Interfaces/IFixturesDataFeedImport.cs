using System.Threading;
using System.Threading.Tasks;
using MailMe.Jobs.CronJobs.ApiFootball.Fixtures;

namespace MailMe.Jobs.Interfaces
{
    public interface IFixturesDataFeedImport
    {
        Task GetFixturesDataFeedAsync(FixturesDataFeedJobSettings options,
            CancellationToken cancellationToken = default);
    }
}