namespace BookTradingPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;

        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        [HttpGet("all")]
        public async Task<IActionResult> getAllPost([FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _postRepository.getAllPostAsync(page);
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
                var apiResponse = await _postRepository.getPostByIdAsync(id);
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

        [HttpGet("comment/{id}")]
        public async Task<IActionResult> getCommentInPost([FromRoute] int id, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _postRepository.getCommentInPostAsync(id, page);
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

        [HttpGet("search")]
        public async Task<IActionResult> searchPost([FromForm] string search, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _postRepository.searchPostAsync(search, page);
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

        [HttpPost("create")]
        public async Task<IActionResult> createPost([FromForm] PostVM postVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiMessage { Message = Message.CREATE_FAILED.ToString() });
                }
                var apiResponse = await _postRepository.createPostAsync(postVM);
                if (apiResponse.NumberOfRecords != 0)
                {
                    return Ok(apiResponse);
                }
                else
                {
                    if (apiResponse.Message == Message.NOT_YET_LOGIN.ToString())
                    {
                        return BadRequest(apiResponse);
                    }
                    return NotFound(apiResponse);
                }
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.CREATE_FAILED.ToString() });
            }
        }

        [HttpPost("comment/create/{id}")]
        public async Task<IActionResult> commentPost([FromRoute] int id, [FromForm] CommentVM commentVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiMessage { Message = Message.FAILED.ToString() });
                }
                var apiMessage = await _postRepository.commentPostAsync(id, commentVM);
                if (apiMessage.Message == Message.SUCCESS.ToString())
                {
                    return Ok(apiMessage);
                }
                if (apiMessage.Message == Message.NOT_YET_LOGIN.ToString())
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

        [HttpPut("hide/{id}")]
        public async Task<IActionResult> hidePost([FromRoute] int id)
        {
            try
            {
                var apiMessage = await _postRepository.hidePostAsync(id);
                if (apiMessage.Message == Message.SUCCESS.ToString())
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

        [HttpPut("show/{id}")]
        public async Task<IActionResult> showPost([FromRoute] int id)
        {
            try
            {
                var apiMessage = await _postRepository.showPostAsync(id);
                if (apiMessage.Message == Message.SUCCESS.ToString())
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

    }
}
