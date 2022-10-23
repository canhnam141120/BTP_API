using Org.BouncyCastle.Asn1.Crmf;

namespace BTP_API.Services
{
    public interface IPersonalRepository
    {
        public Task<ApiResponse> getBookCanTradeAsync();
        public Task<ApiResponse> getAllBookAsync();
        public Task<ApiResponse> getBookApprovedAsync();
        public Task<ApiResponse> getBookDeniedAsync();
        public Task<ApiResponse> getBookWaitingAsync();
        public Task<ApiResponse> getAllPostAsync();
        public Task<ApiResponse> getPostApprovedAsync();
        public Task<ApiResponse> getPostDeniedAsync();
        public Task<ApiResponse> getPostWaitingAsync();
        public Task<ApiResponse> getBookByFavoritesAsync();
        public Task<ApiMessage> addBookByFavoritesAsync(int bookId);
        public Task<ApiMessage> deleteBookByFavoritesAsync(int bookId);
        public Task<ApiResponse> getPostByFavoritesAsync();
        public Task<ApiMessage> addPostByFavoritesAsync(int postId);
        public Task<ApiMessage> deletePostByFavoritesAsync(int postId);
        public Task<ApiResponse> getUserByFavoritesAsync();
        public Task<ApiMessage> addUserByFavoritesAsync(int userId);
        public Task<ApiMessage> deleteUserByFavoritesAsync(int userId);
        public Task<ApiResponse> getInfoUserIdAsync();
        public Task<ApiMessage> editInfoAsync(UserVM userVM);
        public Task<ApiMessage> editPasswordAsync(ChangePasswordVM changePasswordVM);
        public Task<ApiResponse> listOfRequestSendAsync();
        public Task<ApiResponse> listOfRequestReceivedSendAsync(int bookId);
        public Task<ApiResponse> myTransactionExchangeAsync();
        public Task<ApiResponse> myTransactionExDetailAsync(int exchangeId);
        public Task<ApiResponse> myTransactionExBillAsync(int exchangeId);
        public Task<ApiResponse> myExBillAllAsync();
        public Task<ApiResponse> myTransactionRentAsync();
        public Task<ApiResponse> myTransactionRentDetailAsync(int rentId);
        public Task<ApiResponse> myTransactionRentBillAsync(int rentId);
        public Task<ApiResponse> myRentBillAllAsync();
        public Task<ApiMessage> updateInfoShippingAsync(ShipInfoVM shipInfoVM);
    }
}