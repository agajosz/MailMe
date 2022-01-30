using MailMe.Data.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MailMe.Data.Datastructure.Subscriptions.Mappings
{
    public class SubscriptionMaps : IEntityTypeConfiguration<Subscription>
    {
        private const string TableName = "Subscriptions";
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.ToTable(TableName);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Season).HasMaxLength(MappingConstants.MaxSeasonLength);
            builder.Property(x => x.NewsletterType).HasMaxLength(MappingConstants.MaxNewsletterTypeLength);
            builder.Property(x => x.NewsletterType).HasMaxLength(MappingConstants.MaxNewsletterTypeLength);

            
        }
    }
}