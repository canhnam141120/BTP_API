using BTP_API.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static BTP_API.Helpers.EnumVariable;

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
            var posts = await _context.Posts.OrderByDescending(f => f.CreatedDate).ToListAsync();
            if (posts.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            var result = PaginatedList<Post>.Create(posts, page, 5);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = result,
                NumberOfRecords = result.Count
            };
        }

        public async Task<ApiResponse> getPostApprovedAsync(int page = 1)
        {
            var posts = await _context.Posts.Where(b => b.Status == StatusRequest.Approved.ToString()).OrderByDescending(f => f.CreatedDate).ToListAsync();
            if (posts.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            var result = PaginatedList<Post>.Create(posts, page, 5);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = result,
                NumberOfRecords = result.Count
            };
        }

        public async Task<ApiResponse> getPostDeniedAsync(int page = 1)
        {
            var posts = await _context.Posts.Where(b => b.Status == StatusRequest.Denied.ToString()).OrderByDescending(f => f.CreatedDate).ToListAsync();
            if (posts.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            var result = PaginatedList<Post>.Create(posts, page, 5);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = result,
                NumberOfRecords = result.Count
            };
        }

        public async Task<ApiResponse> getPostWaitingAsync(int page = 1)
        {
            var posts = await _context.Posts.Where(b => b.Status == StatusRequest.Waiting.ToString()).OrderByDescending(f => f.CreatedDate).ToListAsync();
            if (posts.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            var result = PaginatedList<Post>.Create(posts, page, 5);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = result,
                NumberOfRecords = result.Count
            };
        }

        public async Task<ApiResponse> getPostByIdAsync(int postID)
        {
            var post = await _context.Posts.SingleOrDefaultAsync(b => b.Id == postID);
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

        public async Task<ApiMessage> approvedPostAsync(int postID)
        {
            var post = await _context.Posts.SingleOrDefaultAsync(b => b.Id == postID && b.Status == StatusRequest.Waiting.ToString());
            if (post != null)
            {
                if (post.Status == StatusRequest.Approved.ToString())
                {
                    return new ApiMessage
                    {
                        Message = Message.APPROVED.ToString()
                    };
                }
                post.Status = StatusRequest.Approved.ToString();
                _context.Update(post);
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
            var post = await _context.Posts.SingleOrDefaultAsync(b => b.Id == postID && b.Status == StatusRequest.Waiting.ToString());
            if (post != null)
            {
                if (post.Status == StatusRequest.Denied.ToString())
                {
                    return new ApiMessage
                    {
                        Message = Message.DENIED.ToString()
                    };
                }
                post.Status = StatusRequest.Denied.ToString();
                _context.Update(post);
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
            var comments = await _context.Comments.Include(p => p.User).Where(p => p.PostId == postID).OrderByDescending(f => f.CreatedDate).ToListAsync();
            if (comments.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            var result = PaginatedList<Comment>.Create(comments, page, 10);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = result,
                NumberOfRecords = result.Count
            };
        }

        public async Task<ApiMessage> deleteCommentAsync(int commentId)
        {
            var comment = await _context.Comments.SingleOrDefaultAsync(b => b.Id == commentId);
            if(comment != null)
            {
                _context.Remove(comment);
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
