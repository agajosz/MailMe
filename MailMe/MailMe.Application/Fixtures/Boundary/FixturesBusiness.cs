using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MailMe.Application.Fixtures.Entity;
using MailMe.Application.Fixtures.Interfaces;

namespace MailMe.Application.Fixtures.Boundary
{
    public class FixturesBusiness : IFixturesBusiness
    {
        private readonly IFixturesRepository _fixturesRepository;
        private readonly IFixturesImportRepository _fixturesImportRepository;

        public FixturesBusiness(IFixturesRepository fixturesRepository, IFixturesImportRepository fixturesImportRepository)
        {
            _fixturesRepository = fixturesRepository;
            _fixturesImportRepository = fixturesImportRepository;
        }

        public async Task<ICollection<NewsletterFixture>> GetFixturesForWeeklyLeagueNewsletterAsync(int leagueId, 
            DateTime dateFrom, DateTime dateTo, CancellationToken cancellationToken = default)
        {
            return await _fixturesRepository.GetFixturesForWeeklyLeagueNewsletterAsync(leagueId, dateFrom, dateTo,
                cancellationToken);
        }

        public async Task<ICollection<ImportFixture>> AddImportedFixtures(ICollection<ImportFixture> fixtures, 
            CancellationToken cancellationToken)
        
        {
            return await _fixturesImportRepository.AddRangeAsync(fixtures, cancellationToken);
        }
    }
}