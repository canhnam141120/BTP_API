namespace BTP_API.ServicesImpl
{
    public interface IManageBillRepository
    {
        public Task<ApiResponse> getAllExBillAsync(int page = 1);
        public Task<ApiResponse> getExBillDetailAsync(int exBillId);
        public Task<ApiMessage> updateStatusExBillDetailAsync(int exBillId, ExchangeBillVM exchangeBillVM);

        public Task<ApiResponse> getAllRentBillAsync(int page = 1);
        public Task<ApiResponse> getRentBillDetailAsync(int rentBillId);
        public Task<ApiMessage> updateStatusRentBillDetailAsync(int rentBillId, RentBillVM rentBillVM);

    }
}
