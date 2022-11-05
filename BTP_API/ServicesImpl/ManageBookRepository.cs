namespace BTP_API.Services
{
    public class ManageBookRepository : IManageBookRepository
    {
        private readonly BTPContext _context;

        public ManageBookRepository(BTPContext context)
        {
            _context = context;
        }
        public async Task<ApiResponse> getAllBookAsync(int page = 1)
        {
            var books = await _context.Books.Include(b => b.User).Include(b => b.Category).OrderByDescending(b => b.Id).ToListAsync();
            if (books.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            var result = PaginatedList<Book>.Create(books, page, 10);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = result,
                NumberOfRecords = result.Count
            };
        }
        public async Task<ApiResponse> getAllBookApprovedAsync(int page = 1)
        {
            var books = await _context.Books.Include(b => b.User).Include(b => b.Category).Where(b => b.Status == StatusRequest.Approved.ToString()).OrderByDescending(b => b.Id).ToListAsync();
            if (books.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            var result = PaginatedList<Book>.Create(books, page, 10);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = result,
                NumberOfRecords = result.Count
            };
        }
        public async Task<ApiResponse> getAllBookDeniedAsync(int page = 1)
        {
            var books = await _context.Books.Include(b => b.User).Include(b => b.Category).Where(b => b.Status == StatusRequest.Denied.ToString()).OrderByDescending(b => b.Id).ToListAsync();
            if (books.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            var result = PaginatedList<Book>.Create(books, page, 10);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = result,
                NumberOfRecords = result.Count
            };
        }
        public async Task<ApiResponse> getAllBookWaitingAsync(int page = 1)
        {
            var books = await _context.Books.Include(b => b.User).Include(b => b.Category).Where(b => b.Status == StatusRequest.Waiting.ToString()).OrderByDescending(b => b.Id).ToListAsync();
            if (books.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            var result = PaginatedList<Book>.Create(books, page, 10);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = result,
                NumberOfRecords = result.Count
            };
        }
        public async Task<ApiResponse> getBookByIdAsync(int bookId)
        {
            var book = await _context.Books.Include(b => b.User).Include(b => b.Category).SingleOrDefaultAsync(b => b.Id == bookId);
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
                var notification = new Notification
                {
                    UserId = book.UserId,
                    Content = @"Sách """ + book.Title + @""" của bạn đã được duyệt!",
                    CreatedDate = DateTime.Now,
                    IsRead = false,
                };
                _context.Add(notification);
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
                var notification = new Notification
                {
                    UserId = book.UserId,
                    Content = @"Sách """ + book.Title + @""" của bạn không được duyệt!",
                    CreatedDate = DateTime.Now,
                    IsRead = false,
                };
                _context.Add(notification);
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

        public async Task<ApiResponse> getFeedbackInBookAsync(int bookId, int page = 1)
        {
            var feedbacks = await _context.Feedbacks.Include(p => p.User).Where(p => p.BookId == bookId).OrderByDescending(f => f.CreatedDate).ToListAsync();
            if (feedbacks.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            var result = PaginatedList<Feedback>.Create(feedbacks, page, 10);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = result,
                NumberOfRecords = result.Count
            };
        }

        public async Task<ApiMessage> deleteFeedbackAsync(int feedbackId)
        {
            var feedback = await _context.Feedbacks.SingleOrDefaultAsync(b => b.Id == feedbackId);
            if (feedback != null)
            {
                _context.Remove(feedback);
                var notification = new Notification
                {
                    UserId = feedback.UserId,
                    Content = @"Đánh giá """ + feedback.Content + @""" của bạn bị xóa vì vi phạm nội quy!",
                    CreatedDate = DateTime.Now,
                    IsRead = false,
                };
                _context.Add(notification);
                await _context.SaveChangesAsync();
                return new ApiMessage
                {
                    Message = Message.DELETE_SUCCESS.ToString()
                };
            }
            return new ApiMessage
            {
                Message = Message.COMMENT_NOT_EXIST.ToString()
            };
        }
    }
}
