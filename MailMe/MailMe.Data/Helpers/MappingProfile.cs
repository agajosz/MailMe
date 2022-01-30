using AutoMapper;
using MailMe.Application.Fixtures.Entity;
using MailMe.Application.Subscriptions.Entity;
using MailMe.Application.Users.Entity;

namespace MailMe.Data.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Incoming
            CreateMap<User, Datastructure.Users.User>();
            CreateMap<AddUser, Datastructure.Users.User>();
            CreateMap<Subscription, Datastructure.Subscriptions.Subscription>();
            CreateMap<NewsletterFixture, Datastructure.Fixtures.Fixture>();
            CreateMap<ImportFixture, Datastructure.Fixtures.Fixture>()
                .ForMember(x => x.Id, opt => opt.Ignore());

            //Outgoing
            CreateMap<Datastructure.Users.User, User>();
            CreateMap<Datastructure.Subscriptions.Subscription, Subscription>();
            CreateMap<Datastructure.Subscriptions.SubscriptionUser, Subscription>()
                .ForMember(s => s.Id,
                    opt => opt.MapFrom(su => su.Subscription.Id))
                .ForMember(s => s.LeagueId, opt => opt.MapFrom(su => su.Subscription.LeagueId))
                .ForMember(s => s.Season, opt => opt.MapFrom(su => su.Subscription.Season))
                .ForMember(s => s.NewsletterType, opt => opt.MapFrom(su => su.Subscription.NewsletterType))
                .ForMember(s => s.NewsletterPeriod, opt => opt.MapFrom(su => su.Subscription.NewsletterPeriod));

            CreateMap<Datastructure.Fixtures.Fixture, NewsletterFixture>();
            CreateMap<Datastructure.Fixtures.Fixture, ImportFixture>();
        }
    }
}
