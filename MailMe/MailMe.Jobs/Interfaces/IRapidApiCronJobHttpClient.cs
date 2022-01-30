using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace MailMe.Jobs.Interfaces
{
    public interface IRapidApiCronJobHttpClient
    {
        public Task<Stream> ReadStreamSourceAsync(string url, CancellationToken cancellationToken);
    }
}