using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MailMe.Application.Fixtures.Entity;

namespace MailMe.Application.Fixtures.Interfaces
{
    public interface IFixturesRepository
    {
        Task<NewsletterFixture> AddAsync(NewsletterFixture fixture, CancellationToken cancellationToken = default);

        Task<ICollection<NewsletterFixture>> AddRangeAsync(ICollection<NewsletterFixture> fixtures,
            CancellationToken cancellationToken = default);
        Task RemoveAsync(NewsletterFixture fixture, CancellationToken cancellationToken = default);
        Task<IEnumerable<NewsletterFixture>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<NewsletterFixture> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<ICollection<NewsletterFixture>> GetFixturesForWeeklyLeagueNewsletterAsync(int leagueId, DateTime dateFrom, 
            DateTime dateTo, CancellationToken cancellationToken = default);
    }
}