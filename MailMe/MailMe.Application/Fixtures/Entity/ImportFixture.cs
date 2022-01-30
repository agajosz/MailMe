using System;

namespace MailMe.Application.Fixtures.Entity
{
    public class ImportFixture
    {
        public int LeagueId { get; set; }
        public string Season { get; set; }
        public DateTime FixtureDate { get; set; }
        public string Status { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public int HomeGoals { get; set; }
        public int AwayGoals { get; set; }
    }
}