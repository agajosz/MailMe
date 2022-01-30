using System.Collections.Generic;
using System.Linq;
using MailMe.Application.Users.Entity;
using MailMe.Jobs.CronJobs.Newsletters.Weekly;
using SendGrid.Helpers.Mail;

namespace MailMe.Jobs.Handlers
{
    public class EmailMessageCreationHandler : IMessageCreationHandler
    {
        public SendGridMessage PrepareMessage(string htmlContent, IEnumerable<User> subscribers,
            WeeklyLeagueNewsletterSettings settings)
        {
            var from = new EmailAddress
            {
                Email = settings.MailSenderSettings.SenderEmail,
                Name = settings.MailSenderSettings.SenderName
            };
            var subject = $"{settings.NewsletterPeriod} {settings.LeagueName} {settings.NewsletterType.ToLower()} update!";
            var tos = subscribers
                .Select(subscriber => new EmailAddress(subscriber.Email, subscriber.Username))
                .ToList();
            var plainText = string.Empty;

            return MailHelper.CreateSingleEmailToMultipleRecipients(from, tos, subject, plainText, htmlContent);
        }
    }

    public interface IMessageCreationHandler
    {
        public SendGridMessage PrepareMessage(string htmlContent, IEnumerable<User> subscribers,
            WeeklyLeagueNewsletterSettings settings);
    }
}