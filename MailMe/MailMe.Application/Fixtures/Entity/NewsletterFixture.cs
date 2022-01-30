using System;
using MailMe.Application.Fixtures.Interfaces;

namespace MailMe.Application.Fixtures.Entity
{
    public class NewsletterFixture
    {
        public int Id { get; set; }
        public int LeagueId { get; set; }
        public string Season { get; set; }
        public DateTime FixtureDate { get; set; }
        public string Status { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public int HomeGoals { get; set; }
        public bool HomeWinner => HomeGoals > AwayGoals;
        public int AwayGoals { get; set; }
        public bool AwayWinner => AwayGoals > HomeGoals;
    }
}