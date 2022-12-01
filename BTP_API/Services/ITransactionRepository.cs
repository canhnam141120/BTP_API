namespace BTP_API.Services
{
    public interface ITransactionRepository
    {
        public Task<ApiMessage> cancelExchangeAsync(int exchangeId);
        public Task<ApiMessage> cancelExchangeDetailAsync(int exchangeDetailId);
        public Task<ApiMessage> cancelRentAsync(int rentId);
        public Task<ApiMessage> cancelRentDetailAsync(int userId, int rentDetailId);
        public Task<ApiResponse> createURLPayAsync(int billId);
        public Task<ApiMessage> updatePayAsync(ResultPayment rs);
        public Task<ApiResponse> createURLPayRentAsync(int billId);
        public Task<ApiMessage> updatePayRentAsync(ResultPayment rs);
    }
}
