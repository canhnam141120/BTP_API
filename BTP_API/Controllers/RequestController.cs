namespace BookTradingPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestRepository _requestRepository;

        public RequestController(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        [HttpPost("create/{bookid}")]
        public async Task<IActionResult> createRequest([FromForm] int userId, [FromRoute] int bookid, [FromForm] List<int> bookOffer)
        {
            try
            {
                var apiMessage = await _requestRepository.createRequestAsync(userId, bookid, bookOffer);
                return Ok(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.REQUEST_FAILED.ToString() });
            }
        }

        //Người offer tự hủy
        [HttpPut("cancel/{id}")]
        public async Task<IActionResult> cancelRequest([FromForm] int userId, [FromRoute] int id)
        {
            try
            {
                var apiMessage = await _requestRepository.cancelRequestAsync(userId, id);
                    return Ok(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.FAILED.ToString() });
            }
        }

        [HttpPut("accept/{id}")]
        public async Task<IActionResult> acceptRequest([FromForm] int userId, [FromRoute] int id)
        {
            try
            {
                var apiMessage = await _requestRepository.acceptRequestAsync(userId, id);
                    return Ok(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.FAILED.ToString() });
            }
        }

        [HttpPut("denied/{id}")]
        public async Task<IActionResult> deniedRequest([FromForm] int userId, [FromRoute] int id)
        {
            try
            {
                var apiMessage = await _requestRepository.deniedRequestAsync(userId, id);
                    return Ok(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.FAILED.ToString() });
            }
        }

        [HttpPost("rent/{bookId}")]
        public async Task<IActionResult> rentBook([FromForm] int userId, [FromRoute] int bookId)
        {
            try
            {
                var apiMessage = await _requestRepository.rentBookAsync(userId, bookId);
                    return Ok(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.FAILED.ToString() });
            }
        }



    }
}
