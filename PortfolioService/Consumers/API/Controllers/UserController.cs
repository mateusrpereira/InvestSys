using Application;
using Application.User.Dtos;
using Application.User.Ports;
using Application.User.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserManager _userManager;

        public UserController(ILogger<UserController> logger, IUserManager userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Post(UserDto user)
        {
            var request = new CreateUserRequest
            {
                Data = user
            };

            var res = await _userManager.CreateUser(request);

            if (res.Success) return Created("", res.Data);

            if (res.ErrorCode == ErrorCodes.NOT_FOUND)
            {
                return BadRequest(res);
            }

            _logger.LogError("Response with unknown ErrorCode Returned", res);
            return BadRequest(500);
        }
    }
}
