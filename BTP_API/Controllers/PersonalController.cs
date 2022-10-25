namespace BookTradingPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalController : ControllerBase
    {
        private readonly BTPContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly IPersonalRepository _personalRepository;

        public PersonalController(BTPContext context, IWebHostEnvironment environment, IPersonalRepository personalRepository)
        {
            _context = context;
            _environment = environment;
            _personalRepository = personalRepository;
        }


        [HttpGet("can-trade")]
        public async Task<IActionResult> getBookCanTrade()
        {
            try
            {
                var apiResponse = await _personalRepository.getBookCanTradeAsync();
                if (apiResponse.Message == Message.NOT_YET_LOGIN.ToString())
                {
                    return BadRequest(apiResponse);
                }
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

        [HttpGet("my-book-list")]
        public async Task<IActionResult> getAllBook([FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.getAllBookAsync(page);
                if(apiResponse.Message == Message.NOT_YET_LOGIN.ToString())
                {
                    return BadRequest(apiResponse);
                }
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

        [HttpGet("my-approved-book-list")]
        public async Task<IActionResult> getBookApprovedAsync([FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.getAllBookAsync(page);
                if (apiResponse.Message == Message.NOT_YET_LOGIN.ToString())
                {
                    return BadRequest(apiResponse);
                }
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

        [HttpGet("my-denied-book-list")]
        public async Task<IActionResult> getBookDenied([FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.getBookDeniedAsync(page);
                if(apiResponse.Message == Message.NOT_YET_LOGIN.ToString())
                {
                    return BadRequest(apiResponse);
                }
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

        [HttpGet("my-waiting-book-list")]
        public async Task<IActionResult> getBookWaiting([FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.getBookWaitingAsync(page);
                if (apiResponse.Message == Message.NOT_YET_LOGIN.ToString())
                {
                    return BadRequest(apiResponse);
                }
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

        [HttpGet("my-post-list")]
        public async Task<IActionResult> getAllPost([FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.getAllPostAsync(page);
                if (apiResponse.Message == Message.NOT_YET_LOGIN.ToString())
                {
                    return BadRequest(apiResponse);
                }
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

        [HttpGet("my-approved-post-list")]
        public async Task<IActionResult> getPostApproved([FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.getPostApprovedAsync(page);
                if (apiResponse.Message == Message.NOT_YET_LOGIN.ToString())
                {
                    return BadRequest(apiResponse);
                }
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

        [HttpGet("my-denied-post-list")]
        public async Task<IActionResult> getPostDenied([FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.getPostDeniedAsync(page);
                if (apiResponse.Message == Message.NOT_YET_LOGIN.ToString())
                {
                    return BadRequest(apiResponse);
                }
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

        [HttpGet("my-waiting-post-list")]
        public async Task<IActionResult> getPostWaiting([FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.getPostWaitingAsync(page);
                if (apiResponse.Message == Message.NOT_YET_LOGIN.ToString())
                {
                    return BadRequest(apiResponse);
                }
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


        [HttpGet("my-favorites-book")]
        public async Task<IActionResult> getBookByFavorites([FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.getBookByFavoritesAsync(page);
                if (apiResponse.Message == Message.NOT_YET_LOGIN.ToString())
                {
                    return BadRequest(apiResponse);
                }
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

        [HttpPost("my-favorites-book/add/{bookId}")]
        public async Task<IActionResult> addBookByFavorites([FromRoute] int bookId)
        {
            try
            {
                var apiMessage = await _personalRepository.addBookByFavoritesAsync(bookId);
                if (apiMessage.Message == Message.BOOK_NOT_EXIST.ToString())
                {
                    return NotFound(apiMessage);
                }
                if (apiMessage.Message == Message.ADD_SUCCESS.ToString())
                {
                    return Ok(apiMessage);
                }
                return BadRequest(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.ADD_FAILED.ToString() });
            }
        }

        [HttpDelete("un-favorites-book/{bookId}")]
        public async Task<IActionResult> deleteBookByFavorites([FromRoute] int bookId)
        {
            try
            {
                var apiMessage = await _personalRepository.deleteBookByFavoritesAsync(bookId);
                if (apiMessage.Message == Message.BOOK_NOT_EXIST.ToString())
                {
                    return NotFound(apiMessage);
                }
                if (apiMessage.Message == Message.DELETE_SUCCESS.ToString())
                {
                    return Ok(apiMessage);
                }
                return BadRequest(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.DELETE_FAILED.ToString() });
            }
        }



        [HttpGet("my-favorites-post")]
        public async Task<IActionResult> getPostByFavorites([FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.getPostByFavoritesAsync(page);
                if (apiResponse.Message == Message.NOT_YET_LOGIN.ToString())
                {
                    return BadRequest(apiResponse);
                }
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

        [HttpPost("my-favorites-post/add/{postId}")]
        public async Task<IActionResult> addPostByFavorites([FromRoute] int postId)
        {
            try
            {
                var apiMessage = await _personalRepository.addPostByFavoritesAsync(postId);
                if (apiMessage.Message == Message.BOOK_NOT_EXIST.ToString())
                {
                    return NotFound(apiMessage);
                }
                if (apiMessage.Message == Message.ADD_SUCCESS.ToString())
                {
                    return Ok(apiMessage);
                }
                return BadRequest(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.ADD_FAILED.ToString() });
            }
        }

        [HttpDelete("un-favorites-post/{postId}")]
        public async Task<IActionResult> deletePostByFavorites([FromRoute] int postId)
        {
            try
            {
                var apiMessage = await _personalRepository.deletePostByFavoritesAsync(postId);
                if (apiMessage.Message == Message.BOOK_NOT_EXIST.ToString())
                {
                    return NotFound(apiMessage);
                }
                if (apiMessage.Message == Message.DELETE_SUCCESS.ToString())
                {
                    return Ok(apiMessage);
                }
                return BadRequest(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.DELETE_FAILED.ToString() });
            }
        }


        [HttpGet("my-favorites-user")]
        public async Task<IActionResult> getUserByFavorites([FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.getUserByFavoritesAsync(page);
                if (apiResponse.Message == Message.NOT_YET_LOGIN.ToString())
                {
                    return BadRequest(apiResponse);
                }
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

        [HttpPost("my-favorites-user/add/{userId}")]
        public async Task<IActionResult> addUserByFavorites([FromRoute] int userId)
        {
            try
            {
                var apiMessage = await _personalRepository.addUserByFavoritesAsync(userId);
                if (apiMessage.Message == Message.BOOK_NOT_EXIST.ToString())
                {
                    return NotFound(apiMessage);
                }
                if (apiMessage.Message == Message.ADD_SUCCESS.ToString())
                {
                    return Ok(apiMessage);
                }
                return BadRequest(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.ADD_FAILED.ToString() });
            }
        }

        [HttpDelete("un-favorites-user/{userId}")]
        public async Task<IActionResult> deleteUserByFavorites([FromRoute] int userId)
        {
            try
            {
                var apiMessage = await _personalRepository.deleteUserByFavoritesAsync(userId);
                if (apiMessage.Message == Message.BOOK_NOT_EXIST.ToString())
                {
                    return NotFound(apiMessage);
                }
                if (apiMessage.Message == Message.DELETE_SUCCESS.ToString())
                {
                    return Ok(apiMessage);
                }
                return BadRequest(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.DELETE_FAILED.ToString() });
            }
        }


        [HttpGet("my-profile")]
        public async Task<IActionResult> getInfoUserId()
        {
            try
            {
                var apiResponse = await _personalRepository.getInfoUserIdAsync();
                if (apiResponse.Message == Message.NOT_YET_LOGIN.ToString())
                {
                    return BadRequest(apiResponse);
                }
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

        [HttpPut("edit-profile")]
        public async Task<IActionResult> editInfo([FromForm] UserVM userVM)
        {
            try
            {
                var apiMessage = await _personalRepository.editInfoAsync(userVM);
                if (apiMessage.Message == Message.NOT_YET_LOGIN.ToString())
                {
                    return BadRequest(apiMessage);
                }
                if (apiMessage.Message == Message.UPDATE_SUCCESS.ToString())
                {
                    return Ok(apiMessage);
                }
                return NotFound(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.UPDATE_FAILED.ToString() });
            }
        }

        [HttpPut("edit-password")]
        public async Task<IActionResult> editPassword([FromForm] ChangePasswordVM changePasswordVM)
        {
            try
            {
                var apiMessage = await _personalRepository.editPasswordAsync(changePasswordVM);
                if (apiMessage.Message == Message.USER_NOT_EXIST.ToString())
                {
                    return NotFound(apiMessage);
                }
                if (apiMessage.Message == Message.UPDATE_SUCCESS.ToString())
                {
                    return Ok(apiMessage);
                }
                return BadRequest(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.UPDATE_FAILED.ToString() });
            }
        }


        [HttpGet("request-send")]
        public async Task<IActionResult> listOfRequestSend([FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.listOfRequestSendAsync(page);
                if (apiResponse.Message == Message.NOT_YET_LOGIN.ToString())
                {
                    return BadRequest(apiResponse);
                }
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

        [HttpGet("request-received/{bookId}")]
        public async Task<IActionResult> listOfRequestReceived([FromRoute] int bookId, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.listOfRequestReceivedSendAsync(bookId, page);
                if (apiResponse.Message == Message.NOT_YET_LOGIN.ToString())
                {
                    return BadRequest(apiResponse);
                }
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


        [HttpGet("my-transaction-exchange-all")]
        public async Task<IActionResult> myTransactionExchange([FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.myTransactionExchangeAsync(page);
                if (apiResponse.Message == Message.NOT_YET_LOGIN.ToString())
                {
                    return BadRequest(apiResponse);
                }
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

        [HttpGet("my-transaction-exchange-detail/{id}")]
        public async Task<IActionResult> myTransactionExDetail([FromRoute] int id)
        {
            try
            {
                var apiResponse = await _personalRepository.myTransactionExDetailAsync(id);
                if (apiResponse.Message == Message.NOT_YET_LOGIN.ToString())
                {
                    return BadRequest(apiResponse);
                }
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

        [HttpGet("my-transaction-exchange-bill/{id}")]
        public async Task<IActionResult> myTransactionExBill([FromRoute] int id)
        {
            try
            {
                var apiResponse = await _personalRepository.myTransactionExBillAsync(id);
                if (apiResponse.Message == Message.NOT_YET_LOGIN.ToString())
                {
                    return BadRequest(apiResponse);
                }
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

        [HttpGet("my-exchange-bill-all")]
        public async Task<IActionResult> myExBillAll([FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.myExBillAllAsync(page);
                if (apiResponse.Message == Message.NOT_YET_LOGIN.ToString())
                {
                    return BadRequest(apiResponse);
                }
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

        [HttpGet("my-transaction-rent-all")]
        public async Task<IActionResult> myTransactionRent([FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.myTransactionRentAsync(page);
                if (apiResponse.Message == Message.NOT_YET_LOGIN.ToString())
                {
                    return BadRequest(apiResponse);
                }
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

        [HttpGet("my-transaction-rent-detail/{id}")]
        public async Task<IActionResult> myTransactionRentDetail([FromRoute] int id)
        {
            try
            {
                var apiResponse = await _personalRepository.myTransactionRentDetailAsync(id);
                if (apiResponse.Message == Message.NOT_YET_LOGIN.ToString())
                {
                    return BadRequest(apiResponse);
                }
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

        [HttpGet("my-transaction-rent-bill/{id}")]
        public async Task<IActionResult> myTransactionRentBill([FromRoute] int id)
        {
            try
            {
                var apiResponse = await _personalRepository.myTransactionRentBillAsync(id);
                if (apiResponse.Message == Message.NOT_YET_LOGIN.ToString())
                {
                    return BadRequest(apiResponse);
                }
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

        [HttpGet("my-rent-bill-all")]
        public async Task<IActionResult> myRentBillAll([FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.myRentBillAllAsync(page);
                if (apiResponse.Message == Message.NOT_YET_LOGIN.ToString())
                {
                    return BadRequest(apiResponse);
                }
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

        [HttpPut("update-info-shipping")]
        public async Task<IActionResult> updateInfoShipping([FromForm] ShipInfoVM shipInfoVM)
        {
            try
            {
                var apiMessage = await _personalRepository.updateInfoShippingAsync(shipInfoVM);
                if (apiMessage.Message == Message.NOT_YET_LOGIN.ToString())
                {
                    return BadRequest(apiMessage);
                }
                if (apiMessage.Message == Message.UPDATE_SUCCESS.ToString())
                {
                    return Ok(apiMessage);
                }
                return NotFound(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.UPDATE_FAILED.ToString() });
            }
        }
    }
}
