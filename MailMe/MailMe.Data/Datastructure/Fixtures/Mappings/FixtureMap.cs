using MailMe.Data.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MailMe.Data.Datastructure.Fixtures.Mappings
{
    public class FixtureMap : IEntityTypeConfiguration<Fixture>
    {
        private const string TableName = "Fixtures";
        public void Configure(EntityTypeBuilder<Fixture> builder)
        {
            builder.ToTable(TableName);
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => new { x.HomeTeam, x.AwayTeam, x.FixtureDate })
                .IsUnique();
            builder.Property(x => x.LeagueId);
            builder.Property(x => x.FixtureDate);
            builder.Property(x => x.Season).HasMaxLength(MappingConstants.MaxSeasonLength);
            builder.Property(x => x.Status).HasMaxLength(MappingConstants.MaxStatusLength);
            builder.Property(x => x.AwayGoals);
            builder.Property(x => x.AwayTeam).HasMaxLength(MappingConstants.MaxTeamNameLength);
            builder.Property(x => x.HomeGoals);
            builder.Property(x => x.HomeTeam).HasMaxLength(MappingConstants.MaxTeamNameLength);
        }
    }
}