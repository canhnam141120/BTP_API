namespace BookTradingPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagePostController : ControllerBase
    {
        private readonly IManagePostRepository _managePostRepository;

        public ManagePostController(IManagePostRepository managePostRepository)
        {
            _managePostRepository = managePostRepository;
        }

        [HttpGet("all")]
        public async Task<IActionResult> getAllPost()
        {
            try
            {
                var apiResponse = await _managePostRepository.getAllPostAsync();
                if (apiResponse.NumberOfRecords != 0)
                {
                    return Ok(apiResponse);
                }
                return NotFound(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpGet("approved")]
        public async Task<IActionResult> getPostApproved()
        {
            try
            {
                var apiResponse = await _managePostRepository.getPostApprovedAsync();
                if (apiResponse.NumberOfRecords != 0)
                {
                    return Ok(apiResponse);
                }
                return NotFound(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpGet("denied")]
        public async Task<IActionResult> getPostDenied()
        {
            try
            {
                var apiResponse = await _managePostRepository.getPostDeniedAsync();
                if (apiResponse.NumberOfRecords != 0)
                {
                    return Ok(apiResponse);
                }
                return NotFound(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpGet("waiting")]
        public async Task<IActionResult> getPostWaiting()
        {
            try
            {
                var apiResponse = await _managePostRepository.getPostWaitingAsync();
                if (apiResponse.NumberOfRecords != 0)
                {
                    return Ok(apiResponse);
                }
                return NotFound(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getPostById([FromRoute] int id)
        {
            try
            {
                var apiResponse = await _managePostRepository.getPostByIdAsync(id);
                if (apiResponse.NumberOfRecords != 0)
                {
                    return Ok(apiResponse);
                }
                return NotFound(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpPut("approved/{id}")]
        public async Task<IActionResult> approvedPost([FromRoute] int id)
        {
            try
            {
                var apiMessage = await _managePostRepository.approvedPostAsync(id);
                if (apiMessage.Message == Message.SUCCESS.ToString())
                {
                    return Ok(apiMessage);
                }
                if (apiMessage.Message == Message.APPROVED.ToString())
                {
                    return BadRequest(apiMessage);
                }
                return NotFound(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.FAILED.ToString() });
            }
        }

        [HttpPut("denied/{id}")]
        public async Task<IActionResult> deniedPost([FromRoute] int id)
        {
            try
            {
                var apiMessage = await _managePostRepository.deniedPostAsync(id);
                if (apiMessage.Message == Message.SUCCESS.ToString())
                {
                    return Ok(apiMessage);
                }
                if (apiMessage.Message == Message.DENIED.ToString())
                {
                    return BadRequest(apiMessage);
                }
                return NotFound(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.FAILED.ToString() });
            }
        }

        [HttpGet("{id}/comment")]
        public async Task<IActionResult> getCommentInPost([FromRoute] int id)
        {
            try
            {
                var apiResponse = await _managePostRepository.getCommentInPostAsync(id);
                if (apiResponse.NumberOfRecords != 0)
                {
                    return Ok(apiResponse);
                }
                return NotFound(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpDelete("delete-comment/{id}")]
        public async Task<IActionResult> deleteComment([FromRoute] int id)
        {
            try
            {
                var apiMessage = await _managePostRepository.deleteCommentAsync(id);
                if (apiMessage.Message == Message.DELETE_SUCCESS.ToString())
                {
                    return Ok(apiMessage);
                }
                return NotFound(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.DELETE_FAILED.ToString() });
            }
        }
    }
}
