namespace BTP_API.ServicesImpl
{
    public interface IManagePostRepository
    {
        public Task<ApiResponse> getAllPostAsync();
        public Task<ApiResponse> getPostApprovedAsync();
        public Task<ApiResponse> getPostDeniedAsync();
        public Task<ApiResponse> getPostWaitingAsync();
        public Task<ApiResponse> getPostByIdAsync(int postId);
        public Task<ApiMessage> approvedPostAsync(int postId);
        public Task<ApiMessage> deniedPostAsync(int postId);
        public Task<ApiResponse> getCommentInPostAsync(int postId);
        public Task<ApiMessage> deleteCommentAsync(int commentId);
    }
}
