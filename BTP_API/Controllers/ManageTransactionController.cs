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

        [HttpGet("exchange/{id}")]
        public async Task<IActionResult> getExchangeByID([FromRoute] int id)
        {
            try
            {
                var apiResponse = await _manageTransactionRepository.getExchangeByIDAsync(id);
                return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpGet("exchange/waiting")]
        public async Task<IActionResult> getAllExchangeWaiting([FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _manageTransactionRepository.getAllExchangeWaitingAsync(page);
                return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpGet("exchange/trading")]
        public async Task<IActionResult> getAllExchangeTrading([FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _manageTransactionRepository.getAllExchangeTradingAsync(page);
                return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }
        [HttpGet("exchange/complete")]
        public async Task<IActionResult> getAllExchangeComplete([FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _manageTransactionRepository.getAllExchangeCompleteAsync(page);
                return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }
        [HttpGet("exchange/cancel")]
        public async Task<IActionResult> getAllExchangeCancel([FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _manageTransactionRepository.getAllExchangeCancelAsync(page);
                return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpPost("exchange/search")]
        public async Task<IActionResult> searchExchange([FromForm] int? id, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _manageTransactionRepository.searchExchangeAsync(id, page);
                return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
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
        public async Task<IActionResult> updateStatusExchange([FromRoute] int id, [FromForm] ExchangeVM exchangeVM)
        {
            try
            {
                var apiMessage = await _manageTransactionRepository.updateStatusExchangeAsync(id, exchangeVM);
                    return Ok(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.UPDATE_FAILED.ToString() });
            }
        }

        [HttpPut("exchange/update-refund/{id}")]
        public async Task<IActionResult> updateRefundExchange([FromRoute] int id)
        {
            try
            {
                var apiMessage = await _manageTransactionRepository.updateRefundExchangeAsync(id);
                return Ok(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.UPDATE_FAILED.ToString() });
            }
        }

        [HttpPut("exchange/trading/{id}")]
        public async Task<IActionResult> tradingExchange([FromRoute] int id)
        {
            try
            {
                var apiMessage = await _manageTransactionRepository.tradingExchangeAsync(id);
                return Ok(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.UPDATE_FAILED.ToString() });
            }
        }

        [HttpPut("exchange/complete/{id}")]
        public async Task<IActionResult> completeExchange([FromRoute] int id)
        {
            try
            {
                var apiMessage = await _manageTransactionRepository.completeExchangeAsync(id);
                return Ok(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.UPDATE_FAILED.ToString() });
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

        [HttpGet("rent/{id}")]
        public async Task<IActionResult> getRentByID([FromRoute] int id)
        {
            try
            {
                var apiResponse = await _manageTransactionRepository.getRentByIDAsync(id);
                return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpGet("rent/waiting")]
        public async Task<IActionResult> getAllRentWaiting([FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _manageTransactionRepository.getAllRentWaitingAsync(page);
                return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpGet("rent/trading")]
        public async Task<IActionResult> getAllRentTrading([FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _manageTransactionRepository.getAllRentTradingAsync(page);
                return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }
        [HttpGet("rent/complete")]
        public async Task<IActionResult> getAllRentComplete([FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _manageTransactionRepository.getAllRentCompleteAsync(page);
                return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }
        [HttpGet("rent/cancel")]
        public async Task<IActionResult> getAllRentCancel([FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _manageTransactionRepository.getAllRentCancelAsync(page);
                return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpPost("rent/search")]
        public async Task<IActionResult> searchRent([FromForm] int? id, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _manageTransactionRepository.searchRentAsync(id, page);
                return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
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
        public async Task<IActionResult> updateStatusRent([FromRoute] int id, [FromForm] RentVM rentVM)
        {
            try
            {
                var apiMessage = await _manageTransactionRepository.updateStatusRentAsync(id, rentVM);
                    return Ok(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.UPDATE_FAILED.ToString() });
            }
        }

        [HttpPut("rent/update-refund/{id}")]
        public async Task<IActionResult> updateRefundRent([FromRoute] int id)
        {
            try
            {
                var apiMessage = await _manageTransactionRepository.updateRefundRentAsync(id);
                return Ok(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.UPDATE_FAILED.ToString() });
            }
        }

        [HttpPut("rent/trading/{id}")]
        public async Task<IActionResult> tradingRent([FromRoute] int id)
        {
            try
            {
                var apiMessage = await _manageTransactionRepository.tradingRentAsync(id);
                return Ok(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.UPDATE_FAILED.ToString() });
            }
        }

        [HttpPut("rent/complete/{id}")]
        public async Task<IActionResult> completeRent([FromRoute] int id)
        {
            try
            {
                var apiMessage = await _manageTransactionRepository.completeRentAsync(id);
                return Ok(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.UPDATE_FAILED.ToString() });
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


        [HttpPost("dashBoard")]
        public async Task<IActionResult> dashboard([FromForm] int id)
        {
            try
            {
                var apiResponse = await _manageTransactionRepository.dashboardAsync(id);
                return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpPut("autoTradingExchange")]
        public async Task<IActionResult> autoTradingExchange()
        {
            try
            {
                var apiResponse = await _manageTransactionRepository.autoTradingExchangeAsync();
                return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.FAILED.ToString() });
            }
        }

        [HttpPut("autoTradingRent")]
        public async Task<IActionResult> autoTradingRent()
        {
            try
            {
                var apiResponse = await _manageTransactionRepository.autoTradingRentAsync();
                return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.FAILED.ToString() });
            }
        }
    }
}
