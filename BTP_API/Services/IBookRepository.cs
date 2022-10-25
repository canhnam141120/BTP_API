namespace BTP_API.ServicesImpl
{
    public interface IBookRepository
    {
        public Task<ApiResponse> getAllBookAsync(int page = 1);
        public Task<ApiResponse> getBookByIdAsync(int bookId);
        public Task<ApiResponse> getFeedbackInBookAsync(int bookId, int page = 1);
        public Task<ApiResponse> getBookByCategoryAsync(int categoryId, int page = 1);
        public Task<ApiResponse> getBookByUserAsync(int userId, int page = 1);
        public Task<ApiResponse> searchBookByTitleAsync(string search, int page = 1);
        public Task<ApiResponse> createBookAsync(BookVM bookVM);
        public Task<ApiMessage> feedbackBookAsync(int bookId, FeedbackVM feedbackVM);
        public Task<ApiMessage> updateBookAsync(int bookId, BookVM bookVM);
        public Task<ApiMessage> hideBookAsync(int bookId);
        public Task<ApiMessage> showBookAsync(int bookId);
    }
}
