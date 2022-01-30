using System;
using System.Collections.Generic;
using Hangfire;
using Hangfire.Console;
using Hangfire.MemoryStorage;
using Hangfire.SqlServer;
using HangfireBasicAuthenticationFilter;
using MailMe.Jobs.CronJobs;
using MailMe.Jobs.CronJobs.ApiFootball.Fixtures;
using MailMe.Jobs.CronJobs.Newsletters.Weekly;
using MailMe.Jobs.Handlers;
using MailMe.Jobs.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SendGrid.Extensions.DependencyInjection;

namespace MailMe.Jobs.Extensions
{
    public static class HangfireConfigurationExtensions
    {
        public static void UseRecurringJobManager(this IApplicationBuilder app, IConfiguration configuration)
        {
            var recurringJobManager = app.ApplicationServices.GetRequiredService<IRecurringJobManager>();

            var fixturesDataFeedImportJobsSettings = configuration
                .GetSection("Jobs:FixturesWeeklyPerLeague")
                .Get<List<FixturesDataFeedJobSettings>>();
            
            var weeklyLeagueNewsletterSettings = configuration
                .GetSection("Jobs:WeeklyLeagueNewsletters")
                .Get<List<WeeklyLeagueNewsletterSettings>>();
            
            recurringJobManager
                .AddFixturesDataFeedJobs(fixturesDataFeedImportJobsSettings);
            recurringJobManager
                .AddWeeklyLeagueNewsletterJobs(weeklyLeagueNewsletterSettings);
        }

        public static IServiceCollection AddFixturesDataFeedJobs(this IServiceCollection services) =>
            services
                .AddApiFootballJobClient()
                .AddTransient<IFixturesDataFeedJobInitializer, FixturesDataFeedJobInitializer>()
                .AddTransient<IFixturesDataFeedImport, FixturesDataFeedImportJob>();
        public static IServiceCollection AddWeeklyLeagueNewsletterJobs(this IServiceCollection services) =>
            services
                .AddTransient<IWeeklyLeagueNewsletterJob, WeeklyLeagueNewsletterJob>()
                .AddTransient<IWeeklyLeagueNewsletterInitializer, WeeklyLeagueNewsletterInitializer>();

        public static IServiceCollection AddCronJobHandler(this IServiceCollection services,
            IConfiguration configuration)
        {
            return services
                .AddHangfire(options =>
                {
                    var config = options
                        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                        .UseSimpleAssemblyNameTypeSerializer()
                        .UseRecommendedSerializerSettings()
                        .UseConsole();

                    if (configuration.GetValue<bool>("UseInMemoryDatabase"))
                    {
                        config.UseMemoryStorage();
                    }
                    else
                    {
                        var connectionString = configuration.GetConnectionString("HangFire");
                        config.UseSqlServerStorage(connectionString, new SqlServerStorageOptions
                        {
                            CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                            SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                            QueuePollInterval = TimeSpan.Zero,
                            UseRecommendedIsolationLevel = true,
                            DisableGlobalLocks = true,
                            SchemaName = "HangFire"
                        });
                    }
                })
                .AddHangfireServer()
                .AddSendGrid(cfg =>
                {   var apiKey = configuration.GetValue<string>("SendGrid:ApiKey");
                    Console.WriteLine(apiKey);
                    cfg.ApiKey = apiKey;
                })
                .Services
                .AddTransient<MailSendingHandler>()
                .AddTransient<IContentCreationHandler, EmailContentCreationHandler>()
                .AddTransient<IMessageCreationHandler, EmailMessageCreationHandler>();
        }

        public static IEndpointRouteBuilder AddHangfireDashboard(this IEndpointRouteBuilder builder,
            IConfiguration configuration)
        {
            var auth = configuration.GetSection("Hangfire:Authentication").Get<HangfireAuthentication>();
            
            builder.MapHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[]
                {
                    new HangfireCustomBasicAuthenticationFilter
                    {
                        User = auth.User,
                        Pass = auth.Password
                    }
                }
            });

            return builder;
        }
    }
}