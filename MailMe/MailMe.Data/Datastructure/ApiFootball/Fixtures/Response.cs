using MailMe.Data.Datastructure.ApiFootball.Leagues;
using MailMe.Data.Datastructure.ApiFootball.Scores;

namespace MailMe.Data.Datastructure.ApiFootball.Fixtures
{
    public class Response
    {
        public Fixture Fixture { get; set; }
        public League League { get; set; }
        public Teams.Teams Teams { get; set; }
        public Goals.Goals Goals { get; set; }
        public Score Score { get; set; }
    }
}