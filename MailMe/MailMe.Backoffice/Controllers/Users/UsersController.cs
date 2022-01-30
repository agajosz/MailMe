using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MailMe.Application.Users.Interfaces;
using MailMe.Backend.Carriers.Requests.Users;
using MailMe.Backend.Carriers.Responses.Users;
using Microsoft.AspNetCore.Mvc;

namespace MailMe.Backend.Controllers.Users
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersBusiness _usersBusiness;
        private readonly IMapper _mapper;

        public UsersController(IUsersBusiness usersBusiness, IMapper mapper)
        {
            _usersBusiness = usersBusiness;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> AddUser([FromBody] AddUserRequestDto request)
        {
            var user = await _usersBusiness.AddAsync(_mapper.Map<Application.Users.Entity.AddUser>(request));
            return Ok(_mapper.Map<UserDto>(user));
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<UserDto>>> GetAllUsers()
        {
            var results = await _usersBusiness.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<UserDto>>(results));
        }
        
        [HttpDelete]
        public async Task<ActionResult> RemoveUser(int userId)
        {
            await _usersBusiness.RemoveAsync(userId);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser([FromBody] UpdateUserRequestDto request)
        {
            var user = _mapper.Map<Application.Users.Entity.User>(request);
            
            await _usersBusiness.UpdateUser(user);

            return Ok();
        }

        [HttpPost("bindUserWithSubscription")]
        public async Task<ActionResult> BindUserWithSubscription([FromBody] BindUserWithSubscriptionDto request)
        {
            await _usersBusiness.BindUserWithSubscription(request.UserId, request.SubscriptionIds);
            return Ok();
        }
    }
}