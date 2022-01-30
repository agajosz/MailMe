using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MailMe.Application.Fixtures.Entity;
using MailMe.Application.Fixtures.Interfaces;
using MailMe.Data.Datastructure.ApiFootball.ImportResponse;
using MailMe.Jobs.Handlers;
using MailMe.Jobs.Helpers;
using MailMe.Jobs.Interfaces;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace MailMe.Jobs.CronJobs.ApiFootball.Fixtures
{
    public class FixturesDataFeedImportJob : IFixturesDataFeedImport
    {
        private readonly IFixturesBusiness _fixturesBusiness;
        private readonly IRapidApiCronJobHttpClient _client;

        public FixturesDataFeedImportJob(IRapidApiCronJobHttpClient client, IFixturesBusiness fixturesBusiness)
        {
            _client = client;
            _fixturesBusiness = fixturesBusiness;
        }

        public async Task GetFixturesDataFeedAsync(FixturesDataFeedJobSettings options, 
            CancellationToken cancellationToken = default)
        {
            var url = PrepareUrl(options);
            var source = await ReadSource(url, cancellationToken);
            ValidateResponse(source);

            var fixturesCreationHandler = new FixturesCreationHandler();
            var fixtures = fixturesCreationHandler.ProcessApiResponse(source);
            await SaveFixturesDataAsync(fixtures, cancellationToken);
        }

        private async Task<ImportResponse> ReadSource(string url, CancellationToken cancellationToken)
        {
            try
            {
                await using var jsonString = await _client.ReadStreamSourceAsync(url, cancellationToken);
                var source = await JsonSerializer.DeserializeAsync<ImportResponse>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }, cancellationToken);
                await jsonString.DisposeAsync();
                return source;
            }
            catch (JsonException)
            {
                throw new ArgumentException($"Unable to parse json for '{url}'");
            }
        }

        private string PrepareUrl(FixturesDataFeedJobSettings options)
        {
            var fromDate = PrepareDate(DateTime.Today.AddDays(-7));
            var toDate = PrepareDate(DateTime.Today.AddMinutes(-1));
            var url = UriHelper.BuildBaseAddress(options.BasicUrl, options.Endpoint);
            url.AppendQuery("league", options.LeagueId.ToString())
                .AppendQuery("season", options.Season)
                .AppendQuery("from", fromDate)
                .AppendQuery("to", toDate);
            
            return url.ToString();
        }
        
        private string PrepareDate(DateTime date)
        {
            var month = ProcessDate(date.Month);
            var day = ProcessDate(date.Day);
            var dateAsParameter = $"{date.Year}-{month}-{day}";
            return dateAsParameter;
        }

        private string ProcessDate(int date)
        {
            return (date > 9) ? date.ToString() : "0" + date;
        }

        private async Task SaveFixturesDataAsync(ICollection<ImportFixture> importFixtures,
            CancellationToken cancellationToken)
        {
           await _fixturesBusiness.AddImportedFixtures(importFixtures, cancellationToken);
        }

        private void ValidateResponse(ImportResponse response)
        {
            if (response.Errors.Count <= 0) return;
            var message = response.Errors.FirstOrDefault()?.ToString();
            throw new Exception(message);
        }
    }
    
}