using System.Reflection;
using MailMe.Data.Datastructure.Fixtures;
using MailMe.Data.Datastructure.Subscriptions;
using MailMe.Data.Datastructure.Users;
using Microsoft.EntityFrameworkCore;

namespace MailMe.Data
{
    public class MailMeDbContext : DbContext
    {
        public MailMeDbContext(DbContextOptions options) : base(options)
        {
        }

        public MailMeDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        #region Sets

        public DbSet<User> Users { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<SubscriptionUser> SubscriptionsUsers { get; set; }
        public DbSet<Fixture> Fixtures { get; set; }
        #endregion
    }
}
