namespace BTP_API.Services
{
    public interface IManageUserRepository
    {
        public Task<ApiResponse> getAllUserAsync();
        public Task<ApiResponse> getAllUserBanAsync();
        public Task<ApiResponse> getAllUserActiveAsync();
        public Task<ApiResponse> getUserByIdAsync(int userId);
        public Task<ApiResponse> searchUserAsync(string search);
        public Task<ApiMessage> banAccAsync(int userId);
        public Task<ApiMessage> activeAccAsync(int userId);
        public Task<ApiMessage> authorityAdminAsync(int userId);
    }
}
