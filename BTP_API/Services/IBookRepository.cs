namespace BTP_API.ServicesImpl
{
    public interface IBookRepository
    {
        public Task<ApiResponse> getAllBookFromFavoriteUserAsync(int userId, int page = 1);
        public Task<ApiResponse> get6BookAsync();
        public Task<ApiResponse> getAllBookAsync(int page = 1);
        public Task<ApiResponse> getBookByFilterAsync(string filter1, int filter2, string filter3, string filter4, int page = 1);
        public Task<ApiResponse> getAllBookIsExchangeAsync(int page = 1);
        public Task<ApiResponse> getAllBookIsRentAsync(int page = 1);
        public Task<ApiResponse> getBookByIdAsync(int bookId);
        public Task<ApiResponse> getFeedbackInBookAsync(int bookId, int page = 1);
        public Task<ApiResponse> getBookByCategoryAsync(int categoryId, int page = 1);
        public Task<ApiResponse> get6BookByCategoryAsync(int categoryId);
        public Task<ApiResponse> getBookByUserAsync(int userId, int page = 1);
        public Task<ApiResponse> searchBookOfUserAsync(int userId, string search, int page = 1);
        public Task<ApiResponse> get6BookByUserAsync(int userId);
        public Task<ApiResponse> searchBookByTitleAsync(string search, int page = 1);
        public Task<ApiResponse> createBookAsync(int userId, BookVM bookVM);
        public Task<ApiMessage> feedbackBookAsync(int userid, int bookId, FeedbackVM feedbackVM);
        public Task<ApiMessage> updateBookAsync(int bookId, BookVM bookVM);
        public Task<ApiMessage> hideBookAsync(int bookId);
        public Task<ApiMessage> showBookAsync(int bookId);
    }
}
