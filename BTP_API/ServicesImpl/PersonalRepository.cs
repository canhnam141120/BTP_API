﻿using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;
using System;
using System.Text.RegularExpressions;

namespace BTP_API.ServicesImpl
{
    public class PersonalRepository : IPersonalRepository
    {
        private readonly BTPContext _context;
        private readonly IWebHostEnvironment _environment;

        public PersonalRepository(BTPContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<ApiResponse> getAllNotificationAsync(int userId, int page = 1)
        {
            var notifications = await _context.Notifications.Where(b => b.UserId == userId).OrderByDescending(b => b.Id).Skip(20*(page-1)).Take(20).ToListAsync();
            var count = await _context.Notifications.Where(b => b.UserId == userId).CountAsync();
            //var result = PaginatedList<Notification>.Create(notifications, page, 10);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = notifications,
                NumberOfRecords = count
            };
        }

        public async Task<ApiResponse> getAllNotificationNotReadAsync(int userId, int page = 1)
        {
            var notifications = await _context.Notifications.Where(b => b.UserId == userId && b.IsRead == false).OrderByDescending(b => b.Id).Skip(20 * (page - 1)).Take(20).ToListAsync();
            var count = await _context.Notifications.Where(b => b.UserId == userId && b.IsRead == false).CountAsync();
            //var result = PaginatedList<Notification>.Create(notifications, page, 10);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = notifications,
                NumberOfRecords = count
            };
        }

        public async Task<ApiResponse> get10NewNotificationAsync(int userId)
        {
            var notifications = await _context.Notifications.Where(b => b.UserId == userId).OrderByDescending(b => b.Id).Take(10).ToListAsync();
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = notifications,
                NumberOfRecords = notifications.Count
            };
        }

        public async Task<ApiMessage> markReadNotificationAsync(int userId, int nottificationId)
        {
            var check = await _context.Notifications.SingleOrDefaultAsync(f => f.Id == nottificationId && f.UserId == userId);
            if (check != null)
            {
                check.IsRead = true;
                _context.Update(check);
                await _context.SaveChangesAsync();
                return new ApiMessage
                {
                    Message = Message.SUCCESS.ToString()
                };
            }
            return new ApiMessage
            {
                Message = Message.NOT_EXIST.ToString()
            };
        }

        public async Task<ApiMessage> markReadNotificationAllAsync(int userId)
        {
            var check = await _context.Notifications.Where(f => f.IsRead == false && f.UserId == userId).ToListAsync();
                foreach(var item in check)
                {
                    item.IsRead = true;
                }
                await _context.SaveChangesAsync();
                return new ApiMessage
                {
                    Message = Message.SUCCESS.ToString()
                };
        }

        public async Task<ApiResponse> getBookCanTradeAsync(int userId)
        {
            var books = await _context.Books.Include(b => b.Category).Where(b => b.UserId == userId && b.IsTrade == false && b.IsReady == true && b.Status == StatusRequest.Approved.ToString()).OrderByDescending(b => b.Id).ToListAsync();
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = books,
                NumberOfRecords = books.Count
            };
        }
        public async Task<ApiResponse> getAllBookAsync(int userId, int page = 1)
        {
            var books = await _context.Books.Include(b => b.Category).Where(b => b.UserId == userId).OrderByDescending(b => b.Id).Skip(6*(page-1)).Take(6).ToListAsync();
            var count = await _context.Books.Where(b => b.UserId == userId).CountAsync();
            //var result = PaginatedList<Book>.Create(books, page, 9);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = books,
                NumberOfRecords = count
            };
        }
        public async Task<ApiResponse> getBookApprovedAsync(int userId, int page = 1)
        {
            var books = await _context.Books.Include(b => b.Category).Where(b => b.UserId == userId && b.Status == StatusRequest.Approved.ToString()).OrderByDescending(b => b.Id).Skip(6 * (page - 1)).Take(6).ToListAsync();
            var count = await _context.Books.Where(b => b.UserId == userId && b.Status == StatusRequest.Approved.ToString()).CountAsync();
            //var result = PaginatedList<Book>.Create(books, page, 9);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = books,
                NumberOfRecords = count
            };
        }
        public async Task<ApiResponse> getBookDeniedAsync(int userId, int page = 1)
        {
            var books = await _context.Books.Include(b => b.Category).Where(b => b.UserId == userId && b.Status == StatusRequest.Denied.ToString()).OrderByDescending(b => b.Id).Skip(6 * (page - 1)).Take(6).ToListAsync();
            var count = await _context.Books.Where(b => b.UserId == userId && b.Status == StatusRequest.Denied.ToString()).CountAsync();
            //var result = PaginatedList<Book>.Create(books, page, 9);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = books,
                NumberOfRecords = count
            };
        }
        public async Task<ApiResponse> getBookWaitingAsync(int userId, int page = 1)
        {
            var books = await _context.Books.Include(b => b.Category).Where(b => b.UserId == userId && b.Status == StatusRequest.Waiting.ToString()).OrderByDescending(b => b.Id).Skip(6 * (page - 1)).Take(6).ToListAsync();
            var count = await _context.Books.Where(b => b.UserId == userId && b.Status == StatusRequest.Waiting.ToString()).CountAsync();
            //var result = PaginatedList<Book>.Create(books, page, 9);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = books,
                NumberOfRecords = count
            };
        }

        public async Task<ApiResponse> searchBookAsync(int userId, string search, int page = 1)
        {
            //List<Book> books;
            /*if (search != null)
            {
                search = search.ToLower().Trim();
                books = await _context.Books.Include(b => b.Category).Where(b => b.Title.ToLower().Contains(search) && b.UserId == userId).OrderByDescending(b => b.Id).ToListAsync();
            }
            else
            {
                books = await _context.Books.Include(b => b.Category).Where(b => b.UserId == userId).OrderByDescending(b => b.Id).ToListAsync();
            }

            //var result = PaginatedList<Book>.Create(books, page, 9);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = books.Skip(6 * (page - 1)).Take(6),
                NumberOfRecords = books.Count
            };*/


            List<Book> books = new List<Book>();
            if (search != null)
            {
                search = search.ToLower().Trim();

                search = search.RemoveAccents();

                RegexOptions options = RegexOptions.None;
                Regex regex = new Regex("[ ]{2,}", options);

                search = regex.Replace(search, " ");

                search = search.Replace(" ", "|");

                string query = @"SELECT * FROM ""Book"" Where ""UserID"" = " + userId + @"AND title_tsv @@ to_tsquery('" + search + "') ORDER BY ts_rank(title_tsv, to_tsquery('" + search + "')) desc";

                string sqlDataSource = "Host=ec2-3-214-57-29.compute-1.amazonaws.com:5432;Database=dr8bb7r6ai6bk;Username=pywgupxdwfpxrf;Password=72920fd6643b9123783422128cd07c8ab0381d206608988768d3e1be53fc3441;SSL Mode=Require;Trust Server Certificate=true";

                using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                    {
                        using (var dataReader = myCommand.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                var values = new object[dataReader.FieldCount];
                                for (int i = 0; i < dataReader.FieldCount; i++)
                                {
                                    values[i] = dataReader[i];
                                }

                                var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == Int16.Parse(values[1].ToString()));
                                var category = await _context.Categories.SingleOrDefaultAsync(u => u.Id == Int16.Parse(values[2].ToString()));

                                books.Add(new Book
                                {
                                    Id = Int16.Parse(values[0].ToString()),
                                    UserId = Int16.Parse(values[1].ToString()),
                                    CategoryId = Int16.Parse(values[2].ToString()),
                                    Title = values[3].ToString(),
                                    Description = values[4].ToString(),
                                    Author = values[5].ToString(),
                                    Publisher = values[6].ToString(),
                                    Year = Int16.Parse(values[7].ToString()),
                                    Language = values[8].ToString(),
                                    NumberOfPages = Int16.Parse(values[9].ToString()),
                                    Weight = float.Parse(values[10].ToString()),
                                    CoverPrice = float.Parse(values[11].ToString()),
                                    DepositPrice = float.Parse(values[12].ToString()),
                                    StatusBook = values[13].ToString(),
                                    Image = values[14].ToString(),
                                    PostedDate = DateOnly.FromDateTime(DateTime.Parse(values[15].ToString())),
                                    IsExchange = bool.Parse(values[16].ToString()),
                                    IsRent = bool.Parse(values[17].ToString()),
                                    RentFee = float.Parse(values[18].ToString()),
                                    NumberOfDays = Int16.Parse(values[19].ToString()),
                                    IsReady = bool.Parse(values[20].ToString()),
                                    IsTrade = bool.Parse(values[21].ToString()),
                                    Status = values[22].ToString(),
                                    User = user,
                                    Category = category
                                });
                            }
                        }
                        myCon.Close();
                    }
                }

                return new ApiResponse
                {
                    Message = Message.GET_SUCCESS.ToString(),
                    Data = books.Skip(6 * (page - 1)).Take(6),
                    NumberOfRecords = books.Count
                };
            }
            else
            {
                books = await _context.Books.Include(b => b.User).Include(b => b.Category).Where(b => b.UserId == userId).OrderByDescending(b => b.Id).ToListAsync();
                return new ApiResponse
                {
                    Message = Message.GET_SUCCESS.ToString(),
                    Data = books.Skip(6 * (page - 1)).Take(6),
                    NumberOfRecords = books.Count
                };
            }

        }

        public async Task<ApiResponse> searchPostAsync(int userId, string search, int page = 1)
        {
            List<Post> post;
            if (search != null)
            {
                search = search.ToLower().Trim();
                post = await _context.Posts.Where(b => b.Title.ToLower().Contains(search) && b.UserId == userId).OrderByDescending(b => b.Id).ToListAsync();
            }
            else
            {
                post = await _context.Posts.Where(b => b.UserId == userId).OrderByDescending(b => b.Id).ToListAsync();
            }

            //var result = PaginatedList<Book>.Create(books, page, 9);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = post.Skip(5 * (page - 1)).Take(5),
                NumberOfRecords = post.Count
            };
        }



        public async Task<ApiResponse> getAllPostAsync(int userId, int page = 1)
        {
            var posts = await _context.Posts.Where(b => b.UserId == userId).OrderByDescending(b => b.Id).Skip(5 * (page - 1)).Take(5).ToListAsync();
            var count = await _context.Posts.Where(b => b.UserId == userId).CountAsync();
            //var result = PaginatedList<Post>.Create(posts, page, 6);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = posts,
                NumberOfRecords = count
            };
        }
        public async Task<ApiResponse> getPostApprovedAsync(int userId, int page = 1)
        {
            var posts = await _context.Posts.Where(b => b.UserId == userId && b.Status == StatusRequest.Approved.ToString()).OrderByDescending(b => b.Id).Skip(5 * (page - 1)).Take(5).ToListAsync();
            var count = await _context.Posts.Where(b => b.UserId == userId && b.Status == StatusRequest.Approved.ToString()).CountAsync();
            //var result = PaginatedList<Post>.Create(posts, page, 6);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = posts,
                NumberOfRecords = count
            };
        }
        public async Task<ApiResponse> getPostDeniedAsync(int userId, int page = 1)
        {
            var posts = await _context.Posts.Where(b => b.UserId == userId && b.Status == StatusRequest.Denied.ToString()).OrderByDescending(b => b.Id).Skip(5 * (page - 1)).Take(5).ToListAsync();
            var count = await _context.Posts.Where(b => b.UserId == userId && b.Status == StatusRequest.Denied.ToString()).CountAsync();
            //var result = PaginatedList<Post>.Create(posts, page, 6);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = posts,
                NumberOfRecords = count
            };
        }
        public async Task<ApiResponse> getPostWaitingAsync(int userId, int page = 1)
        {
            var posts = await _context.Posts.Where(b => b.UserId == userId && b.Status == StatusRequest.Waiting.ToString()).OrderByDescending(b => b.Id).Skip(5 * (page - 1)).Take(5).ToListAsync();
            var count = await _context.Posts.Where(b => b.UserId == userId && b.Status == StatusRequest.Waiting.ToString()).CountAsync();
            //var result = PaginatedList<Post>.Create(posts, page, 6);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = posts,
                NumberOfRecords = count
            };
        }
        public async Task<ApiResponse> getBookByFavoritesAsync(int userId, int page = 1)
        {
            var favoriteBooks = await _context.FavoriteBookLists.Include(f => f.Book.User).Include(f => f.Book.Category).Where(f => f.UserId == userId).OrderByDescending(b => b.Id).Skip(6 * (page - 1)).Take(6).ToListAsync();
            var count = await _context.FavoriteBookLists.Where(f => f.UserId == userId).CountAsync();
            //var result = PaginatedList<FavoriteBookList>.Create(favoriteBooks, page, 6);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = favoriteBooks,
                NumberOfRecords = count
            };
        }
        public async Task<ApiMessage> addBookByFavoritesAsync(int userId, int bookId)
        {
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
        public async Task<ApiMessage> deleteBookByFavoritesAsync(int userId, int bookId)
        {
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
        public async Task<ApiResponse> getPostByFavoritesAsync(int userId, int page = 1)
        {
 
            var favoritePosts = await _context.FavoritePostLists.Include(f => f.Post.User).Where(f => f.UserId == userId).OrderByDescending(b => b.Id).Skip(5 * (page - 1)).Take(5).ToListAsync();
            var count = await _context.FavoritePostLists.Where(f => f.UserId == userId).CountAsync();
            //var result = PaginatedList<FavoritePostList>.Create(favoritePosts, page, 6);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = favoritePosts,
                NumberOfRecords = count
            };
        }
        public async Task<ApiMessage> addPostByFavoritesAsync(int userId, int postId)
        {
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);

            var post = await _context.Posts.Include(f => f.User).SingleOrDefaultAsync(f => f.Id == postId);
            if (post == null)
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
            var notification = new Notification
            {
                UserId = post.UserId,
                Content = user.Fullname + @" đã yêu thích bài đăng """ + post.Title  + @""" của bạn!",
                CreatedDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo),
                IsRead = false,
            };
            _context.Add(notification);
            await _context.SaveChangesAsync();
            return new ApiMessage
            {
                Message = Message.ADD_SUCCESS.ToString()
            };
        }
        public async Task<ApiMessage> deletePostByFavoritesAsync(int userId, int postId)
        {
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

        public async Task<ApiMessage> checkUserLikeAsync(int userId, int id)
        {
            var check = "FALSE";
            var favoriteUsers = await _context.FavoriteUserLists.Where(f => f.UserId == userId).ToListAsync();

            foreach(var item in favoriteUsers)
            {
                if(id == item.FavoriteUserId)
                {
                    check = "TRUE";
                }
            }
            return new ApiMessage
            {
                Message = check
            };
        }

        public async Task<ApiMessage> checkBookLikeAsync(int userId, int id)
        {
            var check = "FALSE";
            var favoriteUsers = await _context.FavoriteBookLists.Where(f => f.UserId == userId).ToListAsync();

            foreach (var item in favoriteUsers)
            {
                if (id == item.BookId)
                {
                    check = "TRUE";
                }
            }
            return new ApiMessage
            {
                Message = check
            };
        }

        public async Task<ApiMessage> checkPostLikeAsync(int userId, int id)
        {
            var check = "FALSE";
            var favoriteUsers = await _context.FavoritePostLists.Where(f => f.UserId == userId).ToListAsync();

            foreach (var item in favoriteUsers)
            {
                if (id == item.PostId)
                {
                    check = "TRUE";
                }
            }
            return new ApiMessage
            {
                Message = check
            };
        }

        public async Task<ApiResponse> getUserByFavoritesAsync(int userId, int page = 1)
        {
            var favoriteUsers = await _context.FavoriteUserLists.Include(f => f.FavoriteUser).Where(f => f.UserId == userId).OrderByDescending(b => b.Id).Skip(8*(page-1)).Take(8).ToListAsync();
            var count = await _context.FavoriteUserLists.Where(f => f.UserId == userId).CountAsync();
            //var result = PaginatedList<FavoriteUserList>.Create(favoriteUsers, page, 6);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = favoriteUsers,
                NumberOfRecords = count
            };
        }
        public async Task<ApiMessage> addUserByFavoritesAsync(int userId, int favoriteUserId)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            var userFavorite = await _context.Users.SingleOrDefaultAsync(f => f.Id == favoriteUserId);
            if (userFavorite == null)
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
            userFavorite.LikeNumber += 1;
            _context.Add(favoriteUser);
            var notification = new Notification
            {
                UserId = userFavorite.Id,
                Content = user.Fullname + " đã yêu thích bạn!",
                CreatedDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo),
                IsRead = false,
            };
            _context.Add(notification);
            await _context.SaveChangesAsync();
            return new ApiMessage
            {
                Message = Message.ADD_SUCCESS.ToString()
            };
        }
        public async Task<ApiMessage> deleteUserByFavoritesAsync(int userId, int favoriteUserId)
        {
            var user = await _context.Users.SingleOrDefaultAsync(f => f.Id == favoriteUserId);
            if (user == null)
            {
                return new ApiMessage
                {
                    Message = Message.USER_NOT_EXIST.ToString()
                };
            }

            var check = await _context.FavoriteUserLists.SingleOrDefaultAsync(f => f.FavoriteUserId == favoriteUserId && f.UserId == userId);
            if (check != null)
            {
                user.LikeNumber -= 1;
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
        public async Task<ApiResponse> getInfoUserIdAsync(int userId)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = user
            };
        }
        public async Task<ApiMessage> editInfoAsync(int userId, UserVM userVM)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return new ApiMessage
                {
                    Message = Message.USER_NOT_EXIST.ToString()
                };
            }
            user.Fullname = userVM.Fullname;
            user.Phone = userVM.Phone;
            user.AddressMain = userVM.AddressMain;
            user.Avatar = userVM.Avatar;
            await _context.SaveChangesAsync();
            return new ApiMessage
            {
                Message = Message.UPDATE_SUCCESS.ToString()
            };
        }
        public async Task<ApiMessage> editPasswordAsync(int userId, ChangePasswordVM changePasswordVM)
        {
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
        public async Task<ApiResponse> listOfRequestSendAsync(int userId, int page = 1)
        {
            var myBooks = await _context.Books.Where(b => b.UserId == userId && b.Status == StatusRequest.Approved.ToString()).ToListAsync();

            List<ExchangeRequest> exchangeRequests = new List<ExchangeRequest>();

            foreach (var book in myBooks)
            {
                var data = await _context.ExchangeRequests.Include(r => r.Book).Include(r => r.BookOffer).Where(r => r.BookOfferId == book.Id && r.Status == StatusRequest.Waiting.ToString()).OrderBy(b => b.Id).ToListAsync();
                foreach (var item in data)
                {
                    exchangeRequests.Add(item);
                }
            }

            //var result = PaginatedList<ExchangeRequest>.Create(exchangeRequests, page, 5);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = exchangeRequests.OrderByDescending(r => r.Id).Skip(4*(page-1)).Take(4),
                NumberOfRecords = exchangeRequests.Count
            };
        }
        public async Task<ApiResponse> listOfRequestReceivedSendAsync(int userId, int bookId)
        {
            var data = await _context.ExchangeRequests.Include(r => r.BookOffer.User).Include(r => r.BookOffer.Category).Where(r => r.BookId == bookId && r.Status == StatusRequest.Waiting.ToString()).OrderByDescending(b => b.Id).ToListAsync();
            var count = await _context.ExchangeRequests.Where(r => r.BookId == bookId).CountAsync();
            //var result = PaginatedList<ExchangeRequest>.Create(data, page, 5);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = data,
                NumberOfRecords = count
            };
        }
        public async Task<ApiResponse> myTransactionExchangeAsync(int userId, int page = 1)
        {
            var exchanges = await _context.Exchanges.Include(b => b.UserId1Navigation).Include(b => b.UserId2Navigation).Where(b => b.UserId1 == userId || b.UserId2 == userId).OrderByDescending(b => b.Id).Skip(10 * (page - 1)).Take(5).ToListAsync();
            var count = await _context.Exchanges.Where(b => b.UserId1 == userId || b.UserId2 == userId).CountAsync();

            //var result = PaginatedList<Exchange>.Create(exchanges, page, 10);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = exchanges,
                NumberOfRecords = count
            };
        }
        public async Task<ApiResponse> myTransactionExDetailAsync(int userId, int exchangeId)
        {
            var exchangeDetails = await _context.ExchangeDetails.Include(b => b.Book1).Include(b => b.Book2).Where(b => b.ExchangeId == exchangeId).ToListAsync();
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = exchangeDetails,
                NumberOfRecords = exchangeDetails.Count
            };
        }
        public async Task<ApiResponse> myTransactionExBillAsync(int userId, int exchangeId)
        {
            var exchangeBill = await _context.ExchangeBills.Include(b=>b.FeeId1Navigation).Include(b=>b.FeeId2Navigation).Include(b=>b.FeeId3Navigation).SingleOrDefaultAsync(b => b.ExchangeId == exchangeId && b.UserId == userId);

            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = exchangeBill
            };
        }
        public async Task<ApiResponse> myExBillAllAsync(int userId, int page = 1)
        {
            var exchangeBills = await _context.ExchangeBills.Where(b => b.UserId == userId).OrderByDescending(b => b.Id).Skip(10*(page-1)).Take(10).ToListAsync();
            var count = await _context.ExchangeBills.Where(b => b.UserId == userId).CountAsync();
            //var result = PaginatedList<ExchangeBill>.Create(exchangeBills, page, 10);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = exchangeBills,
                NumberOfRecords = count
            };
        }
        public async Task<ApiResponse> myTransactionRentAsync(int userId, int page = 1)
        {
            var rents = await _context.Rents.Include(b => b.Owner).Include(b=> b.Renter).Where(b => b.OwnerId == userId || b.RenterId == userId).OrderByDescending(b => b.Id).Skip(10 * (page - 1)).Take(10).ToListAsync();
            var count = await _context.Rents.Where(b => b.OwnerId == userId || b.RenterId == userId).CountAsync();
            //var result = PaginatedList<Rent>.Create(rents, page, 10);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = rents,
                NumberOfRecords = count
            };
        }
        public async Task<ApiResponse> myTransactionRentDetailAsync(int userId, int rentId)
        {
            var rentDetails = await _context.RentDetails.Include(b => b.Book).Where(b => b.RentId == rentId).ToListAsync();
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = rentDetails,
                NumberOfRecords = rentDetails.Count
            };
        }
        public async Task<ApiResponse> myTransactionRentBillAsync(int userId, int rentId)
        {
            var rentBill = await _context.RentBills.Include(b=>b.FeeId1Navigation).Include(b=>b.FeeId2Navigation).Include(b=>b.FeeId3Navigation).SingleOrDefaultAsync(b => b.RentId == rentId && b.UserId == userId);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = rentBill
            };
        }
        public async Task<ApiResponse> myRentBillAllAsync(int userId, int page = 1)
        {
            var rentBills = await _context.RentBills.Where(b => b.UserId == userId).OrderByDescending(b => b.Id).Skip(10 * (page - 1)).Take(10).ToListAsync();
            var count = await _context.RentBills.Where(b => b.UserId == userId).CountAsync();
            //var result = PaginatedList<RentBill>.Create(rentBills, page, 10);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = rentBills,
                NumberOfRecords = count
            };
        }

        public async Task<ApiResponse> getInfoShippingAsync(int userId)
        {
            var info = await _context.ShipInfos.SingleOrDefaultAsync(s => s.UserId == userId);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = info
            };
        }

            public async Task<ApiMessage> updateInfoShippingAsync(int userId, ShipInfoVM shipInfoVM)
        {
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
