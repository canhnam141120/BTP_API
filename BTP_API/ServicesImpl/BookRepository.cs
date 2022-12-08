using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.WebSockets;

namespace BTP_API.Services
{
    public class BookRepository : IBookRepository
    {
        private readonly BTPContext _context;
        private readonly IWebHostEnvironment _environment;
        
        public BookRepository(BTPContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<ApiResponse> getAllBookFromFavoriteUserAsync(int userId, int page = 1)
        {
            List<Book> bookList = new List<Book>();
            var favoriteUsers = await _context.FavoriteUserLists.Where(f => f.UserId == userId).ToListAsync();
            foreach (var favoriteUser in favoriteUsers)
            {
                var books = await _context.Books.Include(b => b.User).Where(b => b.UserId == favoriteUser.FavoriteUserId && b.Status == StatusRequest.Approved.ToString() && b.IsReady == true).OrderByDescending(b => b.Id).ToListAsync();
                foreach(var book in books)
                {
                    bookList.Add(book);
                }
            }
            //var result = PaginatedList<Book>.Create(bookList, page, 9);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = bookList.Skip(9 * (page -1)).Take(9),
                NumberOfRecords = bookList.Count
            };
        }

        public async Task<ApiResponse> get6BookAsync()
        {
            var books = await _context.Books.Include(b => b.User).Include(b => b.Category).Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true).OrderByDescending(b => b.Id).Take(6).ToListAsync();
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = books,
                NumberOfRecords = books.Count
            };
        }

        public async Task<ApiResponse> getAllBookAsync(int page = 1)
        {
            var books = await _context.Books.Include(b => b.User).Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true).OrderByDescending(b => b.Id).Skip(9 * (page - 1)).Take(9).ToListAsync();
            var count = await _context.Books.Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true).CountAsync();
            //var books = PaginatedList<Book>.Create(books, page, 9);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = books,
                NumberOfRecords = count
            };
        }

        public async Task<ApiResponse> getBookByFilterAsync(string filter1, int filter2, string filter3, string filter4, int page = 1)
        {
            //1234
            if(filter1 == "All" && filter2 == 0 && filter3 == "All" && filter4 == "All")
            {
                var books = await _context.Books.Include(b => b.User).Include(b => b.Category).Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true).OrderByDescending(b => b.Id).Skip(9 * (page - 1)).Take(9).ToListAsync();
                var count = await _context.Books.Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true).CountAsync();
                return new ApiResponse
                {
                    Message = Message.GET_SUCCESS.ToString(),
                    Data = books,
                    NumberOfRecords = count
                };
            }
            //123
            if (filter1 == "All" && filter2 == 0 && filter3 == "All" && filter4 != "All")
            {
                var books = await _context.Books.Include(b => b.User).Include(b => b.Category).Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true && b.Language == filter4).OrderByDescending(b => b.Id).Skip(9 * (page - 1)).Take(9).ToListAsync();
                var count = await _context.Books.Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true && b.Language == filter4).CountAsync();
                return new ApiResponse
                {
                    Message = Message.GET_SUCCESS.ToString(),
                    Data = books,
                    NumberOfRecords = count
                };
            }
            //124
            if (filter1 == "All" && filter2 == 0 && filter3 != "All" && filter4 == "All")
            {
                var price = filter3.Split("-"); 
                var books = await _context.Books.Include(b => b.User).Include(b => b.Category)
                    .Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true && b.DepositPrice > float.Parse(price[0]) && b.DepositPrice <= float.Parse(price[1]))
                    .OrderByDescending(b => b.Id).Skip(9 * (page - 1)).Take(9).ToListAsync();
                var count = await _context.Books
                    .Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true && b.DepositPrice > float.Parse(price[0]) && b.DepositPrice <= float.Parse(price[1])).CountAsync();
                return new ApiResponse
                {
                    Message = Message.GET_SUCCESS.ToString(),
                    Data = books,
                    NumberOfRecords = count
                };
            }
            //134
            if (filter1 == "All" && filter2 != 0 && filter3 == "All" && filter4 == "All")
            {
                var books = await _context.Books.Include(b => b.User).Include(b => b.Category)
                    .Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true && b.CategoryId == filter2)
                    .OrderByDescending(b => b.Id).Skip(9 * (page - 1)).Take(9).ToListAsync();
                var count = await _context.Books.Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true && b.CategoryId == filter2).CountAsync();
                return new ApiResponse
                {
                    Message = Message.GET_SUCCESS.ToString(),
                    Data = books,
                    NumberOfRecords = count
                };
            }
            //234
            if (filter1 != "All" && filter2 == 0 && filter3 == "All" && filter4 == "All")
            {
                if(filter1 == "Exchange")
                {
                    var books = await _context.Books.Include(b => b.User).Include(b => b.Category).Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true && b.IsExchange == true).OrderByDescending(b => b.Id).Skip(9 * (page - 1)).Take(9).ToListAsync();
                    var count = await _context.Books.Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true && b.IsExchange == true).CountAsync();
                    return new ApiResponse
                    {
                        Message = Message.GET_SUCCESS.ToString(),
                        Data = books,
                        NumberOfRecords = count
                    };
                }
                if (filter1 == "Rent")
                {
                    var books = await _context.Books.Include(b => b.User).Include(b => b.Category).Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true && b.IsRent == true).OrderByDescending(b => b.Id).Skip(9 * (page - 1)).Take(9).ToListAsync();
                    var count = await _context.Books.Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true && b.IsRent == true).CountAsync();
                    return new ApiResponse
                    {
                        Message = Message.GET_SUCCESS.ToString(),
                        Data = books,
                        NumberOfRecords = count
                    };
                }
            }
            //12
            if (filter1 == "All" && filter2 == 0 && filter3 != "All" && filter4 != "All")
            {
                var price = filter3.Split("-");
                var books = await _context.Books.Include(b => b.User).Include(b => b.Category)
                    .Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true && b.DepositPrice > float.Parse(price[0]) && b.DepositPrice <= float.Parse(price[1]) && b.Language == filter4)
                    .OrderByDescending(b => b.Id).Skip(9 * (page - 1)).Take(9).ToListAsync();
                var count = await _context.Books.Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true && b.DepositPrice > float.Parse(price[0]) && b.DepositPrice <= float.Parse(price[1]) && b.Language == filter4).CountAsync();
                return new ApiResponse
                {
                    Message = Message.GET_SUCCESS.ToString(),
                    Data = books,
                    NumberOfRecords = count
                };
            }
            //13
            if (filter1 == "All" && filter2 != 0 && filter3 == "All" && filter4 != "All")
            {
                var books = await _context.Books.Include(b => b.User).Include(b => b.Category)
                    .Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true && b.CategoryId == filter2 && b.Language == filter4)
                    .OrderByDescending(b => b.Id).Skip(9 * (page - 1)).Take(9).ToListAsync();
                var count = await _context.Books.Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true && b.CategoryId == filter2 && b.Language == filter4).CountAsync();
                return new ApiResponse
                {
                    Message = Message.GET_SUCCESS.ToString(),
                    Data = books,
                    NumberOfRecords = count
                };
            }
            //14
            if (filter1 == "All" && filter2 != 0 && filter3 != "All" && filter4 == "All")
            {
                var price = filter3.Split("-");
                var books = await _context.Books.Include(b => b.User).Include(b => b.Category)
                    .Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true && b.CategoryId == filter2 && b.DepositPrice > float.Parse(price[0]) && b.DepositPrice <= float.Parse(price[1]))
                    .OrderByDescending(b => b.Id).Skip(9 * (page - 1)).Take(9).ToListAsync();
                var count = await _context.Books
                    .Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true && b.CategoryId == filter2 && b.DepositPrice > float.Parse(price[0]) && b.DepositPrice <= float.Parse(price[1])).CountAsync();
                return new ApiResponse
                {
                    Message = Message.GET_SUCCESS.ToString(),
                    Data = books,
                    NumberOfRecords = count
                };
            }
            //23
            if (filter1 != "All" && filter2 == 0 && filter3 == "All" && filter4 != "All")
            {
                if (filter1 == "Exchange")
                {
                    var books = await _context.Books.Include(b => b.User).Include(b => b.Category).Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true && b.IsExchange == true && b.Language == filter4).OrderByDescending(b => b.Id).Skip(9 * (page - 1)).Take(9).ToListAsync();
                    var count = await _context.Books.Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true && b.IsExchange == true && b.Language == filter4).CountAsync();
                    return new ApiResponse
                    {
                        Message = Message.GET_SUCCESS.ToString(),
                        Data = books,
                        NumberOfRecords = count
                    };
                }
                if (filter1 == "Rent")
                {
                    var books = await _context.Books.Include(b => b.User).Include(b => b.Category).Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true && b.IsRent == true && b.Language == filter4).OrderByDescending(b => b.Id).Skip(9 * (page - 1)).Take(9).ToListAsync();
                    var count = await _context.Books.Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true && b.IsRent == true && b.Language == filter4).CountAsync();
                    return new ApiResponse
                    {
                        Message = Message.GET_SUCCESS.ToString(),
                        Data = books,
                        NumberOfRecords = count
                    };
                }
            }
            //24
            if (filter1 != "All" && filter2 == 0 && filter3 != "All" && filter4 == "All")
            {
                var price = filter3.Split("-");
                if (filter1 == "Exchange")
                {
                    var books = await _context.Books.Include(b => b.User).Include(b => b.Category).Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true && b.IsExchange == true && b.DepositPrice > float.Parse(price[0]) && b.DepositPrice <= float.Parse(price[1])).OrderByDescending(b => b.Id).Skip(9 * (page - 1)).Take(9).ToListAsync();
                    var count = await _context.Books.Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true && b.IsExchange == true && b.DepositPrice > float.Parse(price[0]) && b.DepositPrice <= float.Parse(price[1])).CountAsync();
                    return new ApiResponse
                    {
                        Message = Message.GET_SUCCESS.ToString(),
                        Data = books,
                        NumberOfRecords = count
                    };
                }
                if (filter1 == "Rent")
                {
                    var books = await _context.Books.Include(b => b.User).Include(b => b.Category).Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true && b.IsRent == true && b.DepositPrice > float.Parse(price[0]) && b.DepositPrice <= float.Parse(price[1])).OrderByDescending(b => b.Id).Skip(9 * (page - 1)).Take(9).ToListAsync();
                    var count = await _context.Books.Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true && b.IsRent == true && b.DepositPrice > float.Parse(price[0]) && b.DepositPrice <= float.Parse(price[1])).CountAsync();
                    return new ApiResponse
                    {
                        Message = Message.GET_SUCCESS.ToString(),
                        Data = books,
                        NumberOfRecords = count
                    };
                }
            }
            //34
            if (filter1 != "All" && filter2 != 0 && filter3 == "All" && filter4 == "All")
            {
                if (filter1 == "Exchange")
                {
                    var books = await _context.Books.Include(b => b.User).Include(b => b.Category).Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true && b.IsExchange == true && b.CategoryId == filter2).OrderByDescending(b => b.Id).Skip(9 * (page - 1)).Take(9).ToListAsync();
                    var count = await _context.Books.Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true && b.IsExchange == true && b.CategoryId == filter2).CountAsync();
                    return new ApiResponse
                    {
                        Message = Message.GET_SUCCESS.ToString(),
                        Data = books,
                        NumberOfRecords = count
                    };
                }
                if (filter1 == "Rent")
                {
                    var books = await _context.Books.Include(b => b.User).Include(b => b.Category).Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true && b.IsRent == true && b.CategoryId == filter2).OrderByDescending(b => b.Id).Skip(9 * (page - 1)).Take(9).ToListAsync();
                    var count = await _context.Books.Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true && b.IsRent == true && b.CategoryId == filter2).CountAsync();
                    return new ApiResponse
                    {
                        Message = Message.GET_SUCCESS.ToString(),
                        Data = books,
                        NumberOfRecords = count
                    };
                }
            }
            //1
            if (filter1 == "All" && filter2 != 0 && filter3 != "All" && filter4 != "All")
            {
                var price = filter3.Split("-");
                var books = await _context.Books.Include(b => b.User).Include(b => b.Category)
                    .Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true && b.CategoryId == filter2 && b.Language == filter4 && b.DepositPrice > float.Parse(price[0]) && b.DepositPrice <= float.Parse(price[1]))
                    .OrderByDescending(b => b.Id).Skip(9 * (page - 1)).Take(9).ToListAsync();
                var count = await _context.Books
                    .Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true && b.CategoryId == filter2 && b.Language == filter4 && b.DepositPrice > float.Parse(price[0]) && b.DepositPrice <= float.Parse(price[1])).CountAsync();
                return new ApiResponse
                {
                    Message = Message.GET_SUCCESS.ToString(),
                    Data = books,
                    NumberOfRecords = count
                };
            }
            //2
            if (filter1 != "All" && filter2 == 0 && filter3 != "All" && filter4 != "All")
            {
                var price = filter3.Split("-");
                if (filter1 == "Exchange")
                {
                    var books = await _context.Books.Include(b => b.User).Include(b => b.Category).Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true && b.IsExchange == true && b.Language == filter4 && b.DepositPrice > float.Parse(price[0]) && b.DepositPrice <= float.Parse(price[1])).OrderByDescending(b => b.Id).Skip(9 * (page - 1)).Take(9).ToListAsync();
                    var count = await _context.Books.Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true && b.IsExchange == true && b.Language == filter4 && b.DepositPrice > float.Parse(price[0]) && b.DepositPrice <= float.Parse(price[1])).CountAsync();
                    return new ApiResponse
                    {
                        Message = Message.GET_SUCCESS.ToString(),
                        Data = books,
                        NumberOfRecords = count
                    };
                }
                if (filter1 == "Rent")
                {
                    var books = await _context.Books.Include(b => b.User).Include(b => b.Category).Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true && b.IsRent == true && b.Language == filter4 && b.DepositPrice > float.Parse(price[0]) && b.DepositPrice <= float.Parse(price[1])).OrderByDescending(b => b.Id).Skip(9 * (page - 1)).Take(9).ToListAsync();
                    var count = await _context.Books.Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true && b.IsRent == true && b.Language == filter4 && b.DepositPrice > float.Parse(price[0]) && b.DepositPrice <= float.Parse(price[1])).CountAsync();
                    return new ApiResponse
                    {
                        Message = Message.GET_SUCCESS.ToString(),
                        Data = books,
                        NumberOfRecords = count
                    };
                }
            }
            //3
            if (filter1 != "All" && filter2 != 0 && filter3 == "All" && filter4 != "All")
            {
                if (filter1 == "Exchange")
                {
                    var books = await _context.Books.Include(b => b.User).Include(b => b.Category).Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true && b.IsExchange == true && b.Language == filter4 && b.CategoryId == filter2).OrderByDescending(b => b.Id).Skip(9 * (page - 1)).Take(9).ToListAsync();
                    var count = await _context.Books.Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true && b.IsExchange == true && b.Language == filter4 && b.CategoryId == filter2).CountAsync();
                    return new ApiResponse
                    {
                        Message = Message.GET_SUCCESS.ToString(),
                        Data = books,
                        NumberOfRecords = count
                    };
                }
                if (filter1 == "Rent")
                {
                    var books = await _context.Books.Include(b => b.User).Include(b => b.Category).Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true && b.IsRent == true && b.Language == filter4 && b.CategoryId == filter2).OrderByDescending(b => b.Id).Skip(9 * (page - 1)).Take(9).ToListAsync();
                    var count = await _context.Books.Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true && b.IsRent == true && b.Language == filter4 && b.CategoryId == filter2).CountAsync();
                    return new ApiResponse
                    {
                        Message = Message.GET_SUCCESS.ToString(),
                        Data = books,
                        NumberOfRecords = count
                    };
                }
            }
            //4
            if (filter1 != "All" && filter2 != 0 && filter3 != "All" && filter4 == "All")
            {
                var price = filter3.Split("-");
                if (filter1 == "Exchange")
                {
                    var books = await _context.Books.Include(b => b.User).Include(b => b.Category).Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true && b.IsExchange == true && b.CategoryId == filter2 && b.DepositPrice > float.Parse(price[0]) && b.DepositPrice <= float.Parse(price[1])).OrderByDescending(b => b.Id).Skip(9 * (page - 1)).Take(9).ToListAsync();
                    var count = await _context.Books.Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true && b.IsExchange == true && b.CategoryId == filter2 && b.DepositPrice > float.Parse(price[0]) && b.DepositPrice <= float.Parse(price[1])).CountAsync();
                    return new ApiResponse
                    {
                        Message = Message.GET_SUCCESS.ToString(),
                        Data = books,
                        NumberOfRecords = count
                    };
                }
                if (filter1 == "Rent")
                {
                    var books = await _context.Books.Include(b => b.User).Include(b => b.Category).Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true && b.IsRent == true && b.CategoryId == filter2 && b.DepositPrice > float.Parse(price[0]) && b.DepositPrice <= float.Parse(price[1])).OrderByDescending(b => b.Id).Skip(9 * (page - 1)).Take(9).ToListAsync();
                    var count = await _context.Books.Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true && b.IsRent == true && b.CategoryId == filter2 && b.DepositPrice > float.Parse(price[0]) && b.DepositPrice <= float.Parse(price[1])).CountAsync();
                    return new ApiResponse
                    {
                        Message = Message.GET_SUCCESS.ToString(),
                        Data = books,
                        NumberOfRecords = count
                    };
                }
            }
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = null
            };
        }

        public async Task<ApiResponse> getAllBookIsExchangeAsync(int page = 1)
        {
            var books = await _context.Books.Include(b => b.User).Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true && b.IsExchange == true).OrderByDescending(b => b.Id).Skip(9 * (page - 1)).Take(9).ToListAsync();
            var count = await _context.Books.Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true && b.IsExchange == true).CountAsync();
            //var books = PaginatedList<Book>.Create(books, page, 9);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = books,
                NumberOfRecords = count
            };
        }

        public async Task<ApiResponse> getAllBookIsRentAsync(int page = 1)
        {
            var books = await _context.Books.Include(b => b.User).Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true && b.IsRent == true).OrderByDescending(b => b.Id).Skip(9 * (page - 1)).Take(9).ToListAsync();
            var count = await _context.Books.Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true && b.IsRent == true).CountAsync();
            //var books = PaginatedList<Book>.Create(books, page, 9);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = books,
                NumberOfRecords = count
            };
        }

        public async Task<ApiResponse> getBookByIdAsync(int bookId)
        {
            var book = await _context.Books.Include(b => b.User).Include(b => b.Category).SingleOrDefaultAsync(b => b.Id == bookId && b.Status == StatusRequest.Approved.ToString() && b.IsReady == true);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = book
            };
        }
        public async Task<ApiResponse> getFeedbackInBookAsync(int bookId, int page = 1)
        {
            var feedbacks = await _context.Feedbacks.Include(p => p.User).Where(p => p.BookId == bookId).OrderByDescending(p => p.Id).Skip(5*(page-1)).Take(5).ToListAsync();
            var count = await _context.Feedbacks.Where(p => p.BookId == bookId).CountAsync();
            //var result = PaginatedList<Feedback>.Create(feedbacks, page, 5);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = feedbacks,
                NumberOfRecords = count
            };
        }
        public async Task<ApiResponse> getBookByCategoryAsync(int categoryId, int page = 1)
        {
            var books = await _context.Books.Include(b => b.User).Where(b => b.CategoryId == categoryId && b.IsReady == true && b.Status == StatusRequest.Approved.ToString()).OrderByDescending(b => b.Id).Skip(9 *(page-1)).Take(9).ToListAsync();
            var count = await _context.Books.Where(b => b.CategoryId == categoryId && b.IsReady == true && b.Status == StatusRequest.Approved.ToString()).CountAsync();
            //var result = PaginatedList<Book>.Create(books, page, 9);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = books,
                NumberOfRecords = count
            };
        }

        public async Task<ApiResponse> get6BookByCategoryAsync(int categoryId)
        {
            var books = await _context.Books.Include(b => b.User).Where(b => b.CategoryId == categoryId && b.IsReady == true && b.Status == StatusRequest.Approved.ToString()).OrderByDescending(b => b.Id).Take(6).ToListAsync();
            //var result = PaginatedList<Book>.Create(books, page, 9);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = books,
                NumberOfRecords = books.Count
            };
        }

        public async Task<ApiResponse> getBookByUserAsync(int userId, int page = 1)
        {
            var books = await _context.Books.Include(b => b.Category).Where(b => b.UserId == userId && b.Status == StatusRequest.Approved.ToString() && b.IsReady == true).OrderByDescending(b => b.Id).Skip(10*(page-1)).Take(10).ToListAsync();
            var count = await _context.Books.Where(b => b.UserId == userId && b.Status == StatusRequest.Approved.ToString() && b.IsReady == true).CountAsync();
            //var result = PaginatedList<Book>.Create(books, page, 6);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = books,
                NumberOfRecords = count
            };
        }

        public async Task<ApiResponse> get6BookByUserAsync(int userId)
        {
            var books = await _context.Books.Include(b => b.Category).Where(b => b.UserId == userId && b.Status == StatusRequest.Approved.ToString() && b.IsReady == true).OrderByDescending(b => b.Id).Take(6).ToListAsync();
            //var result = PaginatedList<Book>.Create(books, page, 6);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = books,
                NumberOfRecords = books.Count
            };
        }

        public async Task<ApiResponse> searchBookOfUserAsync(int userId, string search, int page = 1)
        {
            List<Book> books = new List<Book>();
            if (search != null)
            {

                search = search.ToLower().Trim();
                
                var listStr = search.Split(' ');

                foreach(var s in listStr)
                {
                    var bookResult = await _context.Books.Where(b => b.Title.ToLower().Contains(s) && b.IsReady == true && b.Status == StatusRequest.Approved.ToString() && b.UserId == userId).OrderByDescending(b => b.Id).ToListAsync();
                    books.AddRange(bookResult);
                }
            }
            else
            {
                books = await _context.Books.Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true && b.UserId == userId).OrderByDescending(b => b.Id).ToListAsync();
            }
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = books.Skip(10 * (page - 1)).Take(10),
                NumberOfRecords = books.Count
            };
        }

        public async Task<ApiResponse> searchBookByTitleAsync(string search, int page = 1)
        {
            List<Book> books = new List<Book>();
            if (search != null)
            {
                search = search.ToLower().Trim();

                search = search.RemoveAccents();

                var listStr = search.Split(' ');

                foreach (var s in listStr)
                {

                    var bookResult = _context.Books.Include(b => b.User).Include(b => b.Category).AsEnumerable().Where(b => b.Title.ToLower().RemoveAccents().Contains(s) & b.IsReady == true && b.Status == StatusRequest.Approved.ToString()).ToList();

                    /*var bookResult = await _context.Books.Include(b => b.User).Include(b => b.Category).AsAsyncEnumerable().Where(b => b.Title.ToLower().RemoveAccents().Contains(s) & b.IsReady == true && b.Status == StatusRequest.Approved.ToString()).ToListAsync();*/

                    foreach(var book in bookResult)
                    {
                        var check = books.SingleOrDefault(b => b.Id == book.Id);
                        if (check == null)
                        {
                            books.Add(book);
                        }
                    }
                }
                return new ApiResponse
                {
                    Message = Message.GET_SUCCESS.ToString(),
                    Data = books.OrderBy(b => b.Title).Skip(9 * (page - 1)).Take(9),
                    NumberOfRecords = books.Count
                };
            }
            else
            {
                books = await _context.Books.Include(b => b.User).Include(b => b.Category).Where(b => b.Status == StatusRequest.Approved.ToString() && b.IsReady == true).OrderByDescending(b => b.Id).ToListAsync();
                return new ApiResponse
                {
                    Message = Message.GET_SUCCESS.ToString(),
                    Data = books.Skip(9 * (page - 1)).Take(9),
                    NumberOfRecords = books.Count
                };
            }   
        }

        public async Task<ApiResponse> createBookAsync(int userId, BookVM bookVM)
        {
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
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
                Image = bookVM.Image,
                PostedDate = DateOnly.FromDateTime(TimeZoneInfo.ConvertTime(DateTime.Today, timeZoneInfo)),
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
        public async Task<ApiMessage> feedbackBookAsync(int userId, int bookId, FeedbackVM feedbackVM)
        {
            var book = await _context.Books.SingleOrDefaultAsync(b => b.Id == bookId && b.Status == StatusRequest.Approved.ToString() && b.IsReady == true);
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            if (book != null)
            {
                var feedback = new Feedback
                {
                    BookId = bookId,
                    UserId = userId,
                    Content = feedbackVM.Content,
                    CreatedDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo),
                };
                _context.Feedbacks.Add(feedback);
                await _context.SaveChangesAsync();
                return new ApiMessage { Message = Message.SUCCESS.ToString() };
            }
            return new ApiMessage { Message = Message.BOOK_NOT_EXIST.ToString() };
        }

        public async Task<ApiMessage> updateBookAsync(int bookId, BookVM bookVM)
        {
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            var book = await _context.Books.SingleOrDefaultAsync(b => b.Id == bookId);
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
				book.Image = bookVM.Image;
                book.IsExchange = bookVM.IsExchange;
                book.IsRent = bookVM.IsRent;
                book.RentFee = bookVM.RentFee;
                book.IsReady = true;
                book.IsTrade = false;
                book.PostedDate = DateOnly.FromDateTime(TimeZoneInfo.ConvertTime(DateTime.Today, timeZoneInfo));
                book.Status = StatusRequest.Waiting.ToString();
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
