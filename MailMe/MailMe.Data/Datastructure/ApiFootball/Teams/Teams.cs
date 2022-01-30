namespace MailMe.Data.Datastructure.ApiFootball.Teams
{
    public class Teams
    {
        public Home Home { get; set; }
        public Away Away { get; set; }
    }

    public class Home : Team
    {
    }

    public class Away : Team
    {
    }
}