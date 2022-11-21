namespace BTP_API.ServicesImpl
{
    public interface IManagePostRepository
    {
        public Task<ApiResponse> getAllPostAsync(int page = 1);
        public Task<ApiResponse> getPostApprovedAsync(int page = 1);
        public Task<ApiResponse> getPostDeniedAsync(int page = 1);
        public Task<ApiResponse> getPostWaitingAsync(int page = 1);
        public Task<ApiResponse> getPostByIdAsync(int postId);
        public Task<ApiResponse> searchPostAsync(string search, int page = 1);
        public Task<ApiMessage> approvedPostAsync(int postId);
        public Task<ApiMessage> deniedPostAsync(int postId);
        public Task<ApiResponse> getCommentInPostAsync(int postId, int page = 1);
        public Task<ApiMessage> deleteCommentAsync(int commentId);
    }
}
