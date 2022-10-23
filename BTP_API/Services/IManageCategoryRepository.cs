namespace BTP_API.ServicesImpl
{
    public interface IManageCategoryRepository
    {
        public Task<ApiResponse> getAllCategoryAsync();
        public Task<ApiResponse> getCategoryByIdAsync(int categoryId);
        public Task<ApiResponse> createCategoryAsync(CategoryVM categoryVM);
        public Task<ApiMessage> updateCategoryAsync(int categoryId, CategoryVM categoryVM);
        public Task<ApiMessage> deleteCategoryAsync(int categoryId);
    }
}
