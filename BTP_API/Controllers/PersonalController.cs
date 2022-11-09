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

        [HttpPost("notification/all")]
        public async Task<IActionResult> getAllNotification([FromForm] string token, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.getAllNotificationAsync(token, page);
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

        [HttpPost("notification/top10new")]
        public async Task<IActionResult> get10NewNotification([FromForm] string token)
        {
            try
            {
                var apiResponse = await _personalRepository.get10NewNotificationAsync(token);
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


        [HttpDelete("notification/mark-read")]
        public async Task<IActionResult> markReadNotification([FromForm] string token, [FromRoute] int id)
        {
            try
            {
                var apiMessage = await _personalRepository.markReadNotificationAsync(token, id);
                if (apiMessage.Message == Message.NOTIFICATION_NOT_EXIST.ToString())
                {
                    return NotFound(apiMessage);
                }
                if (apiMessage.Message == Message.SUCCESS.ToString())
                {
                    return Ok(apiMessage);
                }
                return BadRequest(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.FAILED.ToString() });
            }
        }

        [HttpPost("can-trade")]
        public async Task<IActionResult> getBookCanTrade([FromForm] string token)
        {
            try
            {
                var apiResponse = await _personalRepository.getBookCanTradeAsync(token);
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

        [HttpPost("my-book-list")]
        public async Task<IActionResult> getAllBook([FromForm] string token, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.getAllBookAsync(token, page);
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

        [HttpPost("my-approved-book-list")]
        public async Task<IActionResult> getBookApprovedAsync([FromForm] string token, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.getAllBookAsync(token, page);
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

        [HttpPost("my-denied-book-list")]
        public async Task<IActionResult> getBookDenied([FromForm] string token, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.getBookDeniedAsync(token, page);
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

        [HttpPost("my-waiting-book-list")]
        public async Task<IActionResult> getBookWaiting([FromForm] string token, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.getBookWaitingAsync(token, page);
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

        [HttpPost("my-post-list")]
        public async Task<IActionResult> getAllPost([FromForm] string token, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.getAllPostAsync(token, page);
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

        [HttpPost("my-approved-post-list")]
        public async Task<IActionResult> getPostApproved([FromForm] string token, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.getPostApprovedAsync(token, page);
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

        [HttpPost("my-denied-post-list")]
        public async Task<IActionResult> getPostDenied([FromForm] string token, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.getPostDeniedAsync(token, page);
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

        [HttpPost("my-waiting-post-list")]
        public async Task<IActionResult> getPostWaiting([FromForm] string token, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.getPostWaitingAsync(token, page);
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


        [HttpPost("my-favorites-book")]
        public async Task<IActionResult> getBookByFavorites([FromForm] string token, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.getBookByFavoritesAsync(token, page);
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
        public async Task<IActionResult> addBookByFavorites([FromForm] string token, [FromRoute] int bookId)
        {
            try
            {
                var apiMessage = await _personalRepository.addBookByFavoritesAsync(token, bookId);
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
        public async Task<IActionResult> deleteBookByFavorites([FromForm] string token, [FromRoute] int bookId)
        {
            try
            {
                var apiMessage = await _personalRepository.deleteBookByFavoritesAsync(token, bookId);
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



        [HttpPost("my-favorites-post")]
        public async Task<IActionResult> getPostByFavorites([FromForm] string token, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.getPostByFavoritesAsync(token, page);
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
        public async Task<IActionResult> addPostByFavorites([FromForm] string token, [FromRoute] int postId)
        {
            try
            {
                var apiMessage = await _personalRepository.addPostByFavoritesAsync(token, postId);
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
        public async Task<IActionResult> deletePostByFavorites([FromForm] string token, [FromRoute] int postId)
        {
            try
            {
                var apiMessage = await _personalRepository.deletePostByFavoritesAsync(token, postId);
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


        [HttpPost("my-favorites-user")]
        public async Task<IActionResult> getUserByFavorites([FromForm] string token, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.getUserByFavoritesAsync(token, page);
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
        public async Task<IActionResult> addUserByFavorites([FromForm] string token, [FromRoute] int userId)
        {
            try
            {
                var apiMessage = await _personalRepository.addUserByFavoritesAsync(token, userId);
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
        public async Task<IActionResult> deleteUserByFavorites([FromForm] string token, [FromRoute] int userId)
        {
            try
            {
                var apiMessage = await _personalRepository.deleteUserByFavoritesAsync(token, userId);
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


        [HttpPost("my-profile")]
        public async Task<IActionResult> getInfoUserId([FromForm] string token)
        {
            try
            {
                var apiResponse = await _personalRepository.getInfoUserIdAsync(token);
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
        public async Task<IActionResult> editInfo([FromForm] string token, [FromForm] UserVM userVM)
        {
            try
            {
                var apiMessage = await _personalRepository.editInfoAsync(token, userVM);
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
        public async Task<IActionResult> editPassword([FromForm] string token, [FromForm] ChangePasswordVM changePasswordVM)
        {
            try
            {
                var apiMessage = await _personalRepository.editPasswordAsync(token, changePasswordVM);
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


        [HttpPost("request-send")]
        public async Task<IActionResult> listOfRequestSend([FromForm] string token, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.listOfRequestSendAsync(token, page);
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

        [HttpPost("request-received/{bookId}")]
        public async Task<IActionResult> listOfRequestReceived([FromForm] string token, [FromRoute] int bookId, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.listOfRequestReceivedSendAsync(token, bookId, page);
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


        [HttpPost("my-transaction-exchange-all")]
        public async Task<IActionResult> myTransactionExchange([FromForm] string token, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.myTransactionExchangeAsync(token, page);
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

        [HttpPost("my-transaction-exchange-detail/{id}")]
        public async Task<IActionResult> myTransactionExDetail([FromForm] string token, [FromRoute] int id)
        {
            try
            {
                var apiResponse = await _personalRepository.myTransactionExDetailAsync(token, id);
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

        [HttpPost("my-transaction-exchange-bill/{id}")]
        public async Task<IActionResult> myTransactionExBill([FromForm] string token, [FromRoute] int id)
        {
            try
            {
                var apiResponse = await _personalRepository.myTransactionExBillAsync(token, id);
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

        [HttpPost("my-exchange-bill-all")]
        public async Task<IActionResult> myExBillAll([FromForm] string token, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.myExBillAllAsync(token, page);
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

        [HttpPost("my-transaction-rent-all")]
        public async Task<IActionResult> myTransactionRent([FromForm] string token, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.myTransactionRentAsync(token, page);
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

        [HttpPost("my-transaction-rent-detail/{id}")]
        public async Task<IActionResult> myTransactionRentDetail([FromForm] string token, [FromRoute] int id)
        {
            try
            {
                var apiResponse = await _personalRepository.myTransactionRentDetailAsync(token, id);
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

        [HttpPost("my-transaction-rent-bill/{id}")]
        public async Task<IActionResult> myTransactionRentBill([FromForm] string token, [FromRoute] int id)
        {
            try
            {
                var apiResponse = await _personalRepository.myTransactionRentBillAsync(token, id);
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

        [HttpPost("my-rent-bill-all")]
        public async Task<IActionResult> myRentBillAll([FromForm] string token, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.myRentBillAllAsync(token, page);
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
        public async Task<IActionResult> updateInfoShipping([FromForm] string token, [FromForm] ShipInfoVM shipInfoVM)
        {
            try
            {
                var apiMessage = await _personalRepository.updateInfoShippingAsync(token, shipInfoVM);
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
