using System.Collections.Generic;
using System.Linq;
using System.Text;
using MailMe.Application.Fixtures.Entity;
using MailMe.Jobs.CronJobs.Newsletters.Weekly;

namespace MailMe.Jobs.Handlers
{
    public class EmailContentCreationHandler : IContentCreationHandler
    {
        public string CreateLeagueWeeklyHtmlContent(IEnumerable<NewsletterFixture> fixtures, 
            WeeklyLeagueNewsletterSettings settings)
        {
            var newsletterFixtures = fixtures as NewsletterFixture[] ?? fixtures.ToArray();
            if (!newsletterFixtures.Any())
            {
                return @"<div style=""display: flex; flex-direction: column; align-items: center;"" class=""main-container"">
                                <div class=""hello"">
                                    <h2>No match was played last week :(</h2>
                                </div>                
                         </div>";
            }
            var summaryFixtureDivs = PrepareFixtureDivs(newsletterFixtures);
            var htmlContent = @$"
<div style=""display: flex; flex-direction: column; align-items: center;"" class=""main-container"">
<div class=""hello"">
    <h2>Below you can find latest update from {settings.LeagueName} fields!</h2>
</div>
<div class=""fixtures-table"">
            <table cellpading=""3"" cellspacing=""2"" class=""fixtures"">
                <tr align = ""center"" class=""titles"">
                    <th>Date</th>
                    <th>Status</th>
                    <th>Home</th>
                    <th>Home goals</th>
                    <th>Away goals</th>
                    <th>Away</th>
                </tr>
                {summaryFixtureDivs}
            </table>
        </div>
    </div>
";
            return htmlContent;
        }

        private string PrepareFixtureDivs(IEnumerable<NewsletterFixture> fixtures)
        {
            var summaryFixtureDivs = new StringBuilder();
            var fixturesSortedByDate = fixtures.OrderBy(x => x.FixtureDate);
            foreach (var fixture in fixturesSortedByDate)
            {
                var fixtureDiv = $@"
                <tr align=""center"">
                    <td>{fixture.FixtureDate}</td>
                    <td>{fixture.Status}</td>
                    <td>{fixture.HomeTeam}</td>
                    <td>{fixture.HomeGoals}</td> 
                    <td>{fixture.AwayGoals}</td>
                    <td>{fixture.AwayTeam}</td>
                </tr>";

                summaryFixtureDivs.Append(fixtureDiv);
            }

            return summaryFixtureDivs.ToString();
        }
    }

    public interface IContentCreationHandler
    {
        public string CreateLeagueWeeklyHtmlContent(IEnumerable<NewsletterFixture> fixtures,
            WeeklyLeagueNewsletterSettings settings);
    }
}