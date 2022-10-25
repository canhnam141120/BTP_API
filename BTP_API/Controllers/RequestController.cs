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
        public async Task<IActionResult> createRequest([FromRoute] int bookid, [FromForm] List<int> bookOffer)
        {
            try
            {
                var apiMessage = await _requestRepository.createRequestAsync(bookid, bookOffer);
                return Ok(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.REQUEST_FAILED.ToString() });
            }
        }

        //Người offer tự hủy
        [HttpPut("cancel/{id}")]
        public async Task<IActionResult> cancelRequest([FromRoute] int id)
        {
            try
            {
                var apiMessage = await _requestRepository.cancelRequestAsync(id);
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
        public async Task<IActionResult> acceptRequest([FromRoute] int id)
        {
            try
            {
                var apiMessage = await _requestRepository.acceptRequestAsync(id);
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
        public async Task<IActionResult> deniedRequest([FromRoute] int id)
        {
            try
            {
                var apiMessage = await _requestRepository.deniedRequestAsync(id);
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
        public async Task<IActionResult> rentBook([FromRoute] int bookId)
        {
            try
            {
                var apiMessage = await _requestRepository.rentBookAsync(bookId);
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
