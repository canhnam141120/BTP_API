﻿namespace BookTradingPlatform.Controllers
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
        public async Task<IActionResult> getAllUser()
        {
            try
            {
                var apiResponse = await _manageUserRepository.getAllUserAsync();
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

        [HttpGet("ban-list")]
        public async Task<IActionResult> getAllUserBan()
        {
            try
            {
                var apiResponse = await _manageUserRepository.getAllUserBanAsync();
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

        [HttpGet("active-list")]
        public async Task<IActionResult> getAllUserActive()
        {

            try
            {
                var apiResponse = await _manageUserRepository.getAllUserActiveAsync();
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

        [HttpGet("{id}")]
        public async Task<IActionResult> getUserById([FromRoute] int id)
        {
            try
            {
                var apiResponse = await _manageUserRepository.getUserByIdAsync(id);
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

        [HttpPost("search")]
        public async Task<IActionResult> searchUser([FromQuery] string search)
        {
            try
            {
                var apiResponse = await _manageUserRepository.searchUserAsync(search);
                if (apiResponse.NumberOfRecords != 0)
                {
                    return Ok(apiResponse);
                }
                return NotFound(apiResponse);
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

        [HttpPut("active/{id}")]
        public async Task<IActionResult> activeAcc([FromRoute] int id)
        {
            try
            {
                var apiMessage = await _manageUserRepository.activeAccAsync(id);
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

        [HttpPut("authority/{id}")]
        public async Task<IActionResult> authorityAdmin([FromRoute] int id)
        {
            try
            {
                var apiMessage = await _manageUserRepository.authorityAdminAsync(id);
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
    }
}
