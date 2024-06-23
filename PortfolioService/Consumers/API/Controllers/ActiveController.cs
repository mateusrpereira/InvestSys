using Application;
using Application.Active.Dtos;
using Application.Active.Ports;
using Application.Active.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActiveController : ControllerBase
    {
        private readonly ILogger<ActiveController> _logger;
        private readonly IActiveManager _activeManager;

        public ActiveController(ILogger<ActiveController> logger, IActiveManager activeManager)
        {
            _logger = logger;
            _activeManager = activeManager;
        }

        [HttpPost]
        public async Task<ActionResult<ActiveDto>> Post(ActiveDto active)
        {
            var request = new CreateActiveRequest
            {
                Data = active
            };

            var res = await _activeManager.CreateActive(request);

            if (res.Success) return Created("", res.Data);

            if (res.ErrorCode == ErrorCodes.NOT_FOUND)
            {
                return BadRequest(res);
            }

            _logger.LogError("Response with unknown ErrorCode Returned", res);
            return BadRequest(500);
        }

        [HttpGet]
        public async Task<ActionResult<ActiveDto>> Get(int activeId)
        {
            var res = await _activeManager.GetActive(activeId);

            if (res.Success) return Created("", res.Data);

            return NotFound(res);
        }
    }
}
