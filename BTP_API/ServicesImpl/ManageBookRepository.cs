namespace BTP_API.Services
{
    public class ManageBookRepository : IManageBookRepository
    {
        private readonly BTPContext _context;

        public ManageBookRepository(BTPContext context)
        {
            _context = context;
        }
        public async Task<ApiResponse> getAllBookAsync()
        {
            var books = await _context.Books.ToListAsync();
            if (books.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = books,
                NumberOfRecords = books.Count
            };
        }
        public async Task<ApiResponse> getAllBookApprovedAsync()
        {
            var books = await _context.Books.Where(b => b.Status == StatusRequest.Approved.ToString()).ToListAsync();
            if (books.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = books,
                NumberOfRecords = books.Count
            };
        }
        public async Task<ApiResponse> getAllBookDeniedAsync()
        {
            var books = await _context.Books.Where(b => b.Status == StatusRequest.Denied.ToString()).ToListAsync();
            if (books.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = books,
                NumberOfRecords = books.Count
            };
        }
        public async Task<ApiResponse> getAllBookWaitingAsync()
        {
            var books = await _context.Books.Where(b => b.Status == StatusRequest.Waiting.ToString()).ToListAsync();
            if (books.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = books,
                NumberOfRecords = books.Count
            };
        }
        public async Task<ApiResponse> getBookByIdAsync(int bookId)
        {
            var book = await _context.Books.SingleOrDefaultAsync(b => b.Id == bookId);
            if (book == null)
            {
                return new ApiResponse
                {
                    Message = Message.BOOK_NOT_EXIST.ToString()
                };
            }
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = book,
                NumberOfRecords = 1
            };
        }
        public async Task<ApiMessage> approvedBookAsync(int bookId)
        {
            var book = await _context.Books.SingleOrDefaultAsync(b => b.Id == bookId);
            if (book != null)
            {
                if(book.Status == StatusRequest.Approved.ToString())
                {
                    return new ApiMessage
                    {
                        Message = Message.APPROVED.ToString()
                    };
                }
                book.Status = StatusRequest.Approved.ToString();
                _context.Update(book);
                await _context.SaveChangesAsync();
                return new ApiMessage
                {
                    Message = Message.SUCCESS.ToString()
                };
            }
            return new ApiMessage
            {
                Message = Message.BOOK_NOT_EXIST.ToString()
            };
        }
        public async Task<ApiMessage> deniedBookAsync(int bookId)
        {
            var book = await _context.Books.SingleOrDefaultAsync(b => b.Id == bookId);
            if (book != null)
            {
                if (book.Status == StatusRequest.Denied.ToString())
                {
                    return new ApiMessage
                    {
                        Message = Message.DENIED.ToString()
                    };
                }
                book.Status = StatusRequest.Denied.ToString();
                _context.Update(book);
                await _context.SaveChangesAsync();
                return new ApiMessage
                {
                    Message = Message.SUCCESS.ToString()
                };
            }
            return new ApiMessage
            {
                Message = Message.BOOK_NOT_EXIST.ToString()
            };
        }
    }
}
