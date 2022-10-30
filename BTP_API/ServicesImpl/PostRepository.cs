using BTP_API.Models;
using BTP_API.ViewModels;
using Microsoft.Extensions.Hosting;
using System.Xml.Linq;
using static System.Reflection.Metadata.BlobBuilder;

namespace BTP_API.ServicesImpl
{
    public class PostRepository : IPostRepository
    {
        private readonly BTPContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PostRepository(BTPContext context, IWebHostEnvironment environment, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _environment = environment;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResponse> getAllPostAsync(int page = 1)
        {
            var posts = await _context.Posts.Include(p => p.User).Where(p => p.Status == StatusRequest.Approved.ToString() && p.IsHide == false).ToListAsync();
            if (posts.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            var result = PaginatedList<Post>.Create(posts, page, 10);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = result,
                NumberOfRecords = result.Count
            };
        }

        public async Task<ApiResponse> getPostByIdAsync(int postId)
        {
            var post = await _context.Posts.Include(p => p.User).SingleOrDefaultAsync(p => p.Id == postId && p.Status == StatusRequest.Approved.ToString() && p.IsHide == false);
            if (post == null)
            {
                return new ApiResponse
                {
                    Message = Message.POST_NOT_EXIST.ToString()
                };
            }
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = post,
                NumberOfRecords = 1
            };
        }
        public async Task<ApiResponse> getCommentInPostAsync(int postId, int page = 1)
        {
            var check = await _context.Posts.AnyAsync(p => p.Id == postId);
            if (!check)
            {
                return new ApiResponse
                {
                    Message = Message.POST_NOT_EXIST.ToString()
                };
            }
            var comments = await _context.Comments.Include(p => p.User).Where(p => p.PostId == postId).ToListAsync();
            if (comments.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            var result = PaginatedList<Comment>.Create(comments, page, 3);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = result,
                NumberOfRecords = result.Count
            };
        }
        public async Task<ApiResponse> searchPostAsync(string search, int page = 1)
        {
            List<Post> posts;
            if (search != null)
            {
                search = search.ToLower().Trim();
                posts = await _context.Posts.Include(b => b.User).Where(b => b.Hashtag.ToLower().Contains(search) && b.Status == StatusRequest.Approved.ToString() && b.IsHide == false || b.Title.ToLower().Contains(search) && b.Status == StatusRequest.Approved.ToString() && b.IsHide == false).ToListAsync();
            }
            else
            {
                posts = await _context.Posts.Include(p => p.User).Where(p => p.Status == StatusRequest.Approved.ToString() && p.IsHide == false).ToListAsync();
            }
            
            if (posts.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            var result = PaginatedList<Post>.Create(posts, page, 10);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = result,
                NumberOfRecords = result.Count
            };
        }
        public async Task<ApiResponse> createPostAsync(PostVM postVM)
        {
            UploadFile uploadFile = new UploadFile();
            Cookie cookie = new Cookie(_httpContextAccessor);
            string fileImageName = uploadFile.UploadPostImage(postVM, _environment);

            var userId = cookie.GetUserId();
            if (userId == 0)
            {
                return new ApiResponse
                {
                    Message = Message.NOT_YET_LOGIN.ToString()
                };
            }
            var post = new Post
            {
                UserId = cookie.GetUserId(),
                Title = postVM.Title,
                Content = postVM.Content,
                Image = fileImageName,
                Hashtag = postVM.Hashtag,
                CreatedDate = DateTime.Now,
                IsHide = false,
                Status = Status.Waiting.ToString()
            };
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return new ApiResponse
            {
                Message = Message.CREATE_SUCCESS.ToString(),
                Data = post,
                NumberOfRecords = 1
            };
        }
        public async Task<ApiMessage> commentPostAsync(int postId, CommentVM commentVM)
        {
            Cookie cookie = new Cookie(_httpContextAccessor);
            int userId = cookie.GetUserId();
            if (userId == 0)
            {
                return new ApiMessage { Message = Message.NOT_YET_LOGIN.ToString() };
            }
            var post = await _context.Posts.FindAsync(postId);
            if (post != null)
            {
                var comment = new Comment
                {
                    PostId = postId,
                    UserId = cookie.GetUserId(),
                    Content = commentVM.Content,
                    CreatedDate = DateTime.Now
                };
                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();
                return new ApiMessage { Message = Message.SUCCESS.ToString() };
            }
            return new ApiMessage { Message = Message.POST_NOT_EXIST.ToString() };
        }
        public async Task<ApiMessage> hidePostAsync(int postId)
        {
            var post = await _context.Posts.SingleOrDefaultAsync(u => u.Id == postId);
            if (post != null)
            {
                post.IsHide = true;
                _context.Update(post);
                await _context.SaveChangesAsync();
                return new ApiMessage { Message = Message.SUCCESS.ToString() };
            }
            return new ApiMessage { Message = Message.POST_NOT_EXIST.ToString() };
        }
        public async Task<ApiMessage> showPostAsync(int postId)
        {
            var post = await _context.Posts.SingleOrDefaultAsync(u => u.Id == postId);
            if (post != null)
            {
                post.IsHide = false;
                _context.Update(post);
                await _context.SaveChangesAsync();
                return new ApiMessage { Message = Message.SUCCESS.ToString() };
            }
            return new ApiMessage { Message = Message.POST_NOT_EXIST.ToString() };
        }
    }
}
