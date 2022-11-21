namespace BTP_API.ServicesImpl
{
    public interface IManageCategoryRepository
    {
        public Task<ApiResponse> getAllCategoryAsync(int page = 1);
        public Task<ApiResponse> getCategoryByIdAsync(int categoryId);
        public Task<ApiResponse> searchCategoryAsync(string search, int page = 1);
        public Task<ApiResponse> createCategoryAsync(CategoryVM categoryVM);
        public Task<ApiMessage> updateCategoryAsync(int categoryId, CategoryVM categoryVM);
        public Task<ApiMessage> deleteCategoryAsync(int categoryId);
    }
}
