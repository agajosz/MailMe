using System.Collections.Generic;
using System.Linq;
using MailMe.Application.Fixtures.Entity;
using MailMe.Data.Datastructure.ApiFootball.ImportResponse;

namespace MailMe.Jobs.Handlers
{
    internal class FixturesCreationHandler
    {
        internal ICollection<ImportFixture> ProcessApiResponse(ImportResponse apiResponse)
        {
            return apiResponse.Response.Select(response => new ImportFixture
                {
                    LeagueId = response.League.Id,
                    Season = apiResponse.Parameters.Season,
                    FixtureDate = response.Fixture.Date,
                    Status = response.Fixture.Status.Short,
                    HomeTeam = response.Teams.Home.Name,
                    AwayTeam = response.Teams.Away.Name,
                    HomeGoals = response.Goals.Home,
                    AwayGoals = response.Goals.Away
                })
                .ToList();
        }
    }
}