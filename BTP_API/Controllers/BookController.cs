using AutoMapper.Internal;
using BTP_API.Helpers;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Cmp;

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

        [HttpGet("all")]
        public async Task<IActionResult> getAllBooks()
        {
            try
            {
                var apiResponse = await _bookRepository.getAllBookAsync();
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
        public async Task<IActionResult> getBookById(int id)
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

        [HttpGet("{id}/feedback")]
        public async Task<IActionResult> getFeedbackInBook(int id)
        {
            try
            {
                var apiResponse = await _bookRepository.getFeedbackInBookAsync(id);
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
        public async Task<IActionResult> getBookByCategory(int id)
        {
            try
            {
                var apiResponse = await _bookRepository.getBookByCategoryAsync(id);
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
        public async Task<IActionResult> getBookByUser(int id)
        {
            try
            {
                var apiResponse = await _bookRepository.getBookByUserAsync(id);
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

        [HttpPost("search-by-title/{search}")]
        public async Task<IActionResult> searchBookByTitle(string search)
        {
            try
            {
                var apiResponse = await _bookRepository.searchBookByTitleAsync(search);
                if (apiResponse.NumberOfRecords != 0)
                {
                    return Ok(apiResponse);
                }
                return NotFound(apiResponse);
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
                var apiResponse = await _bookRepository.createBookAsync(bookVM);
                return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.CREATE_FAILED.ToString() });
            }
        }

        [HttpPost("{id}/feedback")]
        public async Task<IActionResult> feedbackBook(int id, FeedbackVM feedbackVM)
        {
            try
            {
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
        public async Task<IActionResult> updateBook(int id, BookVM bookVM)
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
        public async Task<IActionResult> hideBook(int id)
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
        public async Task<IActionResult> showBook(int id)
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
