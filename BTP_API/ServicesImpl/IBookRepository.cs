using BTP_API.Models;

namespace BTP_API.ServicesImpl
{
    public interface IBookRepository
    {
        public Task<List<Book>> getAllBookAsync();
    }
}
