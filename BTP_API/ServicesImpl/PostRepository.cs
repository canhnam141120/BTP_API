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
            if (posts.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = posts,
                NumberOfRecords = posts.Count
            };
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
            var check = await _context.Posts.AnyAsync(p => p.Id == postId && p.Status == StatusRequest.Approved.ToString() && p.IsHide == false);
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
                posts = await _context.Posts.Include(b => b.User).Where(b =>  b.Title.ToLower().Contains(search) && b.Status == StatusRequest.Approved.ToString() && b.IsHide == false).ToListAsync();
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
        public async Task<ApiResponse> createPostAsync(string token, PostVM postVM)
        {
            UploadFile uploadFile = new UploadFile();
            Cookie cookie = new Cookie();
            string fileImageName = uploadFile.UploadPostImage(postVM, _environment);

            var userId = cookie.GetUserId(token);
            if (userId == 0)
            {
                return new ApiResponse
                {
                    Message = Message.NOT_YET_LOGIN.ToString()
                };
            }
            var post = new Post
            {
                UserId = userId,
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
        public async Task<ApiMessage> commentPostAsync(string token, int postId, CommentVM commentVM)
        {
            Cookie cookie = new Cookie();
            int userId = cookie.GetUserId(token);
            if (userId == 0)
            {
                return new ApiMessage { Message = Message.NOT_YET_LOGIN.ToString() };
            }
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
