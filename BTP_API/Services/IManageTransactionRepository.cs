namespace BTP_API.Services
{
    public interface IManageTransactionRepository
    {
        public Task<ApiResponse> getAllExchangeAsync();
        public Task<ApiResponse> getAllExchangeDetailAsync(int exchangeId);
        public Task<ApiResponse> getAllExchangeBillAsync(int exchangeId);
        public Task<ApiMessage> updateStatusExchangeAsync(int exchangeId, string status);
        public Task<ApiMessage> updateExchangeDetailAsync(int exchangeDetailId, ExchangeDetailVM exchangeDetailVM);

        public Task<ApiResponse> getAllRentAsync();
        public Task<ApiResponse> getAllRentDetailAsync(int rentId);
        public Task<ApiResponse> getAllRentBillAsync(int rentId);
        public Task<ApiMessage> updateStatusRentAsync(int rentId, string status);
        public Task<ApiMessage> updateRentDetailAsync(int rentDetailId, RentDetailVM rentDetailVM);
    }
}
