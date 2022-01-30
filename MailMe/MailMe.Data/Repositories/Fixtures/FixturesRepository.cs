using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MailMe.Application.Fixtures.Entity;
using MailMe.Application.Fixtures.Interfaces;
using MailMe.Data.Datastructure.Fixtures;
using Microsoft.EntityFrameworkCore;

namespace MailMe.Data.Repositories.Fixtures
{
    public class FixturesRepository : IFixturesRepository
    {
        private readonly MailMeDbContext _dbContext;
        private readonly IMapper _mapper;

        public FixturesRepository(MailMeDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<NewsletterFixture> AddAsync(NewsletterFixture fixture, 
            CancellationToken cancellationToken = default)
        {
            var fixtureToAdd = _mapper.Map<Fixture>(fixture);
            _dbContext.Fixtures.Add(fixtureToAdd);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<NewsletterFixture>(fixtureToAdd);
        }

        public async Task<ICollection<NewsletterFixture>> AddRangeAsync(ICollection<NewsletterFixture> fixtures, CancellationToken cancellationToken = default)
        {
            var fixturesToAdd = _mapper.Map<ICollection<Fixture>>(fixtures);
            await _dbContext.Fixtures.AddRangeAsync(fixturesToAdd, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ICollection<NewsletterFixture>>(fixturesToAdd);
        }

        public async Task RemoveAsync(NewsletterFixture fixture, CancellationToken cancellationToken = default)
        {
            var fixtureToRemove = _mapper.Map<Fixture>(fixture);
            _dbContext.Remove(fixtureToRemove);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<NewsletterFixture>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Fixtures
                .ProjectTo<NewsletterFixture>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }

        public async Task<NewsletterFixture> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var result = await _dbContext.Fixtures
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
            return _mapper.Map<NewsletterFixture>(result);
        }

        public async Task<ICollection<NewsletterFixture>> GetFixturesForWeeklyLeagueNewsletterAsync(int leagueId, 
            DateTime dateFrom, DateTime dateTo, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Fixtures
                .Where(x => x.LeagueId == leagueId)
                .Where(x => x.FixtureDate > dateFrom || x.FixtureDate < dateTo)
                .ProjectTo<NewsletterFixture>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}