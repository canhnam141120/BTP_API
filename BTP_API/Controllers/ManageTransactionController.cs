namespace BookTradingPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageTransactionController : ControllerBase
    {
        private readonly IManageTransactionRepository _manageTransactionRepository;

        public ManageTransactionController(IManageTransactionRepository manageTransactionRepository)
        {
            _manageTransactionRepository = manageTransactionRepository;
        }

        [HttpGet("exchange/all")]
        public async Task<IActionResult> getAllExchange([FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _manageTransactionRepository.getAllExchangeAsync(page);
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpGet("exchange/detail/{id}")]
        public async Task<IActionResult> getAllExchangeDetail([FromRoute] int id)
        {///
            try
            {
                var apiResponse = await _manageTransactionRepository.getAllExchangeDetailAsync(id);
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpGet("exchange/bill/{id}")]
        public async Task<IActionResult> getAllExchangeBill([FromRoute] int id)
        {
            try
            {
                var apiResponse = await _manageTransactionRepository.getAllExchangeBillAsync(id);
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpPut("exchange/update-status/{id}")]
        public async Task<IActionResult> updateStatusExchange([FromRoute] int id, [FromForm] string status)
        {
            try
            {
                var apiMessage = await _manageTransactionRepository.updateStatusExchangeAsync(id, status);
                    return Ok(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.UPDATE_FAILED.ToString() });
            }
        }

        [HttpPut("exchange-detail/update-status/{id}")]
        public async Task<IActionResult> updateExchangeDetail([FromRoute] int id, [FromForm] ExchangeDetailVM exchangeDetailVM)
        {
            try
            {
                var apiMessage = await _manageTransactionRepository.updateExchangeDetailAsync(id, exchangeDetailVM);
                    return Ok(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.UPDATE_FAILED.ToString() });
            }
        }

        [HttpGet("rent/all")]
        public async Task<IActionResult> getAllRent([FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _manageTransactionRepository.getAllRentAsync(page);
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpGet("rent/detail/{id}")]
        public async Task<IActionResult> getAllRentDetail([FromRoute] int id)
        {
            try
            {
                var apiResponse = await _manageTransactionRepository.getAllRentDetailAsync(id);
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpGet("rent/bill/{id}")]
        public async Task<IActionResult> getAllRentBill([FromRoute] int id)
        {
            try
            {
                var apiResponse = await _manageTransactionRepository.getAllRentBillAsync(id);
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpPut("rent/update-status/{id}")]
        public async Task<IActionResult> updateStatusRent([FromRoute] int id, [FromForm] string status)
        {
            try
            {
                var apiMessage = await _manageTransactionRepository.updateStatusRentAsync(id, status);
                    return Ok(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.UPDATE_FAILED.ToString() });
            }
        }

        [HttpPut("rent-detail/update-status/{id}")]
        public async Task<IActionResult> updateRentDetail  ([FromRoute] int id, [FromForm] RentDetailVM rentDetailVM)
        {
            try
            {
                var apiMessage = await _manageTransactionRepository.updateRentDetailAsync(id, rentDetailVM);
                    return Ok(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.UPDATE_FAILED.ToString() });
            }
        }

    }
}
