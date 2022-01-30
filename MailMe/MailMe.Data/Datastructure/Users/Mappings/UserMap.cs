using System;
using MailMe.Data.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MailMe.Data.Datastructure.Users.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        private const string TableName = "Users";
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(TableName);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Username).HasMaxLength(MappingConstants.MaxUsernameLength);
            builder.Property(x => x.Email).HasMaxLength(MappingConstants.MaxEmailLength);

            builder
                .HasMany(x => x.Subscriptions)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .HasConstraintName("FK_SubscriptionUser_UserId")
                .OnDelete(DeleteBehavior.ClientCascade);

        }
    }
}
