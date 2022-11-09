namespace BTP_API.Services
{
    public interface IRequestRepository
    {
        public Task<ApiMessage> createRequestAsync(string token, int bookid, List<int> bookOffer);
        public Task<ApiMessage> cancelRequestAsync(string token, int requestId);
        public Task<ApiMessage> acceptRequestAsync(string token, int requestId);
        public Task<ApiMessage> deniedRequestAsync(string token, int requestId);
        public Task<ApiMessage> rentBookAsync(string token, int bookId);
    }
}
