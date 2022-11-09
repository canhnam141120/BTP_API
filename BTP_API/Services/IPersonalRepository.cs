using Org.BouncyCastle.Asn1.Crmf;

namespace BTP_API.Services
{
    public interface IPersonalRepository
    {
        public Task<ApiResponse> getAllNotificationAsync(string token, int page = 1);
        public Task<ApiResponse> get10NewNotificationAsync(string token);
        public Task<ApiMessage> markReadNotificationAsync(string token, int nottificationId);
        public Task<ApiResponse> getBookCanTradeAsync(string token);
        public Task<ApiResponse> getAllBookAsync(string token, int page = 1);
        public Task<ApiResponse> getBookApprovedAsync(string token, int page = 1);
        public Task<ApiResponse> getBookDeniedAsync(string token, int page = 1);
        public Task<ApiResponse> getBookWaitingAsync(string token, int page = 1);
        public Task<ApiResponse> getAllPostAsync(string token, int page = 1);
        public Task<ApiResponse> getPostApprovedAsync(string token, int page = 1);
        public Task<ApiResponse> getPostDeniedAsync(string token, int page = 1);
        public Task<ApiResponse> getPostWaitingAsync(string token, int page = 1);
        public Task<ApiResponse> getBookByFavoritesAsync(string token, int page = 1);
        public Task<ApiMessage> addBookByFavoritesAsync(string token, int bookId);
        public Task<ApiMessage> deleteBookByFavoritesAsync(string token, int bookId);
        public Task<ApiResponse> getPostByFavoritesAsync(string token, int page = 1);
        public Task<ApiMessage> addPostByFavoritesAsync(string token, int postId);
        public Task<ApiMessage> deletePostByFavoritesAsync(string token, int postId);
        public Task<ApiResponse> getUserByFavoritesAsync(string token, int page = 1);
        public Task<ApiMessage> addUserByFavoritesAsync(string token, int userId);
        public Task<ApiMessage> deleteUserByFavoritesAsync(string token, int userId);
        public Task<ApiResponse> getInfoUserIdAsync(string token);
        public Task<ApiMessage> editInfoAsync(string token, UserVM userVM);
        public Task<ApiMessage> editPasswordAsync(string token, ChangePasswordVM changePasswordVM);
        public Task<ApiResponse> listOfRequestSendAsync(string token, int page = 1);
        public Task<ApiResponse> listOfRequestReceivedSendAsync(string token, int bookId, int page = 1);
        public Task<ApiResponse> myTransactionExchangeAsync(string token, int page = 1);
        public Task<ApiResponse> myTransactionExDetailAsync(string token, int exchangeId);
        public Task<ApiResponse> myTransactionExBillAsync(string token, int exchangeId);
        public Task<ApiResponse> myExBillAllAsync(string token, int page = 1);
        public Task<ApiResponse> myTransactionRentAsync(string token, int page = 1);
        public Task<ApiResponse> myTransactionRentDetailAsync(string token, int rentId);
        public Task<ApiResponse> myTransactionRentBillAsync(string token, int rentId);
        public Task<ApiResponse> myRentBillAllAsync(string token, int page = 1);
        public Task<ApiMessage> updateInfoShippingAsync(string token, ShipInfoVM shipInfoVM);
    }
}