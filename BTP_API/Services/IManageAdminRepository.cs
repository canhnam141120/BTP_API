namespace BTP_API.ServicesImpl
{
    public interface IManageAdminRepository
    {
        public Task<ApiResponse> getAllAdminAsync(int page = 1);
        public Task<ApiMessage> removeAdminAsync(int userId);
        public Task<ApiResponse> searchAdminAsync(string search, int page = 1);
    }
}
