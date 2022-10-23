namespace BTP_API.ServicesImpl
{
    public interface IBookRepository
    {
        public Task<ApiResponse> getAllBookAsync();
        public Task<ApiResponse> getBookByIdAsync(int bookId);
        public Task<ApiResponse> getFeedbackInBookAsync(int bookId);
        public Task<ApiResponse> getBookByCategoryAsync(int categoryId);
        public Task<ApiResponse> getBookByUserAsync(int userId);
        public Task<ApiResponse> searchBookByTitleAsync(string search);
        public Task<ApiResponse> createBookAsync(BookVM bookVM);
        public Task<ApiMessage> feedbackBookAsync(int bookId, FeedbackVM feedbackVM);
        public Task<ApiMessage> updateBookAsync(int bookId, BookVM bookVM);
        public Task<ApiMessage> hideBookAsync(int bookId);
        public Task<ApiMessage> showBookAsync(int bookId);
    }
}
