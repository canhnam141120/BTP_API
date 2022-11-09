using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.WebSockets;

namespace BTP_API.Services
{
    public class BookRepository : IBookRepository
    {
        private readonly BTPContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public BookRepository(BTPContext context, IWebHostEnvironment environment, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _environment = environment;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResponse> getAllBookFromFavoriteUserAsync(string token, int page = 1)
        {
            Cookie cookie = new Cookie();
            int userId = cookie.GetUserId(token);
            if (userId == 0)
            {
                return new ApiResponse { Message = Message.NOT_YET_LOGIN.ToString() };
            }

            List<Book> bookList = new List<Book>();

            var favoriteUsers = await _context.FavoriteUserLists.Where(f => f.UserId == userId).ToListAsync();
            foreach (var favoriteUser in favoriteUsers)
            {
                var books = await _context.Books.Include(b => b.User).Include(b => b.Category).Where(b => b.UserId == favoriteUser.FavoriteUserId && b.Status == StatusRequest.Approved.ToString() && b.IsReady == true).OrderByDescending(b => b.Id).ToListAsync();
                foreach(var book in books)
                {
                    bookList.Add(book);
                }
            }
           
            if (bookList.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            var result = PaginatedList<Book>.Create(bookList, page, 9);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = result,
                NumberOfRecords = result.Count
            };
        }

        public async Task<ApiResponse> getAllBookAsync(int page = 1)
        {
            var books = await _context.Books.Include(b => b.User).Include(b => b.Category).Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true).OrderByDescending(b => b.Id).ToListAsync();
            if (books.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            var result = PaginatedList<Book>.Create(books, page, 9);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = result,
                NumberOfRecords = result.Count
            };
        }
        public async Task<ApiResponse> getBookByIdAsync(int bookId)
        {
            var book = await _context.Books.Include(b => b.User).Include(b => b.Category).SingleOrDefaultAsync(b => b.Id == bookId && b.Status == StatusRequest.Approved.ToString() && b.IsReady == true);
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
        public async Task<ApiResponse> getFeedbackInBookAsync(int bookId, int page = 1)
        {
            var check = await _context.Books.AnyAsync(b => b.Id == bookId && b.Status == StatusRequest.Approved.ToString() && b.IsReady == true);
            if (!check)
            {
                return new ApiResponse
                {
                    Message = Message.BOOK_NOT_EXIST.ToString()
                };
            }
            var feedbacks = await _context.Feedbacks.Include(p => p.User).Where(p => p.BookId == bookId).OrderByDescending(p => p.CreatedDate).ToListAsync();
            if (feedbacks.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            var result = PaginatedList<Feedback>.Create(feedbacks, page, 5);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = result,
                NumberOfRecords = result.Count
            };
        }
        public async Task<ApiResponse> getBookByCategoryAsync(int categoryId, int page = 1)
        {
            var check = await _context.Categories.AnyAsync(u => u.Id == categoryId);
            if (!check)
            {
                return new ApiResponse
                {
                    Message = Message.CATEGORY_NOT_EXIST.ToString()
                };
            }
            var books = await _context.Books.Include(b => b.User).Include(b => b.Category).Where(b => b.CategoryId == categoryId && b.IsReady == true && b.Status == StatusRequest.Approved.ToString()).OrderByDescending(b => b.Id).ToListAsync();
            if (books.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            var result = PaginatedList<Book>.Create(books, page, 9);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = result,
                NumberOfRecords = result.Count
            };
        }
        public async Task<ApiResponse> getBookByUserAsync(int userId, int page = 1)
        {
            var check = await _context.Users.AnyAsync(u => u.Id == userId);
            if (!check)
            {
                return new ApiResponse
                {
                    Message = Message.USER_NOT_EXIST.ToString()
                };
            }
            var books = await _context.Books.Include(b => b.User).Where(b => b.UserId == userId && b.Status == StatusRequest.Approved.ToString() && b.IsReady == true).OrderByDescending(b => b.Id).ToListAsync();
            if (books.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            var result = PaginatedList<Book>.Create(books, page, 6);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = result,
                NumberOfRecords = result.Count
            };
        }

        public async Task<ApiResponse> searchBookByTitleAsync(string search, int page = 1)
        {
            List<Book> books;
            if (search != null)
            {
                search = search.ToLower().Trim();
                books = await _context.Books.Include(b => b.User).Where(b => b.Title.ToLower().Contains(search) && b.IsReady == true && b.Status == StatusRequest.Approved.ToString()).OrderByDescending(b => b.Id).ToListAsync();
            }
            else
            {
                books = await _context.Books.Include(b => b.User).Include(b => b.Category).Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true).OrderByDescending(b => b.Id).ToListAsync();
            }

            if (books.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            var result = PaginatedList<Book>.Create(books, page, 9);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = result,
                NumberOfRecords = result.Count
            };
        }

        public async Task<ApiResponse> createBookAsync(string token, BookVM bookVM)
        {
            UploadFile uploadFile = new UploadFile();
            Cookie cookie = new Cookie();
            string fileImageName = uploadFile.UploadBookImage(bookVM, _environment);

            var userId = cookie.GetUserId(token);
            if(userId == 0)
            {
                return new ApiResponse
                {
                    Message = Message.NOT_YET_LOGIN.ToString()
                };
            }

            var book = new Book
            {
                UserId = userId,
                CategoryId = bookVM.CategoryId,
                Title = bookVM.Title,
                Description = bookVM.Description,
                Author = bookVM.Author,
                Publisher = bookVM.Publisher,
                Year = bookVM.Year,
                Language = bookVM.Language,
                NumberOfPages = bookVM.NumberOfPages,
                Weight = bookVM.Weight,
                CoverPrice = bookVM.CoverPrice,
                DepositPrice = bookVM.DepositPrice,
                StatusBook = bookVM.StatusBook,
                Image = fileImageName,
                PostedDate = DateOnly.FromDateTime(DateTime.Today),
                IsExchange = bookVM.IsExchange,
                IsRent = bookVM.IsRent,
                RentFee = bookVM.RentFee,
                NumberOfDays = (int)(Math.Ceiling((double)bookVM.NumberOfPages / 100) * 5),
                IsReady = true,
                IsTrade = false,
                Status = StatusRequest.Waiting.ToString(),
            };
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return new ApiResponse
            {
                Message = Message.CREATE_SUCCESS.ToString(),
                Data = book,
                NumberOfRecords = 1
            };
        }
        public async Task<ApiMessage> feedbackBookAsync(string token, int bookId, FeedbackVM feedbackVM)
        {
            Cookie cookie = new Cookie();
            int userId = cookie.GetUserId(token);
            if (userId == 0)
            {
                return new ApiMessage { Message = Message.NOT_YET_LOGIN.ToString() };
            }

            var book = await _context.Books.SingleOrDefaultAsync(b => b.Id == bookId && b.Status == StatusRequest.Approved.ToString() && b.IsReady == true);
            if (book != null)
            {
                var feedback = new Feedback
                {
                    BookId = bookId,
                    UserId = userId,
                    Content = feedbackVM.Content,
                    CreatedDate = DateTime.Now
                };
                _context.Feedbacks.Add(feedback);
                await _context.SaveChangesAsync();
                return new ApiMessage { Message = Message.SUCCESS.ToString() };
            }
            return new ApiMessage { Message = Message.BOOK_NOT_EXIST.ToString() };
        }

        public async Task<ApiMessage> updateBookAsync(int bookId, BookVM bookVM)
        {
            var book = await _context.Books.SingleOrDefaultAsync(b => b.Id == bookId && b.Status == StatusRequest.Approved.ToString() && b.IsReady == true );
            if (book != null)
            {
                book.CategoryId = bookVM.CategoryId;
                book.Title = bookVM.Title;
                book.Description = bookVM.Description;
                book.Author = bookVM.Author;
                book.Publisher = bookVM.Publisher;
                book.Year = bookVM.Year;
                book.Language = bookVM.Language;
                book.NumberOfPages = bookVM.NumberOfPages;
                book.Weight = bookVM.Weight;
                book.CoverPrice = bookVM.CoverPrice;
                book.DepositPrice = bookVM.DepositPrice;
                book.StatusBook = bookVM.StatusBook;
                book.IsExchange = bookVM.IsExchange;
                book.IsRent = bookVM.IsRent;
                book.RentFee = bookVM.RentFee;
                _context.Books.Update(book);
                await _context.SaveChangesAsync();
                return new ApiMessage { Message = Message.UPDATE_SUCCESS.ToString() };
            }
            else
            {
                return new ApiMessage { Message = Message.BOOK_NOT_EXIST.ToString() };
            }
        }
        public async Task<ApiMessage> hideBookAsync(int bookId)
        {
            var book = await _context.Books.SingleOrDefaultAsync(u => u.Id == bookId && u.Status == StatusRequest.Approved.ToString());
            if (book != null)
            {
                book.IsReady = false;
                _context.Books.Update(book);
                await _context.SaveChangesAsync();
                return new ApiMessage { Message = Message.SUCCESS.ToString() };
            }
            return new ApiMessage { Message = Message.BOOK_NOT_EXIST.ToString() };
        }
        public async Task<ApiMessage> showBookAsync(int bookId)
        {
            var book = await _context.Books.SingleOrDefaultAsync(u => u.Id == bookId && u.Status == StatusRequest.Approved.ToString());
            if (book != null)
            {
                book.IsReady = true;
                _context.Books.Update(book);
                await _context.SaveChangesAsync();
                return new ApiMessage { Message = Message.SUCCESS.ToString() };
            }
            return new ApiMessage { Message = Message.BOOK_NOT_EXIST.ToString() };
        }
    }
}
