using Application;
using Application.Transaction.Dtos;
using Application.Transaction.Ports;
using Application.Transaction.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ILogger<TransactionController> _logger;
        private readonly ITransactionManager _transactionManager;

        public TransactionController(
            ILogger<TransactionController> logger, ITransactionManager transactionManager)
        {
            _logger = logger;
            _transactionManager = transactionManager;
        }

        [HttpPost]
        public async Task<ActionResult<TransactionDto>> Post(TransactionDto transaction)
        {
            var request = new CreateTransactionRequest
            {
                Data = transaction
            };

            var res = await _transactionManager.CreateTransaction(request);

            if (res.Success) return Created("", res.Data);

            if (res.ErrorCode == ErrorCodes.NOT_FOUND)
            {
                return BadRequest(res);
            }
            _logger.LogError("Response with unknown ErrorCode Returned", res);
            return BadRequest(500);
        }
        [HttpGet]
        public async Task<ActionResult<TransactionDto>> Get(int transactionId)
        {
            var res = await _transactionManager.GetTransaction(transactionId);

            if (res.Success) return Created("", res.Data);//Alterar para Ok

            return NotFound(res);
        }
    }
}
