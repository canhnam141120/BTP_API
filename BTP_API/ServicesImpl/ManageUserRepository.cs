using BTP_API.Models;

namespace BTP_API.ServicesImpl
{
    public class ManageUserRepository : IManageUserRepository
    {
        private readonly BTPContext _context;

        public ManageUserRepository(BTPContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse> getAllUserAsync(int page = 1)
        {
            var users = await _context.Users.Where(b => b.RoleId == 3).OrderByDescending(u => u.Id).ToListAsync();
            if (users.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            var result = PaginatedList<User>.Create(users, page, 20);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = result,
                NumberOfRecords = result.Count
            };
        }

        public async Task<ApiResponse> getAllUserActiveAsync(int page = 1)
        {
            var users = await _context.Users.Where(b => b.RoleId == 3 && b.IsActive == true).OrderByDescending(u => u.Id).ToListAsync();
            if (users.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            var result = PaginatedList<User>.Create(users, page, 20);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = result,
                NumberOfRecords = result.Count
            };
        }

        public async Task<ApiResponse> getAllUserBanAsync(int page = 1)
        {
            var users = await _context.Users.Where(b => b.RoleId == 3 && b.IsActive == false).OrderByDescending(u => u.Id).ToListAsync();
            if (users.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            var result = PaginatedList<User>.Create(users, page, 20);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = result,
                NumberOfRecords = result.Count
            };
        }

        public async Task<ApiResponse> getTopUserLikeAsync()
        {
            var users = await _context.Users.Where(b => b.RoleId == 3 && b.IsActive == true).OrderByDescending(b => b.LikeNumber).Take(5).ToListAsync();
            if (users.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = users,
                NumberOfRecords = users.Count
            };
        }

        public async Task<ApiResponse> getUserByIdAsync(int userId)
        {
            var user = await _context.Users.SingleOrDefaultAsync(b => b.RoleId == 3 && b.Id == userId);
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

        public async Task<ApiResponse> searchUserAsync(string search, int page = 1)
        {
            List<User> users;
            if(search != null)
            {
                search = search.ToLower().Trim();
                users = await _context.Users.Where(b => b.RoleId == 3 && b.Email.ToLower().Contains(search) || b.RoleId == 3 && b.Phone.Contains(search)).OrderByDescending(u => u.Id).ToListAsync();
            }
            else
            {
                users = await _context.Users.Where(b => b.RoleId == 3).OrderByDescending(u => u.Id).ToListAsync();
            }
            
            if (users.Count == 0)
            {
                return new ApiResponse
                {
                    Message = Message.LIST_EMPTY.ToString()
                };
            }
            var result = PaginatedList<User>.Create(users, page, 20);
            return new ApiResponse
            {
                Message = Message.GET_SUCCESS.ToString(),
                Data = result,
                NumberOfRecords = result.Count
            };
        }

        public async Task<ApiMessage> banAccAsync(int userId)
        {
            var user = await _context.Users.SingleOrDefaultAsync(b => b.RoleId == 3 && b.Id == userId);
            if(user != null)
            {
                user.IsActive = false;
                _context.Update(user);
                await _context.SaveChangesAsync();
                return new ApiMessage
                {
                    Message = Message.SUCCESS.ToString()
                };
            }
            return new ApiMessage
            {
                Message = Message.USER_NOT_EXIST.ToString()
            };
        }

        public async Task<ApiMessage> activeAccAsync(int userId)
        {
            var user = await _context.Users.SingleOrDefaultAsync(b => b.RoleId == 3 && b.Id == userId);
            if (user != null)
            {
                user.IsActive = true;
                _context.Update(user);
                await _context.SaveChangesAsync();
                return new ApiMessage
                {
                    Message = Message.SUCCESS.ToString()
                };
            }
            return new ApiMessage
            {
                Message = Message.USER_NOT_EXIST.ToString()
            };
        }

        public async Task<ApiMessage> authorityAdminAsync(int userId)
        {
            var user = await _context.Users.SingleOrDefaultAsync(b => b.RoleId == 3 && b.Id == userId);
            if (user != null)
            {
                user.RoleId = 2;
                _context.Update(user);
                await _context.SaveChangesAsync();
                return new ApiMessage
                {
                    Message = Message.SUCCESS.ToString()
                };
            }
            return new ApiMessage
            {
                Message = Message.USER_NOT_EXIST.ToString()
            };
        }
    }
}
