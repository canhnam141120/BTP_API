namespace BookTradingPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        [HttpPut("exchange/cancel/{id}")]
        public async Task<IActionResult> cancelExchange([FromRoute] int id)
        {
            try
            {
                var apiMessage = await _transactionRepository.cancelExchangeAsync(id);
                if(apiMessage.Message == Message.SUCCESS.ToString())
                {
                    return Ok(apiMessage);
                }
                return NotFound(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.FAILED.ToString() });
            }
        }

        [HttpPut("exchange-detail/cancel/{id}")]
        public async Task<IActionResult> cancelExchangeDetail([FromRoute] int id)
        {
            try
            {
                var apiMessage = await _transactionRepository.cancelExchangeDetailAsync(id);
                if (apiMessage.Message == Message.SUCCESS.ToString())
                {
                    return Ok(apiMessage);
                }
                if (apiMessage.Message == Message.NOT_YET_LOGIN.ToString())
                {
                    return BadRequest(apiMessage);
                }
                return NotFound(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.FAILED.ToString() });
            }
        }

        [HttpPut("rent/cancel/{id}")]
        public async Task<IActionResult> cancelRent([FromRoute] int id)
        {
            try
            {
                var apiMessage = await _transactionRepository.cancelRentAsync(id);
                if (apiMessage.Message == Message.SUCCESS.ToString())
                {
                    return Ok(apiMessage);
                }
                return NotFound(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.FAILED.ToString() });
            }
        }

        [HttpPut("rent-detail/cancel/{id}")]
        public async Task<IActionResult> cancelRentDetail([FromRoute] int id)
        {
            try
            {
                var apiMessage = await _transactionRepository.cancelRentDetailAsync(id);
                if (apiMessage.Message == Message.SUCCESS.ToString())
                {
                    return Ok(apiMessage);
                }
                if (apiMessage.Message == Message.NOT_YET_LOGIN.ToString())
                {
                    return BadRequest(apiMessage);
                }
                return NotFound(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.FAILED.ToString() });
            }
        }
    }
}
