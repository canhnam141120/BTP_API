using BTP_API.Helpers;
using BTP_API.Models;
using BTP_API.ViewModels;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Cmp;

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
        public async Task<IActionResult> getAllPost()
        {
            try
            {
                var apiResponse = await _postRepository.getAllPostAsync();
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
        public async Task<IActionResult> getPostById(int id)
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

        [HttpGet("{id}/comment")]
        public async Task<IActionResult> getCommentInPost(int id)
        {
            try
            {
                var apiResponse = await _postRepository.getCommentInPostAsync(id);
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

        [HttpGet("search-by-hashtag/{search}")]
        public async Task<IActionResult> searchPostByHashtag(string search)
        {
            try
            {
                var apiResponse = await _postRepository.searchPostByHashtagAsync(search);
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
                var apiResponse = await _postRepository.createPostAsync(postVM);
                return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.CREATE_FAILED.ToString() });
            }
        }

        [HttpPost("post{postID}/comment")]
        public async Task<IActionResult> commentPost(int postID, CommentVM commentVM)
        {
            try
            {
                var apiMessage = await _postRepository.commentPostAsync(postID, commentVM);
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
        public async Task<IActionResult> hidePost(int id)
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
        public async Task<IActionResult> showPost(int id)
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
