using Application;
using Application.Portfolio.Dtos;
using Application.Portfolio.Ports;
using Application.Portfolio.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class PortfolioController : ControllerBase
    {
        private readonly ILogger<PortfolioController> _logger;
        private readonly IPortfolioManager _portfolioManager;

        public PortfolioController(ILogger<PortfolioController> logger, IPortfolioManager portfolioManager)
        {
            _logger = logger;
            _portfolioManager = portfolioManager;
        }

        [HttpPost]
        public async Task<ActionResult<PortfolioDto>> Post(PortfolioDto portfolio)
        {
            var request = new CreatePortfolioRequest
            {
                Data = portfolio
            };

            var res = await _portfolioManager.CreatePortfolio(request);

            if(res.Success) return Created("", res.Data);

            if (res.ErrorCode == ErrorCodes.NOT_FOUND)
            {
                return BadRequest(res);
            }

            _logger.LogError("Response with unknown ErrorCode Returned", res);
            return BadRequest(500);
        }

        [HttpGet]
        public async Task<ActionResult<PortfolioDto>> Get(int portfolioId)
        {
            var res = await _portfolioManager.GetPortfolio(portfolioId);

            if (res.Success) return Created("", res.Data);

            return NotFound(res);
        }

        [HttpPut]
        public async Task<ActionResult<PortfolioDto>> Put(PortfolioDto portfolio)
        {
            var request = new UpdatePortfolioRequest
            {
                Data = portfolio
            };

            var res = await _portfolioManager.UpdatePortfolio(request);

            if (res.ErrorCode == ErrorCodes.MISSING_REQUIRED_INFORMATION) return BadRequest(res);

            if (res.ErrorCode == ErrorCodes.COULD_NOT_STORE_DATA) return BadRequest(res);

            if (res.Success) return Ok(res.Data);

            _logger.LogError("Response with unknown ErrorCode Returned", res);

            return BadRequest(500);
        }
        
        [HttpDelete]
        public async Task<ActionResult<PortfolioDto>> Delete(int portfolioId)
        {
            var res = await _portfolioManager.DeletePortfolio(portfolioId);

            if (res.Success) return Ok(res.Data);

            return NotFound(res);
        }
    }
}
