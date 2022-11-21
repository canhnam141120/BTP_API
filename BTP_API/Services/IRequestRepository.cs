namespace BTP_API.Services
{
    public interface IRequestRepository
    {
        public Task<ApiMessage> createRequestAsync(int userId, int bookid, List<int> bookOffer);
        public Task<ApiMessage> cancelRequestAsync(int userId, int requestId);
        public Task<ApiMessage> acceptRequestAsync(int userId, int requestId);
        public Task<ApiMessage> deniedRequestAsync(int userId, int requestId);
        public Task<ApiMessage> rentBookAsync(int userId, int bookId);
    }
}
