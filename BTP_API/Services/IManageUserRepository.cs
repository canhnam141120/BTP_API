namespace BTP_API.Services
{
    public interface IManageUserRepository
    {
        public Task<ApiResponse> getAllUserAsync(int page = 1);
        public Task<ApiResponse> getAllUserBanAsync(int page = 1);
        public Task<ApiResponse> getAllUserActiveAsync(int page = 1);
        public Task<ApiResponse> getTopUserLikeAsync();
        public Task<ApiResponse> getUserByIdAsync(int userId);
        public Task<ApiResponse> searchUserAsync(string search, int page = 1);
        public Task<ApiMessage> banAccAsync(int userId);
        public Task<ApiMessage> activeAccAsync(int userId);
        public Task<ApiMessage> authorityAdminAsync(int userId);
    }
}
