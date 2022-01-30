using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MailMe.Data.Datastructure.Subscriptions.Mappings
{
    public class SubscriptionUserMap : IEntityTypeConfiguration<SubscriptionUser>
    {
        private const string TableName = "SubscriptionUsers";
        public void Configure(EntityTypeBuilder<SubscriptionUser> builder)
        {
            builder.HasKey(x => new { x.SubscriptionId, x.UserId });
            builder.ToTable(TableName);
        }
    }
}