namespace BTP_API.ServicesImpl
{
    public interface IManageAdminRepository
    {
        public Task<ApiResponse> getAllAdminAsync();
        public Task<ApiMessage> removeAdminAsync(int userId);
        public Task<ApiResponse> searchAdminAsync(string search);
    }
}
