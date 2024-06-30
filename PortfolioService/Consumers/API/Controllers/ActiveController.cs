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

        [HttpPut]
        public async Task<ActionResult<ActiveDto>> Put(ActiveDto active)
        {
            var request = new UpdateActiveRequest
            {
                Data = active
            };

            var res = await _activeManager.UpdateActive(request);

            if (res.ErrorCode == ErrorCodes.MISSING_REQUIRED_INFORMATION) return BadRequest(res);

            if (res.ErrorCode == ErrorCodes.COULD_NOT_STORE_DATA) return BadRequest(res);

            if (res.Success) return Ok(res.Data);

            _logger.LogError("Response with unknown ErrorCode Returned", res);
            return BadRequest(500);
        }
        [HttpDelete]
        public async Task<ActionResult<ActiveDto>> Delete(int activeId)
        {
            var res = await _activeManager.DeleteActive(activeId);
            
            if (res.Success) return Ok(res.Data);

            return NotFound(res);
        }
    }
}
