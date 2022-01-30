using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MailMe.Application.Fixtures.Entity;

namespace MailMe.Application.Fixtures.Interfaces
{
    public interface IFixturesImportRepository
    {
        Task<ICollection<ImportFixture>> AddRangeAsync(ICollection<ImportFixture> fixtures, 
            CancellationToken cancellationToken = default);
    }
}