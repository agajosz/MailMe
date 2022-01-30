namespace MailMe.Jobs.CronJobs.ApiFootball.Fixtures
{
    public class FixturesDataFeedJobSettings
    {
        public int LeagueId { get; set; }
        public string Season { get; set; }
        public string BasicUrl { get; set; }
        public string Endpoint { get; set; }
        public string CronSchedule { get; set; }

    }
}