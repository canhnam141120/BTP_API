namespace BTP_API.ServicesImpl
{
    public interface IManageFeeRepository
    {
        public Task<ApiResponse> getAllFeeAsync(int page = 1);
        public Task<ApiResponse> getFeeByIdAsync(int feeId);
        public Task<ApiResponse> createFeeAsync(FeeVM feeVM);
    }
}
