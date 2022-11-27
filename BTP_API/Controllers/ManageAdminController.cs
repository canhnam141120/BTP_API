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
        public async Task<IActionResult> getAllAdmin()
        {
            try
            {
                var apiResponse = await _manageAdminRepository.getAllAdminAsync();
                return Ok(apiResponse);
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
                    return Ok(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.FAILED.ToString() });
            }
        }

        [HttpPost("search")]
        public async Task<IActionResult> searchAdmin([FromForm] string search)
        {
            try
            {
                var apiResponse = await _manageAdminRepository.searchAdminAsync(search);
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.SEARCH_FAILED.ToString() });
            }
        }
    }
}
