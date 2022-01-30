using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MailMe.Application.Fixtures.Entity;
using MailMe.Application.Fixtures.Interfaces;
using MailMe.Data.Datastructure.Fixtures;

namespace MailMe.Data.Repositories.Fixtures
{
    public class FixturesImportRepository : IFixturesImportRepository
    {
        private readonly MailMeDbContext _dbContext;
        private readonly IMapper _mapper;

        public FixturesImportRepository(MailMeDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        public async Task<ICollection<ImportFixture>> AddRangeAsync(ICollection<ImportFixture> fixtures, CancellationToken
            cancellationToken)
        {
            var fixturesToAdd = _mapper.Map<ICollection<Fixture>>(fixtures);
            await _dbContext.AddRangeAsync(fixturesToAdd, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ICollection<ImportFixture>>(fixturesToAdd);
        }
    }
}