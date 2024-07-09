using Application.User.Dtos;
using Application.User.Ports;
<<<<<<< HEAD
=======
using Microsoft.AspNetCore.Authorization;
>>>>>>> 6c3766f (Implementação da autenticação - JWT Token)
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
<<<<<<< HEAD
=======
        [AllowAnonymous]
>>>>>>> 6c3766f (Implementação da autenticação - JWT Token)
        [HttpPost]
        public async Task<object> Login([FromBody] LoginDto loginDto, [FromServices] ILoginManager service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (loginDto == null)
            {
                return BadRequest();
            }

            try
            {
                var result = await service.FindByLogin(loginDto);
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return NotFound();
                }
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
