using Org.BouncyCastle.Asn1.Crmf;

namespace BTP_API.Services
{
    public interface IPersonalRepository
    {
        public Task<ApiResponse> getAllNotificationAsync(int page = 1);
        public Task<ApiResponse> get10NewNotificationAsync();
        public Task<ApiMessage> markReadNotificationAsync(int nottificationId);
        public Task<ApiResponse> getBookCanTradeAsync();
        public Task<ApiResponse> getAllBookAsync(int page = 1);
        public Task<ApiResponse> getBookApprovedAsync(int page = 1);
        public Task<ApiResponse> getBookDeniedAsync(int page = 1);
        public Task<ApiResponse> getBookWaitingAsync(int page = 1);
        public Task<ApiResponse> getAllPostAsync(int page = 1);
        public Task<ApiResponse> getPostApprovedAsync(int page = 1);
        public Task<ApiResponse> getPostDeniedAsync(int page = 1);
        public Task<ApiResponse> getPostWaitingAsync(int page = 1);
        public Task<ApiResponse> getBookByFavoritesAsync(int page = 1);
        public Task<ApiMessage> addBookByFavoritesAsync(int bookId);
        public Task<ApiMessage> deleteBookByFavoritesAsync(int bookId);
        public Task<ApiResponse> getPostByFavoritesAsync(int page = 1);
        public Task<ApiMessage> addPostByFavoritesAsync(int postId);
        public Task<ApiMessage> deletePostByFavoritesAsync(int postId);
        public Task<ApiResponse> getUserByFavoritesAsync(int page = 1);
        public Task<ApiMessage> addUserByFavoritesAsync(int userId);
        public Task<ApiMessage> deleteUserByFavoritesAsync(int userId);
        public Task<ApiResponse> getInfoUserIdAsync();
        public Task<ApiMessage> editInfoAsync(UserVM userVM);
        public Task<ApiMessage> editPasswordAsync(ChangePasswordVM changePasswordVM);
        public Task<ApiResponse> listOfRequestSendAsync(int page = 1);
        public Task<ApiResponse> listOfRequestReceivedSendAsync(int bookId, int page = 1);
        public Task<ApiResponse> myTransactionExchangeAsync(int page = 1);
        public Task<ApiResponse> myTransactionExDetailAsync(int exchangeId);
        public Task<ApiResponse> myTransactionExBillAsync(int exchangeId);
        public Task<ApiResponse> myExBillAllAsync(int page = 1);
        public Task<ApiResponse> myTransactionRentAsync(int page = 1);
        public Task<ApiResponse> myTransactionRentDetailAsync(int rentId);
        public Task<ApiResponse> myTransactionRentBillAsync(int rentId);
        public Task<ApiResponse> myRentBillAllAsync(int page = 1);
        public Task<ApiMessage> updateInfoShippingAsync(ShipInfoVM shipInfoVM);
    }
}