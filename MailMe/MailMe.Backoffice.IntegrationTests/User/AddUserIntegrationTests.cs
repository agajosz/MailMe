using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using MailMe.Backend.Carriers.Requests.Users;
using MailMe.Backend.Carriers.Responses.Users;
using MailMe.Backoffice.IntegrationTests.Helpers;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;

namespace MailMe.Backoffice.IntegrationTests
{
    public class AddUserIntegrationTests
    {
        private TestServer _server;
        [OneTimeSetUp]
        public async Task Setup()
        {
            _server = IntegrationTestsConfigurationHelper.CreateTestServer();
        }

        [Test]
        public async Task WhenAddingCorrectUser_ShouldReturnHttpStatusOK()
        {
            const HttpStatusCode expectedStatusCode = HttpStatusCode.OK;
            const string expectedEmail = "email";
            const string expectedUsername = "username";
            var requestDto = new AddUserRequestDto
            {
                Email = expectedEmail,
                Username = expectedUsername
            };
            var client = _server.CreateClient();

            var response = await UserIntegrationTestHelper.GetAddUserPostResponse(requestDto, client);
            var content = await response.Content.ReadAsStreamAsync();

            var user = await JsonSerializer.DeserializeAsync<UserDto>(content,
                IntegrationTestsConfigurationHelper.Options);
            
            Assert.AreEqual(expectedStatusCode, response.StatusCode);
            Assert.AreEqual(expectedEmail, user.Email);
            Assert.AreEqual(expectedUsername, user.Username);
        }
    }
}