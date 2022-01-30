using System.IO;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace MailMe.Backoffice.IntegrationTests.Helpers
{
    public class IntegrationTestsConfigurationHelper
    {
        public static TestServer CreateTestServer() =>
            new TestServer(
                new WebHostBuilder()
                    .UseStartup<IntegrationTestStartup>()
                    .ConfigureAppConfiguration(config =>
                        config.AddJsonFile(
                            Path.Combine(TestContext.CurrentContext.TestDirectory,
                                "Helpers/appsettings.IntegrationTests.json"
                            )
                        )
                    )
            );

        public static JsonSerializerOptions Options => new JsonSerializerOptions
        {
            AllowTrailingCommas = true,
            PropertyNameCaseInsensitive = true
        };
    }
}