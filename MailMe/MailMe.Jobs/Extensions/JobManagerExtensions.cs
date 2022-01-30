using System;
using System.Collections.Generic;
using System.Linq;
using Hangfire;
using Hangfire.Common;
using MailMe.Jobs.CronJobs.ApiFootball.Fixtures;
using MailMe.Jobs.CronJobs.Newsletters.Weekly;
using MailMe.Jobs.Interfaces;

namespace MailMe.Jobs.Extensions
{
    public static class JobManagerExtensions
    {
        public static void AddFixturesDataFeedJobs(this IRecurringJobManager recurringJobManager,
            IEnumerable<FixturesDataFeedJobSettings> fixturesJobsSettings)
        {
            var fixturesJobsSettingsEnumerable = fixturesJobsSettings as FixturesDataFeedJobSettings[] ??
                                                 fixturesJobsSettings.ToArray();
            if (!fixturesJobsSettingsEnumerable.Any())
            {
                throw new ArgumentException("Fixtures data feed jobs null or empty.");
            }

            foreach (var settings in fixturesJobsSettingsEnumerable)
            {
                var jobName =
                    $"{nameof(IFixturesDataFeedImport.GetFixturesDataFeedAsync)}-LeagueId:{settings.LeagueId}";
                recurringJobManager.AddOrUpdate(jobName,
                    Job.FromExpression<IFixturesDataFeedJobInitializer>(x => x.InvokeAsync(settings)),
                    settings.CronSchedule, new RecurringJobOptions
                    {
                        QueueName = "default",
                        TimeZone = TimeZoneInfo.Utc
                    });
            }
        }

        public static void AddWeeklyLeagueNewsletterJobs(this IRecurringJobManager recurringJobManager,
            IEnumerable<WeeklyLeagueNewsletterSettings> weeklyLeagueNewslettersJobsSettings)
        {
            var weeklyLeagueNewslettersJobsSettingsEnumerable =
                weeklyLeagueNewslettersJobsSettings as WeeklyLeagueNewsletterSettings[] ??
                weeklyLeagueNewslettersJobsSettings.ToArray();

            if (!weeklyLeagueNewslettersJobsSettingsEnumerable.Any())
            {
                throw new ArgumentException("Weekly newsletter jobs null or empty.");
            }

            foreach (var settings in weeklyLeagueNewslettersJobsSettingsEnumerable)
            {
                var jobName = $"{nameof(IWeeklyLeagueNewsletterJob.DeliverNewsletterAsync)}-Weekly-LeagueId:{settings.LeagueId}";
                recurringJobManager.AddOrUpdate(jobName,
                    Job.FromExpression<IWeeklyLeagueNewsletterInitializer>(x => x.InvokeAsync(settings)),
                    settings.CronSchedule, new RecurringJobOptions
                    {
                        QueueName = "default",
                        TimeZone = TimeZoneInfo.Utc
                    });
            }
        }
    }
}