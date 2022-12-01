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

        [HttpPost("from-favorite-users")]
        public async Task<IActionResult> getAllBookFromFavoriteUser([FromForm] int userId, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _bookRepository.getAllBookFromFavoriteUserAsync(userId, page);
                return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage
                {
                    Message = Message.GET_FAILED.ToString()
                });
            }
        }

        [HttpGet("top-book")]
        public async Task<IActionResult> get6Book()
        {
            try
            {
                var apiResponse = await _bookRepository.get6BookAsync();
                return Ok(apiResponse);
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
                return Ok(apiResponse);
                
            }
            catch
            {
                return BadRequest(new ApiMessage
                {
                    Message = Message.GET_FAILED.ToString()
                });
            }
        }

        [HttpGet("by-filter")]
        public async Task<IActionResult> getBookByFilter([FromForm] string filter1, [FromForm] int filter2, [FromForm] string filter3, [FromForm] string filter4, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _bookRepository.getBookByFilterAsync(filter1, filter2, filter3, filter4, page);
                return Ok(apiResponse);

            }
            catch
            {
                return BadRequest(new ApiMessage
                {
                    Message = Message.GET_FAILED.ToString()
                });
            }
        }

        [HttpGet("allIsExchange")]
        public async Task<IActionResult> getAllBooksIsExchange([FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _bookRepository.getAllBookIsExchangeAsync(page);
                return Ok(apiResponse);

            }
            catch
            {
                return BadRequest(new ApiMessage
                {
                    Message = Message.GET_FAILED.ToString()
                });
            }
        }

        [HttpGet("allIsRent")]
        public async Task<IActionResult> getAllBooksIsRent([FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _bookRepository.getAllBookIsRentAsync(page);
                return Ok(apiResponse);

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
                
                    return Ok(apiResponse);
               
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
                return Ok(apiResponse);
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
                return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage
                {
                    Message = Message.GET_FAILED.ToString()
                });
            }
        }

        [HttpGet("6book/category{id}")]
        public async Task<IActionResult> get6BookByCategory([FromRoute] int id)
        {
            try
            {
                var apiResponse = await _bookRepository.get6BookByCategoryAsync(id);
                return Ok(apiResponse);
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
                return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage {Message = Message.GET_FAILED.ToString()
                });
            }
        }

        [HttpGet("6book/user{id}")]
        public async Task<IActionResult> get6BookByUser([FromRoute] int id)
        {
            try
            {
                var apiResponse = await _bookRepository.get6BookByUserAsync(id);
                return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage
                {
                    Message = Message.GET_FAILED.ToString()
                });
            }
        }

        [HttpPost("search-book-user/user{id}")]
        public async Task<IActionResult> searchBookOfUser([FromRoute] int id, [FromForm] string search, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _bookRepository.searchBookOfUserAsync(id, search, page);
                return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.SEARCH_FAILED.ToString() });
            }
        }

        [HttpPost("search-by-title")]
        public async Task<IActionResult> searchBookByTitle([FromForm] string search, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _bookRepository.searchBookByTitleAsync(search, page);
                return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage {Message = Message.SEARCH_FAILED.ToString() });
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> createBook([FromForm] int userId, [FromForm] BookVM bookVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiMessage { Message = Message.CREATE_FAILED.ToString() });
                }
                var apiResponse = await _bookRepository.createBookAsync(userId, bookVM);
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.CREATE_FAILED.ToString() });
            }
        }

        [HttpPost("feedback/create/{id}")]
        public async Task<IActionResult> feedbackBook([FromForm] int userId, [FromRoute] int id, [FromForm] FeedbackVM feedbackVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiMessage { Message = Message.FAILED.ToString() });
                }
                var apiMessage = await _bookRepository.feedbackBookAsync(userId, id, feedbackVM);
                return Ok(apiMessage);
                
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
                return Ok(apiMessage);
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
                return Ok(apiMessage);
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
                return Ok(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.FAILED.ToString()  });
            }
        }
    }
}
