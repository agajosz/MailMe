using System.Threading;
using System.Threading.Tasks;
using MailMe.Jobs.CronJobs.Newsletters.Weekly;

namespace MailMe.Jobs.Interfaces
{
    public interface IWeeklyLeagueNewsletterJob
    {
        public Task DeliverNewsletterAsync(WeeklyLeagueNewsletterSettings settings,
            CancellationToken cancellationToken = default);
    }
}