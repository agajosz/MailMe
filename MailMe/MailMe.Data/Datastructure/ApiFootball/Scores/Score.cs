namespace MailMe.Data.Datastructure.ApiFootball.Scores
{
    public class Score
    {
        public Halftime Halftime { get; set; }
        public Fulltime Fulltime { get; set; }
        public Extratime Extratime { get; set; }
        public Penalty Penalty { get; set; }
    }

    public class Halftime : ScoreBase
    {
    }

    public class Fulltime : ScoreBase
    {
    }

    public class Extratime : ScoreBase
    {
    }

    public class Penalty : ScoreBase
    {
    }
}