namespace BTP_API.Services
{
    public interface IManageTransactionRepository
    {
        public Task<ApiResponse> getAllExchangeAsync(int page = 1);
        public Task<ApiResponse> getExchangeByIDAsync(int exchangeId);
        public Task<ApiResponse> getAllExchangeWaitingAsync(int page = 1);
        public Task<ApiResponse> getAllExchangeTradingAsync(int page = 1);
        public Task<ApiResponse> getAllExchangeCompleteAsync(int page = 1);
        public Task<ApiResponse> getAllExchangeCancelAsync(int page = 1);
        public Task<ApiResponse> searchExchangeAsync(int? id, int page = 1);
        public Task<ApiResponse> getAllExchangeDetailAsync(int exchangeId);
        public Task<ApiResponse> getAllExchangeBillAsync(int exchangeId);
        public Task<ApiMessage> updateStatusExchangeAsync(int exchangeId, ExchangeVM exchangeVM);
        public Task<ApiMessage> tradingExchangeAsync(int exchangeId);
        public Task<ApiMessage> completeExchangeAsync(int exchangeId);
        public Task<ApiMessage> updateExchangeDetailAsync(int exchangeDetailId, ExchangeDetailVM exchangeDetailVM);
        public Task<ApiResponse> getAllRentAsync(int page = 1);
        public Task<ApiResponse> getRentByIDAsync(int rentId);
        public Task<ApiResponse> getAllRentWaitingAsync(int page = 1);
        public Task<ApiResponse> getAllRentTradingAsync(int page = 1);
        public Task<ApiResponse> getAllRentCompleteAsync(int page = 1);
        public Task<ApiResponse> getAllRentCancelAsync(int page = 1);
        public Task<ApiResponse> searchRentAsync(int? id,int page = 1);
        public Task<ApiResponse> getAllRentDetailAsync(int rentId);
        public Task<ApiResponse> getAllRentBillAsync(int rentId);
        public Task<ApiMessage> updateStatusRentAsync(int rentId, RentVM rentVM);
        public Task<ApiMessage> tradingRentAsync(int rentId);
        public Task<ApiMessage> completeRentAsync(int rentId);
        public Task<ApiMessage> updateRentDetailAsync(int rentDetailId, RentDetailVM rentDetailVM);
    }
}
