using System.Threading;
using Hangfire;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace MailMe.Jobs.Handlers
{
    public class MailSendingHandler
    {
        private readonly ISendGridClient _client;
        public MailSendingHandler(ISendGridClient client)
        {
            _client = client;
        }
        public string SendEmail(SendGridMessage message, CancellationToken cancellationToken = default)
        {
            var jobId = BackgroundJob.Enqueue(() => _client.SendEmailAsync(message, cancellationToken));
            return jobId;
        }
    }
}