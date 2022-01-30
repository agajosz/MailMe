using MailMe.Jobs.Handlers;

namespace MailMe.Jobs.CronJobs.Newsletters.Weekly
{
    public class WeeklyLeagueNewsletterSettings
    {
        public int LeagueId { get; set; }
        public string LeagueName { get; set; }
        public string CronSchedule { get; set; }
        public string Season { get; set; }
        public string NewsletterPeriod { get; set; }
        public string NewsletterType { get; set; }
        public MailSenderSettings MailSenderSettings { get; set; }
    }
}