namespace BTP_API.ServicesImpl
{
    public interface IManageFeeRepository
    {
        public Task<ApiResponse> getAllFeeAsync();
        public Task<ApiResponse> getFeeByIdAsync(int feeId);
        public Task<ApiResponse> createFeeAsync(FeeVM feeVM);
    }
}
