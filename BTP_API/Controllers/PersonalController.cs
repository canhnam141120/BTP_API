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
        public async Task<IActionResult> getAllNotification([FromForm] int userId, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.getAllNotificationAsync(userId, page);
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpPost("notification/top10new")]
        public async Task<IActionResult> get10NewNotification([FromForm] int userId)
        {
            try
            {
                var apiResponse = await _personalRepository.get10NewNotificationAsync(userId);
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }


        [HttpDelete("notification/mark-read")]
        public async Task<IActionResult> markReadNotification([FromForm] int userId, [FromRoute] int id)
        {
            try
            {
                var apiMessage = await _personalRepository.markReadNotificationAsync(userId, id);
                    return Ok(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.FAILED.ToString() });
            }
        }

        [HttpPost("can-trade")]
        public async Task<IActionResult> getBookCanTrade([FromForm] int userId)
        {
            try
            {
                var apiResponse = await _personalRepository.getBookCanTradeAsync(userId);
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpPost("my-book-list")]
        public async Task<IActionResult> getAllBook([FromForm] int userId, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.getAllBookAsync(userId, page);
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpPost("my-approved-book-list")]
        public async Task<IActionResult> getBookApprovedAsync([FromForm] int userId, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.getBookApprovedAsync(userId, page);
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpPost("my-denied-book-list")]
        public async Task<IActionResult> getBookDenied([FromForm] int userId, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.getBookDeniedAsync(userId, page);
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpPost("my-waiting-book-list")]
        public async Task<IActionResult> getBookWaiting([FromForm] int userId, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.getBookWaitingAsync(userId, page);
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpPost("my-post-list")]
        public async Task<IActionResult> getAllPost([FromForm] int userId, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.getAllPostAsync(userId, page);
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpPost("my-approved-post-list")]
        public async Task<IActionResult> getPostApproved([FromForm] int userId, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.getPostApprovedAsync(userId, page);
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpPost("my-denied-post-list")]
        public async Task<IActionResult> getPostDenied([FromForm] int userId, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.getPostDeniedAsync(userId, page);
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpPost("my-waiting-post-list")]
        public async Task<IActionResult> getPostWaiting([FromForm] int userId, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.getPostWaitingAsync(userId, page);
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }


        [HttpPost("my-favorites-book")]
        public async Task<IActionResult> getBookByFavorites([FromForm] int userId, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.getBookByFavoritesAsync(userId, page);
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpPost("my-favorites-book/add/{bookId}")]
        public async Task<IActionResult> addBookByFavorites([FromForm] int userId, [FromRoute] int bookId)
        {
            try
            {
                var apiMessage = await _personalRepository.addBookByFavoritesAsync(userId, bookId);
                    return Ok(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.ADD_FAILED.ToString() });
            }
        }

        [HttpDelete("un-favorites-book/{bookId}")]
        public async Task<IActionResult> deleteBookByFavorites([FromForm] int userId, [FromRoute] int bookId)
        {
            try
            {
                var apiMessage = await _personalRepository.deleteBookByFavoritesAsync(userId, bookId);
                    return Ok(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.DELETE_FAILED.ToString() });
            }
        }



        [HttpPost("my-favorites-post")]
        public async Task<IActionResult> getPostByFavorites([FromForm] int userId, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.getPostByFavoritesAsync(userId, page);
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpPost("my-favorites-post/add/{postId}")]
        public async Task<IActionResult> addPostByFavorites([FromForm] int userId, [FromRoute] int postId)
        {
            try
            {
                var apiMessage = await _personalRepository.addPostByFavoritesAsync(userId, postId);
                    return Ok(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.ADD_FAILED.ToString() });
            }
        }

        [HttpDelete("un-favorites-post/{postId}")]
        public async Task<IActionResult> deletePostByFavorites([FromForm] int userId, [FromRoute] int postId)
        {
            try
            {
                var apiMessage = await _personalRepository.deletePostByFavoritesAsync(userId, postId);
                    return Ok(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.DELETE_FAILED.ToString() });
            }
        }


        [HttpPost("my-favorites-user")]
        public async Task<IActionResult> getUserByFavorites([FromForm] int userId, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.getUserByFavoritesAsync(userId, page);
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpPost("my-favorites-user/add/{id}")]
        public async Task<IActionResult> addUserByFavorites([FromForm] int userId, [FromRoute] int id)
        {
            try
            {
                var apiMessage = await _personalRepository.addUserByFavoritesAsync(userId, id);
                    return Ok(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.ADD_FAILED.ToString() });
            }
        }

        [HttpDelete("un-favorites-user/{id}")]
        public async Task<IActionResult> deleteUserByFavorites([FromForm] int userId, [FromRoute] int id)
        {
            try
            {
                var apiMessage = await _personalRepository.deleteUserByFavoritesAsync(userId, id);
                    return Ok(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.DELETE_FAILED.ToString() });
            }
        }


        [HttpPost("my-profile")]
        public async Task<IActionResult> getInfoUserId([FromForm] int userId)
        {
            try
            {
                var apiResponse = await _personalRepository.getInfoUserIdAsync(userId);
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpPut("edit-profile")]
        public async Task<IActionResult> editInfo([FromForm] int userId, [FromForm] UserVM userVM)
        {
            try
            {
                var apiMessage = await _personalRepository.editInfoAsync(userId, userVM);
                    return Ok(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.UPDATE_FAILED.ToString() });
            }
        }

        [HttpPut("edit-password")]
        public async Task<IActionResult> editPassword([FromForm] int userId, [FromForm] ChangePasswordVM changePasswordVM)
        {
            try
            {
                var apiMessage = await _personalRepository.editPasswordAsync(userId, changePasswordVM);
                    return Ok(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.UPDATE_FAILED.ToString() });
            }
        }


        [HttpPost("request-send")]
        public async Task<IActionResult> listOfRequestSend([FromForm] int userId, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.listOfRequestSendAsync(userId, page);
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpPost("request-received/{bookId}")]
        public async Task<IActionResult> listOfRequestReceived([FromForm] int userId, [FromRoute] int bookId)
        {
            try
            {
                var apiResponse = await _personalRepository.listOfRequestReceivedSendAsync(userId, bookId);
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }


        [HttpPost("my-transaction-exchange-all")]
        public async Task<IActionResult> myTransactionExchange([FromForm] int userId, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.myTransactionExchangeAsync(userId, page);
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpPost("my-transaction-exchange-detail/{id}")]
        public async Task<IActionResult> myTransactionExDetail([FromForm] int userId, [FromRoute] int id)
        {
            try
            {
                var apiResponse = await _personalRepository.myTransactionExDetailAsync(userId, id);
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpPost("my-transaction-exchange-bill/{id}")]
        public async Task<IActionResult> myTransactionExBill([FromForm] int userId, [FromRoute] int id)
        {
            try
            {
                var apiResponse = await _personalRepository.myTransactionExBillAsync(userId, id);
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpPost("my-exchange-bill-all")]
        public async Task<IActionResult> myExBillAll([FromForm] int userId, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.myExBillAllAsync(userId, page);
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpPost("my-transaction-rent-all")]
        public async Task<IActionResult> myTransactionRent([FromForm] int userId, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.myTransactionRentAsync(userId, page);
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpPost("my-transaction-rent-detail/{id}")]
        public async Task<IActionResult> myTransactionRentDetail([FromForm] int userId, [FromRoute] int id)
        {
            try
            {
                var apiResponse = await _personalRepository.myTransactionRentDetailAsync(userId, id);
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpPost("my-transaction-rent-bill/{id}")]
        public async Task<IActionResult> myTransactionRentBill([FromForm] int userId, [FromRoute] int id)
        {
            try
            {
                var apiResponse = await _personalRepository.myTransactionRentBillAsync(userId, id);
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpPost("my-rent-bill-all")]
        public async Task<IActionResult> myRentBillAll([FromForm] int userId, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _personalRepository.myRentBillAllAsync(userId, page);
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpPost("my-infoShip")]
        public async Task<IActionResult> getInfoShipping([FromForm] int userId)
        {
            try
            {
                var apiResponse = await _personalRepository.getInfoShippingAsync(userId);
                return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }


        [HttpPut("update-info-shipping")]
        public async Task<IActionResult> updateInfoShipping([FromForm] int userId, [FromForm] ShipInfoVM shipInfoVM)
        {
            try
            {
                var apiMessage = await _personalRepository.updateInfoShippingAsync(userId, shipInfoVM);
                    return Ok(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.UPDATE_FAILED.ToString() });
            }
        }
    }
}
