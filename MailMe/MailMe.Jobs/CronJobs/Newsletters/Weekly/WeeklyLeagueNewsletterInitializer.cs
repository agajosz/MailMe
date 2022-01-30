using System;
using System.Threading;
using System.Threading.Tasks;
using MailMe.Jobs.Interfaces;

namespace MailMe.Jobs.CronJobs.Newsletters.Weekly
{
    public class WeeklyLeagueNewsletterInitializer : IWeeklyLeagueNewsletterInitializer
    {
        private readonly IWeeklyLeagueNewsletterJob _job;

        public WeeklyLeagueNewsletterInitializer(IWeeklyLeagueNewsletterJob job)
        {
            _job = job ?? throw new ArgumentNullException(nameof(job));
        }
        public async Task InvokeAsync(WeeklyLeagueNewsletterSettings settings)
        {
            await _job.DeliverNewsletterAsync(settings, CancellationToken.None);
        }
    }

    public interface IWeeklyLeagueNewsletterInitializer
    {
        public Task InvokeAsync(WeeklyLeagueNewsletterSettings settings);
    }
}