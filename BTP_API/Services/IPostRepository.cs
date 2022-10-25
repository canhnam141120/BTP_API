namespace BTP_API.Services
{
    public interface IPostRepository
    {
        public Task<ApiResponse> getAllPostAsync(int page = 1);
        public Task<ApiResponse> getPostByIdAsync(int postId);
        public Task<ApiResponse> getCommentInPostAsync(int postId, int page = 1);
        public Task<ApiResponse> searchPostByHashtagAsync(string search, int page = 1);
        public Task<ApiResponse> createPostAsync(PostVM postVM);
        public Task<ApiMessage> commentPostAsync(int postId, CommentVM commentVM);
        public Task<ApiMessage> hidePostAsync(int postId);
        public Task<ApiMessage> showPostAsync(int postId);
    }
}
