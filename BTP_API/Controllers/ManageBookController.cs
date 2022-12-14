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
                    return Ok(apiResponse);
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
                    return Ok(apiResponse);
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
                    return Ok(apiResponse);
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
                    return Ok(apiResponse);
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
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpPost("search")]
        public async Task<IActionResult> searchBook([FromForm] string search, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _manageBookRepository.searchBookAsync(search, page);
                return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.SEARCH_FAILED.ToString() });
            }
        }

        [HttpPut("approved/{id}")]
        public async Task<IActionResult> approvedBook([FromRoute] int id)
        {
            try
            {
                var apiMessage = await _manageBookRepository.approvedBookAsync(id);
                    return Ok(apiMessage);
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
                    return Ok(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.FAILED.ToString() });
            }
        }

        [HttpGet("feedback/{id}")]
        public async Task<IActionResult> getFeedbackInBook([FromRoute] int id)
        {
            try
            {
                var apiResponse = await _manageBookRepository.getFeedbackInBookAsync(id);
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpDelete("delete-feedback/{id}")]
        public async Task<IActionResult> deleteFeedback([FromRoute] int id)
        {
            try
            {
                var apiMessage = await _manageBookRepository.deleteFeedbackAsync(id);
                    return Ok(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.DELETE_FAILED.ToString() });
            }
        }

        [HttpGet("dashBoard")]
        public async Task<IActionResult> totalBookAndUser()
        {
            try
            {
                var apiResponse = await _manageBookRepository.totalBookAndUserAsync();
                return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }
    }
}
