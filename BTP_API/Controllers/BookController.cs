using BTP_API.Helpers;

namespace BookTradingPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet("from-favorite-users")]
        public async Task<IActionResult> getAllBookFromFavoriteUser([FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _bookRepository.getAllBookFromFavoriteUserAsync(page);
                if (apiResponse.NumberOfRecords != 0)
                {
                    return Ok(apiResponse);
                }
                return NotFound(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage
                {
                    Message = Message.GET_FAILED.ToString()
                });
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> getAllBooks([FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _bookRepository.getAllBookAsync(page);
                if (apiResponse.NumberOfRecords != 0)
                {
                    return Ok(apiResponse);
                }
                return NotFound(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage
                {
                    Message = Message.GET_FAILED.ToString()
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getBookById([FromRoute] int id)
        {
            try
            {
                var apiResponse = await _bookRepository.getBookByIdAsync(id);
                if (apiResponse.NumberOfRecords != 0)
                {
                    return Ok(apiResponse);
                }
                return NotFound(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage
                {
                    Message = Message.GET_FAILED.ToString()
                });
            }
        }

        [HttpGet("feedback/{id}")]
        public async Task<IActionResult> getFeedbackInBook([FromRoute] int id, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _bookRepository.getFeedbackInBookAsync(id, page);
                if(apiResponse.NumberOfRecords != 0)
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

        [HttpGet("category{id}")]
        public async Task<IActionResult> getBookByCategory([FromRoute] int id, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _bookRepository.getBookByCategoryAsync(id, page);
                if (apiResponse.NumberOfRecords != 0)
                {
                    return Ok(apiResponse);
                }
                return NotFound(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage
                {
                    Message = Message.GET_FAILED.ToString()
                });
            }
        }

        [HttpGet("user{id}")]
        public async Task<IActionResult> getBookByUser([FromRoute] int id, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _bookRepository.getBookByUserAsync(id, page);
                if (apiResponse.NumberOfRecords != 0)
                {
                    return Ok(apiResponse);
                }
                return NotFound(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage {Message = Message.GET_FAILED.ToString()
                });
            }
        }

        [HttpPost("search-by-title")]
        public async Task<IActionResult> searchBookByTitle([FromForm] string search, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _bookRepository.searchBookByTitleAsync(search, page);
                if (apiResponse.NumberOfRecords != 0)
                {
                    return Ok(apiResponse);
                }
                else
                {
                    if (apiResponse.Message == Message.NOT_YET_LOGIN.ToString())
                    {
                        return BadRequest(apiResponse);
                    }
                    return NotFound(apiResponse);
                }
            }
            catch
            {
                return BadRequest(new ApiMessage {Message = Message.SEARCH_FAILED.ToString() });
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> createBook([FromForm] BookVM bookVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiMessage { Message = Message.CREATE_FAILED.ToString() });
                }
                var apiResponse = await _bookRepository.createBookAsync(bookVM);
                if (apiResponse.NumberOfRecords != 0)
                {
                    return Ok(apiResponse);
                }
                return NotFound(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.CREATE_FAILED.ToString() });
            }
        }

        [HttpPost("feedback/create/{id}")]
        public async Task<IActionResult> feedbackBook([FromRoute] int id, [FromForm] FeedbackVM feedbackVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiMessage { Message = Message.FAILED.ToString() });
                }
                var apiMessage = await _bookRepository.feedbackBookAsync(id, feedbackVM);
                if(apiMessage.Message == Message.SUCCESS.ToString())
                {
                    return Ok(apiMessage);
                }
                if(apiMessage.Message == Message.NOT_YET_LOGIN.ToString())
                {
                    return BadRequest(apiMessage);
                }
                return NotFound(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage {Message = Message.FAILED.ToString() });
            }
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> updateBook([FromRoute] int id, [FromForm] BookVM bookVM)
        {
            try
            {
                var apiMessage = await _bookRepository.updateBookAsync(id, bookVM);
                if(apiMessage.Message == Message.UPDATE_SUCCESS.ToString())
                {
                    return Ok(apiMessage);
                }
                return NotFound(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.UPDATE_FAILED.ToString()});
            }
        }

        [HttpPut("hide/{id}")]
        public async Task<IActionResult> hideBook([FromRoute] int id)
        {
            try
            {
                var apiMessage = await _bookRepository.hideBookAsync(id);
                if (apiMessage.Message == Message.SUCCESS.ToString())
                {
                    return Ok(apiMessage);
                }
                return NotFound(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.FAILED.ToString()  });
            }
        }

        [HttpPut("show/{id}")]
        public async Task<IActionResult> showBook([FromRoute] int id)
        {
            try
            {
                var apiMessage = await _bookRepository.showBookAsync(id);
                if (apiMessage.Message == Message.SUCCESS.ToString())
                {
                    return Ok(apiMessage);
                }
                return NotFound(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.FAILED.ToString()  });
            }
        }
    }
}
