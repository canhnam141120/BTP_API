namespace BTP_API.ServicesImpl
{
    public interface IManageBillRepository
    {
        public Task<ApiResponse> getAllExBillAsync();
        public Task<ApiResponse> getExBillDetailAsync(int exBillId);
        public Task<ApiMessage> updateStatusExBillDetailAsync(int exBillId, ExchangeBillVM exchangeBillVM);

        public Task<ApiResponse> getAllRentBillAsync();
        public Task<ApiResponse> getRentBillDetailAsync(int rentBillId);
        public Task<ApiMessage> updateStatusRentBillDetailAsync(int rentBillId, RentBillVM rentBillVM);

    }
}
