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
            var books = await _context.Books.Include(b => b.User).OrderByDescending(b => b.Id).Skip(10*(page-1)).Take(10).ToListAsync();
            var count = await _context.Books.CountAsync();
            //var result = PaginatedList<Book>.Create(books, page, 10);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = books,
                NumberOfRecords = count
            };
        }
        public async Task<ApiResponse> getAllBookApprovedAsync(int page = 1)
        {
            var books = await _context.Books.Include(b => b.User).Where(b => b.Status == StatusRequest.Approved.ToString()).OrderByDescending(b => b.Id).Skip(10 * (page - 1)).Take(10).ToListAsync();
            var count = await _context.Books.Where(b => b.Status == StatusRequest.Approved.ToString()).CountAsync();
            //var result = PaginatedList<Book>.Create(books, page, 10);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = books,
                NumberOfRecords = count
            };
        }
        public async Task<ApiResponse> getAllBookDeniedAsync(int page = 1)
        {
            var books = await _context.Books.Include(b => b.User).Where(b => b.Status == StatusRequest.Denied.ToString()).OrderByDescending(b => b.Id).Skip(10 * (page - 1)).Take(10).ToListAsync();
            var count = await _context.Books.Where(b => b.Status == StatusRequest.Denied.ToString()).CountAsync();
            //var result = PaginatedList<Book>.Create(books, page, 10);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = books,
                NumberOfRecords = count
            };
        }
        public async Task<ApiResponse> getAllBookWaitingAsync(int page = 1)
        {
            var books = await _context.Books.Include(b => b.User).Where(b => b.Status == StatusRequest.Waiting.ToString()).Skip(10 * (page - 1)).Take(10).OrderByDescending(b => b.Id).ToListAsync();
            var count = await _context.Books.Where(b => b.Status == StatusRequest.Waiting.ToString()).CountAsync();
            //var result = PaginatedList<Book>.Create(books, page, 10);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = books,
                NumberOfRecords = count
            };
        }
        public async Task<ApiResponse> getBookByIdAsync(int bookId)
        {
            var book = await _context.Books.Include(b => b.User).Include(b => b.Category).SingleOrDefaultAsync(b => b.Id == bookId);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = book
            };
        }

        public async Task<ApiResponse> searchBookAsync(string search, int page = 1)
        {
            List<Book> books;
            if (search != null)
            {
                search = search.ToLower().Trim();
                books = await _context.Books.Include(b => b.User).Where(b => b.Title.ToLower().Contains(search)).OrderByDescending(b => b.Id).ToListAsync();
            }
            else
            {
                books = await _context.Books.Include(b => b.User).OrderByDescending(b => b.Id).ToListAsync();
            }

            //var result = PaginatedList<Book>.Create(books, page, 9);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = books.Skip(10 * (page - 1)).Take(9),
                NumberOfRecords = books.Count
            };
        }

        public async Task<ApiMessage> approvedBookAsync(int bookId)
        {
            var book = await _context.Books.SingleOrDefaultAsync(b => b.Id == bookId);
            if (book != null)
            {
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
            var feedbacks = await _context.Feedbacks.Include(p => p.User).Where(p => p.BookId == bookId).OrderByDescending(f => f.Id).Skip(10*(page-1)).Take(10).ToListAsync();
            var count = await _context.Feedbacks.Where(p => p.BookId == bookId).CountAsync();
            //var result = PaginatedList<Feedback>.Create(feedbacks, page, 10);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = feedbacks,
                NumberOfRecords = count
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
