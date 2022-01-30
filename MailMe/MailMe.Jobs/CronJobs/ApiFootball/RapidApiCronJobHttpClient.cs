using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MailMe.Jobs.Interfaces;

namespace MailMe.Jobs.CronJobs.ApiFootball
{
    public class RapidApiCronJobHttpClient : IRapidApiCronJobHttpClient
    {
        private readonly HttpClient _client;

        public RapidApiCronJobHttpClient(IHttpClientFactory client)
        {
            _client = client.CreateClient();
        }
        
        public async Task<Stream> ReadStreamSourceAsync(string url, CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url),
                Headers =
                {
                    { "x-rapidapi-host", "api-football-v1.p.rapidapi.com" },
                    { "x-rapidapi-key", "329ce1eb79msh6b1619799bf40c9p172803jsn1bb98703824c" },
                },
            };
            var response = await _client.SendAsync(request, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"{response.StatusCode}:{response.ReasonPhrase}");
            }
            var jsonStream = await response.Content.ReadAsStreamAsync(cancellationToken);
            return jsonStream;
        }
    }
}