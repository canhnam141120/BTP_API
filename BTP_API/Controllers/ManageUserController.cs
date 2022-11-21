namespace BookTradingPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageUserController : ControllerBase
    {
        private readonly IManageUserRepository _manageUserRepository;

        public ManageUserController(IManageUserRepository manageUserRepository)
        {
            _manageUserRepository = manageUserRepository;
        }

        [HttpGet("all")]
        public async Task<IActionResult> getAllUser([FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _manageUserRepository.getAllUserAsync(page);
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpGet("ban-list")]
        public async Task<IActionResult> getAllUserBan([FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _manageUserRepository.getAllUserBanAsync(page);
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpGet("active-list")]
        public async Task<IActionResult> getAllUserActive([FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _manageUserRepository.getAllUserActiveAsync(page);
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpGet("top")]
        public async Task<IActionResult> getTopUserLike()
        {

            try
            {
                var apiResponse = await _manageUserRepository.getTopUserLikeAsync();
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getUserById([FromRoute] int id)
        {
            try
            {
                var apiResponse = await _manageUserRepository.getUserByIdAsync(id);
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpPost("search")]
        public async Task<IActionResult> searchUser([FromForm] string search, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _manageUserRepository.searchUserAsync(search, page);
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.SEARCH_FAILED.ToString() });
            }
        }

        [HttpPut("ban/{id}")]
        public async Task<IActionResult> banAcc([FromRoute] int id)
        {
            try
            {
                var apiMessage = await _manageUserRepository.banAccAsync(id);
                    return Ok(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.FAILED.ToString() });
            }
        }

        [HttpPut("active/{id}")]
        public async Task<IActionResult> activeAcc([FromRoute] int id)
        {
            try
            {
                var apiMessage = await _manageUserRepository.activeAccAsync(id);
                    return Ok(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.FAILED.ToString() });
            }
        }

        [HttpPut("authority/{id}")]
        public async Task<IActionResult> authorityAdmin([FromRoute] int id)
        {
            try
            {
                var apiMessage = await _manageUserRepository.authorityAdminAsync(id);
                    return Ok(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.FAILED.ToString() });
            }
        }
    }
}
