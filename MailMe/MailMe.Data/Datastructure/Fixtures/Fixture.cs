using System;

namespace MailMe.Data.Datastructure.Fixtures
{
    public class Fixture
    {
        public int Id { get; set; }
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