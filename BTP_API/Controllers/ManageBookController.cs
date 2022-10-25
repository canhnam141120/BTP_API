namespace BookTradingPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageBookController : ControllerBase
    {
        private readonly IManageBookRepository _manageBookRepository;

        public ManageBookController(IManageBookRepository manageBookRepository)
        {
            _manageBookRepository = manageBookRepository;
        }

        [HttpGet("all")]
        public async Task<IActionResult> getAllBook([FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _manageBookRepository.getAllBookAsync(page);
                if (apiResponse.NumberOfRecords != 0)
                {
                    return Ok(apiResponse);
                }
                return NotFound(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpGet("approved")]
        public async Task<IActionResult> getAllBookApproved([FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _manageBookRepository.getAllBookApprovedAsync(page);
                if (apiResponse.NumberOfRecords != 0)
                {
                    return Ok(apiResponse);
                }
                return NotFound(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpGet("denied")]
        public async Task<IActionResult> getAllBookDenied([FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _manageBookRepository.getAllBookDeniedAsync(page);
                if (apiResponse.NumberOfRecords != 0)
                {
                    return Ok(apiResponse);
                }
                return NotFound(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpGet("waiting")]
        public async Task<IActionResult> getAllBookWaiting([FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _manageBookRepository.getAllBookWaitingAsync(page);
                if (apiResponse.NumberOfRecords != 0)
                {
                    return Ok(apiResponse);
                }
                return NotFound(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getBookById([FromRoute] int id)
        {
            try
            {
                var apiResponse = await _manageBookRepository.getBookByIdAsync(id);
                if (apiResponse.NumberOfRecords != 0)
                {
                    return Ok(apiResponse);
                }
                return NotFound(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpPut("approved/{id}")]
        public async Task<IActionResult> approvedBook([FromRoute] int id)
        {
            try
            {
                var apiMessage = await _manageBookRepository.approvedBookAsync(id);
                if (apiMessage.Message == Message.SUCCESS.ToString())
                {
                    return Ok(apiMessage);
                }
                if(apiMessage.Message == Message.APPROVED.ToString())
                {
                    return BadRequest(apiMessage);
                }
                return NotFound(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.FAILED.ToString() });
            }
        }

        [HttpPut("denied/{id}")]
        public async Task<IActionResult> deniedBook([FromRoute] int id)
        {
            try
            {
                var apiMessage = await _manageBookRepository.deniedBookAsync(id);
                if (apiMessage.Message == Message.SUCCESS.ToString())
                {
                    return Ok(apiMessage);
                }
                if (apiMessage.Message == Message.DENIED.ToString())
                {
                    return BadRequest(apiMessage);
                }
                return NotFound(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.FAILED.ToString() });
            }
        }

    }
}
