using BTP_API.Helpers;

namespace BTP_API.ServicesImpl
{
    public interface IManageBookRepository
    {
        public Task<ApiResponse> getAllBookAsync();
        public Task<ApiResponse> getAllBookApprovedAsync();
        public Task<ApiResponse> getAllBookDeniedAsync();
        public Task<ApiResponse> getAllBookWaitingAsync();
        public Task<ApiResponse> getBookByIdAsync(int bookId);
        public Task<ApiMessage> approvedBookAsync(int bookId);
        public Task<ApiMessage> deniedBookAsync(int bookId);
    }
}
