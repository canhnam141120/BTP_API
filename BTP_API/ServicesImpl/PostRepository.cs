using System.ComponentModel;
using static System.Net.Mime.MediaTypeNames;

namespace BTP_API.ServicesImpl
{
    public class PostRepository : IPostRepository
    {
        private readonly BTPContext _context;
        private readonly IWebHostEnvironment _environment;
        public PostRepository(BTPContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<ApiResponse> get3PostAsync()
        {
            var posts = await _context.Posts.Include(p => p.User).Where(p => p.Status == StatusRequest.Approved.ToString() && p.IsHide == false).OrderByDescending(p => p.Id).Take(3).ToListAsync();
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = posts,
                NumberOfRecords = posts.Count
            };
        }

        public async Task<ApiResponse> get6PostAsync()
        {
            var posts = await _context.Posts.Include(p => p.User).Where(p => p.Status == StatusRequest.Approved.ToString() && p.IsHide == false).OrderByDescending(p => p.Id).Take(6).ToListAsync();
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = posts,
                NumberOfRecords = posts.Count
            };
        }

        public async Task<ApiResponse> getPostByUserAsync(int userId, int page = 1)
        {
            var posts = await _context.Posts.Where(b => b.UserId == userId && b.Status == StatusRequest.Approved.ToString() && b.IsHide == false).OrderByDescending(b => b.Id).Skip(6 * (page - 1)).Take(6).ToListAsync();
            var count = await _context.Posts.Where(b => b.UserId == userId && b.Status == StatusRequest.Approved.ToString() && b.IsHide == false).CountAsync();
            //var result = PaginatedList<Book>.Create(books, page, 6);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = posts,
                NumberOfRecords = count
            };
        }

        public async Task<ApiResponse> getAllPostAsync(int page = 1)
        {
            var posts = await _context.Posts.Include(p => p.User).Where(p => p.Status == StatusRequest.Approved.ToString() && p.IsHide == false).OrderByDescending(p => p.Id).Skip(10*(page-1)).Take(10).ToListAsync();
            var count = await _context.Posts.Where(p => p.Status == StatusRequest.Approved.ToString() && p.IsHide == false).CountAsync();
            //var result = PaginatedList<Post>.Create(posts, page, 10);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = posts,
                NumberOfRecords = count
            };
        }

        public async Task<ApiResponse> getPostByIdAsync(int postId)
        {
            var post = await _context.Posts.Include(p => p.User).SingleOrDefaultAsync(p => p.Id == postId && p.Status == StatusRequest.Approved.ToString() && p.IsHide == false);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = post,
            };
        }
        public async Task<ApiResponse> getCommentInPostAsync(int postId, int page = 1)
        {
            var comments = await _context.Comments.Include(p => p.User).Where(p => p.PostId == postId).OrderByDescending(p => p.Id).Skip(5 * (page - 1)).Take(5).ToListAsync();
            var count = await _context.Comments.Where(p => p.PostId == postId).CountAsync();
            //var result = PaginatedList<Comment>.Create(comments, page, 3);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = comments,
                NumberOfRecords = count
            };
        }

        public async Task<ApiResponse> searchPostOfUserAsync(int userId, string search, int page = 1)
        {
            List<Post> posts;
            if (search != null)
            {
                search = search.ToLower().Trim();
                posts = await _context.Posts.Include(b => b.User).Where(b => b.Title.ToLower().Contains(search) && b.Status == StatusRequest.Approved.ToString() && b.IsHide == false && b.UserId == userId).OrderByDescending(b => b.Id).ToListAsync();
            }
            else
            {
                posts = await _context.Posts.Include(p => p.User).Where(p => p.Status == StatusRequest.Approved.ToString() && p.IsHide == false && p.UserId == userId).OrderByDescending(b => b.Id).ToListAsync();
            }

            //var result = PaginatedList<Post>.Create(posts, page, 10);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = posts.Skip(6 * (page - 1)).Take(6),
                NumberOfRecords = posts.Count
            };
        }

        public async Task<ApiResponse> searchPostAsync(string search, int page = 1)
        {
            List<Post> posts;
            if (search != null)
            {
                search = search.ToLower().Trim();
                posts = await _context.Posts.Include(b => b.User).Where(b =>  b.Title.ToLower().Contains(search) && b.Status == StatusRequest.Approved.ToString() && b.IsHide == false).OrderByDescending(b => b.Id).ToListAsync();
            }
            else
            {
                posts = await _context.Posts.Include(p => p.User).Where(p => p.Status == StatusRequest.Approved.ToString() && p.IsHide == false).OrderByDescending(b => b.Id).ToListAsync();
            }

            //var result = PaginatedList<Post>.Create(posts, page, 10);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = posts.Skip(10 * (page - 1)).Take(10),
                NumberOfRecords = posts.Count
            };
        }
        public async Task<ApiResponse> createPostAsync(int userId, PostVM postVM)
        {
/*            UploadFile uploadFile = new UploadFile();
            string fileImageName = uploadFile.UploadPostImage(postVM, _environment);*/
            var post = new Post
            {
                UserId = userId,
                Title = postVM.Title,
                Content = postVM.Content,
                Image = postVM.Image,
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

        public async Task<ApiMessage> updatePostAsync(int postId, PostVM postVM)
        {
            var post = await _context.Posts.SingleOrDefaultAsync(b => b.Id == postId);

            if (post != null)
            {
                post.Title = postVM.Title;
                post.Content = postVM.Content;
                post.Image = postVM.Image;
                post.IsHide = false;
                post.CreatedDate = DateTime.Now;
                post.Status = Status.Waiting.ToString();
                _context.Posts.Update(post);
                await _context.SaveChangesAsync();
                return new ApiMessage { Message = Message.UPDATE_SUCCESS.ToString() };
            }
            else
            {
                return new ApiMessage { Message = Message.BOOK_NOT_EXIST.ToString() };
            }
        }


        public async Task<ApiMessage> commentPostAsync(int userId, int postId, CommentVM commentVM)
        {
            var post = await _context.Posts.SingleOrDefaultAsync(p => p.Id == postId && p.Status == StatusRequest.Approved.ToString() && p.IsHide == false);
            if (post != null)
            {
                var comment = new Comment
                {
                    PostId = postId,
                    UserId = userId,
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
            var post = await _context.Posts.SingleOrDefaultAsync(u => u.Id == postId && u.Status == StatusRequest.Approved.ToString());
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
            var post = await _context.Posts.SingleOrDefaultAsync(u => u.Id == postId && u.Status == StatusRequest.Approved.ToString());
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
