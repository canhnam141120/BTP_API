namespace BTP_API.Services
{
    public class ManagePostRepository : IManagePostRepository
    {
        private readonly BTPContext _context;

        public ManagePostRepository(BTPContext context)
        {
            _context = context;
        }
        public async Task<ApiResponse> getAllPostAsync(int page = 1)
        {
            var posts = await _context.Posts.Include(f => f.User).OrderByDescending(f => f.Id).Skip(5*(page-1)).Take(5).ToListAsync();
            var count = await _context.Posts.CountAsync();
            //var result = PaginatedList<Post>.Create(posts, page, 5);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = posts,
                NumberOfRecords = count
            };
        }

        public async Task<ApiResponse> getPostApprovedAsync(int page = 1)
        {
            var posts = await _context.Posts.Include(f => f.User).Where(b => b.Status == StatusRequest.Approved.ToString()).OrderByDescending(f => f.Id).Skip(5 * (page - 1)).Take(5).ToListAsync();
            var count = await _context.Posts.Where(b => b.Status == StatusRequest.Approved.ToString()).CountAsync();

            //var result = PaginatedList<Post>.Create(posts, page, 5);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = posts,
                NumberOfRecords = count
            };
        }

        public async Task<ApiResponse> getPostDeniedAsync(int page = 1)
        {
            var posts = await _context.Posts.Include(f => f.User).Where(b => b.Status == StatusRequest.Denied.ToString()).OrderByDescending(f => f.Id).Skip(5 * (page - 1)).Take(5).ToListAsync();
            var count = await _context.Posts.Where(b => b.Status == StatusRequest.Denied.ToString()).CountAsync();

            //var result = PaginatedList<Post>.Create(posts, page, 5);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = posts,
                NumberOfRecords = count
            };
        }

        public async Task<ApiResponse> getPostWaitingAsync(int page = 1)
        {
            var posts = await _context.Posts.Include(f => f.User).Where(b => b.Status == StatusRequest.Waiting.ToString()).OrderByDescending(f => f.Id).Skip(5 * (page - 1)).Take(5).ToListAsync();
            var count = await _context.Posts.Where(b => b.Status == StatusRequest.Waiting.ToString()).CountAsync();
            //var result = PaginatedList<Post>.Create(posts, page, 5);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = posts,
                NumberOfRecords = count
            };
        }

        public async Task<ApiResponse> getPostByIdAsync(int postID)
        {
            var post = await _context.Posts.Include(f => f.User).SingleOrDefaultAsync(b => b.Id == postID);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = post
            };
        }

        public async Task<ApiResponse> searchPostAsync(string search, int page = 1)
        {
            List<Post> posts;
            if (search != null)
            {
                search = search.ToLower().Trim();
                posts = await _context.Posts.Include(b => b.User).Where(b => b.Title.ToLower().Contains(search)).OrderByDescending(b => b.Id).ToListAsync();
            }
            else
            {
                posts = await _context.Posts.Include(b => b.User).OrderByDescending(b => b.Id).ToListAsync();
            }

            //var result = PaginatedList<Book>.Create(books, page, 9);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = posts.Skip(5 * (page - 1)).Take(5),
                NumberOfRecords = posts.Count
            };
        }

        public async Task<ApiMessage> approvedPostAsync(int postID)
        {
            var post = await _context.Posts.SingleOrDefaultAsync(b => b.Id == postID);
            if (post != null)
            {
                post.Status = StatusRequest.Approved.ToString();
                _context.Update(post);
                var notification = new Notification
                {
                    UserId = post.UserId,
                    Content = @"Bài đăng """ + post.Title + @""" của bạn đã được duyệt!",
                    CreatedDate = DateTime.Now,
                    IsRead = false,
                };
                _context.Add(notification);
                await _context.SaveChangesAsync();
                return new ApiMessage
                {
                    Message = Message.SUCCESS.ToString()
                };
            }
            return new ApiMessage
            {
                Message = Message.POST_NOT_EXIST.ToString()
            };
        }

        public async Task<ApiMessage> deniedPostAsync(int postID)
        {
            var post = await _context.Posts.SingleOrDefaultAsync(b => b.Id == postID);
            if (post != null)
            {
                post.Status = StatusRequest.Denied.ToString();
                _context.Update(post);
                var notification = new Notification
                {
                    UserId = post.UserId,
                    Content = @"Bài đăng """ + post.Title + @""" của bạn không được duyệt!",
                    CreatedDate = DateTime.Now,
                    IsRead = false,
                };
                _context.Add(notification);
                await _context.SaveChangesAsync();
                return new ApiMessage
                {
                    Message = Message.SUCCESS.ToString()
                };
            }
            return new ApiMessage
            {
                Message = Message.POST_NOT_EXIST.ToString()
            };
        }

        public async Task<ApiResponse> getCommentInPostAsync(int postID, int page = 1)
        {
            var comments = await _context.Comments.Include(p => p.User).Where(p => p.PostId == postID).OrderByDescending(f => f.Id).Skip(10*(page-1)).Take(10).ToListAsync();
            var count = await _context.Comments.Where(p => p.PostId == postID).CountAsync();
            //var result = PaginatedList<Comment>.Create(comments, page, 10);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = comments,
                NumberOfRecords = count
            };
        }

        public async Task<ApiMessage> deleteCommentAsync(int commentId)
        {
            var comment = await _context.Comments.SingleOrDefaultAsync(b => b.Id == commentId);
            if(comment != null)
            {
                _context.Remove(comment);
                var notification = new Notification
                {
                    UserId = comment.UserId,
                    Content = @"Bình luận """ + comment.Content + @""" của bạn bị xóa vì vi phạm nội quy!",
                    CreatedDate = DateTime.Now,
                    IsRead = false,
                };
                _context.Add(notification);
                await _context.SaveChangesAsync();
                return new ApiMessage
                {
                    Message = Message.DELETE_SUCCESS.ToString()
                };
            }
            return new ApiMessage
            {
                Message = Message.COMMENT_NOT_EXIST.ToString()
            };
        }
    }
}
