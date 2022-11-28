using Org.BouncyCastle.Asn1.Crmf;

namespace BTP_API.Services
{
    public interface IPersonalRepository
    {
        public Task<ApiResponse> getAllNotificationAsync(int userId, int page = 1);
        public Task<ApiResponse> get10NewNotificationAsync(int userId);
        public Task<ApiMessage> markReadNotificationAsync(int userId, int nottificationId);
        public Task<ApiResponse> getBookCanTradeAsync(int userId);
        public Task<ApiResponse> getAllBookAsync(int userId, int page = 1);
        public Task<ApiResponse> getBookApprovedAsync(int userId, int page = 1);
        public Task<ApiResponse> getBookDeniedAsync(int userId, int page = 1);
        public Task<ApiResponse> getBookWaitingAsync(int userId, int page = 1);
        public Task<ApiResponse> getAllPostAsync(int userId, int page = 1);
        public Task<ApiResponse> getPostApprovedAsync(int userId, int page = 1);
        public Task<ApiResponse> getPostDeniedAsync(int userId, int page = 1);
        public Task<ApiResponse> getPostWaitingAsync(int userId, int page = 1);
        public Task<ApiResponse> getBookByFavoritesAsync(int userId, int page = 1);
        public Task<ApiMessage> addBookByFavoritesAsync(int userId, int bookId);
        public Task<ApiMessage> deleteBookByFavoritesAsync(int userId, int bookId);
        public Task<ApiResponse> getPostByFavoritesAsync(int userId, int page = 1);
        public Task<ApiMessage> addPostByFavoritesAsync(int userId, int postId);
        public Task<ApiMessage> deletePostByFavoritesAsync(int userId, int postId);
        public Task<ApiResponse> getUserByFavoritesAsync(int userId, int page = 1);
        public Task<ApiMessage> addUserByFavoritesAsync(int userId, int id);
        public Task<ApiMessage> deleteUserByFavoritesAsync(int userId, int id);
        public Task<ApiResponse> getInfoUserIdAsync(int userId);
        public Task<ApiMessage> editInfoAsync(int userId, UserVM userVM);
        public Task<ApiMessage> editPasswordAsync(int userId, ChangePasswordVM changePasswordVM);
        public Task<ApiResponse> listOfRequestSendAsync(int userId, int page = 1);
        public Task<ApiResponse> listOfRequestReceivedSendAsync(int userId, int bookId);
        public Task<ApiResponse> myTransactionExchangeAsync(int userId, int page = 1);
        public Task<ApiResponse> myTransactionExDetailAsync(int userId, int exchangeId);
        public Task<ApiResponse> myTransactionExBillAsync(int userId, int exchangeId);
        public Task<ApiResponse> myExBillAllAsync(int userId, int page = 1);
        public Task<ApiResponse> myTransactionRentAsync(int userId, int page = 1);
        public Task<ApiResponse> myTransactionRentDetailAsync(int userId, int rentId);
        public Task<ApiResponse> myTransactionRentBillAsync(int userId, int rentId);
        public Task<ApiResponse> myRentBillAllAsync(int userId, int page = 1);
        public Task<ApiResponse> getInfoShippingAsync(int userId);
        public Task<ApiMessage> updateInfoShippingAsync(int userId, ShipInfoVM shipInfoVM);
    }
}