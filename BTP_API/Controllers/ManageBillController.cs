namespace BookTradingPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageBillController : ControllerBase
    {
        private readonly IManageBillRepository _manageBillRepository;

        public ManageBillController(IManageBillRepository manageBillRepository)
        {
            _manageBillRepository = manageBillRepository;
        }

        [HttpGet("exchange-bill/all")]
        public async Task<IActionResult> getAllExBill([FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _manageBillRepository.getAllExBillAsync(page);
                return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpGet("exchange-bill/{id}")]
        public async Task<IActionResult> getExBillDetail([FromRoute] int id)
        {
            try
            {
                var apiResponse = await _manageBillRepository.getExBillDetailAsync(id);
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpPut("exchange-bill/update/{id}")]
        public async Task<IActionResult> updateStatusExBillDetail([FromRoute] int id, [FromForm] ExchangeBillVM exchangeBillVM)
        {
            try
            {
                var apiMessage = await _manageBillRepository.updateStatusExBillDetailAsync(id, exchangeBillVM);
                    return Ok(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.UPDATE_FAILED.ToString() });
            }
        }


        [HttpGet("rent-bill/all")]
        public async Task<IActionResult> getAllRentBill([FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _manageBillRepository.getAllRentBillAsync(page);
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpGet("rent-bill/{id}")]
        public async Task<IActionResult> getRentBillDetail([FromRoute] int id)
        {
            try
            {
                var apiResponse = await _manageBillRepository.getRentBillDetailAsync(id);
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpPut("rent-bill/update/{id}")]
        public async Task<IActionResult> updateStatusTransactionRentDetail([FromRoute] int id, [FromForm] RentBillVM rentBillVM)
        {
            try
            {
                var apiMessage = await _manageBillRepository.updateStatusRentBillDetailAsync(id, rentBillVM);
                    return Ok(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.UPDATE_FAILED.ToString() });
            }
        }
    }
}
