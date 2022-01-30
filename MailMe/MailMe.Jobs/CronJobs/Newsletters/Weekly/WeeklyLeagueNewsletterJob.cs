using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MailMe.Application.Fixtures.Interfaces;
using MailMe.Application.Subscriptions.Entity;
using MailMe.Application.Subscriptions.Interfaces;
using MailMe.Application.Users.Entity;
using MailMe.Application.Users.Interfaces;
using MailMe.Jobs.Handlers;
using MailMe.Jobs.Interfaces;
using SendGrid.Helpers.Mail;

namespace MailMe.Jobs.CronJobs.Newsletters.Weekly
{
    public class WeeklyLeagueNewsletterJob : IWeeklyLeagueNewsletterJob
    {
        private readonly IUsersBusiness _usersBusiness;
        private readonly ISubscriptionsBusiness _subscriptionsBusiness;
        private readonly IFixturesBusiness _fixturesBusiness;
        private readonly IContentCreationHandler _emailContentCreationHandler;
        private readonly IMessageCreationHandler _emailMessageCreationHandler;
        private readonly MailSendingHandler _sender;

        public WeeklyLeagueNewsletterJob(IUsersBusiness usersBusiness, IFixturesBusiness fixturesBusiness,
            ISubscriptionsBusiness subscriptionsBusiness, MailSendingHandler sender, 
            IContentCreationHandler emailContentCreationHandler, IMessageCreationHandler emailMessageCreationHandler)
        {
            _usersBusiness = usersBusiness;
            _fixturesBusiness = fixturesBusiness;
            _subscriptionsBusiness = subscriptionsBusiness;
            _sender = sender;
            _emailContentCreationHandler = emailContentCreationHandler;
            _emailMessageCreationHandler = emailMessageCreationHandler;
        }

        public async Task DeliverNewsletterAsync(WeeklyLeagueNewsletterSettings settings,
            CancellationToken cancellationToken = default)
        {
            var subscribers = await PrepareSubscribers(settings, cancellationToken);
            var enumerable = subscribers as User[] ?? subscribers.ToArray();
            if (!enumerable.Any())
            {
                return;
            }
            var dateTo = DateTime.Today.AddMinutes(-1);
            var dateFrom = DateTime.Today.AddDays(-7);
            var fixtures = await _fixturesBusiness
                .GetFixturesForWeeklyLeagueNewsletterAsync(settings.LeagueId, dateFrom, dateTo, cancellationToken);
            var emailContent = _emailContentCreationHandler.CreateLeagueWeeklyHtmlContent(fixtures, settings);
            var message = _emailMessageCreationHandler.PrepareMessage(emailContent, enumerable, settings);
            SendNewsletterToSubscribers(message);
        }

        private void SendNewsletterToSubscribers(SendGridMessage message)
        {
            try
            {
                _sender.SendEmail(message);
            }
            catch (Exception exception)
            {
                throw new Exception($"Mail sending failed. Error message: {exception.Message}");
            }
        }
        
        private async Task<IEnumerable<User>> PrepareSubscribers(WeeklyLeagueNewsletterSettings settings,
            CancellationToken cancellationToken)
        {
            var details = new SubscriptionDetails
            {
                Season = settings.Season,
                LeagueId = settings.LeagueId,
                NewsletterPeriod = settings.NewsletterPeriod,
                NewsletterType = settings.NewsletterType
            };
            var subscription = _subscriptionsBusiness
                .GetSubscriptionByDetailsAsync(details, cancellationToken);

            return await _usersBusiness.GetBySubscriptionIdAsync(subscription.Id, cancellationToken);
        }
    }
}