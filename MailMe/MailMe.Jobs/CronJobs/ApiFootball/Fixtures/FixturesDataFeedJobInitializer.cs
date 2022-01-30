using System;
using System.Threading;
using System.Threading.Tasks;
using MailMe.Jobs.Interfaces;

namespace MailMe.Jobs.CronJobs.ApiFootball.Fixtures
{
    public class FixturesDataFeedJobInitializer : IFixturesDataFeedJobInitializer
    {
        private readonly IFixturesDataFeedImport _job;

        public FixturesDataFeedJobInitializer(IFixturesDataFeedImport job)
        {
            _job = job ?? throw new ArgumentNullException(nameof(job));
        }

        public async Task InvokeAsync(FixturesDataFeedJobSettings settings)
        {
            await _job.GetFixturesDataFeedAsync(settings, CancellationToken.None);
        }
    }

    public interface IFixturesDataFeedJobInitializer
    {
        public Task InvokeAsync(FixturesDataFeedJobSettings settings);
    }
}