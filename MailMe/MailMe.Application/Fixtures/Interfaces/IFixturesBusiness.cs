using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MailMe.Application.Fixtures.Entity;

namespace MailMe.Application.Fixtures.Interfaces
{
    public interface IFixturesBusiness
    {
        Task<ICollection<NewsletterFixture>> GetFixturesForWeeklyLeagueNewsletterAsync(int leagueId, DateTime dateFrom, 
            DateTime dateTo, CancellationToken cancellationToken = default);

        Task<ICollection<ImportFixture>> AddImportedFixtures(ICollection<ImportFixture> fixtures,
            CancellationToken cancellationToken);
    }
}