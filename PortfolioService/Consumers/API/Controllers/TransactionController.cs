using Application;
using Application.Transaction.Dtos;
using Application.Transaction.Ports;
using Application.Transaction.Requests;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize("Bearer")]
        [HttpPost]
        public async Task<ActionResult<TransactionDto>> Post(TransactionDto transaction)
        {
            var request = new CreateTransactionRequest
            {
                Data = transaction
            };

            var res = await _transactionManager.CreateTransaction(request);

            if (res.Success) return Created("", res.Data);

            else if (res.ErrorCode == ErrorCodes.TRANSACTION_MISSING_REQUIRED_INFORMATION)
            {
                return BadRequest(res);
            }
            else if (res.ErrorCode == ErrorCodes.TRANSACTION_INVALID_TYPE)
            {
                return BadRequest(res);
            }
            else if (res.ErrorCode == ErrorCodes.TRANSACTION_COULD_NOT_STORE_DATA)
            {
                return BadRequest(res);
            }
            else if (res.ErrorCode == ErrorCodes.TRANSACTION_NOT_FOUND)
            {
                return BadRequest(res);
            }
            _logger.LogError("Response with unknown ErrorCode Returned", res);
            return BadRequest(500);
        }

        [Authorize("Bearer")]
        [HttpGet]
        public async Task<ActionResult<TransactionDto>> Get(int transactionId)
        {
            var res = await _transactionManager.GetTransaction(transactionId);

            if (res.Success) return Created("", res.Data);//Alterar para Ok

            return NotFound(res);
        }

        [Authorize("Bearer")]
        [HttpPut]
        public async Task<ActionResult<TransactionDto>> Put(TransactionDto transaction)
        {
            var request = new UpdateTransactionRequest
            {
                Data = transaction
            };

            var res = await _transactionManager.UpdateTransaction(request);

            if (res.ErrorCode == ErrorCodes.MISSING_REQUIRED_INFORMATION) return BadRequest(res);

            if (res.ErrorCode == ErrorCodes.COULD_NOT_STORE_DATA) return BadRequest(res);

            if (res.Success) return Ok(res.Data);

            _logger.LogError("Response with unknown ErrorCode Returned", res);
            return BadRequest(500);
        }

        [Authorize("Bearer")]
        [HttpDelete]
        public async Task<ActionResult<TransactionDto>> Delete(int transactionId)
        {
            var res = await _transactionManager.DeleteTransaction(transactionId);

            if (res.Success) return Ok(res.Data);

            return NotFound(res);
        }
    }
}
