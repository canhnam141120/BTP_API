namespace BTP_API.Services
{
    public interface IRequestRepository
    {
        public Task<ApiMessage> createRequestAsync(int bookid, List<int> bookOffer);
        public Task<ApiMessage> cancelRequestAsync(int requestId);
        public Task<ApiMessage> acceptRequestAsync(int requestId);
        public Task<ApiMessage> deniedRequestAsync(int requestId);
        public Task<ApiMessage> rentBookAsync(int bookId);
    }
}
