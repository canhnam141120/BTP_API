namespace BTP_API.Services
{
    public interface ITransactionRepository
    {
        public Task<ApiMessage> cancelExchangeAsync(int exchangeId);
        public Task<ApiMessage> cancelExchangeDetailAsync(int exchangeDetailId);
        public Task<ApiMessage> cancelRentAsync(int rentId);
        public Task<ApiMessage> cancelRentDetailAsync(string token, int rentDetailId);
        public Task<ApiResponse> createURLPayAsync(int billId);
        public Task<ApiMessage> updatePayAsync(ResultPayment rs);
    }
}
