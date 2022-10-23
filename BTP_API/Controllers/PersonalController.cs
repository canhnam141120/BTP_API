using BTP_API.Helpers;
using BTP_API.Models;
using BTP_API.ViewModels;
using Microsoft.Extensions.Hosting;
using Org.BouncyCastle.Asn1.Cmp;
using static System.Reflection.Metadata.BlobBuilder;

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
        public async Task<IActionResult> getAllBook()
        {
            try
            {
                var apiResponse = await _personalRepository.getAllBookAsync();
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
        public async Task<IActionResult> getBookApprovedAsync()
        {
            try
            {
                var apiResponse = await _personalRepository.getAllBookAsync();
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
        public async Task<IActionResult> getBookDenied()
        {
            try
            {
                var apiResponse = await _personalRepository.getBookDeniedAsync();
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
        public async Task<IActionResult> getBookWaiting()
        {
            try
            {
                var apiResponse = await _personalRepository.getBookWaitingAsync();
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
        public async Task<IActionResult> getAllPost()
        {
            try
            {
                var apiResponse = await _personalRepository.getAllPostAsync();
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
        public async Task<IActionResult> getPostApproved()
        {
            try
            {
                var apiResponse = await _personalRepository.getPostApprovedAsync();
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
        public async Task<IActionResult> getPostDenied()
        {
            try
            {
                var apiResponse = await _personalRepository.getPostDeniedAsync();
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
        public async Task<IActionResult> getPostWaiting()
        {
            try
            {
                var apiResponse = await _personalRepository.getPostWaitingAsync();
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
        public async Task<IActionResult> getBookByFavorites()
        {
            try
            {
                var apiResponse = await _personalRepository.getBookByFavoritesAsync();
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
        public async Task<IActionResult> addBookByFavorites(int bookId)
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
        public async Task<IActionResult> deleteBookByFavorites(int bookId)
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
        public async Task<IActionResult> getPostByFavorites()
        {
            try
            {
                var apiResponse = await _personalRepository.getPostByFavoritesAsync();
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
        public async Task<IActionResult> addPostByFavorites(int postId)
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
        public async Task<IActionResult> deletePostByFavorites(int postId)
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
        public async Task<IActionResult> getUserByFavorites()
        {
            try
            {
                var apiResponse = await _personalRepository.getUserByFavoritesAsync();
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
        public async Task<IActionResult> addUserByFavorites(int userId)
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
        public async Task<IActionResult> deleteUserByFavorites(int userId)
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
        public async Task<IActionResult> editPassword(ChangePasswordVM changePasswordVM)
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
        public async Task<IActionResult> listOfRequestSend()
        {
            try
            {
                var apiResponse = await _personalRepository.listOfRequestSendAsync();
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
        public async Task<IActionResult> listOfRequestReceived(int bookId)
        {
            try
            {
                var apiResponse = await _personalRepository.listOfRequestReceivedSendAsync(bookId);
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
        public async Task<IActionResult> myTransactionExchange()
        {
            try
            {
                var apiResponse = await _personalRepository.myTransactionExchangeAsync();
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
        public async Task<IActionResult> myTransactionExDetail(int id)
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
        public async Task<IActionResult> myTransactionExBill(int id)
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
        public async Task<IActionResult> myExBillAll()
        {
            try
            {
                var apiResponse = await _personalRepository.myExBillAllAsync();
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
        public async Task<IActionResult> myTransactionRent()
        {
            try
            {
                var apiResponse = await _personalRepository.myTransactionRentAsync();
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
        public async Task<IActionResult> myTransactionRentDetail(int id)
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
        public async Task<IActionResult> myTransactionRentBill(int id)
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
        public async Task<IActionResult> myRentBillAll()
        {
            try
            {
                var apiResponse = await _personalRepository.myRentBillAllAsync();
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
        public async Task<IActionResult> updateInfoShipping(ShipInfoVM shipInfoVM)
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
