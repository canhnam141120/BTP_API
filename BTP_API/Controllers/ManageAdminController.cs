namespace BookTradingPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageAdminController : ControllerBase
    {
        private readonly IManageAdminRepository _manageAdminRepository;

        public ManageAdminController(IManageAdminRepository manageAdminRepository)
        {
            _manageAdminRepository = manageAdminRepository;
        }

        [HttpGet("all")]
        //[Authorize(Roles = "1")]
        public async Task<IActionResult> getAllAdmin([FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _manageAdminRepository.getAllAdminAsync(page);
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

        [HttpPut("remove/{id}")]
        //[Authorize(Roles = "1")]
        public async Task<IActionResult> removeAdmin([FromRoute] int id)
        {
            try
            {
                var apiMessage = await _manageAdminRepository.removeAdminAsync(id);
                if(apiMessage.Message == Message.SUCCESS.ToString())
                {
                    return Ok(apiMessage);
                }
                return NotFound(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.FAILED.ToString() });
            }
        }

        [HttpPost("search")]
        public async Task<IActionResult> searchAdmin([FromForm] string search, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _manageAdminRepository.searchAdminAsync(search, page);
                if (apiResponse.NumberOfRecords != 0)
                {
                    return Ok(apiResponse);
                }
                return NotFound(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.SEARCH_FAILED.ToString() });
            }
        }
    }
}
