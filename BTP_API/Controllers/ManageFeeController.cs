namespace BookTradingPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageFeeController : ControllerBase
    {
        private readonly IManageFeeRepository _manageFeeRepository;

        public ManageFeeController(IManageFeeRepository manageFeeRepository)
        {
            _manageFeeRepository = manageFeeRepository;
        }

        [HttpGet("all")]
        public async Task<IActionResult> getAllFee()
        {
            try
            {
                var apiResponse = await _manageFeeRepository.getAllFeeAsync();
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getFeeById([FromRoute] int id)
        {
            try
            {
                var apiResponse = await _manageFeeRepository.getFeeByIdAsync(id);
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> createFee([FromForm] FeeVM feeVM)
        {
            try
            {
                var apiResponse = await _manageFeeRepository.createFeeAsync(feeVM);
                return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.CREATE_FAILED.ToString() });
            }
        }
    }
}
