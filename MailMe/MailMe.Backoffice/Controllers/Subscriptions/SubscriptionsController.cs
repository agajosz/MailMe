using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MailMe.Application.Subscriptions.Interfaces;
using MailMe.Backend.Carriers.Requests.Subscriptions;
using MailMe.Backend.Carriers.Responses.Subscriptions;
using MailMe.Backend.Carriers.Responses.Users;
using MailMe.Data.Datastructure.Subscriptions;
using Microsoft.AspNetCore.Mvc;

namespace MailMe.Backend.Controllers.Subscriptions
{
    [ApiController]
    [Route("subscriptions")]
    public class SubscriptionsController : ControllerBase
    {
        private readonly ISubscriptionsBusiness _subscriptionsBusiness;
        private readonly IMapper _mapper;

        public SubscriptionsController(ISubscriptionsBusiness subscriptionsBusiness, IMapper mapper)
        {
            _subscriptionsBusiness = subscriptionsBusiness;
            _mapper = mapper;
        }
        
        [HttpPost]
        public async Task<ActionResult<SubscriptionDto>> AddSubscription([FromBody] AddSubscriptionRequestDto request)
        {
            var result = await _subscriptionsBusiness
                .AddAsync(_mapper.Map<Application.Subscriptions.Entity.Subscription>(request));
            return Ok(_mapper.Map<SubscriptionDto>(result));
        }

        [HttpGet]
        public async Task<ActionResult<SubscriptionDto>> GetSubscriptionById(int id)
        {
            var result = await _subscriptionsBusiness.GetSubscriptionByIdAsync(id);
            return Ok(_mapper.Map<SubscriptionDto>(result));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateSubscription([FromBody] UpdateSubscriptionRequestDto subscription)
        {
            await _subscriptionsBusiness.UpdateAsync(_mapper.Map<Application.Subscriptions.Entity.Subscription>(subscription));
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveSubscription(int id)
        {
            await _subscriptionsBusiness.RemoveAsync(id);
            return Ok();
        }
    }
}