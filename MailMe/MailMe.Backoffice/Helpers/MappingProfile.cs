using AutoMapper;

namespace MailMe.Backend.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Incoming
            CreateMap<Application.Users.Entity.User, Carriers.Responses.Users.UserDto>();
            CreateMap<Application.Subscriptions.Entity.Subscription,
                Carriers.Responses.Subscriptions.SubscriptionDto>();

            //Outgoing
            CreateMap<Carriers.Requests.Users.AddUserRequestDto, Application.Users.Entity.AddUser>();
            CreateMap<Carriers.Requests.Users.UpdateUserRequestDto, Application.Users.Entity.User>();
            CreateMap<Carriers.Requests.Subscriptions.AddSubscriptionRequestDto,
                Application.Subscriptions.Entity.Subscription>();
            CreateMap<Carriers.Requests.Subscriptions.UpdateSubscriptionRequestDto,
                Application.Subscriptions.Entity.Subscription>();
            
        }
    }
}
