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

        [HttpGet("top-post")]
        public async Task<IActionResult> get3Post()
        {
            try
            {
                var apiResponse = await _postRepository.get3PostAsync();
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpGet("6post")]
        public async Task<IActionResult> get6Post()
        {
            try
            {
                var apiResponse = await _postRepository.get6PostAsync();
                return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpGet("user{id}")]
        public async Task<IActionResult> getPostByUser([FromRoute] int id, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _postRepository.getPostByUserAsync(id, page);
                return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage
                {
                    Message = Message.GET_FAILED.ToString()
                });
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> getAllPost([FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _postRepository.getAllPostAsync(page);
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getPostById([FromRoute] int id)
        {
            try
            {
                var apiResponse = await _postRepository.getPostByIdAsync(id);
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpGet("comment/{id}")]
        public async Task<IActionResult> getCommentInPost([FromRoute] int id, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _postRepository.getCommentInPostAsync(id, page);
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpPost("search/user{id}")]
        public async Task<IActionResult> searchPostOfUser([FromRoute] int id, [FromForm] string search, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _postRepository.searchPostOfUserAsync(id, search, page);
                return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.SEARCH_FAILED.ToString() });
            }
        }

        [HttpPost("search")]
        public async Task<IActionResult> searchPost([FromForm] string search, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _postRepository.searchPostAsync(search, page);
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.SEARCH_FAILED.ToString() });
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> createPost([FromForm] int userId, [FromForm] PostVM postVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiMessage { Message = Message.CREATE_FAILED.ToString() });
                }
                var apiResponse = await _postRepository.createPostAsync(userId, postVM);
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.CREATE_FAILED.ToString() });
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> updatePost([FromRoute] int id, [FromForm] PostVM postVM)
        {
            try
            {
                var apiResponse = await _postRepository.updatePostAsync(id, postVM);
                return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.UPDATE_FAILED.ToString() });
            }
        }

        [HttpPost("comment/create/{id}")]
        public async Task<IActionResult> commentPost([FromForm] int userId, [FromRoute] int id, [FromForm] CommentVM commentVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiMessage { Message = Message.FAILED.ToString() });
                }
                var apiMessage = await _postRepository.commentPostAsync(userId, id, commentVM);
                    return Ok(apiMessage);
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
                    return Ok(apiMessage);
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
                    return Ok(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.FAILED.ToString() });
            }
        }

    }
}
