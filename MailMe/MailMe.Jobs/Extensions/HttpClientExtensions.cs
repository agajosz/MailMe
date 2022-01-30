using MailMe.Jobs.CronJobs;
using MailMe.Jobs.CronJobs.ApiFootball;
using MailMe.Jobs.CronJobs.Newsletters;
using MailMe.Jobs.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MailMe.Jobs.Extensions
{
    public static class HttpClientExtensions
    {
        public static IServiceCollection AddApiFootballJobClient(this IServiceCollection services) =>
            services
                .AddHttpClient<RapidApiCronJobHttpClient>()
                .Services
                .AddTransient<IRapidApiCronJobHttpClient, RapidApiCronJobHttpClient>();
    }
}