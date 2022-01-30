using MailMe.Application.Fixtures.Interfaces;
using MailMe.Application.Subscriptions.Interfaces;
using MailMe.Application.Users.Interfaces;
using MailMe.Data.Repositories.Fixtures;
using MailMe.Data.Repositories.Users;
using MailMe.Data.Repositories.Subscriptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace MailMe.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration) =>
            services
                .AddScoped<IUserRepository, UsersRepository>()
                .AddScoped<ISubscriptionsRepository, SubscriptionsRepository>()
                .AddScoped<IFixturesRepository, FixturesRepository>()
                .AddScoped<IFixturesImportRepository, FixturesImportRepository>()
                .AddDbContext<MailMeDbContext>((s,o) => o
                    .UseSqlServer(configuration.GetConnectionString("MailMe")), ServiceLifetime.Transient);

        public static void MigrateDatabase(this IServiceCollection services)
        {
            services
                .BuildServiceProvider()
                .GetService<MailMeDbContext>()
                ?.Database.Migrate();
        }
    }
}
