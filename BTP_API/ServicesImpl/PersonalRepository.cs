using BTP_API.Models;
using BTP_API.ViewModels;

namespace BTP_API.ServicesImpl
{
    public class PersonalRepository : IPersonalRepository
    {
        private readonly BTPContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PersonalRepository(BTPContext context, IWebHostEnvironment environment, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _environment = environment;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ApiResponse> getBookCanTradeAsync()
        {
            Cookie cookie = new Cookie(_httpContextAccessor);
            int userId = cookie.GetUserId();
            if (userId == 0)
            {
                return new ApiResponse
                {
                    Message = Message.NOT_YET_LOGIN.ToString()
                };
            }
            var books = await _context.Books.Where(b => b.UserId == userId && b.IsTrade == false && b.IsReady == true).ToListAsync();
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
        public async Task<ApiResponse> getAllBookAsync()
        {
            Cookie cookie = new Cookie(_httpContextAccessor);
            int userId = cookie.GetUserId();
            if (userId == 0)
            {
                return new ApiResponse
                {
                    Message = Message.NOT_YET_LOGIN.ToString()
                };
            }
            var books = await _context.Books.Where(b => b.UserId == userId).ToListAsync();
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
        public async Task<ApiResponse> getBookApprovedAsync()
        {
            Cookie cookie = new Cookie(_httpContextAccessor);
            int userId = cookie.GetUserId();
            if (userId == 0)
            {
                return new ApiResponse
                {
                    Message = Message.NOT_YET_LOGIN.ToString()
                };
            }
            var books = await _context.Books.Where(b => b.UserId == userId && b.Status == StatusRequest.Approved.ToString()).ToListAsync();
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
        public async Task<ApiResponse> getBookDeniedAsync()
        {
            Cookie cookie = new Cookie(_httpContextAccessor);
            int userId = cookie.GetUserId();
            if (userId == 0)
            {
                return new ApiResponse
                {
                    Message = Message.NOT_YET_LOGIN.ToString()
                };
            }
            var books = await _context.Books.Where(b => b.UserId == userId && b.Status == StatusRequest.Denied.ToString()).ToListAsync();
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
        public async Task<ApiResponse> getBookWaitingAsync()
        {
            Cookie cookie = new Cookie(_httpContextAccessor);
            int userId = cookie.GetUserId();
            if (userId == 0)
            {
                return new ApiResponse
                {
                    Message = Message.NOT_YET_LOGIN.ToString()
                };
            }
            var books = await _context.Books.Where(b => b.UserId == userId && b.Status == StatusRequest.Waiting.ToString()).ToListAsync();
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
        public async Task<ApiResponse> getAllPostAsync()
        {
            Cookie cookie = new Cookie(_httpContextAccessor);
            int userId = cookie.GetUserId();
            if (userId == 0)
            {
                return new ApiResponse
                {
                    Message = Message.NOT_YET_LOGIN.ToString()
                };
            }
            var posts = await _context.Posts.Where(b => b.UserId == userId).ToListAsync();
            if (posts.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = posts,
                NumberOfRecords = posts.Count
            };
        }
        public async Task<ApiResponse> getPostApprovedAsync()
        {
            Cookie cookie = new Cookie(_httpContextAccessor);
            int userId = cookie.GetUserId();
            if (userId == 0)
            {
                return new ApiResponse
                {
                    Message = Message.NOT_YET_LOGIN.ToString()
                };
            }
            var posts = await _context.Posts.Where(b => b.UserId == userId && b.Status == StatusRequest.Approved.ToString()).ToListAsync();
            if (posts.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = posts,
                NumberOfRecords = posts.Count
            };
        }
        public async Task<ApiResponse> getPostDeniedAsync()
        {
            Cookie cookie = new Cookie(_httpContextAccessor);
            int userId = cookie.GetUserId();
            if (userId == 0)
            {
                return new ApiResponse
                {
                    Message = Message.NOT_YET_LOGIN.ToString()
                };
            }
            var posts = await _context.Posts.Where(b => b.UserId == userId && b.Status == StatusRequest.Denied.ToString()).ToListAsync();
            if (posts.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = posts,
                NumberOfRecords = posts.Count
            };
        }
        public async Task<ApiResponse> getPostWaitingAsync()
        {
            Cookie cookie = new Cookie(_httpContextAccessor);
            int userId = cookie.GetUserId();
            if (userId == 0)
            {
                return new ApiResponse
                {
                    Message = Message.NOT_YET_LOGIN.ToString()
                };
            }
            var posts = await _context.Posts.Where(b => b.UserId == userId && b.Status == StatusRequest.Waiting.ToString()).ToListAsync();
            if (posts.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = posts,
                NumberOfRecords = posts.Count
            };
        }
        public async Task<ApiResponse> getBookByFavoritesAsync()
        {
            Cookie cookie = new Cookie(_httpContextAccessor);
            int userId = cookie.GetUserId();
            if (userId == 0)
            {
                return new ApiResponse
                {
                    Message = Message.NOT_YET_LOGIN.ToString()
                };
            }
            var favoriteBooks = await _context.FavoriteBookLists.Include(f => f.Book).Where(f => f.UserId == userId).ToListAsync();
            if (favoriteBooks.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = favoriteBooks,
                NumberOfRecords = favoriteBooks.Count
            };
        }
        public async Task<ApiMessage> addBookByFavoritesAsync(int bookId)
        {
            Cookie cookie = new Cookie(_httpContextAccessor);
            int userId = cookie.GetUserId();
            if (userId == 0)
            {
                return new ApiMessage
                {
                    Message = Message.NOT_YET_LOGIN.ToString()
                };
            }

            var book = await _context.Books.AnyAsync(f => f.Id == bookId);
            if (!book)
            {
                return new ApiMessage
                {
                    Message = Message.BOOK_NOT_EXIST.ToString()
                };
            }

            var check = await _context.FavoriteBookLists.SingleOrDefaultAsync(f => f.BookId == bookId && f.UserId == userId);
            if (check != null)
            {
                return new ApiMessage
                {
                    Message = Message.EXIST.ToString()
                };
            }
            var favoriteBook = new FavoriteBookList
            {
                BookId = bookId,
                UserId = userId
            };

            _context.Add(favoriteBook);
            await _context.SaveChangesAsync();
            return new ApiMessage
            {
                Message = Message.ADD_SUCCESS.ToString()
            };
        }
        public async Task<ApiMessage> deleteBookByFavoritesAsync(int bookId)
        {
            Cookie cookie = new Cookie(_httpContextAccessor);
            int userId = cookie.GetUserId();
            if (userId == 0)
            {
                return new ApiMessage
                {
                    Message = Message.NOT_YET_LOGIN.ToString()
                };
            }

            var book = await _context.Books.AnyAsync(f => f.Id == bookId);
            if (!book)
            {
                return new ApiMessage
                {
                    Message = Message.BOOK_NOT_EXIST.ToString()
                };
            }

            var check = await _context.FavoriteBookLists.SingleOrDefaultAsync(f => f.BookId == bookId && f.UserId == userId);
            if (check != null)
            {
                _context.Remove(check);
                await _context.SaveChangesAsync();
                return new ApiMessage
                {
                    Message = Message.DELETE_SUCCESS.ToString()
                };
            }
            return new ApiMessage
            {
                Message = Message.NOT_EXIST.ToString()
            };
        }
        public async Task<ApiResponse> getPostByFavoritesAsync()
        {
            Cookie cookie = new Cookie(_httpContextAccessor);
            int userId = cookie.GetUserId();
            if (userId == 0)
            {
                return new ApiResponse
                {
                    Message = Message.NOT_YET_LOGIN.ToString()
                };
            }
            var favoritePosts = await _context.FavoritePostLists.Include(f => f.Post).Where(f => f.UserId == userId).ToListAsync();
            if (favoritePosts.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = favoritePosts,
                NumberOfRecords = favoritePosts.Count
            };
        }
        public async Task<ApiMessage> addPostByFavoritesAsync(int postId)
        {
            Cookie cookie = new Cookie(_httpContextAccessor);
            int userId = cookie.GetUserId();
            if (userId == 0)
            {
                return new ApiMessage
                {
                    Message = Message.NOT_YET_LOGIN.ToString()
                };
            }

            var post = await _context.Posts.AnyAsync(f => f.Id == postId);
            if (!post)
            {
                return new ApiMessage
                {
                    Message = Message.POST_NOT_EXIST.ToString()
                };
            }

            var check = await _context.FavoritePostLists.SingleOrDefaultAsync(f => f.PostId == postId && f.UserId == userId);
            if (check != null)
            {
                return new ApiMessage
                {
                    Message = Message.EXIST.ToString()
                };
            }
            var favoritePost = new FavoritePostList
            {
                PostId = postId,
                UserId = userId
            };

            _context.Add(favoritePost);
            await _context.SaveChangesAsync();
            return new ApiMessage
            {
                Message = Message.ADD_SUCCESS.ToString()
            };
        }
        public async Task<ApiMessage> deletePostByFavoritesAsync(int postId)
        {
            Cookie cookie = new Cookie(_httpContextAccessor);
            int userId = cookie.GetUserId();
            if (userId == 0)
            {
                return new ApiMessage
                {
                    Message = Message.NOT_YET_LOGIN.ToString()
                };
            }

            var post = await _context.Posts.AnyAsync(f => f.Id == postId);
            if (!post)
            {
                return new ApiMessage
                {
                    Message = Message.POST_NOT_EXIST.ToString()
                };
            }

            var check = await _context.FavoritePostLists.SingleOrDefaultAsync(f => f.PostId == postId && f.UserId == userId);
            if (check != null)
            {
                _context.Remove(check);
                await _context.SaveChangesAsync();
                return new ApiMessage
                {
                    Message = Message.DELETE_SUCCESS.ToString()
                };
            }
            return new ApiMessage
            {
                Message = Message.NOT_EXIST.ToString()
            };
        }
        public async Task<ApiResponse> getUserByFavoritesAsync()
        {
            Cookie cookie = new Cookie(_httpContextAccessor);
            int userId = cookie.GetUserId();
            if (userId == 0)
            {
                return new ApiResponse
                {
                    Message = Message.NOT_YET_LOGIN.ToString()
                };
            }
            var favoriteUsers = await _context.FavoriteUserLists.Include(f => f.FavoriteUser).Where(f => f.UserId == userId).ToListAsync();
            if (favoriteUsers.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = favoriteUsers,
                NumberOfRecords = favoriteUsers.Count
            };
        }
        public async Task<ApiMessage> addUserByFavoritesAsync(int favoriteUserId)
        {
            Cookie cookie = new Cookie(_httpContextAccessor);
            int userId = cookie.GetUserId();
            if (userId == 0)
            {
                return new ApiMessage
                {
                    Message = Message.NOT_YET_LOGIN.ToString()
                };
            }

            var user = await _context.Users.AnyAsync(f => f.Id == userId);
            if (!user)
            {
                return new ApiMessage
                {
                    Message = Message.USER_NOT_EXIST.ToString()
                };
            }

            var check = await _context.FavoriteUserLists.SingleOrDefaultAsync(f => f.FavoriteUserId == favoriteUserId && f.UserId == userId);
            if (check != null)
            {
                return new ApiMessage
                {
                    Message = Message.EXIST.ToString()
                };
            }
            var favoriteUser = new FavoriteUserList
            {
                FavoriteUserId = favoriteUserId,
                UserId = userId
            };

            _context.Add(favoriteUser);
            await _context.SaveChangesAsync();
            return new ApiMessage
            {
                Message = Message.ADD_SUCCESS.ToString()
            };
        }
        public async Task<ApiMessage> deleteUserByFavoritesAsync(int favoriteUserId)
        {
            Cookie cookie = new Cookie(_httpContextAccessor);
            int userId = cookie.GetUserId();
            if (userId == 0)
            {
                return new ApiMessage
                {
                    Message = Message.NOT_YET_LOGIN.ToString()
                };
            }

            var user = await _context.Users.AnyAsync(f => f.Id == userId);
            if (!user)
            {
                return new ApiMessage
                {
                    Message = Message.USER_NOT_EXIST.ToString()
                };
            }

            var check = await _context.FavoriteUserLists.SingleOrDefaultAsync(f => f.FavoriteUserId == favoriteUserId && f.UserId == userId);
            if (check != null)
            {
                _context.Remove(check);
                await _context.SaveChangesAsync();
                return new ApiMessage
                {
                    Message = Message.DELETE_SUCCESS.ToString()
                };
            }
            return new ApiMessage
            {
                Message = Message.NOT_EXIST.ToString()
            };
        }
        public async Task<ApiResponse> getInfoUserIdAsync()
        {
            Cookie cookie = new Cookie(_httpContextAccessor);
            int userId = cookie.GetUserId();
            if (userId == 0)
            {
                return new ApiResponse
                {
                    Message = Message.NOT_YET_LOGIN.ToString()
                };
            }
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return new ApiResponse
                {
                    Message = Message.USER_NOT_EXIST.ToString()
                };
            }
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = user,
                NumberOfRecords = 1
            };
        }
        public async Task<ApiMessage> editInfoAsync(UserVM userVM)
        {
            UploadFile uploadFile = new UploadFile();
            Cookie cookie = new Cookie(_httpContextAccessor);
            int userId = cookie.GetUserId();
            if (userId == 0)
            {
                return new ApiMessage
                {
                    Message = Message.NOT_YET_LOGIN.ToString()
                };
            }
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return new ApiMessage
                {
                    Message = Message.USER_NOT_EXIST.ToString()
                };
            }
            user.Fullname = userVM.Fullname;
            user.Age = userVM.Age;
            user.AddressMain = userVM.AddressMain;
            user.AddressSub1 = userVM.AddressSub1;
            user.AddressSub2 = userVM.AddressSub2;
            user.Avatar = uploadFile.UploadUserImage(userVM.Avatar, _environment);
            await _context.SaveChangesAsync();
            return new ApiMessage
            {
                Message = Message.UPDATE_SUCCESS.ToString()
            };
        }
        public async Task<ApiMessage> editPasswordAsync(ChangePasswordVM changePasswordVM)
        {
            Cookie cookie = new Cookie(_httpContextAccessor);
            int userId = cookie.GetUserId();
            if (userId == 0)
            {
                return new ApiMessage
                {
                    Message = Message.NOT_YET_LOGIN.ToString()
                };
            }
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return new ApiMessage
                {
                    Message = Message.USER_NOT_EXIST.ToString()
                };
            }
            bool isValid = BCrypt.Net.BCrypt.Verify(changePasswordVM.OldPassword, user.Password);
            if (!isValid)
            {
                return new ApiMessage
                {
                    Message = Message.OLD_PASSWORD_INCORRECT.ToString()
                };
            }
            int costParameter = 12;
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(changePasswordVM.NewPassword, costParameter);

            user.Password = hashedPassword;
            await _context.SaveChangesAsync();
            return new ApiMessage
            {
                Message = Message.UPDATE_SUCCESS.ToString()
            };
        }
        public async Task<ApiResponse> listOfRequestSendAsync()
        {
            Cookie cookie = new Cookie(_httpContextAccessor);
            int userId = cookie.GetUserId();
            if (userId == 0)
            {
                return new ApiResponse
                {
                    Message = Message.NOT_YET_LOGIN.ToString()
                };
            }
            var myBooks = await _context.Books.Where(b => b.UserId == userId && b.Status == StatusRequest.Approved.ToString()).ToListAsync();

            List<ExchangeRequest> exchangeRequests = new List<ExchangeRequest>();

            foreach (var book in myBooks)
            {
                var data = await _context.ExchangeRequests.Where(r => r.BookOfferId == book.Id).ToListAsync();
                foreach (var item in data)
                {
                    exchangeRequests.Add(item);
                }
            }
            if (exchangeRequests.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = exchangeRequests,
                NumberOfRecords = exchangeRequests.Count
            };
        }
        public async Task<ApiResponse> listOfRequestReceivedSendAsync(int bookId)
        {
            Cookie cookie = new Cookie(_httpContextAccessor);
            int userId = cookie.GetUserId();
            if (userId == 0)
            {
                return new ApiResponse
                {
                    Message = Message.NOT_YET_LOGIN.ToString()
                };
            }

            var check = await _context.Books.Where(b => b.Id == bookId && b.UserId == userId).ToListAsync();
            if (check.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.BOOK_NOT_EXIST.ToString()
                };
            }

            var data = await _context.ExchangeRequests.Include(r => r.BookOffer.User).Where(r => r.BookId == bookId).ToListAsync();
            if (data.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = data,
                NumberOfRecords = data.Count
            };
        }
        public async Task<ApiResponse> myTransactionExchangeAsync()
        {
            Cookie cookie = new Cookie(_httpContextAccessor);
            int userId = cookie.GetUserId();
            if (userId == 0)
            {
                return new ApiResponse
                {
                    Message = Message.NOT_YET_LOGIN.ToString()
                };
            }
            var exchanges = await _context.Exchanges.Where(b => b.UserId1 == userId || b.UserId2 == userId).ToListAsync();
            if (exchanges.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = exchanges,
                NumberOfRecords = exchanges.Count
            };
        }
        public async Task<ApiResponse> myTransactionExDetailAsync(int exchangeId)
        {
            Cookie cookie = new Cookie(_httpContextAccessor);
            int userId = cookie.GetUserId();
            if (userId == 0)
            {
                return new ApiResponse
                {
                    Message = Message.NOT_YET_LOGIN.ToString()
                };
            }

            var exchange = await _context.Exchanges.AnyAsync(e => e.Id == exchangeId);
            if (!exchange)
            {
                return new ApiResponse
                {
                    Message = Message.EXCHANGE_NOT_EXIST.ToString()
                };
            }

            var exchangeDetails = await _context.ExchangeDetails.Where(b => b.ExchangeId == exchangeId).ToListAsync();
            if (exchangeDetails.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = exchangeDetails,
                NumberOfRecords = exchangeDetails.Count
            };
        }
        public async Task<ApiResponse> myTransactionExBillAsync(int exchangeId)
        {
            Cookie cookie = new Cookie(_httpContextAccessor);
            int userId = cookie.GetUserId();
            if (userId == 0)
            {
                return new ApiResponse
                {
                    Message = Message.NOT_YET_LOGIN.ToString()
                };
            }

            var exchange = await _context.Exchanges.AnyAsync(e => e.Id == exchangeId);
            if (!exchange)
            {
                return new ApiResponse
                {
                    Message = Message.EXCHANGE_NOT_EXIST.ToString()
                };
            }

            var exchangeBill = await _context.ExchangeBills.SingleOrDefaultAsync(b => b.ExchangeId == exchangeId && b.UserId == userId);
            if (exchangeBill == null)
            {
                return new ApiResponse
                {
                    Message = Message.BILL_NOT_EXIST.ToString()
                };
            }
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = exchangeBill,
                NumberOfRecords = 1
            };
        }
        public async Task<ApiResponse> myExBillAllAsync()
        {
            Cookie cookie = new Cookie(_httpContextAccessor);
            int userId = cookie.GetUserId();
            if (userId == 0)
            {
                return new ApiResponse
                {
                    Message = Message.NOT_YET_LOGIN.ToString()
                };
            }
            var exchangeBills = await _context.ExchangeBills.Where(b => b.UserId == userId).ToListAsync();
            if (exchangeBills.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = exchangeBills,
                NumberOfRecords = exchangeBills.Count
            };
        }
        public async Task<ApiResponse> myTransactionRentAsync()
        {
            Cookie cookie = new Cookie(_httpContextAccessor);
            int userId = cookie.GetUserId();
            if (userId == 0)
            {
                return new ApiResponse
                {
                    Message = Message.NOT_YET_LOGIN.ToString()
                };
            }
            var rents = await _context.Rents.Where(b => b.OwnerId == userId || b.RenterId == userId).ToListAsync();
            if (rents.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = rents,
                NumberOfRecords = rents.Count
            };
        }
        public async Task<ApiResponse> myTransactionRentDetailAsync(int rentId)
        {
            Cookie cookie = new Cookie(_httpContextAccessor);
            int userId = cookie.GetUserId();
            if (userId == 0)
            {
                return new ApiResponse
                {
                    Message = Message.NOT_YET_LOGIN.ToString()
                };
            }

            var rent = await _context.Rents.AnyAsync(e => e.Id == rentId);
            if (!rent)
            {
                return new ApiResponse
                {
                    Message = Message.RENT_NOT_EXIST.ToString()
                };
            }

            var rentDetails = await _context.RentDetails.Where(b => b.RentId == rentId).ToListAsync();
            if (rentDetails.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = rentDetails,
                NumberOfRecords = rentDetails.Count
            };
        }
        public async Task<ApiResponse> myTransactionRentBillAsync(int rentId)
        {
            Cookie cookie = new Cookie(_httpContextAccessor);
            int userId = cookie.GetUserId();
            if (userId == 0)
            {
                return new ApiResponse
                {
                    Message = Message.NOT_YET_LOGIN.ToString()
                };
            }

            var rent = await _context.Rents.AnyAsync(e => e.Id == rentId);
            if (!rent)
            {
                return new ApiResponse
                {
                    Message = Message.RENT_NOT_EXIST.ToString()
                };
            }

            var rentBill = await _context.RentBills.SingleOrDefaultAsync(b => b.RentId == rentId && b.UserId == userId);
            if (rentBill == null)
            {
                return new ApiResponse
                {
                    Message = Message.BILL_NOT_EXIST.ToString()
                };
            }
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = rentBill,
                NumberOfRecords = 1
            };
        }
        public async Task<ApiResponse> myRentBillAllAsync()
        {
            Cookie cookie = new Cookie(_httpContextAccessor);
            int userId = cookie.GetUserId();
            if (userId == 0)
            {
                return new ApiResponse
                {
                    Message = Message.NOT_YET_LOGIN.ToString()
                };
            }
            var rentBills = await _context.RentBills.Where(b => b.UserId == userId).ToListAsync();
            if (rentBills.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = rentBills,
                NumberOfRecords = rentBills.Count
            };
        }
        public async Task<ApiMessage> updateInfoShippingAsync(ShipInfoVM shipInfoVM)
        {
            Cookie cookie = new Cookie(_httpContextAccessor);
            int userId = cookie.GetUserId();
            if (userId == 0)
            {
                return new ApiMessage
                {
                    Message = Message.NOT_YET_LOGIN.ToString()
                };
            }
            var shipInfo = await _context.ShipInfos.SingleOrDefaultAsync(u => u.UserId == userId);
            if (shipInfo == null)
            {
                return new ApiMessage
                {
                    Message = Message.USER_NOT_EXIST.ToString()
                };
            }

            shipInfo.SendIsMonday = shipInfoVM.SendIsMonday;
            shipInfo.SendIsWednesday = shipInfoVM.SendIsWednesday;
            shipInfo.SendIsFriday = shipInfoVM.SendIsFriday;
            shipInfo.ReceiveIsMonday = shipInfoVM.ReceiveIsMonday;
            shipInfo.ReceiveIsWednesday = shipInfoVM.ReceiveIsWednesday;
            shipInfo.ReceiveIsFriday = shipInfoVM.ReceiveIsFriday;
            shipInfo.RecallIsMonday = shipInfoVM.RecallIsMonday;
            shipInfo.RecallIsWednesday = shipInfoVM.RecallIsWednesday;
            shipInfo.RecallIsFriday = shipInfoVM.RecallIsFriday;
            shipInfo.RefundIsMonday = shipInfoVM.RefundIsMonday;
            shipInfo.RefundIsWednesday = shipInfoVM.RefundIsWednesday;
            shipInfo.RefundIsFriday = shipInfoVM.RefundIsFriday;
            shipInfo.IsUpdate = true;

            await _context.SaveChangesAsync();
            return new ApiMessage
            {
                Message = Message.UPDATE_SUCCESS.ToString()
            };
        }
    }
}
