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
        public async Task<IActionResult> createRequest([FromForm] string token, [FromRoute] int bookid, [FromForm] List<int> bookOffer)
        {
            try
            {
                var apiMessage = await _requestRepository.createRequestAsync(token, bookid, bookOffer);
                return Ok(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.REQUEST_FAILED.ToString() });
            }
        }

        //Người offer tự hủy
        [HttpPut("cancel/{id}")]
        public async Task<IActionResult> cancelRequest([FromForm] string token, [FromRoute] int id)
        {
            try
            {
                var apiMessage = await _requestRepository.cancelRequestAsync(token, id);
                if (apiMessage.Message == Message.SUCCESS.ToString())
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

        [HttpPut("accept/{id}")]
        public async Task<IActionResult> acceptRequest([FromForm] string token, [FromRoute] int id)
        {
            try
            {
                var apiMessage = await _requestRepository.acceptRequestAsync(token, id);
                if (apiMessage.Message == Message.SUCCESS.ToString())
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

        [HttpPut("denied/{id}")]
        public async Task<IActionResult> deniedRequest([FromForm] string token, [FromRoute] int id)
        {
            try
            {
                var apiMessage = await _requestRepository.deniedRequestAsync(token, id);
                if (apiMessage.Message == Message.SUCCESS.ToString())
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

        [HttpPost("rent/{bookId}")]
        public async Task<IActionResult> rentBook([FromForm] string token, [FromRoute] int bookId)
        {
            try
            {
                var apiMessage = await _requestRepository.rentBookAsync(token, bookId);
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
                return BadRequest(new ApiMessage { Message = Message.FAILED.ToString() });
            }
        }



    }
}
