using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MailMe.Backend.Carriers.Requests.Users;

namespace MailMe.Backoffice.IntegrationTests
{
    public static class UserIntegrationTestHelper
    {
        private const string UsersEndpoint = "/users";
        public static async Task<HttpResponseMessage> GetAddUserPostResponse(AddUserRequestDto requestDto, HttpClient client)
        {
            var userJson = JsonSerializer.Serialize(requestDto);
            var stringContent = new StringContent(userJson, Encoding.UTF8, "application/json");

            return await client.PostAsync(UsersEndpoint, stringContent);
        }
    }
}